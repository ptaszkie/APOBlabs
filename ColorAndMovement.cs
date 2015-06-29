using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;
using ImageMagick.Drawables;
namespace APOBlabs
{
    public partial class ColorAndMovement : Form
    {
        List<PictureBox> images = new List<PictureBox>();
        bool ChangeColorMode = false;
        int indexChoosed = -1;
        //float xScale, yScale;
        Color choosedColor = Color.FromArgb(255, 255, 255);

        public ColorAndMovement()
        {
            InitializeComponent();
            //splitContainer1.Panel2.HorizontalScroll.Enabled = true;
            //splitContainer1.Panel1.Controls.Add(Obraz);
            splitContainer1.Panel1MinSize = 300;
            splitContainer1.Panel1MinSize = 200;
            //Obraz.SizeMode = PictureBoxSizeMode.AutoSize;
            Show();
        }

        private void addImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileOpen = new OpenFileDialog();
            FileOpen.Title = "Open Image File";
            FileOpen.Multiselect = true;
            FileOpen.Filter = "All (*.*)|*.*|JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp|TIFF (*.tiff)|*.tiff";
            DialogResult Result = FileOpen.ShowDialog();
            if (Result == DialogResult.OK)
            {
                foreach (String img in FileOpen.FileNames)
                {
                    PictureBox p = new PictureBox();
                    p.Image = Image.FromFile(img);
                    if (images.Count > 0)
                    {
                        if (p.Image.Height != images[0].Image.Height
                         || p.Image.Width != images[0].Image.Width)
                        {
                            MessageBox.Show(img + " posiada inne wymiary niz pierwszy wczytany obraz!");
                            FileOpen.Dispose();
                            UpdateImages();
                            return;
                        }
                    }
                    images.Add(p);
                    p.Click += previevImageClick;
                    p.SizeMode = PictureBoxSizeMode.Zoom;
                }
                UpdateImages();
            }
            else FileOpen.Dispose();
        }

        void UpdateImages()
        {
            splitContainer1.Panel2.Controls.Clear();
            foreach (PictureBox pic in images)
            {
                splitContainer1.Panel2.Controls.Add(pic);
                pic.Height = splitContainer1.Panel2.Height - 20;
                pic.Width = pic.Height;
                pic.Location = new Point(images.IndexOf(pic) * (pic.Width + 5), 0);
            }
            if (images.Count == 0) return;

            //if ((images[0].Width + 5) * images.Count > splitContainer1.Panel2.Width - 5)
            //    splitContainer1.Panel2.HorizontalScroll.Visible = true;
            //else splitContainer1.Panel2.HorizontalScroll.Visible = false;

            Obraz.Image = images[0].Image;
            indexChoosed = 0;
        }

        void previevImageClick(object sender, EventArgs e)
        {
            Obraz.Image = ((PictureBox)sender).Image;
            indexChoosed = images.IndexOf((PictureBox)sender);

        }

        private void Colormode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChangeColorMode == false) ChangeColorMode = true;
            else ChangeColorMode = false;
        }

        private void splitContainer1_Panel2_Resize(object sender, EventArgs e)
        {

            UpdateImages();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.Filter = "GIF|*.gif";
            if (svd.ShowDialog() == DialogResult.OK)
            {
                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    foreach (PictureBox pim in images)
                    {
                        collection.Add(new MagickImage((Bitmap)pim.Image.Clone()));
                        collection[collection.Count - 1].AnimationDelay = 100;
                    }

                    // Optionally reduce colors
                    QuantizeSettings settings = new QuantizeSettings();
                    settings.Colors = 256;
                    collection.Quantize(settings);

                    // Optionally optimize the images (images should have the same size).
                    collection.Optimize();

                    // Save gif
                    collection.Write(svd.FileName);
                }
            }
            else
            {
                svd.Dispose();
            }

        }

        private void Obraz_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ChangeColorMode) return;

            unsafe
            {
                Bitmap pic = new Bitmap(Obraz.Image);
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;

                //************************************
                //kursor.Text = "k: " + ((int)(e.X*xScale)).ToString() + "," + ((int)(e.Y*yScale)).ToString();
                byte* CurrentLine = PtrFirstPixel + (e.Y * bmpDat.Stride) + BytesPerPixel * e.X;

                byte PB = CurrentLine[0],
                     PG = CurrentLine[1],
                     PR = CurrentLine[2];

                for (int y = 0; y < HeightInPixels; ++y)
                {
                    CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    for (int x = 0; x < WidthInBytes; x = x + BytesPerPixel)
                    {
                        if (CurrentLine[x] == PB && CurrentLine[x + 1] == PG && CurrentLine[x + 2] == PR)
                        {
                            CurrentLine[x] = choosedColor.B;
                            CurrentLine[x + 1] = choosedColor.G;
                            CurrentLine[x + 2] = choosedColor.R;
                        }

                    }
                };
                pic.UnlockBits(bmpDat);
                Obraz.Image.Dispose();
                images[indexChoosed].Image = pic;
                Obraz.Image = pic;
                images[indexChoosed].Invalidate();
                Obraz.Invalidate();
            }
        }

        private void coloringOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ChangeColorMode == false)
            {
                ((ToolStripMenuItem)sender).Text = "Coloring on";
                ChangeColorMode = true;
            }
            else
            {
                ((ToolStripMenuItem)sender).Text = "Coloring off";
                ChangeColorMode = false;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            DialogResult res = cd.ShowDialog();

            if (res == DialogResult.OK)
            {
                ((ToolStripMenuItem)sender).BackColor = cd.Color;
                choosedColor = cd.Color;

            }
            cd.Dispose();
        }

    }
}
