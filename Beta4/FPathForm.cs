using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beta4
{
    public partial class FPathForm : Form
    {
        // Path to store the file path in a temporary text file
        private static readonly string FilePathTextFile = Path.Combine(Path.GetTempPath(), "FilePath.txt");

        // Constants for Azure AD and SharePoint configuration
        private const string TenantId = "sdds4353-43534-435-435-3433"; // Update with your tenantID
        private const string ClientId = "hfdshf-sadgfasd-asdg-s"; // Update with your ClientID
        private const string SiteId = "  www.siteId.come. 2342343fgsdgsdfg345, dsgdfsgdf43534w5"; // Enter your siteId that you extract using Graph
        private const string DocumentLibraryPath = "Documents";
        private const string TestFolderPath = "Test";

        // Fields to hold file path and Graph client
        private string _filePath;
        private GraphServiceClient _graphClient;
        private string _accessToken;

        // Public properties for access token and Site ID
        public string AccessToken => _accessToken;
        public string CurrentSiteId => SiteId;

        // Property to store the temporary file path of the downloaded file
        public string TempFilePath { get; private set; }

        public FPathForm()
        {
            InitializeComponent();
            LoadFilePath();

            // Associate the KeyDown event of txtFilePath with the handler
            txtFilePath.KeyDown += new KeyEventHandler(txtFilePath_KeyDown);
        }

        // Load the file path from the FilePath.txt file into the txtFilePath textbox
        private void LoadFilePath()
        {
            btnAutoSearch.Hide(); // Hide the AutoSearch button initially

            // Check if the file path text file exists
            if (System.IO.File.Exists(FilePathTextFile))
            {
                // Load and display the stored file path
                txtFilePath.Text = System.IO.File.ReadAllText(FilePathTextFile).Trim();
            }
            else
            {
                // If no file path is found, prompt the user to enter a new path
                txtFilePath.Text = "No file path found. Please enter a new path.";
            }
        }

        // Event handler for KeyDown event to allow submitting by pressing Enter
        private void txtFilePath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the default beep sound
                btnNext.PerformClick(); // Simulate a click on the Next button
            }
        }

        // Event handler for Edit button click to allow editing the file path
        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAutoSearch.Show(); // Show the AutoSearch button
            txtFilePath.ReadOnly = false; // Make the file path textbox editable
        }

        // Event handler for Next button click to proceed with file download and transition to the next form
        private async void btnNext_Click(object sender, EventArgs e)
        {
            var filePath = txtFilePath.Text.Trim();

            // Check if the file path is empty
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("File path cannot be empty.");
                return;
            }

            // Save the file path to the FilePath.txt file
            System.IO.File.WriteAllText(FilePathTextFile, filePath);

            try
            {
                await HandleLogin(); // Authenticate and get the access token
                await DownloadFileFromSharePoint(); // Download the file from SharePoint

                // Open the PnumSearchForm with the downloaded file path and access token
                var pnumSearchForm = new PnumSearchForm(TempFilePath, AccessToken, CurrentSiteId);
                pnumSearchForm.FormClosed += (s, args) => CleanUpFile(); // Clean up the file when the form is closed
                pnumSearchForm.Show(); // Show the PnumSearchForm
                this.Hide(); // Hide the current form
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        // Handle user login and authentication with Microsoft Graph
        private async Task HandleLogin()
        {
            // Configure the public client application for Azure AD authentication
            var app = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"https://login.microsoftonline.com/{TenantId}")
                .WithRedirectUri("http://localhost")
                .Build();

            // Acquire the access token interactively
            var result = await app.AcquireTokenInteractive(new[] { "https://graph.microsoft.com/.default" }).ExecuteAsync();
            _accessToken = result.AccessToken;

            // Initialize the GraphServiceClient with the access token
            _graphClient = new GraphServiceClient(new DelegateAuthenticationProvider((requestMessage) =>
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                return Task.CompletedTask;
            }));
        }

        // Download the file from SharePoint to the system's temporary directory
        private async Task DownloadFileFromSharePoint()
        {
            try
            {
                // Get the drives associated with the site
                var drives = await _graphClient.Sites[SiteId].Drives.Request().GetAsync();
                var driveId = string.Empty;

                // Find the drive ID for the "Documents" library
                foreach (var drive in drives)
                {
                    if (drive.Name == "Documents")
                    {
                        driveId = drive.Id;
                        break;
                    }
                }

                // If the drive ID is not found, throw an exception
                if (string.IsNullOrEmpty(driveId))
                {
                    throw new Exception("Drive ID not found.");
                }

                // Get the item from SharePoint based on the provided file path
                var item = await _graphClient.Drives[driveId].Root.ItemWithPath(txtFilePath.Text.Trim()).Request().GetAsync();
                var fileStream = await _graphClient.Drives[driveId].Items[item.Id].Content.Request().GetAsync();

                // Use the system temporary directory to store the downloaded file
                TempFilePath = Path.Combine(Path.GetTempPath(), "DownloadedFile.xlsx");

                // Save the downloaded file locally
                using (var file = new FileStream(TempFilePath, FileMode.Create, FileAccess.Write))
                {
                    await fileStream.CopyToAsync(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading file: {ex.Message}");
                throw;
            }
        }

        // Clean up the downloaded file when done
        private void CleanUpFile()
        {
            try
            {
                if (System.IO.File.Exists(TempFilePath))
                {
                    System.IO.File.Delete(TempFilePath); // Delete the file from the temporary directory
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cleaning up file: {ex.Message}");
            }
            finally
            {
                this.Close(); // Close the form
            }
        }

        // Event handler for AutoSearch button click to search for the Excel file in SharePoint
        private async void btnAutoSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Call the method to find the Excel file
                string foundFilePath = await FindExcelFileInSharePoint();

                if (!string.IsNullOrEmpty(foundFilePath))
                {
                    // Update the txtFilePath with the found file path
                    txtFilePath.Text = foundFilePath;
                }
                else
                {
                    MessageBox.Show("No Excel file found in the specified folder.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        // Method to find an Excel file in the specified SharePoint folder
        private async Task<string> FindExcelFileInSharePoint()
        {
            try
            {
                // Ensure the GraphServiceClient instance is initialized
                if (_graphClient == null)
                {
                    await HandleLogin(); // Log in and initialize _graphClient
                }

                // Get the drive ID for the Documents library
                var drives = await _graphClient.Sites[SiteId].Drives.Request().GetAsync();
                var drive = drives.FirstOrDefault(d => d.Name.Equals(DocumentLibraryPath, StringComparison.OrdinalIgnoreCase));

                if (drive == null)
                {
                    throw new Exception("Documents library not found.");
                }

                // Search for Excel files in the specified Test folder
                var items = await _graphClient.Drives[drive.Id].Root.ItemWithPath(TestFolderPath).Children.Request().GetAsync();
                var excelFile = items.FirstOrDefault(i => i.File != null && i.Name.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase));

                if (excelFile != null)
                {
                    // Construct and return the file path depending on where the file is located
                    string filePath = "/Test/" + excelFile.Name;
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finding Excel file: {ex.Message}");
            }

            return null;
        }

        // Event handler for form load event, if any additional logic is required on load
        private void FPathForm_Load(object sender, EventArgs e)
        {
            // Code to execute when the form loads, if needed
        }

        // Event handler for Back button click to go back to the previous form
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the current form and go back
        }
    }
}
