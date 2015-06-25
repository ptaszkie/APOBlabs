using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APOBlabs
{
    public partial class MainWindow : Form
    {
        private ImageWindow aiw = null; // actually active MDIchild keeps image

        public MainWindow()
        {
            InitializeComponent();
            check();
        }

        // open new file with image
        private void MenuFile_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileOpen = new OpenFileDialog();
            FileOpen.Title = "Open Image File";
            FileOpen.Filter = "JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp|TIFF (*.tiff)|*.tiff|All (*.*)|*.*";
            DialogResult Result = FileOpen.ShowDialog();
            if (Result == DialogResult.OK)
            {
                ImageWindow img = new ImageWindow(this, FileOpen.FileName);
            }
            else FileOpen.Dispose();
        }

        // check whether active MDIChild is a ImageWindow and sets up availability menu fields
        public void check(object sender=null, EventArgs e=null)
        {
            if (ActiveMdiChild is ImageWindow)
            {
                aiw = (ImageWindow)ActiveMdiChild;
                
                // Menu image
                MenuImage.Enabled = true;

                if (aiw.checkUndo()) MenuImage_Undo.Enabled = true; // if exists previous version of image
                else MenuImage_Undo.Enabled = false;

                MenuImage_Duplicate.Enabled = true;
                MenuImage_Histogram.Enabled = true;
                
                // Menu file
                MenuFile_SaveAs.Enabled = true;
                if (aiw.getFilepath() != null) MenuFile_Save.Enabled = true; // if image was opened, not duplicated
                else MenuFile_Save.Enabled = false;

                // Menu operations
                MenuOperations.Enabled = true;

                MenuOper_Calculation.Enabled = true;
                MenuOper_Filters.Enabled = true;
                
                // adjustment submenu
                MenuOper_Adj_Gamma.Enabled = true;
                MenuOper_Adj_Contrast.Enabled = true;
                MenuOper_Adj_Brightness.Enabled = true;
                MenuOper_Adj_Greyscale.Enabled = true;
                MenuOper_Adj_Negative.Enabled = true;
                MenuOper_Adj_Treshold.Enabled = true;
                MenuOper_Adj_Posterize.Enabled = true;

                // equalization submenu
                MenuOper_Equ_Average.Enabled = true;
                MenuOper_Equ_Random.Enabled = true;
                MenuOper_Equ_Neighbor.Enabled = true;
                

                

                return;
            }
            aiw = null;

            // Menu image
            MenuImage.Enabled = false;

            MenuImage_Undo.Enabled = false;

            MenuImage_Duplicate.Enabled = false;
            MenuImage_Histogram.Enabled = false;

            // Menu file
            MenuFile_SaveAs.Enabled = false;
            MenuFile_Save.Enabled = false;

            // Menu operations
            MenuOperations.Enabled = false;

            MenuOper_Calculation.Enabled = false;
            MenuOper_Filters.Enabled = false;

            // adjustment submenu
            MenuOper_Adj_Gamma.Enabled = false;
            MenuOper_Adj_Contrast.Enabled = false;
            MenuOper_Adj_Brightness.Enabled = false;
            MenuOper_Adj_Greyscale.Enabled = false;
            MenuOper_Adj_Negative.Enabled = false;
            MenuOper_Adj_Treshold.Enabled = false;
            MenuOper_Adj_Posterize.Enabled = false;

            // equalization submenu
            MenuOper_Equ_Average.Enabled = false;
            MenuOper_Equ_Random.Enabled = false;
            MenuOper_Equ_Neighbor.Enabled = false;
        }

        private void MenuFile_Save_Click(object sender, EventArgs e) // save open file - available for open, not duplicated images
        {
            aiw.getBitmap().Save(aiw.getFilepath());
        }

        private void MenuImage_Duplicate_Click(object sender, EventArgs e)
        {
            new ImageWindow(aiw);
        }

        private void MenuImage_Undo_Click(object sender, EventArgs e) // undo last operation
        {
            aiw.undo();
        }

        private void MenuFile_SaveAs_Click(object sender, EventArgs e) // save image as...
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "JPEG|*.jpg; *.jpeg |Bitmap|*.bmp |TIFF|*.tiff |All files|*.* ";

            if (sf.ShowDialog() == DialogResult.OK)
            {
                aiw.getBitmap().Save(sf.FileName);
                aiw.setFilepath(sf.FileName);
            }
            else sf.Dispose();
        }

        private void MenuAbout_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void MenuImage_Histogram_Click(object sender, EventArgs e)
        {
            new Histogram(aiw);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is MdiClient)
                {
                    ((MdiClient)ctl).BackColor = this.BackColor;
                }
            }
        }

        private void MenuImage_Greyscale_Click(object sender, EventArgs e)
        {
            aiw.ToGreyscale();
        }

        private void MenuOper_Equ_Average_Click(object sender, EventArgs e)
        {
            aiw.Equalisation(1);
        }

        private void MenuOper_Equ_Random_Click(object sender, EventArgs e)
        {
            aiw.Equalisation(2);
        }

        private void MenuOper_Equ_Neighbor_Click(object sender, EventArgs e)
        {
            aiw.Equalisation(3);
        }

        private void MenuOper_Adj_Greyscale_Click(object sender, EventArgs e)
        {
            aiw.ToGreyscale();
        }

        private void MenuOper_Adj_Negative_Click(object sender, EventArgs e)
        {
            aiw.Negative();
        }

        private void MenuOper_Adj_Treshold_Click(object sender, EventArgs e)
        {
            new Treshold(aiw);
        }

        private void MenuOper_Adj_Posterize_Click(object sender, EventArgs e)
        {
            new Posterization(aiw);
        }

        private void MenuOper_Adj_Gamma_Click(object sender, EventArgs e)
        {
            new Gamma(aiw);
        }

        private void MenuOper_Adj_Contrast_Click(object sender, EventArgs e)
        {
            new Contrast(aiw);
        }

        private void MenuOper_Adj_Brightness_Click(object sender, EventArgs e)
        {
            new Brightness(aiw);
        }

        private void MenuOper_Calculation_Click(object sender, EventArgs e)
        {
            new Calculation(aiw);
        }

        private void MenuOper_Filters_Click(object sender, EventArgs e)
        {
            new Filters(aiw);
        }


    }
}
