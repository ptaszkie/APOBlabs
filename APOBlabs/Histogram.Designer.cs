namespace APOBlabs
{
    partial class Histogram
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
            this.checkRed = new System.Windows.Forms.CheckBox();
            this.checkGreen = new System.Windows.Forms.CheckBox();
            this.checkBlue = new System.Windows.Forms.CheckBox();
            this.labelValue = new System.Windows.Forms.Label();
            this.Value = new System.Windows.Forms.Label();
            this.labelR = new System.Windows.Forms.Label();
            this.RValue = new System.Windows.Forms.Label();
            this.GValue = new System.Windows.Forms.Label();
            this.labelG = new System.Windows.Forms.Label();
            this.BValue = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.GreyValue = new System.Windows.Forms.Label();
            this.labelGrey = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramImage)).BeginInit();
            this.SuspendLayout();
            // 
            // HistogramImage
            // 
            this.HistogramImage.BackColor = System.Drawing.Color.Black;
            this.HistogramImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.HistogramImage.Location = new System.Drawing.Point(0, 0);
            this.HistogramImage.Margin = new System.Windows.Forms.Padding(0);
            this.HistogramImage.Name = "HistogramImage";
            this.HistogramImage.Size = new System.Drawing.Size(514, 220);
            this.HistogramImage.TabIndex = 0;
            this.HistogramImage.TabStop = false;
            this.HistogramImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HistogramImage_MouseMove);
            // 
            // checkRed
            // 
            this.checkRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkRed.AutoSize = true;
            this.checkRed.ForeColor = System.Drawing.Color.White;
            this.checkRed.Location = new System.Drawing.Point(308, 223);
            this.checkRed.Name = "checkRed";
            this.checkRed.Size = new System.Drawing.Size(56, 21);
            this.checkRed.TabIndex = 1;
            this.checkRed.Text = "Red";
            this.checkRed.UseVisualStyleBackColor = true;
            this.checkRed.CheckedChanged += new System.EventHandler(this.DrawHistogram);
            // 
            // checkGreen
            // 
            this.checkGreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkGreen.AutoSize = true;
            this.checkGreen.ForeColor = System.Drawing.Color.White;
            this.checkGreen.Location = new System.Drawing.Point(370, 223);
            this.checkGreen.Name = "checkGreen";
            this.checkGreen.Size = new System.Drawing.Size(70, 21);
            this.checkGreen.TabIndex = 2;
            this.checkGreen.Text = "Green";
            this.checkGreen.UseVisualStyleBackColor = true;
            this.checkGreen.CheckedChanged += new System.EventHandler(this.DrawHistogram);
            // 
            // checkBlue
            // 
            this.checkBlue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBlue.AutoSize = true;
            this.checkBlue.ForeColor = System.Drawing.Color.White;
            this.checkBlue.Location = new System.Drawing.Point(446, 223);
            this.checkBlue.Name = "checkBlue";
            this.checkBlue.Size = new System.Drawing.Size(58, 21);
            this.checkBlue.TabIndex = 3;
            this.checkBlue.Text = "Blue";
            this.checkBlue.UseVisualStyleBackColor = true;
            this.checkBlue.CheckedChanged += new System.EventHandler(this.DrawHistogram);
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.ForeColor = System.Drawing.Color.White;
            this.labelValue.Location = new System.Drawing.Point(12, 224);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(48, 17);
            this.labelValue.TabIndex = 4;
            this.labelValue.Text = "Value:";
            // 
            // Value
            // 
            this.Value.AutoSize = true;
            this.Value.ForeColor = System.Drawing.Color.White;
            this.Value.Location = new System.Drawing.Point(62, 224);
            this.Value.Name = "Value";
            this.Value.Size = new System.Drawing.Size(16, 17);
            this.Value.TabIndex = 5;
            this.Value.Text = "0";
            // 
            // labelR
            // 
            this.labelR.AutoSize = true;
            this.labelR.ForeColor = System.Drawing.Color.White;
            this.labelR.Location = new System.Drawing.Point(13, 247);
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(22, 17);
            this.labelR.TabIndex = 6;
            this.labelR.Text = "R:";
            // 
            // RValue
            // 
            this.RValue.AutoSize = true;
            this.RValue.ForeColor = System.Drawing.Color.White;
            this.RValue.Location = new System.Drawing.Point(31, 247);
            this.RValue.Name = "RValue";
            this.RValue.Size = new System.Drawing.Size(31, 17);
            this.RValue.TabIndex = 7;
            this.RValue.Text = "- - -";
            // 
            // GValue
            // 
            this.GValue.AutoSize = true;
            this.GValue.ForeColor = System.Drawing.Color.White;
            this.GValue.Location = new System.Drawing.Point(210, 247);
            this.GValue.Name = "GValue";
            this.GValue.Size = new System.Drawing.Size(31, 17);
            this.GValue.TabIndex = 9;
            this.GValue.Text = "- - -";
            // 
            // labelG
            // 
            this.labelG.AutoSize = true;
            this.labelG.ForeColor = System.Drawing.Color.White;
            this.labelG.Location = new System.Drawing.Point(192, 247);
            this.labelG.Name = "labelG";
            this.labelG.Size = new System.Drawing.Size(23, 17);
            this.labelG.TabIndex = 8;
            this.labelG.Text = "G:";
            // 
            // BValue
            // 
            this.BValue.AutoSize = true;
            this.BValue.ForeColor = System.Drawing.Color.White;
            this.BValue.Location = new System.Drawing.Point(398, 247);
            this.BValue.Name = "BValue";
            this.BValue.Size = new System.Drawing.Size(31, 17);
            this.BValue.TabIndex = 11;
            this.BValue.Text = "- - -";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.ForeColor = System.Drawing.Color.White;
            this.labelB.Location = new System.Drawing.Point(380, 247);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(21, 17);
            this.labelB.TabIndex = 10;
            this.labelB.Text = "B:";
            // 
            // GreyValue
            // 
            this.GreyValue.AutoSize = true;
            this.GreyValue.ForeColor = System.Drawing.Color.White;
            this.GreyValue.Location = new System.Drawing.Point(197, 224);
            this.GreyValue.Name = "GreyValue";
            this.GreyValue.Size = new System.Drawing.Size(16, 17);
            this.GreyValue.TabIndex = 13;
            this.GreyValue.Text = "0";
            // 
            // labelGrey
            // 
            this.labelGrey.AutoSize = true;
            this.labelGrey.ForeColor = System.Drawing.Color.White;
            this.labelGrey.Location = new System.Drawing.Point(155, 224);
            this.labelGrey.Name = "labelGrey";
            this.labelGrey.Size = new System.Drawing.Size(43, 17);
            this.labelGrey.TabIndex = 12;
            this.labelGrey.Text = "Grey:";
            // 
            // Histogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(514, 273);
            this.Controls.Add(this.GreyValue);
            this.Controls.Add(this.labelGrey);
            this.Controls.Add(this.BValue);
            this.Controls.Add(this.labelB);
            this.Controls.Add(this.GValue);
            this.Controls.Add(this.labelG);
            this.Controls.Add(this.RValue);
            this.Controls.Add(this.labelR);
            this.Controls.Add(this.Value);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.checkBlue);
            this.Controls.Add(this.checkGreen);
            this.Controls.Add(this.checkRed);
            this.Controls.Add(this.HistogramImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(1000, 320);
            this.Name = "Histogram";
            this.Text = "Histogram";
            this.ResizeEnd += new System.EventHandler(this.Histogram_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.HistogramImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox HistogramImage;
        private System.Windows.Forms.CheckBox checkRed;
        private System.Windows.Forms.CheckBox checkGreen;
        private System.Windows.Forms.CheckBox checkBlue;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.Label Value;
        private System.Windows.Forms.Label labelR;
        private System.Windows.Forms.Label RValue;
        private System.Windows.Forms.Label GValue;
        private System.Windows.Forms.Label labelG;
        private System.Windows.Forms.Label BValue;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Label GreyValue;
        private System.Windows.Forms.Label labelGrey;
    }
}