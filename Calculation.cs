using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace APOBlabs
{
    public partial class Calculation : Form
    {
        public Calculation(ImageWindow iw)
        {
            InitializeComponent();
            MdiParent = iw.MdiParent;

            List<ImageWindow> iwl = MdiParent.MdiChildren.OfType<ImageWindow>().ToList<ImageWindow>();

            Image1.DataSource = iwl;
            Image1.DisplayMember = "Text";
            Image2.BindingContext = new BindingContext();
            Image2.DataSource = iwl;
            Image2.DisplayMember = "Text";
            Operation.SelectedIndex = 0;

            Show();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Bitmap img1 = ((ImageWindow)(Image1.SelectedItem)).getBitmap(),
                   img2 = ((ImageWindow)(Image2.SelectedItem)).getBitmap();
            int nHeight = Math.Max(img1.Height, img2.Height),
                nWidth = Math.Max(img1.Width, img2.Width);
            
            string name = Operation.Text;

            Bitmap res = new Bitmap(nWidth, nHeight);
            
            for(int x=0;x<res.Width;x++){
                for(int y=0;y<res.Height;y++){
                    if (x < img1.Width && y < img1.Height)
                        res.SetPixel(x, y, img1.GetPixel(x, y));
                    else res.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                }
            }

            for (int x = 0; x < img2.Width; x++)
            {
                for (int y = 0; y < img2.Height; y++)
                {
                    Color p1 = img2.GetPixel(x, y);
                    Color p2 = res.GetPixel(x,y);

                    int R, G, B;
                    
                    
                    if (name.Equals("ADD"))
                    {
                        R = p1.R + p2.R;
                        G = p1.G + p2.G;
                        B = p1.B + p2.B;

                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        res.SetPixel(x, y, Color.FromArgb(R, G, B));

                    }
                    else if (name.Equals("SUB"))
                    {
                        R = p1.R - p2.R;
                        G = p1.G - p2.G;
                        B = p1.B - p2.B;

                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;

                        res.SetPixel(x, y, Color.FromArgb(R, G, B));

                    }
                    else if (name.Equals("AND"))
                    {
                        R = p1.R & p2.R;
                        G = p1.G & p2.G;
                        B = p1.B & p2.B;

                       res.SetPixel(x, y, Color.FromArgb(R, G, B));

                    }
                    else if (name.Equals("OR"))
                    {
                        R = p1.R | p2.R;
                        G = p1.G | p2.G;
                        B = p1.B | p2.B;

                        res.SetPixel(x, y, Color.FromArgb(R, G, B));

                    }
                    else if (name.Equals("XOR"))
                    {
                        R = p1.R ^ p2.R;
                        G = p1.G ^ p2.G;
                        B = p1.B ^ p2.B;

                        res.SetPixel(x, y, Color.FromArgb(R, G, B));

                    }
                }
            }

                new ImageWindow((MainWindow)MdiParent, res, name);
                Dispose();
                Close();
            
        }
    }
}
