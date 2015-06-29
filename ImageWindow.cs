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

        public void updateLUT() // aktualizacja tablicy LUT
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

        public bool checkGreyscale() // spr. czy obraz jest szaroodcieniowy
        {
            return greyscale;
        }

        private UInt32 getCDF() // srednia wartosc pikseli
        {
            UInt32 PixelCounter = 0;
            for (UInt16 i = 0; i < 256; ++i)
            {
                PixelCounter += LUT[3, i];
            }

            return PixelCounter / 256;
        }

        public void Equalisation(int mode) // wyrownywanie histogramu: 1 - srednia, 2 - losowa, 3 - sasiedztwa
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

        public void ToGreyscale() // konwersja do szaroodcieniowego
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
        } 

        public void Negative() // negatyw
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

        public void Treshold(int val, bool rev) // progowanie
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

        public void Gamma(int val) // regulacja gammy
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

        public void Contrast(int val) // regulacja kontrastu
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

        public void Brightness(int val) // regulacja jasnosci
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

        public Bitmap Filter(int[,] t, int d, bool ret = false) // filtry
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
                if (ret)
                {
                    return pic;
                } 

                Picture.Image.Dispose();
                Picture.Image = pic;
                Picture.Invalidate();

                return null;
            }

        }

        public void Median(int sizeX, int sizeY) // mediana //TODO: poprawa wydajnosci
        {
            Bitmap pic = new Bitmap(Picture.Image);
            int h = pic.Height,
                w = pic.Width;


            for (int x = sizeX / 2; x < w - sizeX / 2; x++)
            {
                for (int y = sizeY / 2; y < h - sizeY / 2; y++)
                {
                    
                    List<byte> R = new List<byte>();
                    List<byte> G = new List<byte>();
                    List<byte> B = new List<byte>();
                    
                    for(int i=-(sizeX/2);i<=sizeX/2;i++){
                        for(int j=-(sizeY/2);j<=sizeY/2;j++){

                            Color p = pic.GetPixel(x+i,y+j);
                            R.Add(p.R);
                            G.Add(p.G);
                            B.Add(p.B);
                            //MessageBox.Show("dla: " + x.ToString() + "," + y.ToString() + " => " + (x + i).ToString() + "," + (y + j).ToString());
                        }
                    }

                    R.Sort();
                    G.Sort();
                    B.Sort();

                    pic.SetPixel(x,y,Color.FromArgb(R[R.Count/2],G[G.Count/2],B[B.Count/2]));
                }

            }
            Picture.Image.Dispose();
            Picture.Image = pic;
            Picture.Invalidate();
        }

        public void Dilation(bool enhanted = true) // dylatacja
        {
            if (enhanted) rememberImage();

            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                Bitmap res = (Bitmap)pic.Clone();
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                BitmapData resDat = res.LockBits(new Rectangle(0, 0, res.Width, res.Height), ImageLockMode.ReadWrite, res.PixelFormat);
                
                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;
                byte* res_PtrFirstPixel = (byte*)resDat.Scan0;

                Parallel.For(1, HeightInPixels-1, y=>
                {
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    byte* resCLine = res_PtrFirstPixel + (y * resDat.Stride);
                    for (int x = BytesPerPixel; x < WidthInBytes-BytesPerPixel; x = x + BytesPerPixel)
                    {
                        List<byte> B = new List<byte>();
                        B.Add(CurrentLine[x - BytesPerPixel - bmpDat.Stride]);
                        B.Add(CurrentLine[x - bmpDat.Stride]);
                        B.Add(CurrentLine[x + BytesPerPixel - bmpDat.Stride]);

                        B.Add(CurrentLine[x - BytesPerPixel]);
                        B.Add(CurrentLine[x]);
                        B.Add(CurrentLine[x + BytesPerPixel]);

                        B.Add(CurrentLine[x - BytesPerPixel + bmpDat.Stride]);
                        B.Add(CurrentLine[x + bmpDat.Stride]);
                        B.Add(CurrentLine[x + BytesPerPixel + bmpDat.Stride]);

                        List<byte> G = new List<byte>();
                        G.Add(CurrentLine[x + 1 - BytesPerPixel - bmpDat.Stride]);
                        G.Add(CurrentLine[x + 1 - bmpDat.Stride]);
                        G.Add(CurrentLine[x + 1 + BytesPerPixel - bmpDat.Stride]);

                        G.Add(CurrentLine[x + 1 - BytesPerPixel]);
                        G.Add(CurrentLine[x + 1]);
                        G.Add(CurrentLine[x + 1 + BytesPerPixel]);

                        G.Add(CurrentLine[x + 1 - BytesPerPixel + bmpDat.Stride]);
                        G.Add(CurrentLine[x + 1 + bmpDat.Stride]);
                        G.Add(CurrentLine[x + 1 + BytesPerPixel + bmpDat.Stride]);

                        List<byte> R = new List<byte>();
                        R.Add(CurrentLine[x + 2 - BytesPerPixel - bmpDat.Stride]);
                        R.Add(CurrentLine[x + 2 - bmpDat.Stride]);
                        R.Add(CurrentLine[x + 2 + BytesPerPixel - bmpDat.Stride]);

                        R.Add(CurrentLine[x + 2 - BytesPerPixel]);
                        R.Add(CurrentLine[x + 2]);
                        R.Add(CurrentLine[x + 2 + BytesPerPixel]);

                        R.Add(CurrentLine[x + 2 - BytesPerPixel + bmpDat.Stride]);
                        R.Add(CurrentLine[x + 2 + bmpDat.Stride]);
                        R.Add(CurrentLine[x + 2 + BytesPerPixel + bmpDat.Stride]);

                        resCLine[x] = B.Max();
                        resCLine[x + 1] = G.Max();
                        resCLine[x + 2] = R.Max();
                    }
                });
                res.UnlockBits(resDat);
                pic.UnlockBits(bmpDat);
                
                Picture.Image.Dispose();
                Picture.Image = res;
                Picture.Invalidate();
            }
            if (enhanted)
            {
                
                updateLUT();
            }
        }

        public void Erosion(bool enhanted=true) // dylatacja
        {
            if (enhanted) rememberImage();

            unsafe
            {
                Bitmap pic = new Bitmap(Picture.Image);
                Bitmap res = (Bitmap)pic.Clone();
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                BitmapData resDat = res.LockBits(new Rectangle(0, 0, res.Width, res.Height), ImageLockMode.ReadWrite, res.PixelFormat);

                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;
                byte* res_PtrFirstPixel = (byte*)resDat.Scan0;

                 Parallel.For(1, HeightInPixels-1, y=>
                {
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    byte* resCLine = res_PtrFirstPixel + (y * resDat.Stride);
                    for (int x = BytesPerPixel; x < WidthInBytes - BytesPerPixel; x = x + BytesPerPixel)
                    {
                        List<byte> B = new List<byte>();
                        B.Add(CurrentLine[x - BytesPerPixel - bmpDat.Stride]);
                        B.Add(CurrentLine[x - bmpDat.Stride]);
                        B.Add(CurrentLine[x + BytesPerPixel - bmpDat.Stride]);

                        B.Add(CurrentLine[x - BytesPerPixel]);
                        B.Add(CurrentLine[x]);
                        B.Add(CurrentLine[x + BytesPerPixel]);

                        B.Add(CurrentLine[x - BytesPerPixel + bmpDat.Stride]);
                        B.Add(CurrentLine[x + bmpDat.Stride]);
                        B.Add(CurrentLine[x + BytesPerPixel + bmpDat.Stride]);

                        List<byte> G = new List<byte>();
                        G.Add(CurrentLine[x + 1 - BytesPerPixel - bmpDat.Stride]);
                        G.Add(CurrentLine[x + 1 - bmpDat.Stride]);
                        G.Add(CurrentLine[x + 1 + BytesPerPixel - bmpDat.Stride]);

                        G.Add(CurrentLine[x + 1 - BytesPerPixel]);
                        G.Add(CurrentLine[x + 1]);
                        G.Add(CurrentLine[x + 1 + BytesPerPixel]);

                        G.Add(CurrentLine[x + 1 - BytesPerPixel + bmpDat.Stride]);
                        G.Add(CurrentLine[x + 1 + bmpDat.Stride]);
                        G.Add(CurrentLine[x + 1 + BytesPerPixel + bmpDat.Stride]);

                        List<byte> R = new List<byte>();
                        R.Add(CurrentLine[x + 2 - BytesPerPixel - bmpDat.Stride]);
                        R.Add(CurrentLine[x + 2 - bmpDat.Stride]);
                        R.Add(CurrentLine[x + 2 + BytesPerPixel - bmpDat.Stride]);

                        R.Add(CurrentLine[x + 2 - BytesPerPixel]);
                        R.Add(CurrentLine[x + 2]);
                        R.Add(CurrentLine[x + 2 + BytesPerPixel]);

                        R.Add(CurrentLine[x + 2 - BytesPerPixel + bmpDat.Stride]);
                        R.Add(CurrentLine[x + 2 + bmpDat.Stride]);
                        R.Add(CurrentLine[x + 2 + BytesPerPixel + bmpDat.Stride]);

                        resCLine[x] = B.Min();
                        resCLine[x + 1] = G.Min();
                        resCLine[x + 2] = R.Min();
                    }
                });
                res.UnlockBits(resDat);
                pic.UnlockBits(bmpDat);

                Picture.Image.Dispose();
                Picture.Image = res;
                Picture.Invalidate();

            }
            if(enhanted) {
                updateLUT();
            }
        }

        public void ImgClosing()
        {
            rememberImage();

            Dilation(false);
            Erosion(false);
            Picture.Invalidate();
            updateLUT();
        }

        public void ImgOpening()
        {
            rememberImage();

            Erosion(false);
            Dilation(false);
            Picture.Invalidate();
            updateLUT();
        }

    #region thining --- pomoc do ImgThining
        public static bool[][] Image2Bool(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            bool[][] s = new bool[bmp.Height][];
            for (int y = 0; y < bmp.Height; y++)
            {
                s[y] = new bool[bmp.Width];
                for (int x = 0; x < bmp.Width; x++)
                    s[y][x] = bmp.GetPixel(x, y).GetBrightness() < 0.4;
            }
            return s;

        }
        public static Image Bool2Image(bool[][] s)
        {
            Bitmap bmp = new Bitmap(s[0].Length, s.Length);
            using (Graphics g = Graphics.FromImage(bmp)) g.Clear(Color.White);
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    if (s[y][x]) bmp.SetPixel(x, y, Color.Black);

            return (Bitmap)bmp;
        }
        public static bool[][] ZhangSuenThinning(bool[][] s)
        {
            bool[][] temp = s;
            bool even = true;

            for (int a = 1; a < s.Length - 1; a++)
            {
                for (int b = 1; b < s[0].Length - 1; b++)
                {
                    if (SuenThinningAlg(a, b, temp, even))
                    {
                        temp[a][b] = false;
                    }
                    even = !even;
                }
            }

            return temp;
        }
        static bool SuenThinningAlg(int x, int y, bool[][] s, bool even)
        {
            bool p2 = s[x][y - 1];
            bool p3 = s[x + 1][y - 1];
            bool p4 = s[x + 1][y];
            bool p5 = s[x + 1][y + 1];
            bool p6 = s[x][y + 1];
            bool p7 = s[x - 1][y + 1];
            bool p8 = s[x - 1][y];
            bool p9 = s[x - 1][y - 1];


            int bp1 = NumberOfNonZeroNeighbors(x, y, s);
            if (bp1 >= 2 && bp1 <= 6)//2nd condition
            {
                if (NumberOfZeroToOneTransitionFromP9(x, y, s) == 1)
                {
                    if (even)
                    {
                        if (!((p2 && p4) && p8))
                        {
                            if (!((p2 && p6) && p8))
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (!((p2 && p4) && p6))
                        {
                            if (!((p4 && p6) && p8))
                            {
                                return true;
                            }
                        }
                    }
                }
            }


            return false;
        }
        static int NumberOfZeroToOneTransitionFromP9(int x, int y, bool[][] s)
        {
            bool p2 = s[x][y - 1];
            bool p3 = s[x + 1][y - 1];
            bool p4 = s[x + 1][y];
            bool p5 = s[x + 1][y + 1];
            bool p6 = s[x][y + 1];
            bool p7 = s[x - 1][y + 1];
            bool p8 = s[x - 1][y];
            bool p9 = s[x - 1][y - 1];

            int A = Convert.ToInt32((p2 == false && p3 == true)) + Convert.ToInt32((p3 == false && p4 == true)) +
                     Convert.ToInt32((p4 == false && p5 == true)) + Convert.ToInt32((p5 == false && p6 == true)) +
                     Convert.ToInt32((p6 == false && p7 == true)) + Convert.ToInt32((p7 == false && p8 == true)) +
                     Convert.ToInt32((p8 == false && p9 == true)) + Convert.ToInt32((p9 == false && p2 == true));
            return A;
        }
        static int NumberOfNonZeroNeighbors(int x, int y, bool[][] s)
        {
            int count = 0;
            if (s[x - 1][y])
                count++;
            if (s[x - 1][y + 1])
                count++;
            if (s[x - 1][y - 1])
                count++;
            if (s[x][y + 1])
                count++;
            if (s[x][y - 1])
                count++;
            if (s[x + 1][y])
                count++;
            if (s[x + 1][y + 1])
                count++;
            if (s[x + 1][y - 1])
                count++;
            return count;
        }
    #endregion 
        public void ImgThining()
        {
            bool[][] t = Image2Bool(Picture.Image);
            t = ZhangSuenThinning(t);
            Picture.Image = Bool2Image(t);
        }

        public void Gradient()
        {
            rememberImage();

            int[,] t = new int[3, 3]{{-1,-2,-1},{0,0,0},{1,2,1}};

            Bitmap res = Filter(t, 1, true);
            t = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            Bitmap pic = Filter(t, 1, true);

           unsafe
            {
                BitmapData bmpDat = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadWrite, pic.PixelFormat);
                BitmapData resDat = res.LockBits(new Rectangle(0, 0, res.Width, res.Height), ImageLockMode.ReadWrite, res.PixelFormat);

                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(pic.PixelFormat) / 8;
                int HeightInPixels = bmpDat.Height;
                int WidthInBytes = bmpDat.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bmpDat.Scan0;
                byte* res_PtrFirstPixel = (byte*)resDat.Scan0;

                 //Parallel.For(1, HeightInPixels-1, y=>
               for(int y=0;y<HeightInPixels;++y) 
               {
                    byte* CurrentLine = PtrFirstPixel + (y * bmpDat.Stride);
                    byte* resCLine = res_PtrFirstPixel + (y * resDat.Stride);
                    for (int x = BytesPerPixel; x < WidthInBytes - BytesPerPixel; x = x + BytesPerPixel)
                    {

                        resCLine[x] = (byte)Math.Sqrt(Math.Pow(resCLine[x], 2) + Math.Pow(CurrentLine[x], 2));
                        resCLine[x + 1] = (byte)Math.Sqrt(Math.Pow(resCLine[x+1], 2) + Math.Pow(CurrentLine[x+1], 2));
                        resCLine[x + 2] = (byte)Math.Sqrt(Math.Pow(resCLine[x + 2], 2) + Math.Pow(CurrentLine[x + 2], 2));
                    }
                };
                res.UnlockBits(resDat);
                pic.UnlockBits(bmpDat);

                Picture.Image.Dispose();
                Picture.Image = res;
                Picture.Invalidate();

            }

            }
        }
    }
