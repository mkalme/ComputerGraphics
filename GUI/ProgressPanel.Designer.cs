namespace GUI {
    partial class ProgressPanel {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LinesRichtextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // LinesRichtextBox
            // 
            this.LinesRichtextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.LinesRichtextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LinesRichtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LinesRichtextBox.ForeColor = System.Drawing.Color.LightGray;
            this.LinesRichtextBox.Location = new System.Drawing.Point(5, 5);
            this.LinesRichtextBox.Name = "LinesRichtextBox";
            this.LinesRichtextBox.ReadOnly = true;
            this.LinesRichtextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LinesRichtextBox.Size = new System.Drawing.Size(235, 107);
            this.LinesRichtextBox.TabIndex = 0;
            this.LinesRichtextBox.Text = "";
            this.LinesRichtextBox.WordWrap = false;
            // 
            // ProgressPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.LinesRichtextBox);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ProgressPanel";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(245, 117);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox LinesRichtextBox;
    }
}
