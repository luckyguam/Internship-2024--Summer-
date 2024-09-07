namespace Beta4
{
    partial class PnumSearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private PictureBox pbLogo;
        private TextBox txtModelNumber;
        private Button btnSearch;
        private TextBox txtResult;
        private Label lblModelNumber;
        private Label lblResult;
        private Button btnOpenFolder;
        private Button btnToggleToFnSearchForm; // New button for toggling to FnSearchForm

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pbLogo = new PictureBox();
            txtModelNumber = new TextBox();
            btnSearch = new Button();
            txtResult = new TextBox();
            lblModelNumber = new Label();
            lblResult = new Label();
            btnOpenFolder = new Button();
            btnToggleToFnSearchForm = new Button();
            label2 = new Label();
            label3 = new Label();
            btnDownloadFM = new Button();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            SuspendLayout();
            // 
            // pbLogo
            // 
            pbLogo.BackColor = SystemColors.ActiveCaptionText;
            pbLogo.Image = Properties.Resources.VJX;
            pbLogo.Location = new Point(13, 21);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(154, 47);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 0;
            pbLogo.TabStop = false;
            // 
            // txtModelNumber
            // 
            txtModelNumber.Location = new Point(109, 100);
            txtModelNumber.Margin = new Padding(4, 2, 4, 2);
            txtModelNumber.Name = "txtModelNumber";
            txtModelNumber.Size = new Size(256, 23);
            txtModelNumber.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(152, 127);
            btnSearch.Margin = new Padding(4, 2, 4, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(93, 31);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(13, 162);
            txtResult.Margin = new Padding(4, 2, 4, 2);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.ScrollBars = ScrollBars.Vertical;
            txtResult.Size = new Size(363, 146);
            txtResult.TabIndex = 2;
            // 
            // lblModelNumber
            // 
            lblModelNumber.AutoSize = true;
            lblModelNumber.BackColor = SystemColors.ButtonFace;
            lblModelNumber.Location = new Point(13, 103);
            lblModelNumber.Margin = new Padding(4, 0, 4, 0);
            lblModelNumber.Name = "lblModelNumber";
            lblModelNumber.Size = new Size(88, 15);
            lblModelNumber.TabIndex = 3;
            lblModelNumber.Text = "Model Number";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.BackColor = SystemColors.ButtonFace;
            lblResult.ForeColor = SystemColors.ControlText;
            lblResult.Location = new Point(13, 143);
            lblResult.Margin = new Padding(4, 0, 4, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(45, 15);
            lblResult.TabIndex = 4;
            lblResult.Text = "Result: ";
            // 
            // btnOpenFolder
            // 
            btnOpenFolder.Location = new Point(253, 127);
            btnOpenFolder.Margin = new Padding(4, 2, 4, 2);
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.Size = new Size(92, 31);
            btnOpenFolder.TabIndex = 5;
            btnOpenFolder.Text = "Open Folder";
            btnOpenFolder.UseVisualStyleBackColor = true;
            btnOpenFolder.Click += btnOpenFolder_Click;
            // 
            // btnToggleToFnSearchForm
            // 
            btnToggleToFnSearchForm.Location = new Point(12, 315);
            btnToggleToFnSearchForm.Name = "btnToggleToFnSearchForm";
            btnToggleToFnSearchForm.Size = new Size(180, 23);
            btnToggleToFnSearchForm.TabIndex = 6;
            btnToggleToFnSearchForm.Text = "Search by Firmware#";
            btnToggleToFnSearchForm.UseVisualStyleBackColor = true;
            btnToggleToFnSearchForm.Click += btnToggleToFnSearchForm_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 8;
            label2.Text = "label2";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 10F);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(180, 9);
            label3.Name = "label3";
            label3.Size = new Size(185, 77);
            label3.TabIndex = 9;
            label3.Text = "This Application allow you to find the FirmwareNumber and open it's folder from Sharepoint";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnDownloadFM
            // 
            btnDownloadFM.Location = new Point(253, 313);
            btnDownloadFM.Name = "btnDownloadFM";
            btnDownloadFM.Size = new Size(80, 25);
            btnDownloadFM.TabIndex = 10;
            btnDownloadFM.Text = "Download-F";
            btnDownloadFM.UseVisualStyleBackColor = true;
            btnDownloadFM.Click += btnDownloadFM_Click;
            // 
            // PnumSearchForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(386, 350);
            Controls.Add(btnDownloadFM);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnToggleToFnSearchForm);
            Controls.Add(btnOpenFolder);
            Controls.Add(lblResult);
            Controls.Add(lblModelNumber);
            Controls.Add(txtResult);
            Controls.Add(btnSearch);
            Controls.Add(txtModelNumber);
            Controls.Add(pbLogo);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4, 2, 4, 2);
            Name = "PnumSearchForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pnum Search Form";
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label2;
        private Label label3;
        private Button btnDownloadFM;
    }
}
