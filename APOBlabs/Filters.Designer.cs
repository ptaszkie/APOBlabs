namespace APOBlabs
{
    partial class Filters
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
            this.t00 = new System.Windows.Forms.TextBox();
            this.t01 = new System.Windows.Forms.TextBox();
            this.t02 = new System.Windows.Forms.TextBox();
            this.t10 = new System.Windows.Forms.TextBox();
            this.t11 = new System.Windows.Forms.TextBox();
            this.t12 = new System.Windows.Forms.TextBox();
            this.t20 = new System.Windows.Forms.TextBox();
            this.t21 = new System.Windows.Forms.TextBox();
            this.t22 = new System.Windows.Forms.TextBox();
            this.Preset = new System.Windows.Forms.ComboBox();
            this.divider = new System.Windows.Forms.TextBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // t00
            // 
            this.t00.Location = new System.Drawing.Point(12, 89);
            this.t00.Name = "t00";
            this.t00.Size = new System.Drawing.Size(52, 22);
            this.t00.TabIndex = 0;
            this.t00.Text = "0";
            this.t00.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t00.TextChanged += new System.EventHandler(this.t00_TextChanged);
            // 
            // t01
            // 
            this.t01.Location = new System.Drawing.Point(70, 89);
            this.t01.Name = "t01";
            this.t01.Size = new System.Drawing.Size(52, 22);
            this.t01.TabIndex = 1;
            this.t01.Text = "0";
            this.t01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t01.TextChanged += new System.EventHandler(this.t00_TextChanged);
            // 
            // t02
            // 
            this.t02.Location = new System.Drawing.Point(128, 89);
            this.t02.Name = "t02";
            this.t02.Size = new System.Drawing.Size(52, 22);
            this.t02.TabIndex = 2;
            this.t02.Text = "0";
            this.t02.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t02.TextChanged += new System.EventHandler(this.t00_TextChanged);
            // 
            // t10
            // 
            this.t10.Location = new System.Drawing.Point(12, 117);
            this.t10.Name = "t10";
            this.t10.Size = new System.Drawing.Size(52, 22);
            this.t10.TabIndex = 4;
            this.t10.Text = "0";
            this.t10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t10.TextChanged += new System.EventHandler(this.t00_TextChanged);
            // 
            // t11
            // 
            this.t11.Location = new System.Drawing.Point(70, 117);
            this.t11.Name = "t11";
            this.t11.Size = new System.Drawing.Size(52, 22);
            this.t11.TabIndex = 5;
            this.t11.Text = "1";
            this.t11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t11.TextChanged += new System.EventHandler(this.t00_TextChanged);
            // 
            // t12
            // 
            this.t12.Location = new System.Drawing.Point(128, 117);
            this.t12.Name = "t12";
            this.t12.Size = new System.Drawing.Size(52, 22);
            this.t12.TabIndex = 6;
            this.t12.Text = "0";
            this.t12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t12.TextChanged += new System.EventHandler(this.t00_TextChanged);
            // 
            // t20
            // 
            this.t20.Location = new System.Drawing.Point(12, 145);
            this.t20.Name = "t20";
            this.t20.Size = new System.Drawing.Size(52, 22);
            this.t20.TabIndex = 10;
            this.t20.Text = "0";
            this.t20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t20.TextChanged += new System.EventHandler(this.t00_TextChanged);
            // 
            // t21
            // 
            this.t21.Location = new System.Drawing.Point(70, 145);
            this.t21.Name = "t21";
            this.t21.Size = new System.Drawing.Size(52, 22);
            this.t21.TabIndex = 11;
            this.t21.Text = "0";
            this.t21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t21.TextChanged += new System.EventHandler(this.t00_TextChanged);
            // 
            // t22
            // 
            this.t22.Location = new System.Drawing.Point(128, 145);
            this.t22.Name = "t22";
            this.t22.Size = new System.Drawing.Size(52, 22);
            this.t22.TabIndex = 12;
            this.t22.Text = "0";
            this.t22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t22.TextChanged += new System.EventHandler(this.t00_TextChanged);
            // 
            // Preset
            // 
            this.Preset.FormattingEnabled = true;
            this.Preset.Items.AddRange(new object[] {
            "Custom",
            "Laplasjan",
            "Gradiant Gx",
            "Gradiant Gy",
            "Edge detection #1",
            "Edge detection #2",
            "Edge detection #3",
            "Sobel Gx",
            "Sobel Gy"});
            this.Preset.Location = new System.Drawing.Point(12, 28);
            this.Preset.Name = "Preset";
            this.Preset.Size = new System.Drawing.Size(168, 24);
            this.Preset.TabIndex = 13;
            this.Preset.SelectedIndexChanged += new System.EventHandler(this.Preset_SelectedIndexChanged);
            // 
            // divider
            // 
            this.divider.Location = new System.Drawing.Point(128, 173);
            this.divider.Name = "divider";
            this.divider.Size = new System.Drawing.Size(52, 22);
            this.divider.TabIndex = 14;
            this.divider.Text = "1";
            this.divider.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnCancel.Location = new System.Drawing.Point(49, 213);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 28);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOK
            // 
            this.BtnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnOK.Location = new System.Drawing.Point(131, 213);
            this.BtnOK.Margin = new System.Windows.Forms.Padding(4);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(49, 28);
            this.BtnOK.TabIndex = 15;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Preset:";
            // 
            // Filters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(195, 254);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.divider);
            this.Controls.Add(this.Preset);
            this.Controls.Add(this.t22);
            this.Controls.Add(this.t21);
            this.Controls.Add(this.t20);
            this.Controls.Add(this.t12);
            this.Controls.Add(this.t11);
            this.Controls.Add(this.t10);
            this.Controls.Add(this.t02);
            this.Controls.Add(this.t01);
            this.Controls.Add(this.t00);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Filters";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox t00;
        private System.Windows.Forms.TextBox t01;
        private System.Windows.Forms.TextBox t02;
        private System.Windows.Forms.TextBox t10;
        private System.Windows.Forms.TextBox t11;
        private System.Windows.Forms.TextBox t12;
        private System.Windows.Forms.TextBox t20;
        private System.Windows.Forms.TextBox t21;
        private System.Windows.Forms.TextBox t22;
        private System.Windows.Forms.ComboBox Preset;
        private System.Windows.Forms.TextBox divider;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Label label1;
    }
}