namespace TrafficLights
{
    partial class SavedManagerForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxColumn = new System.Windows.Forms.TextBox();
            this.textBoxRow = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPlace = new System.Windows.Forms.Button();
            this.tbPlaceId = new System.Windows.Forms.TextBox();
            this.tbPlaceColumn = new System.Windows.Forms.TextBox();
            this.tbPlaceRow = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(15, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 357);
            this.panel1.TabIndex = 0;
            // 
            // textBoxColumn
            // 
            this.textBoxColumn.Location = new System.Drawing.Point(451, 49);
            this.textBoxColumn.Name = "textBoxColumn";
            this.textBoxColumn.Size = new System.Drawing.Size(100, 22);
            this.textBoxColumn.TabIndex = 1;
            // 
            // textBoxRow
            // 
            this.textBoxRow.Location = new System.Drawing.Point(451, 92);
            this.textBoxRow.Name = "textBoxRow";
            this.textBoxRow.Size = new System.Drawing.Size(100, 22);
            this.textBoxRow.TabIndex = 2;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(415, 133);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(109, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(395, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Column";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Row";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(398, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(401, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Column";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(401, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Row";
            // 
            // btnPlace
            // 
            this.btnPlace.Location = new System.Drawing.Point(415, 321);
            this.btnPlace.Name = "btnPlace";
            this.btnPlace.Size = new System.Drawing.Size(75, 23);
            this.btnPlace.TabIndex = 9;
            this.btnPlace.Text = "Place";
            this.btnPlace.UseVisualStyleBackColor = true;
            this.btnPlace.Click += new System.EventHandler(this.btnPlace_Click);
            // 
            // tbPlaceId
            // 
            this.tbPlaceId.Location = new System.Drawing.Point(451, 187);
            this.tbPlaceId.Name = "tbPlaceId";
            this.tbPlaceId.Size = new System.Drawing.Size(100, 22);
            this.tbPlaceId.TabIndex = 10;
            // 
            // tbPlaceColumn
            // 
            this.tbPlaceColumn.Location = new System.Drawing.Point(451, 230);
            this.tbPlaceColumn.Name = "tbPlaceColumn";
            this.tbPlaceColumn.Size = new System.Drawing.Size(100, 22);
            this.tbPlaceColumn.TabIndex = 11;
            // 
            // tbPlaceRow
            // 
            this.tbPlaceRow.Location = new System.Drawing.Point(451, 273);
            this.tbPlaceRow.Name = "tbPlaceRow";
            this.tbPlaceRow.Size = new System.Drawing.Size(100, 22);
            this.tbPlaceRow.TabIndex = 12;
            // 
            // SavedManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 379);
            this.Controls.Add(this.tbPlaceRow);
            this.Controls.Add(this.tbPlaceColumn);
            this.Controls.Add(this.tbPlaceId);
            this.Controls.Add(this.btnPlace);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxRow);
            this.Controls.Add(this.textBoxColumn);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Baskerville Old Face", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SavedManagerForm";
            this.Text = "SavedManagerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxColumn;
        private System.Windows.Forms.TextBox textBoxRow;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPlace;
        private System.Windows.Forms.TextBox tbPlaceId;
        private System.Windows.Forms.TextBox tbPlaceColumn;
        private System.Windows.Forms.TextBox tbPlaceRow;
    }
}