namespace Beta4
{
    partial class FnSearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private PictureBox pbLogo;
        private TextBox txtModelNumber;
        private Button btnSearch;
        private TextBox txtResult;
        private Label lblModelNumber;
        private Button btnToggleToPnumSearchForm;
        private ListBox lstModelNumbers; // Added ListBox

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
            btnToggleToPnumSearchForm = new Button();
            lstModelNumbers = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            SuspendLayout();
            // 
            // pbLogo
            // 
            pbLogo.BackColor = SystemColors.ActiveCaptionText;
            pbLogo.Image = Properties.Resources.VJX;
            pbLogo.Location = new Point(12, 16);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(164, 59);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 0;
            pbLogo.TabStop = false;
            // 
            // txtModelNumber
            // 
            txtModelNumber.Location = new Point(107, 100);
            txtModelNumber.Margin = new Padding(4, 2, 4, 2);
            txtModelNumber.Name = "txtModelNumber";
            txtModelNumber.Size = new Size(95, 23);
            txtModelNumber.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(109, 127);
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
            txtResult.Location = new Point(13, 304);
            txtResult.Margin = new Padding(4, 2, 4, 2);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.ScrollBars = ScrollBars.Vertical;
            txtResult.Size = new Size(352, 124);
            txtResult.TabIndex = 2;
            // 
            // lblModelNumber
            // 
            lblModelNumber.AutoSize = true;
            lblModelNumber.BackColor = SystemColors.ButtonFace;
            lblModelNumber.Location = new Point(13, 103);
            lblModelNumber.Margin = new Padding(4, 0, 4, 0);
            lblModelNumber.Name = "lblModelNumber";
            lblModelNumber.Size = new Size(86, 15);
            lblModelNumber.TabIndex = 3;
            lblModelNumber.Text = "FirmwareNum:";
            // 
            // btnToggleToPnumSearchForm
            // 
            btnToggleToPnumSearchForm.Location = new Point(12, 434);
            btnToggleToPnumSearchForm.Name = "btnToggleToPnumSearchForm";
            btnToggleToPnumSearchForm.Size = new Size(155, 23);
            btnToggleToPnumSearchForm.TabIndex = 5;
            btnToggleToPnumSearchForm.Text = "Search By Model#";
            btnToggleToPnumSearchForm.UseVisualStyleBackColor = true;
            btnToggleToPnumSearchForm.Click += btnToggleToPnumSearchForm_Click;
            // 
            // lstModelNumbers
            // 
            lstModelNumbers.ItemHeight = 15;
            lstModelNumbers.Location = new Point(12, 163);
            lstModelNumbers.Name = "lstModelNumbers";
            lstModelNumbers.Size = new Size(353, 124);
            lstModelNumbers.TabIndex = 6;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.CausesValidation = false;
            label1.Font = new Font("Segoe UI", 10F);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.ImageAlign = ContentAlignment.BottomCenter;
            label1.Location = new Point(182, 16);
            label1.Name = "label1";
            label1.Size = new Size(183, 59);
            label1.TabIndex = 7;
            label1.Text = "This application allow you to Find Model Number using the Firmware number";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 8F);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(209, 103);
            label2.Name = "label2";
            label2.Size = new Size(156, 43);
            label2.TabIndex = 8;
            label2.Text = "Note: If you don't know the full Firmware# type a part of it Example:- P0, P00,P000.R1";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(120, 272);
            label3.Name = "label3";
            label3.Size = new Size(245, 15);
            label3.TabIndex = 9;
            label3.Text = "Click on the Firmware to view model number";
            // 
            // FnSearchForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(388, 470);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnToggleToPnumSearchForm);
            Controls.Add(lblModelNumber);
            Controls.Add(txtResult);
            Controls.Add(btnSearch);
            Controls.Add(txtModelNumber);
            Controls.Add(pbLogo);
            Controls.Add(lstModelNumbers);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4, 2, 4, 2);
            Name = "FnSearchForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fn Search Form";
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label label2;
        private Label label3;
    }
}
