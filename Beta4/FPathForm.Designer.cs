namespace Beta4
{
    partial class FPathForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Button btnAutoSearch;

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
            txtFilePath = new TextBox();
            btnNext = new Button();
            btnEdit = new Button();
            label1 = new Label();
            pbLogo = new PictureBox();
            btnAutoSearch = new Button();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            SuspendLayout();
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(13, 77);
            txtFilePath.Multiline = true;
            txtFilePath.Name = "txtFilePath";
            txtFilePath.ReadOnly = true;
            txtFilePath.Size = new Size(260, 32);
            txtFilePath.TabIndex = 0;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(157, 115);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(83, 38);
            btnNext.TabIndex = 2;
            btnNext.Text = "Save and Go";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(36, 115);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(83, 38);
            btnEdit.TabIndex = 5;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(36, 167);
            label1.Name = "label1";
            label1.Size = new Size(204, 35);
            label1.TabIndex = 3;
            label1.Text = "Note: If Update the Filepath, Make sure to Hit the SAVE button.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.UseWaitCursor = true;
            // 
            // pbLogo
            // 
            pbLogo.BackColor = SystemColors.ActiveCaptionText;
            pbLogo.Image = Properties.Resources.VJX;
            pbLogo.Location = new Point(13, 13);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(149, 47);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 4;
            pbLogo.TabStop = false;
            // 
            // btnAutoSearch
            // 
            btnAutoSearch.Location = new Point(177, 22);
            btnAutoSearch.Name = "btnAutoSearch";
            btnAutoSearch.Size = new Size(83, 38);
            btnAutoSearch.TabIndex = 6;
            btnAutoSearch.Text = "AutoSearch";
            btnAutoSearch.UseVisualStyleBackColor = true;
            btnAutoSearch.Click += btnAutoSearch_Click;
            // 
            // FPathForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoScroll = true;
            AutoSize = true;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(282, 212);
            Controls.Add(pbLogo);
            Controls.Add(label1);
            Controls.Add(btnNext);
            Controls.Add(btnEdit);
            Controls.Add(txtFilePath);
            Controls.Add(btnAutoSearch);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            HelpButton = true;
            Name = "FPathForm";
            Padding = new Padding(10);
            Text = "File Path Manager";
            Load += FPathForm_Load;
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
