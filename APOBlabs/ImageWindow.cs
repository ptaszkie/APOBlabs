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
    public partial class ImageWindow : Form
    {
        private string filepath = null; // filepath to image
        private uint CopyNumber = 1;   // number of copy
        private Bitmap PreviousImage = null; // image before last change
        // IMAGE info
        public int Deph { get; private set; }
        public UInt32[,] LUT;
        private bool greyscale = false;

        public ImageWindow(MainWindow parent, string fp) // take Image from filepath
        {
            InitializeComponent();
            MdiParent = parent;
            filepath = fp;
            Picture.Image = Image.FromFile(filepath);
            Text = System.IO.Path.GetFileName(filepath);
            LUT = new UInt32[4, 256];
            updateLUT();
            Width = Picture.Image.Width;
            Height = Picture.Image.Height+15;

            Show();
        }

        public ImageWindow(ImageWindow iw) // take Image from another ImageWindow
        {
            InitializeComponent();
            MdiParent = iw.MdiParent;
            Picture.Image = iw.getBitmap();

            Text = iw.Text + " (" + iw.CopyNumber++.ToString() + ")";
            LUT = (UInt32[,])iw.LUT.Clone();
            Width = iw.Width;
            Height = iw.Height;
            Show();
        }

        public ImageWindow(MainWindow parent, Bitmap bmp, string name) // new ImageWindow from bitmap
        {
            InitializeComponent();
            MdiParent = parent;
            Picture.Image = (Bitmap)bmp.Clone();
            Text = name;
            LUT = new UInt32[4, 256];
            updateLUT();
            Width = Picture.Image.Width;
            Height = Picture.Image.Height + 15;

            Show();
        }

        public void setFilepath(string fp) // set path to file
        {
            filepath = fp;
        }

        public string getFilepath() // get path to file
        {
            return filepath;
        }

        public Bitmap getBitmap() // get image bitmap
        {
            return new Bitmap(Picture.Image);
        }

        public void setBitmap(Bitmap bmp)
        {
            Picture.Image.Dispose();
            Picture.Image = new Bitmap(bmp);
            Height = bmp.Height+15;
            Width = bmp.Width;
            Picture.Invalidate();
            //updateLUT();
        }

        public void setPrevBitmap(Bitmap bmp)
        {
            if(PreviousImage!=null) PreviousImage.Dispose();
            PreviousImage = (Bitmap)bmp.Clone();
        }

        public uint getCopyNumber() // get which copy it is
        {
            return CopyNumber;
        }

        public void rememberImage() // save previous image
        {
            if(PreviousImage!=null) PreviousImage.Dispose();
            PreviousImage = new Bitmap(Picture.Image);
            ((MainWindow)MdiParent).check();
        }

        public bool checkUndo() // check whether exists previous version of image
        {
            if (PreviousImage == null) return false;
            return true;
        }

        public void undo() // cancel last operation
        {
            Picture.Image.Dispose();
            Picture.Image = (Bitmap)PreviousImage.Clone();
            PreviousImage.Dispose();
            PreviousImage = null;
            ((MainWindow)MdiParent).check();

            updateLUT();
        }

        public void updateLUT() // update LUT table
        {
            Parallel.For(0, 256, i =>
            {
                LUT[0, i] = 0;
                LUT[1, i] = 0;
                LUT[2, i] = 0;
                LUT[3, i] = 0; 
            });
            greyscale = true;
            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadOnly, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;

                Parallel.For(0, HeightInPixels, y =>
                {
                    int R, G, B, Grey;
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    for (int x = 0; x < WidthInBytes; x +=BytesPerPixel)
                    {
                        B = CurrentLine[x];
                        G = CurrentLine[x + 1];
                        R = CurrentLine[x + 2];
                        Grey = (int)((R+G+B+1)/3);

                        if (R == G && R == B && G == B)
                        {
                            LUT[0, Grey] += 1;
                            LUT[1, Grey] += 1;
                            LUT[2, Grey] += 1;
                            LUT[3, Grey] += 1;
                        }
                        else
                        {
                            LUT[0, R] += 1;
                            LUT[1, G] += 1;
                            LUT[2, B] += 1;
                            LUT[3, Grey] += 1;
                            greyscale = false;
                        }
                    }
                }); // end parallel
                pic.UnlockBits(bmpDat);
            }

        } 

        public bool checkGreyscale()
        {
            return greyscale;
        } // check whether image is greyscale

        private UInt32 getCDF() 
        {
            UInt32 PixelCounter = 0;
            for (UInt16 i = 0; i < 256; ++i)
            {
                PixelCounter += LUT[3, i];
            }

            return PixelCounter / 256;
        }

        public void Equalisation(int mode) // 1 -average, 2 - random, 3 - neighbours
        {
            rememberImage();
            if (!greyscale) ToGreyscale();

            UInt32 CDF;
            Bitmap pic = new Bitmap(Picture.Image);
            Int32[] tmpLUT = new Int32[256];
            int RI = 0;
            int Hint = 0;
            int[] L = new int[256];
            int[] R = new int[256];

            CDF = getCDF();

            for (ushort i = 0; i < 256; ++i)
            {
                L[i] = RI;
                Hint += (int)LUT[3, i];
                while (Hint > CDF)
                {
                    Hint -= (int)CDF;
                    RI++;
                }
                if (RI > 255)
                    RI = 255;
                R[i] = RI;
                
                if(mode==1)
                    tmpLUT[i] = (L[i] + R[i]) / 2;
                else if(mode==2)
                    tmpLUT[i] = R[i] + L[i];

            }

            unsafe
            {
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;

                Random rnd = new Random();

                Parallel.For(0, HeightInPixels, y =>
                {
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    int val;
                    for (int x = 0; x < WidthInBytes; x = x + BytesPerPixel)
                    {
                        if(L[CurrentLine[x]]==R[CurrentLine[x]])
                            CurrentLine[x + 2] = CurrentLine[x + 1] = CurrentLine[x] = (byte)L[CurrentLine[x]];
                        else
                        {
                            if (mode == 1)
                            {
                                CurrentLine[x + 2] = CurrentLine[x + 1] = CurrentLine[x] = (byte)tmpLUT[CurrentLine[x]];
                            }
                            else if(mode==2)
                            {
                                //todo: Needs better random generator!
                                val = rnd.Next(tmpLUT[CurrentLine[x]]) + L[CurrentLine[x]];
                                if (val > 255) val = 255;
                                CurrentLine[x + 2] = CurrentLine[x + 1] = CurrentLine[x] = (byte)val;
                            }
                            else
                            {
                                if (x >BytesPerPixel && y>0 && x<bmpDat.Stride-BytesPerPixel && y<HeightInPixels-1)
                                {
                                    byte pom = CurrentLine[x] = (byte)((CurrentLine[x+BytesPerPixel] + CurrentLine[x-BytesPerPixel] + CurrentLine[x+WidthInBytes] + CurrentLine[x-WidthInBytes])/4);
                                    if (pom > R[CurrentLine[x]])
                                        CurrentLine[x + 2] = CurrentLine[x + 1] =  CurrentLine[x] = (byte)R[CurrentLine[x]];
                                    else
                                        CurrentLine[x + 2] = CurrentLine[x + 1] = CurrentLine[x]  = pom;
                                }
                            }
                        }

                    }
                }); // end parallel
                pic.UnlockBits(bmpDat);
                Picture.Image.Dispose();
                Picture.Image = pic;
                Picture.Invalidate();
            }

            updateLUT();

        }

        public void ToGreyscale()
        {
            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;

                Parallel.For(0, HeightInPixels, y =>
                {
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    for (int x = 0; x < WidthInBytes; x = x + BytesPerPixel)
                    {
                        int R = CurrentLine[x + 2];
                        int G = CurrentLine[x + 1];
                        int B = CurrentLine[x];
                        
                        byte grey = (byte)(0.3F * (float)R + 0.59F * (float)G + 0.11F * (float)B);

                        CurrentLine[x] = grey;
                        CurrentLine[x + 1]= grey;
                        CurrentLine[x + 2] = grey;
                    }
                }); // end parallel
                pic.UnlockBits(bmpDat);
                Picture.Image.Dispose();
                Picture.Image = pic;
                Picture.Invalidate();
            }
            updateLUT();
        } // convert image to greyscale

        public void Negative()
        {
            rememberImage();

            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;

                Parallel.For(0, HeightInPixels, y =>
                {
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    for (int x = 0; x < WidthInBytes; x = x + BytesPerPixel)
                    {
                        CurrentLine[x] = (byte)(255-CurrentLine[x]);
                        CurrentLine[x + 1] = (byte)(255-CurrentLine[x + 1]);
                        CurrentLine[x + 2] = (byte)(255-CurrentLine[x + 2]);
                    }
                }); // end parallel
                pic.UnlockBits(bmpDat);
                Picture.Image.Dispose();
                Picture.Image = pic;
                Picture.Invalidate();
            }
            updateLUT();
        }

        public void Treshold(int val, bool rev)
        {
            if (!greyscale) ToGreyscale();

            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;

                Parallel.For(0, HeightInPixels, y =>
                {
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    for (int x = 0; x < WidthInBytes; x = x + BytesPerPixel)
                    {
                        int pix = (int) CurrentLine[x];
                        if (rev)
                        {
                            if (pix < val)
                                CurrentLine[x] = CurrentLine[x + 1] = CurrentLine[x + 2] = 0;
                            else
                                CurrentLine[x] = CurrentLine[x + 1] = CurrentLine[x + 2] = 255;
                        }
                        else
                        {
                            if (pix <= val)
                                CurrentLine[x] = CurrentLine[x + 1] = CurrentLine[x + 2] = 255;
                            else
                                CurrentLine[x] = CurrentLine[x + 1] = CurrentLine[x + 2] = 0;
                        }
                    }
                }); // end parallel
                pic.UnlockBits(bmpDat);
                Picture.Image.Dispose();
                Picture.Image = pic;
                Picture.Invalidate();
            }
        }

        public void Posterize(int val) // posterization
        {
            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;
                --val;
                Parallel.For(0, HeightInPixels, y =>
                {
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    for (int x = 0; x < WidthInBytes; x = x + BytesPerPixel)
                    {
                        float R, G, B;
                        B = (float)CurrentLine[x] / 255.0F;
                        G = (float)CurrentLine[x+1] / 255.0F;
                        R = (float)CurrentLine[x+2] / 255.0F;
                        CurrentLine[x] = (byte)(255 * Math.Round(B * val) / val);
                        CurrentLine[x + 1] = (byte)(255 * Math.Round(G * val) / val);
                        CurrentLine[x + 2] = (byte)(255 * Math.Round(R * val) / val);

                    }
                }); // end parallel
                pic.UnlockBits(bmpDat);
                Picture.Image.Dispose();
                Picture.Image = pic;
                Picture.Invalidate();
            }
        }

        public void Gamma(int val)
        {
            float gamma = (float)val / 100.0F;
            int[] tmpLUT = new int[256];
            for (int i = 0; i < 256; ++i)
            {
                int p = (int)(255 * Math.Pow(i / 255.0, 1 / gamma));
                if (p > 255)
                {
                    tmpLUT[i] = 255;
                }
                else
                {
                    tmpLUT[i] = p;
                }
            };

            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;

                Parallel.For(0, HeightInPixels, y =>
                {
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    for (int x = 0; x < WidthInBytes; x = x + BytesPerPixel)
                    {
                        CurrentLine[x] = (byte)tmpLUT[CurrentLine[x]];
                        CurrentLine[x + 1] = (byte)tmpLUT[CurrentLine[x + 1]];
                        CurrentLine[x + 2] = (byte)tmpLUT[CurrentLine[x + 2]];

                    }
                }); // end parallel
                pic.UnlockBits(bmpDat);
                Picture.Image.Dispose();
                Picture.Image = pic;
                Picture.Invalidate();
                
            }
        }

        public void Contrast(int val)
        {
            double contrastLevel = Math.Pow((100.0 + val) / 100.0, 2);

            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;

                Parallel.For(0, HeightInPixels, y =>
                {
                    double R, G, B;
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    for (int x = 0; x < WidthInBytes; x = x + BytesPerPixel)
                    {

                        B = ((((CurrentLine[x] / 255.0) - 0.5) * contrastLevel) + 0.5) * 255.0;

                        G = ((((CurrentLine[x + 1] / 255.0) - 0.5) * contrastLevel) + 0.5) * 255.0;

                        R = ((((CurrentLine[x + 2] / 255.0) - 0.5) * contrastLevel) + 0.5) * 255.0;


                        if (R > 255) R = 255;
                        else if (R < 0) R = 0;

                        if (G > 255) G = 255;
                        else if (G < 0) G = 0;

                        if (B > 255) B = 255;
                        else if (B < 0) B = 0;

                        CurrentLine[x] = (byte)B;
                        CurrentLine[x+1] = (byte)G;
                        CurrentLine[x+2] = (byte)R;
                    }
                }); // end parallel
                pic.UnlockBits(bmpDat);
                Picture.Image.Dispose();
                Picture.Image = pic;
                Picture.Invalidate();
                
            }
        }

        public void Brightness(int val)
        {
            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;

                Parallel.For(0, HeightInPixels, y =>
                {
                    int R, G, B;
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    for (int x = 0; x < WidthInBytes; x = x + BytesPerPixel)
                    {

                        B = CurrentLine[x] + val;
                        G = CurrentLine[x + 1] + val;
                        R = CurrentLine[x + 2] + val;


                        if (R > 255) R = 255;
                        else if (R < 0) R = 0;

                        if (G > 255) G = 255;
                        else if (G < 0) G = 0;

                        if (B > 255) B = 255;
                        else if (B < 0) B = 0;

                        CurrentLine[x] = (byte)B;
                        CurrentLine[x + 1] = (byte)G;
                        CurrentLine[x + 2] = (byte)R;
                    }
                }); // end parallel
                pic.UnlockBits(bmpDat);
                Picture.Image.Dispose();
                Picture.Image = pic;
                Picture.Invalidate();
            }
        }

        public void Filter(int[,] t, int d){
            rememberImage();

            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;

                for (int y = 1; y < HeightInPixels - 1; ++y)
                {
                    int R, G, B;
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    for (int x = BytesPerPixel; x < WidthInBytes - BytesPerPixel; x = x + BytesPerPixel)
                    {

                        B = t[0, 0] * CurrentLine[x - BytesPerPixel - bmpDat.Stride] + t[0, 1] * CurrentLine[x - bmpDat.Stride] + t[0, 2] * CurrentLine[x + BytesPerPixel - bmpDat.Stride] +
                            t[1, 0] * CurrentLine[x - BytesPerPixel] + t[1, 1] * CurrentLine[x] + t[1, 2] * CurrentLine[x + BytesPerPixel] +
                            t[2, 0] * CurrentLine[x - BytesPerPixel + bmpDat.Stride] + t[2, 1] * CurrentLine[x + bmpDat.Stride] + t[2, 2] * CurrentLine[x + BytesPerPixel + bmpDat.Stride];
                        G = t[0, 0] * CurrentLine[x + 1 - BytesPerPixel - bmpDat.Stride] + t[0, 1] * CurrentLine[x + 1 - bmpDat.Stride] + t[0, 2] * CurrentLine[x + 1 + BytesPerPixel - bmpDat.Stride] +
                            t[1, 0] * CurrentLine[x + 1 - BytesPerPixel] + t[1, 1] * CurrentLine[x + 1] + t[1, 2] * CurrentLine[x + 1 + BytesPerPixel] +
                            t[2, 0] * CurrentLine[x + 1 - BytesPerPixel + bmpDat.Stride] + t[2, 1] * CurrentLine[x + 1 + bmpDat.Stride] + t[2, 2] * CurrentLine[x + 1 + BytesPerPixel + bmpDat.Stride];
                        R = t[0, 0] * CurrentLine[x + 2 - BytesPerPixel - bmpDat.Stride] + t[0, 1] * CurrentLine[x + 2 - bmpDat.Stride] + t[0, 2] * CurrentLine[x + 2 + BytesPerPixel - bmpDat.Stride] +
                            t[1, 0] * CurrentLine[x + 2 - BytesPerPixel] + t[1, 1] * CurrentLine[x] + t[1, 2] * CurrentLine[x + BytesPerPixel] +
                            t[2, 0] * CurrentLine[x + 2 - BytesPerPixel + bmpDat.Stride] + t[2, 1] * CurrentLine[x + 2 + bmpDat.Stride] + t[2, 2] * CurrentLine[x + 2 + BytesPerPixel + bmpDat.Stride];

                        B /= d;
                        G /= d;
                        R /= d;
                        if (R > 255) R = 255;
                        else if (R < 0) R = 0;

                        if (G > 255) G = 255;
                        else if (G < 0) G = 0;

                        if (B > 255) B = 255;
                        else if (B < 0) B = 0;

                        CurrentLine[x] = (byte)B;
                        CurrentLine[x + 1] = (byte)G;
                        CurrentLine[x + 2] = (byte)R;
                    }
                }; // end parallel
                pic.UnlockBits(bmpDat);
                Picture.Image.Dispose();
                Picture.Image = pic;
                Picture.Invalidate();
            }

        }



    }
}