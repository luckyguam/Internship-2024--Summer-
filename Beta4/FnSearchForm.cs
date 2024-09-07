using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Beta4
{
    public partial class FnSearchForm : Form
    {
        // Variables to store the local file path, access token, and site ID
        private readonly string _localFilePath;
        private readonly string _accessToken;
        private readonly string _siteId;

        // Constructor initializing the form with necessary parameters
        public FnSearchForm(string localFilePath, string accessToken, string siteId)
        {
            InitializeComponent();
            SetLogoImage(); // Set the logo image on the form
            _localFilePath = localFilePath;
            _accessToken = accessToken;
            _siteId = siteId;

            // Event handlers for key press and selection change
            txtModelNumber.KeyDown += TxtModelNumber_KeyDown;
            lstModelNumbers.SelectedIndexChanged += LstModelNumbers_SelectedIndexChanged;
        }

        // Event handler to trigger search when Enter key is pressed
        private void TxtModelNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearch.PerformClick(); // Trigger the search button click event
            }
        }

        // Sets the logo image in the PictureBox control
        private void SetLogoImage()
        {
            this.pbLogo.Image = Beta4.Properties.Resources.VJX; // Source of your picture
        }

        // Event handler for the Search button click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtModelNumber.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            try
            {
                lstModelNumbers.Items.Clear(); // Clear previous results
                bool dataFound = false;

                using (var workbook = new XLWorkbook(_localFilePath))
                {
                    foreach (var sheet in workbook.Worksheets)
                    {
                        var rows = sheet.RowsUsed();
                        foreach (var row in rows)
                        {
                            string cellValue = row.Cell(2).GetString().Trim().ToUpper(); // Retrieve data from Column B

                            if (cellValue.Contains(searchTerm))
                            {
                                dataFound = true;

                                // Extract and add firmware number to the ListBox
                                string firmwareNumber = row.Cell(2).GetString(); // Column B
                                lstModelNumbers.Items.Add(firmwareNumber);
                            }
                        }
                    }
                }

                if (!dataFound)
                {
                    lstModelNumbers.Items.Add("No matching data found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving data: {ex.Message}");
            }
        }

        // Event handler when a selection is made in the ListBox
        private void LstModelNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstModelNumbers.SelectedItem != null)
            {
                string selectedFirmwareNumber = lstModelNumbers.SelectedItem.ToString().Trim().ToUpper();

                // Search for model numbers associated with the selected firmware number
                SearchFirmwareForModelNumbers(selectedFirmwareNumber);
            }
        }

        // Searches for model numbers associated with a specific firmware number
        private void SearchFirmwareForModelNumbers(string firmwareNumber)
        {
            try
            {
                var firmwareModelMap = new Dictionary<string, List<string>>();
                var resultBuilder = new StringBuilder();
                resultBuilder.AppendLine();
                resultBuilder.AppendLine($"Model's associated with Firmware Number: {firmwareNumber}");
                resultBuilder.AppendLine();

                using (var workbook = new XLWorkbook(_localFilePath))
                {
                    foreach (var sheet in workbook.Worksheets)
                    {
                        var rows = sheet.RowsUsed();
                        foreach (var row in rows)
                        {
                            // Get the model number from Column G
                            string modelNumber = row.Cell(7).GetString().Trim(); // Column G

                            // Get the firmware number from Column B
                            string rowFirmwareNumber = row.Cell(2).GetString().Trim().ToUpper(); // Column B

                            // Handle empty firmware number by checking previous rows
                            if (string.IsNullOrEmpty(rowFirmwareNumber))
                            {
                                for (int rowNumber = row.RowNumber() - 1; rowNumber >= 1; rowNumber--)
                                {
                                    var previousRow = sheet.Row(rowNumber);
                                    rowFirmwareNumber = previousRow.Cell(2).GetString().Trim().ToUpper(); // Column B

                                    if (!string.IsNullOrEmpty(rowFirmwareNumber))
                                    {
                                        break;
                                    }
                                }
                            }

                            // If the firmware number matches, add the model number to results
                            if (string.Equals(rowFirmwareNumber, firmwareNumber, StringComparison.OrdinalIgnoreCase))
                            {
                                resultBuilder.AppendLine($"Model Number: {modelNumber}");
                            }
                        }
                    }
                }

                // Display search results in the txtResult control
                txtResult.Text = resultBuilder.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving data: {ex.Message}");
            }
        }

        // Event handler for switching to the PnumSearchForm
        private void btnToggleToPnumSearchForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            var pnumSearchForm = new PnumSearchForm(_localFilePath, _accessToken, _siteId);
            pnumSearchForm.Show();
        }
    }
}
