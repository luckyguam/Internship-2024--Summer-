using Azure.Core;
using ClosedXML.Excel;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace Beta4
{
    public partial class PnumSearchForm : Form
    {
        private readonly string _localFilePath;
        private const string TempFilePath = "tempfile.xlsx"; // Temporary file path
        private const string FirmwareBaseUrl = "http:// "; // Base folder URL required for the opereation below 
        private const string SiteId = "copany.sharepoint.com,598csdfsadfsaa,dsafads-asdf-as2";
        private readonly string _accessToken;
        private readonly string _siteId;
        private GraphServiceClient _graphClient;
        private const string DocumentLibraryPath = "Documents";

        String FWfile;
        String FWNum;
        public PnumSearchForm(string localFilePath, string accessToken, string siteId)
        {
            InitializeComponent();
            SetLogoImage();

            _localFilePath = localFilePath;
            _accessToken = accessToken;
            _siteId = siteId;
            System.Windows.Forms.Application.ApplicationExit += (sender, e) =>
            {
                CleanUpFile();
            };

            FormClosed += (sender, e) =>
            {
                CleanUpFile();
            };

            txtModelNumber.KeyDown += TxtModelNumber_KeyDown;
        }

        private void TxtModelNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearch.PerformClick();
            }
        }

        private void SetLogoImage()
        {
            this.pbLogo.Image = Beta4.Properties.Resources.VJX;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string modelNumberInput = txtModelNumber.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(modelNumberInput))
            {
                MessageBox.Show("Please enter a model number.");
                return;
            }

            try
            {
                bool modelFound = false;
                var resultBuilder = new StringBuilder();

                using (var workbook = new XLWorkbook(_localFilePath))
                {
                    foreach (var sheet in workbook.Worksheets)
                    {
                        var rows = sheet.RowsUsed();
                        foreach (var row in rows)
                        {
                            string rowModelNumber = row.Cell(7).GetString().Trim().ToUpper();
                            if (string.Equals(rowModelNumber, modelNumberInput, StringComparison.OrdinalIgnoreCase))
                            {
                                modelFound = true;

                                string firmwareNumber = row.Cell(2).GetString();
                                string firmwareFileName = row.Cell(4).GetString();
                                string checksum = row.Cell(3).GetString();

                                if (string.IsNullOrEmpty(firmwareNumber) || string.IsNullOrEmpty(checksum))
                                {
                                    FindFirmwareInPreviousRows(sheet, row.RowNumber(), out firmwareNumber, out firmwareFileName);
                                    FindChecksumInPreviousRows(sheet, row.RowNumber(), out checksum);
                                }
                                FWNum = firmwareNumber;
                                FWfile = firmwareFileName;
                                resultBuilder.AppendLine($"Model Number: {rowModelNumber}");
                                resultBuilder.AppendLine($"Firmware Number: {firmwareNumber}");
                                resultBuilder.AppendLine($"Firmware File: {firmwareFileName}");
                                resultBuilder.AppendLine($"Checksum: {checksum}");
                                resultBuilder.AppendLine();

                                if (!string.IsNullOrEmpty(firmwareFileName))
                                {
                                    btnOpenFolder.Tag = firmwareNumber;
                                }

                                break;
                            }
                        }

                        if (modelFound)
                        {
                            break;
                        }
                    }
                }

                if (!modelFound)
                {
                    resultBuilder.AppendLine("Model number not found.");
                }

                txtResult.Text = resultBuilder.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving data: {ex.Message}");
            }
        }

        private void btnToggleToFnSearchForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            var fnSearchForm = new FnSearchForm(_localFilePath, _accessToken, SiteId);
            fnSearchForm.Show();
        }

        private void CleanUpFile()
        {
            try
            {
                if (System.IO.File.Exists(_localFilePath))
                {
                    System.IO.File.Delete(_localFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting temporary file: {ex.Message}");
            }
        }

        private void FindFirmwareInPreviousRows(IXLWorksheet sheet, int currentRowNumber, out string firmwareNumber, out string firmwareFileName)
        {
            firmwareNumber = string.Empty;
            firmwareFileName = string.Empty;

            for (int rowNumber = currentRowNumber - 1; rowNumber >= 1; rowNumber--)
            {
                var row = sheet.Row(rowNumber);
                firmwareNumber = row.Cell(2).GetString();
                firmwareFileName = row.Cell(4).GetString();

                if (!string.IsNullOrEmpty(firmwareNumber) && !string.IsNullOrEmpty(firmwareFileName))
                {
                    break;
                }
            }
        }

        private void FindChecksumInPreviousRows(IXLWorksheet sheet, int currentRowNumber, out string checksum)
        {
            checksum = string.Empty;

            for (int rowNumber = currentRowNumber - 1; rowNumber >= 1; rowNumber--)
            {
                var row = sheet.Row(rowNumber);
                checksum = row.Cell(3).GetString();

                if (!string.IsNullOrEmpty(checksum))
                {
                    break;
                }
            }
        }







        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            string firmwareNumber = btnOpenFolder.Tag?.ToString();

            if (!string.IsNullOrEmpty(firmwareNumber))
            {
                string firmwareUrl = $"{FirmwareBaseUrl}{firmwareNumber}";

                try
                {
                    // Ensure the firmware URL is valid
                    if (Uri.IsWellFormedUriString(firmwareUrl, UriKind.Absolute))
                    {
                        // Use Process.Start with a properly formatted URL
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = firmwareUrl,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("The constructed URL is not valid.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error accessing firmware folder: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("No firmware file selected.");
            }
        }

        private async void btnDownloadFM_Click(object sender, EventArgs e)
        {
           
            {
                // Ensure that the firmware number and filename are provided
                string firmwareNumber = FWNum;
                string firmwareFilename = FWfile;

                if (string.IsNullOrEmpty(firmwareNumber) || string.IsNullOrEmpty(firmwareFilename))
                {
                    MessageBox.Show("Please enter both Firmware Number and Firmware Filename.");
                    return;
                }

                // Construct the path to the firmware file in SharePoint
                string firmwareFilePath = $"/{firmwareNumber}/{firmwareFilename}";

                // Download the firmware file
                await DownloadFirmwareFileFromSharePoint(firmwareFilePath);

            }
        }
        private async Task DownloadFirmwareFileFromSharePoint(string firmwareFilePath)
        {
            if (string.IsNullOrEmpty(firmwareFilePath))
            {
                throw new ArgumentException("Firmware file path cannot be null or empty.", nameof(firmwareFilePath));
            }

            if (_graphClient == null)
            {
                try
                {
                    var clientId = "dfgsdfgd-dfsgsdf-gsdf-gsdf-gsdf"; // Replace with your client ID
                    var tenantId = "dsfgsdfgdsfg-sdfg-sdfg-sdfg-sdf"; // Replace with your tenant ID
                    var redirectUri = "http://localhost"; // Replace with your redirect URI if necessary

                    var app = PublicClientApplicationBuilder.Create(clientId)
                        .WithAuthority($"https://login.microsoftonline.com/{tenantId}")
                        .WithRedirectUri(redirectUri)
                        .Build();

                    var result = await app.AcquireTokenInteractive(new[] { "https://graph.microsoft.com/.default" }).ExecuteAsync();

                    _graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
                    {
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error initializing Graph client: {ex.Message}");
                    throw;
                }
            }

            try
            {
                // Get the drive ID for the Documents library
                var drives = await _graphClient.Sites[SiteId].Drives.Request().GetAsync();
                var drive = drives.FirstOrDefault(d => d.Name.Equals(DocumentLibraryPath, StringComparison.OrdinalIgnoreCase));

                if (drive == null)
                {
                    throw new Exception("Documents library not found.");
                }

                // Get the file item from SharePoint
                var item = await _graphClient.Drives[drive.Id].Root.ItemWithPath(firmwareFilePath).Request().GetAsync();
                if (item == null)
                {
                    throw new Exception($"File '{firmwareFilePath}' not found.");
                }

                var fileStream = await _graphClient.Drives[drive.Id].Items[item.Id].Content.Request().GetAsync();

                // Extract the file name from the firmwareFilePath
                string fileName = Path.GetFileName(firmwareFilePath);
                if (string.IsNullOrEmpty(fileName))
                {
                    throw new Exception("Invalid file name extracted from firmwareFilePath.");
                }

                // Attempt to find the Downloads folder
                string downloadsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

                // Check if the Downloads folder is under "Favorites" and adjust if necessary -- this is because sometimes the download folder's is under favourite
                if (!System.IO.Directory.Exists(downloadsFolderPath))
                {
                    downloadsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Favorites), "Downloads");
                }

                // Ensure the folder exists
                if (!System.IO.Directory.Exists(downloadsFolderPath))
                {
                    throw new DirectoryNotFoundException("Downloads folder could not be found.");
                }

                // Combine it with the file name
                string fullDownloadPath = Path.Combine(downloadsFolderPath, fileName);

                // Download the file
                using (var file = new FileStream(fullDownloadPath, FileMode.Create, FileAccess.Write))
                {
                    await fileStream.CopyToAsync(file);
                }

                MessageBox.Show($"File downloaded successfully to: {fullDownloadPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading firmware file: {ex.Message}");
                throw;
            }
        }




    }
}
