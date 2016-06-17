namespace TrafficLights
{
    partial class Manual
    {
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
            if (disposing && (components != null))
            {
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
            this.rtbxManual = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbxManual
            // 
            this.rtbxManual.Location = new System.Drawing.Point(12, 12);
            this.rtbxManual.Name = "rtbxManual";
            this.rtbxManual.Size = new System.Drawing.Size(665, 452);
            this.rtbxManual.TabIndex = 0;
            this.rtbxManual.Text = "";
            // 
            // Manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 476);
            this.Controls.Add(this.rtbxManual);
            this.Name = "Manual";
            this.Text = "Manual";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbxManual;
    }
}