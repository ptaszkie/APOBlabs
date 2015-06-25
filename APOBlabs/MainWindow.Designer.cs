namespace APOBlabs
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFile_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuImage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuImage_Undo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuImage_Duplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuImage_Histogram = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOperations = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOperations_Adjustment = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Adj_Gamma = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Adj_Contrast = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Adj_Brightness = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuOper_Adj_Greyscale = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Adj_Negative = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Adj_Treshold = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Adj_Posterize = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOperations_Equ_Neighborhood = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Equ_Average = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Equ_Random = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Equ_Neighbor = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Calculation = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOper_Filters = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuBar
            // 
            this.MenuBar.BackColor = System.Drawing.Color.Black;
            this.MenuBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuImage,
            this.MenuOperations,
            this.MenuAbout});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(870, 28);
            this.MenuBar.TabIndex = 5;
            this.MenuBar.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.BackColor = System.Drawing.Color.Black;
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.MenuFile_Save,
            this.MenuFile_SaveAs});
            this.MenuFile.ForeColor = System.Drawing.Color.DarkGray;
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(44, 24);
            this.MenuFile.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(226, 24);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.MenuFile_Open_Click);
            // 
            // MenuFile_Save
            // 
            this.MenuFile_Save.Enabled = false;
            this.MenuFile_Save.Name = "MenuFile_Save";
            this.MenuFile_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuFile_Save.Size = new System.Drawing.Size(226, 24);
            this.MenuFile_Save.Text = "Save";
            this.MenuFile_Save.DropDownOpened += new System.EventHandler(this.check);
            // 
            // MenuFile_SaveAs
            // 
            this.MenuFile_SaveAs.Enabled = false;
            this.MenuFile_SaveAs.Name = "MenuFile_SaveAs";
            this.MenuFile_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.MenuFile_SaveAs.Size = new System.Drawing.Size(226, 24);
            this.MenuFile_SaveAs.Text = "Save as...";
            this.MenuFile_SaveAs.Click += new System.EventHandler(this.MenuFile_SaveAs_Click);
            // 
            // MenuImage
            // 
            this.MenuImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuImage_Undo,
            this.MenuImage_Duplicate,
            this.toolStripSeparator1,
            this.MenuImage_Histogram});
            this.MenuImage.Enabled = false;
            this.MenuImage.ForeColor = System.Drawing.Color.DarkGray;
            this.MenuImage.Name = "MenuImage";
            this.MenuImage.Size = new System.Drawing.Size(63, 24);
            this.MenuImage.Text = "Image";
            this.MenuImage.Click += new System.EventHandler(this.check);
            // 
            // MenuImage_Undo
            // 
            this.MenuImage_Undo.Name = "MenuImage_Undo";
            this.MenuImage_Undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.MenuImage_Undo.Size = new System.Drawing.Size(201, 24);
            this.MenuImage_Undo.Text = "Undo";
            this.MenuImage_Undo.Click += new System.EventHandler(this.MenuImage_Undo_Click);
            // 
            // MenuImage_Duplicate
            // 
            this.MenuImage_Duplicate.Name = "MenuImage_Duplicate";
            this.MenuImage_Duplicate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.MenuImage_Duplicate.Size = new System.Drawing.Size(201, 24);
            this.MenuImage_Duplicate.Text = "Duplicate";
            this.MenuImage_Duplicate.Click += new System.EventHandler(this.MenuImage_Duplicate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // MenuImage_Histogram
            // 
            this.MenuImage_Histogram.Name = "MenuImage_Histogram";
            this.MenuImage_Histogram.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.MenuImage_Histogram.Size = new System.Drawing.Size(201, 24);
            this.MenuImage_Histogram.Text = "Histogram";
            this.MenuImage_Histogram.Click += new System.EventHandler(this.MenuImage_Histogram_Click);
            // 
            // MenuOperations
            // 
            this.MenuOperations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOperations_Adjustment,
            this.MenuOperations_Equ_Neighborhood,
            this.MenuOper_Calculation,
            this.MenuOper_Filters});
            this.MenuOperations.Enabled = false;
            this.MenuOperations.ForeColor = System.Drawing.Color.DarkGray;
            this.MenuOperations.Name = "MenuOperations";
            this.MenuOperations.Size = new System.Drawing.Size(94, 24);
            this.MenuOperations.Text = "Operations";
            // 
            // MenuOperations_Adjustment
            // 
            this.MenuOperations_Adjustment.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOper_Adj_Gamma,
            this.MenuOper_Adj_Contrast,
            this.MenuOper_Adj_Brightness,
            this.toolStripSeparator2,
            this.MenuOper_Adj_Greyscale,
            this.MenuOper_Adj_Negative,
            this.MenuOper_Adj_Treshold,
            this.MenuOper_Adj_Posterize});
            this.MenuOperations_Adjustment.Name = "MenuOperations_Adjustment";
            this.MenuOperations_Adjustment.Size = new System.Drawing.Size(175, 24);
            this.MenuOperations_Adjustment.Text = "Adjustment";
            // 
            // MenuOper_Adj_Gamma
            // 
            this.MenuOper_Adj_Gamma.Name = "MenuOper_Adj_Gamma";
            this.MenuOper_Adj_Gamma.Size = new System.Drawing.Size(193, 24);
            this.MenuOper_Adj_Gamma.Text = "Gamma";
            this.MenuOper_Adj_Gamma.Click += new System.EventHandler(this.MenuOper_Adj_Gamma_Click);
            // 
            // MenuOper_Adj_Contrast
            // 
            this.MenuOper_Adj_Contrast.Name = "MenuOper_Adj_Contrast";
            this.MenuOper_Adj_Contrast.Size = new System.Drawing.Size(193, 24);
            this.MenuOper_Adj_Contrast.Text = "Contrast";
            this.MenuOper_Adj_Contrast.Click += new System.EventHandler(this.MenuOper_Adj_Contrast_Click);
            // 
            // MenuOper_Adj_Brightness
            // 
            this.MenuOper_Adj_Brightness.Name = "MenuOper_Adj_Brightness";
            this.MenuOper_Adj_Brightness.Size = new System.Drawing.Size(193, 24);
            this.MenuOper_Adj_Brightness.Text = "Brightness";
            this.MenuOper_Adj_Brightness.Click += new System.EventHandler(this.MenuOper_Adj_Brightness_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
            // 
            // MenuOper_Adj_Greyscale
            // 
            this.MenuOper_Adj_Greyscale.Name = "MenuOper_Adj_Greyscale";
            this.MenuOper_Adj_Greyscale.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.MenuOper_Adj_Greyscale.Size = new System.Drawing.Size(193, 24);
            this.MenuOper_Adj_Greyscale.Text = "Greyscale";
            this.MenuOper_Adj_Greyscale.Click += new System.EventHandler(this.MenuOper_Adj_Greyscale_Click);
            // 
            // MenuOper_Adj_Negative
            // 
            this.MenuOper_Adj_Negative.Name = "MenuOper_Adj_Negative";
            this.MenuOper_Adj_Negative.Size = new System.Drawing.Size(193, 24);
            this.MenuOper_Adj_Negative.Text = "Negative";
            this.MenuOper_Adj_Negative.Click += new System.EventHandler(this.MenuOper_Adj_Negative_Click);
            // 
            // MenuOper_Adj_Treshold
            // 
            this.MenuOper_Adj_Treshold.Name = "MenuOper_Adj_Treshold";
            this.MenuOper_Adj_Treshold.Size = new System.Drawing.Size(193, 24);
            this.MenuOper_Adj_Treshold.Text = "Treshold";
            this.MenuOper_Adj_Treshold.Click += new System.EventHandler(this.MenuOper_Adj_Treshold_Click);
            // 
            // MenuOper_Adj_Posterize
            // 
            this.MenuOper_Adj_Posterize.Name = "MenuOper_Adj_Posterize";
            this.MenuOper_Adj_Posterize.Size = new System.Drawing.Size(193, 24);
            this.MenuOper_Adj_Posterize.Text = "Posterization";
            this.MenuOper_Adj_Posterize.Click += new System.EventHandler(this.MenuOper_Adj_Posterize_Click);
            // 
            // MenuOperations_Equ_Neighborhood
            // 
            this.MenuOperations_Equ_Neighborhood.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOper_Equ_Average,
            this.MenuOper_Equ_Random,
            this.MenuOper_Equ_Neighbor});
            this.MenuOperations_Equ_Neighborhood.Name = "MenuOperations_Equ_Neighborhood";
            this.MenuOperations_Equ_Neighborhood.Size = new System.Drawing.Size(175, 24);
            this.MenuOperations_Equ_Neighborhood.Text = "Equalization";
            // 
            // MenuOper_Equ_Average
            // 
            this.MenuOper_Equ_Average.Name = "MenuOper_Equ_Average";
            this.MenuOper_Equ_Average.Size = new System.Drawing.Size(176, 24);
            this.MenuOper_Equ_Average.Text = "Average";
            this.MenuOper_Equ_Average.Click += new System.EventHandler(this.MenuOper_Equ_Average_Click);
            // 
            // MenuOper_Equ_Random
            // 
            this.MenuOper_Equ_Random.Name = "MenuOper_Equ_Random";
            this.MenuOper_Equ_Random.Size = new System.Drawing.Size(176, 24);
            this.MenuOper_Equ_Random.Text = "Random";
            this.MenuOper_Equ_Random.Click += new System.EventHandler(this.MenuOper_Equ_Random_Click);
            // 
            // MenuOper_Equ_Neighbor
            // 
            this.MenuOper_Equ_Neighbor.Name = "MenuOper_Equ_Neighbor";
            this.MenuOper_Equ_Neighbor.Size = new System.Drawing.Size(176, 24);
            this.MenuOper_Equ_Neighbor.Text = "Neighborhood";
            this.MenuOper_Equ_Neighbor.Click += new System.EventHandler(this.MenuOper_Equ_Neighbor_Click);
            // 
            // MenuOper_Calculation
            // 
            this.MenuOper_Calculation.Name = "MenuOper_Calculation";
            this.MenuOper_Calculation.Size = new System.Drawing.Size(175, 24);
            this.MenuOper_Calculation.Text = "Calculation";
            this.MenuOper_Calculation.Click += new System.EventHandler(this.MenuOper_Calculation_Click);
            // 
            // MenuOper_Filters
            // 
            this.MenuOper_Filters.Name = "MenuOper_Filters";
            this.MenuOper_Filters.Size = new System.Drawing.Size(175, 24);
            this.MenuOper_Filters.Text = "Filters";
            this.MenuOper_Filters.Click += new System.EventHandler(this.MenuOper_Filters_Click);
            // 
            // MenuAbout
            // 
            this.MenuAbout.ForeColor = System.Drawing.Color.DarkGray;
            this.MenuAbout.Name = "MenuAbout";
            this.MenuAbout.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.MenuAbout.Size = new System.Drawing.Size(62, 24);
            this.MenuAbout.Text = "About";
            this.MenuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(870, 534);
            this.Controls.Add(this.MenuBar);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ForeColor = System.Drawing.Color.Maroon;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MenuBar;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "APOBlabs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MdiChildActivate += new System.EventHandler(this.check);
            this.Click += new System.EventHandler(this.MenuFile_Save_Click);
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuFile_Save;
        private System.Windows.Forms.ToolStripMenuItem MenuFile_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem MenuImage;
        private System.Windows.Forms.ToolStripMenuItem MenuOperations;
        private System.Windows.Forms.ToolStripMenuItem MenuImage_Duplicate;
        private System.Windows.Forms.ToolStripMenuItem MenuImage_Undo;
        private System.Windows.Forms.ToolStripMenuItem MenuAbout;
        private System.Windows.Forms.ToolStripMenuItem MenuImage_Histogram;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuOperations_Equ_Neighborhood;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Equ_Average;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Equ_Random;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Equ_Neighbor;
        private System.Windows.Forms.ToolStripMenuItem MenuOperations_Adjustment;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Adj_Greyscale;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Adj_Negative;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Adj_Treshold;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Adj_Posterize;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Adj_Gamma;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Adj_Contrast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Adj_Brightness;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Calculation;
        private System.Windows.Forms.ToolStripMenuItem MenuOper_Filters;

    }
}

