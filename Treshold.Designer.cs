namespace APOBlabs
{
    partial class Treshold
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
            this.HistogramImage = new System.Windows.Forms.PictureBox();
            this.ZipVal = new System.Windows.Forms.TrackBar();
            this.Level = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Reverse = new System.Windows.Forms.CheckBox();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnAbort = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZipVal)).BeginInit();
            this.SuspendLayout();
            // 
            // HistogramImage
            // 
            this.HistogramImage.BackColor = System.Drawing.Color.Black;
            this.HistogramImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.HistogramImage.Location = new System.Drawing.Point(0, 0);
            this.HistogramImage.Margin = new System.Windows.Forms.Padding(0);
            this.HistogramImage.Name = "HistogramImage";
            this.HistogramImage.Size = new System.Drawing.Size(515, 197);
            this.HistogramImage.TabIndex = 0;
            this.HistogramImage.TabStop = false;
            // 
            // ZipVal
            // 
            this.ZipVal.Location = new System.Drawing.Point(-9, 188);
            this.ZipVal.Margin = new System.Windows.Forms.Padding(0);
            this.ZipVal.Maximum = 255;
            this.ZipVal.Name = "ZipVal";
            this.ZipVal.Size = new System.Drawing.Size(533, 56);
            this.ZipVal.TabIndex = 1;
            this.ZipVal.TickFrequency = 5;
            this.ZipVal.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.ZipVal.ValueChanged += new System.EventHandler(this.ZipVal_ValueChanged);
            // 
            // Level
            // 
            this.Level.Location = new System.Drawing.Point(65, 227);
            this.Level.Margin = new System.Windows.Forms.Padding(4);
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(52, 22);
            this.Level.TabIndex = 2;
            this.Level.Text = "0";
            this.Level.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Level.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 230);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Level";
            // 
            // Reverse
            // 
            this.Reverse.AutoSize = true;
            this.Reverse.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Reverse.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Reverse.ForeColor = System.Drawing.Color.White;
            this.Reverse.Location = new System.Drawing.Point(196, 229);
            this.Reverse.Margin = new System.Windows.Forms.Padding(4);
            this.Reverse.Name = "Reverse";
            this.Reverse.Size = new System.Drawing.Size(83, 21);
            this.Reverse.TabIndex = 4;
            this.Reverse.Text = "Reverse";
            this.Reverse.UseVisualStyleBackColor = true;
            this.Reverse.CheckedChanged += new System.EventHandler(this.DrawHistogram);
            // 
            // BtnOK
            // 
            this.BtnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnOK.Location = new System.Drawing.Point(459, 224);
            this.BtnOK.Margin = new System.Windows.Forms.Padding(4);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(49, 28);
            this.BtnOK.TabIndex = 5;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnAbort
            // 
            this.BtnAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnAbort.Location = new System.Drawing.Point(377, 224);
            this.BtnAbort.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAbort.Name = "BtnAbort";
            this.BtnAbort.Size = new System.Drawing.Size(75, 28);
            this.BtnAbort.TabIndex = 6;
            this.BtnAbort.Text = "Cancel";
            this.BtnAbort.UseVisualStyleBackColor = true;
            this.BtnAbort.Click += new System.EventHandler(this.BtnAbort_Click);
            // 
            // Treshold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(515, 258);
            this.Controls.Add(this.BtnAbort);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.Reverse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Level);
            this.Controls.Add(this.HistogramImage);
            this.Controls.Add(this.ZipVal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(533, 305);
            this.MinimumSize = new System.Drawing.Size(533, 305);
            this.Name = "Treshold";
            this.Text = "Treshold";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.HistogramImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZipVal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox HistogramImage;
        private System.Windows.Forms.TrackBar ZipVal;
        private System.Windows.Forms.TextBox Level;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Reverse;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnAbort;
    }
}