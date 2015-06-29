namespace APOBlabs
{
    partial class Compression
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
            this.Compressor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.aSize = new System.Windows.Forms.Label();
            this.bSize = new System.Windows.Forms.Label();
            this.CompressionLevel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Compressor
            // 
            this.Compressor.FormattingEnabled = true;
            this.Compressor.Items.AddRange(new object[] {
            "Huffman",
            "Difference",
            "Block"});
            this.Compressor.Location = new System.Drawing.Point(12, 29);
            this.Compressor.Name = "Compressor";
            this.Compressor.Size = new System.Drawing.Size(193, 24);
            this.Compressor.TabIndex = 0;
            this.Compressor.SelectedIndexChanged += new System.EventHandler(this.Compressor_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Compression";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(9, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Size before:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(9, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Size after:";
            // 
            // aSize
            // 
            this.aSize.ForeColor = System.Drawing.Color.White;
            this.aSize.Location = new System.Drawing.Point(113, 88);
            this.aSize.Name = "aSize";
            this.aSize.Size = new System.Drawing.Size(92, 17);
            this.aSize.TabIndex = 5;
            this.aSize.Text = "0";
            this.aSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bSize
            // 
            this.bSize.ForeColor = System.Drawing.Color.White;
            this.bSize.Location = new System.Drawing.Point(113, 71);
            this.bSize.Name = "bSize";
            this.bSize.Size = new System.Drawing.Size(92, 17);
            this.bSize.TabIndex = 4;
            this.bSize.Text = "0";
            this.bSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CompressionLevel
            // 
            this.CompressionLevel.ForeColor = System.Drawing.Color.White;
            this.CompressionLevel.Location = new System.Drawing.Point(142, 105);
            this.CompressionLevel.Name = "CompressionLevel";
            this.CompressionLevel.Size = new System.Drawing.Size(63, 17);
            this.CompressionLevel.TabIndex = 7;
            this.CompressionLevel.Text = "0";
            this.CompressionLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(9, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Compression level:";
            // 
            // Compression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(216, 135);
            this.Controls.Add(this.CompressionLevel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.aSize);
            this.Controls.Add(this.bSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Compressor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Compression";
            this.Text = "Compression";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Compressor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label aSize;
        private System.Windows.Forms.Label bSize;
        private System.Windows.Forms.Label CompressionLevel;
        private System.Windows.Forms.Label label5;
    }
}