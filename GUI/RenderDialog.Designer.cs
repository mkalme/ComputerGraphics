namespace GUI {
    partial class RenderDialog {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GraphicsPictureBox = new System.Windows.Forms.PictureBox();
            this.PictureBoxContainer = new System.Windows.Forms.Panel();
            this.ProgressPanelContainer = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsPictureBox)).BeginInit();
            this.PictureBoxContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // GraphicsPictureBox
            // 
            this.GraphicsPictureBox.Location = new System.Drawing.Point(0, 0);
            this.GraphicsPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.GraphicsPictureBox.Name = "GraphicsPictureBox";
            this.GraphicsPictureBox.Size = new System.Drawing.Size(1256, 751);
            this.GraphicsPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.GraphicsPictureBox.TabIndex = 1;
            this.GraphicsPictureBox.TabStop = false;
            // 
            // PictureBoxContainer
            // 
            this.PictureBoxContainer.AutoScroll = true;
            this.PictureBoxContainer.Controls.Add(this.GraphicsPictureBox);
            this.PictureBoxContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBoxContainer.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxContainer.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBoxContainer.Name = "PictureBoxContainer";
            this.PictureBoxContainer.Size = new System.Drawing.Size(1256, 751);
            this.PictureBoxContainer.TabIndex = 2;
            this.PictureBoxContainer.SizeChanged += new System.EventHandler(this.Container_SizeChanged);
            // 
            // ProgressPanelContainer
            // 
            this.ProgressPanelContainer.AutoSize = true;
            this.ProgressPanelContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ProgressPanelContainer.Location = new System.Drawing.Point(12, 12);
            this.ProgressPanelContainer.Name = "ProgressPanelContainer";
            this.ProgressPanelContainer.Size = new System.Drawing.Size(0, 0);
            this.ProgressPanelContainer.TabIndex = 2;
            // 
            // RenderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1256, 751);
            this.Controls.Add(this.ProgressPanelContainer);
            this.Controls.Add(this.PictureBoxContainer);
            this.Name = "RenderDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rendered scene";
            this.Load += new System.EventHandler(this.RenderDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsPictureBox)).EndInit();
            this.PictureBoxContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GraphicsPictureBox;
        private System.Windows.Forms.Panel PictureBoxContainer;
        private System.Windows.Forms.Panel ProgressPanelContainer;
    }
}