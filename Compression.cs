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
using System.IO;


namespace APOBlabs
{
    public partial class Compression : Form
    {
        Bitmap picture;
        int before_size;
        int z, x, counter, after, i, j, power, how_many;
        float Compression_degree;
        int[] hist = new int[256];
        public int[,] tab;

        Bitmap Compressed;
        ImageCodecInfo CodecInfo;
        System.Drawing.Imaging.Encoder encoder;
        EncoderParameter encoderParameter;
        EncoderParameters encoderParameters;

        public Compression(ImageWindow iw)
        {
            InitializeComponent();
            picture = iw.getBitmap();
            before_size = picture.Width * picture.Height;
            bSize.Text = before_size.ToString();
            MdiParent = iw.MdiParent;
            Compressor.SelectedIndex = 0;
            z = 0;
            x = 2;
            counter = 0;
            after = 0;
            power = 2;
            how_many = 0;
            budujTablice();

            Show();
        }
        #region NeededPixeltable
        public int[,] budujTablice()
        {
            tab = new int[picture.Width, picture.Height];

            for (int y = 0; y < picture.Height; y++)
                for (int x = 0; x < picture.Width; x++)
                {

                    tab[x, y] = Convert.ToInt32(picture.GetPixel(x, y).GetBrightness() * 255);
                }
            return tab;
        }
        #endregion
        #region Huffman
        public void Huffman()
        {
            for (i = 0; i < picture.Height; i++)
                for (j = 0; j < picture.Width; j++)
                {
                    z = (int)((0.3 * (float)picture.GetPixel(j, i).R) +
                                (0.59 * (float)picture.GetPixel(j, i).G) +
                                (0.11 * (float)picture.GetPixel(j, i).B));

                    hist[z] = hist[z] + 1;
                }


            for (i = 0; i < 256; i++)
            {
                if (hist[i] != 0)
                {
                    counter++;
                    after += (counter * hist[i]);

                    if (counter >= Math.Pow((double)x, (double)power))
                    {
                        counter = 0;
                        power++;
                    }
                    how_many++;
                }

                if (counter == 1) after = before_size + 1;
            }
            after /= 8;
            after += (how_many * 2);
            Compression_degree = (float)(float)before_size / (float)after;
        }
        #endregion
        #region Difference
        public void Difference()
        {
            const int PIXELLEN = 1;
            const int WORDLEN = 1;

            const int block_size = 4;

            int width = picture.Width;
            int height = picture.Height;
            int fld = width * height;

            before_size = fld * PIXELLEN;

            int i, j, I, J, K, ml, mu;
            float m;
            int[,] b = new int[block_size, block_size];

            for (i = 0; i < width - block_size; i = i + block_size)
                for (j = 0; j < height - block_size; j = j + block_size)
                {
                    m = 0;
                    mu = 0;
                    ml = 0; K = 0;

                    for (I = 0; I < block_size; I++)
                        for (J = 0; J < block_size; J++)
                            m = m + tab[i + I, j + J];

                    m /= block_size;

                    for (I = 0; I < block_size; I++)
                        for (J = 0; J < block_size; J++)
                        {
                            if (tab[i + I, j + J] > m)
                            { mu = mu + tab[i + I, j + J]; b[I, J] = 1; K++; }
                            else
                            { ml = ml + tab[i + I, j + J]; b[I, J] = 0; }
                        }

                    if (K == 0) K = 1;
                    mu = mu / K;
                    ml = ml / (block_size * block_size - K);

                    after += K * PIXELLEN + WORDLEN;

                }
            Compression_degree = (float)(float)before_size / (float)after;
        }
        #endregion
        #region Block
        public void Block()
        {
            const int PIXELLEN = 1; //4 bajty - 32 bity
            const int WORDLEN = 1; //4 bajty - 32 bity

            const int block_size = 4;

            int width = picture.Width;
            int height = picture.Height;
            int fld = width * height;

            before_size = fld * PIXELLEN; //4 bajty - 32 bity

            int i, j, I, J, K, ml, mu;
            float m;
            int[,] b = new int[block_size, block_size];

            for (i = 0; i < width - 4; i = i + block_size)
                for (j = 0; j < height - 4; j = j + block_size)
                {
                    m = 0;
                    mu = 0;
                    ml = 0; K = 0;

                    for (I = 0; I < block_size; I++)
                        for (J = 0; J < block_size; J++)
                            m = m + tab[i + I, j + J];

                    m /= block_size;

                    for (I = 0; I < block_size; I++)
                        for (J = 0; J < block_size; J++)
                        {
                            if (tab[i + I, j + J] > m)
                            { mu = mu + tab[i + I, j + J]; b[I, J] = 1; K++; }
                            else
                            { ml = ml + tab[i + I, j + J]; b[I, J] = 0; }
                        }

                    if (K == 0) K = 1;
                    mu = mu / K;
                    ml = ml / (block_size * block_size - K);

                    after += K * PIXELLEN + WORDLEN;

                }
            Compression_degree = (float)(float)before_size / (float)after;
            WriteNewDescriptionInImage(picture, "Block.jpg");
        }
        #endregion
        #region Cosinus
        public void Cosinus()
        {

            int szerokosc = picture.Width;
            int dlugosc = picture.Height;
            int[,] tablica = null;
            int szerokosc1 = szerokosc - (szerokosc % 8);
            int dlugosc1 = dlugosc - (dlugosc % 8);
            int pom1 = 0, pom2 = 0;
            int temp1 = 0, temp2 = 0, fk1 = 0;
            double ulamek1 = 0, ulamek2 = 0;
            int temp11, temp22, pom11, pom22;
            double Cm = 0, Cn = 0;
            int x1 = 8, x2 = 8;
            int krok = 8;
            double liczba = 0;
            int i, j;
            pom1 = x1;
            pom2 = x2;
            tablica = new int[szerokosc1, dlugosc1];
            System.Windows.Forms.SaveFileDialog sF = new System.Windows.Forms.SaveFileDialog();
            StreamWriter sw = null;
            sF.Filter = "txt Files | *.txt; *.txt|All files| *.*";
            if (sF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sw = new StreamWriter(sF.FileName);
            }
            else return;
            while (temp2 != dlugosc1)
            {
                while (temp1 != szerokosc1)
                {
                    temp11 = temp1;
                    temp22 = temp2;
                    pom11 = pom1;
                    pom22 = pom2;
                    for (i = temp1; i < pom1; i++)
                    {
                        for (j = temp2; j < pom2; j++)
                        {
                            if (i == 0) Cm = 1.41;
                            else Cm = 1;
                            if (j == 0) Cn = 1.41;
                            else Cn = 1;
                            for (int a = temp11; a < pom11; a++)
                            {
                                for (int b = temp22; b < pom22; b++)
                                {
                                    fk1 = Convert.ToInt16(picture.GetPixel(a, b).B);
                                    ulamek1 = (double)Math.Cos(((Math.PI) * i * (2 * (a % 8) + 1)) / 16);
                                    ulamek2 = (double)Math.Cos(((Math.PI) * j * (2 * (a % 8) + 1)) / 16);
                                    liczba = (liczba + fk1 * ulamek1 * ulamek2);
                                }
                            }
                            tablica[i, j] = (int)(((0.25) * Cm * Cn) * liczba * ulamek1 *
                            ulamek2);
                            liczba = 0;
                        }
                    }
                    temp1 = temp1 + krok;
                    pom1 = pom1 + krok;
                }
                temp1 = 0;
                pom1 = krok;
                temp2 = temp2 + krok;
                pom2 = pom2 + krok;
            }
            for (i = 0; i < szerokosc1; i++)
                for (j = 0; j < dlugosc1; j++)
                {
                    sw.Write(tablica[i, j]);
                    sw.Write(' ');
                }
            sw.Close();
            System.Windows.Forms.MessageBox.Show("File compressed.");
            WriteNewDescriptionInImage(picture, "Cosinus.jpg");

        }
        private void WriteNewDescriptionInImage(Image Pic, string NewDescription)
        {
            byte[] bDescription = new Byte[NewDescription.Length];
            int i;
            System.Drawing.Imaging.Encoder Enc = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters EncParms = new EncoderParameters(1);
            EncoderParameter EncParm;
            ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");

            for (i = 0; i < NewDescription.Length; i++) bDescription[i] = (byte)NewDescription[i];
            if (Compression_degree != 0)
                EncParm = new EncoderParameter(Enc, (long)100 / (long)Compression_degree);
            else EncParm = new EncoderParameter(Enc, (long)Compression_degree);
            EncParms.Param[0] = EncParm;

            Pic.Save(NewDescription, CodecInfo, EncParms);

            GC.Collect();

            EncParm = new EncoderParameter(Enc, (long)EncoderValue.TransformRotate270);
            EncParms.Param[0] = EncParm;
            Pic.Save("Cosinus", CodecInfo, EncParms);

            GC.Collect();

        }

        #endregion
        #region CloseButton
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region LZW
        public void LZW_Compress()
        {

            Bitmap bitmap = new Bitmap(picture);
            CodecInfo = GetEncoderInfo("image/tiff");

            encoder = System.Drawing.Imaging.Encoder.Compression;

            encoderParameters = new EncoderParameters(1);

            encoderParameter = new EncoderParameter(
                encoder,
                (long)EncoderValue.CompressionLZW);
            encoderParameters.Param[0] = encoderParameter;

            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.Filter = "tif Files | *.tif; *.tif|All files| *.*";


            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bitmap.Save(saveFileDialog1.FileName, CodecInfo, encoderParameters);
                Compressed = new Bitmap(saveFileDialog1.FileName);
            }
        }
        #endregion
        #region CodecInfo
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        #endregion
        private void Compressor_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Compressor.SelectedIndex)
            {
                case 0:
                    Huffman();
                    break;
                case 1:
                    Difference();
                    break;
                case 2:
                    Block();
                    break;
                default:
                    Huffman();
                    break;
            }

            aSize.Text = after.ToString();
            CompressionLevel.Text = Compression_degree.ToString();

        }
    }
}
