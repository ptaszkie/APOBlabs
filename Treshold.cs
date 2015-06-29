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
    public partial class Treshold : Form
    {
        ImageWindow iw = null;
        private Bitmap prevImg = null;
        private UInt32[,] LUT; // R, G, B, Grey
        private float x_scale;
        private float y_scale;
        private int hHeight;
        private int hWidth;
        private UInt32 max;

        public Treshold(ImageWindow iw)
        {
            InitializeComponent();
            this.iw = iw;
            MdiParent = iw.MdiParent;
            Text = Text + " " + iw.Text;
            HistogramImage.Image = new Bitmap(HistogramImage.Width, HistogramImage.Height);
            LUT = (UInt32[,])iw.LUT.Clone();

            max = 0;
            for (int i = 0; i < 256; i++)
            {
                if (LUT[0, i] > max) max = LUT[0, i];
                if (LUT[1, i] > max) max = LUT[1, i];
                if (LUT[2, i] > max) max = LUT[2, i];
                if (LUT[3, i] > max) max = LUT[3, i];
            }

            prevImg = iw.getBitmap();
            iw.Treshold(ZipVal.Value,Reverse.Checked);
            hHeight = HistogramImage.Height;
            hWidth = HistogramImage.Width;
            x_scale = ((float)hWidth - 10.0F) / 256.0F;
            y_scale = ((float)hHeight - 15.0F) / (float)max;

            DrawHistogram();
            Show();
        }

        private void DrawHistogram(object sender=null, EventArgs e=null)
        {
            HistogramImage.Image.Dispose();
            HistogramImage.Image = new Bitmap(HistogramImage.Width, HistogramImage.Height);

            Graphics gr = Graphics.FromImage(HistogramImage.Image);          
            
            Pen Pen_R = new Pen(new SolidBrush(Color.FromArgb(100, 255, 0, 0)), x_scale);
            Pen Pen_G = new Pen(new SolidBrush(Color.FromArgb(100, 0, 255, 0)), x_scale);
            Pen Pen_B = new Pen(new SolidBrush(Color.FromArgb(100, 0, 0, 255)), x_scale);
            Pen Pen_Grey = new Pen(new SolidBrush(Color.FromArgb(100, 150,150,150)), x_scale);

            Brush red_brush = new SolidBrush(Color.FromArgb(100, 200,0,0));
            Pen red = new Pen(red_brush);
            gr.DrawLine(red, 5, HistogramImage.Height - 9, HistogramImage.Width - 5, HistogramImage.Height - 9);
            gr.DrawString("0", new Font("Arial", 7), red_brush, new PointF(1.0F, hHeight - 10));
            gr.DrawString("255", new Font("Arial", 7), red_brush, new PointF(hWidth-17, hHeight - 10));
            gr.DrawLine(red, 5, HistogramImage.Height - 9, HistogramImage.Width - 5, HistogramImage.Height - 9);
            gr.DrawString("0", new Font("Arial", 7), red_brush, new PointF(1.0F, hHeight - 10));
            gr.DrawString("255", new Font("Arial", 7), red_brush, new PointF(hWidth - 17, hHeight - 10));

            int val = int.Parse(Level.Text);
            for (int i = 0; i < 256; i++)
            {
                if(Reverse.Checked){
                    if(i<val)
                        gr.DrawLine(new Pen(Color.FromArgb(100, 150,150,150)), i * x_scale + 5, hHeight - 10, i * x_scale + 5, (float)(hHeight - 10 - (float)((float)LUT[3, i] * y_scale)));
                    else 
                        gr.DrawLine(new Pen(Color.OrangeRed), i * x_scale + 5, hHeight - 10, i * x_scale + 5, (float)(hHeight - 10 - (float)((float)LUT[3, i] * y_scale)));
                } else {
                    if(i<=val)
                        gr.DrawLine(new Pen(Color.OrangeRed), i * x_scale + 5, hHeight - 10, i * x_scale + 5, (float)(hHeight - 10 - (float)((float)LUT[3, i] * y_scale)));
                    else 
                        gr.DrawLine(new Pen(Color.FromArgb(100, 150,150,150)), i * x_scale + 5, hHeight - 10, i * x_scale + 5, (float)(hHeight - 10 - (float)((float)LUT[3, i] * y_scale)));
                }
            }

            iw.setBitmap(prevImg);
            iw.Treshold(val, Reverse.Checked);

            HistogramImage.Invalidate();
            gr.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int val = 0;
            int.TryParse(Level.Text,out val);
            if (val > 255) val = 255;
            else if (val < 0) val = 0;

            ZipVal.Value = val;
            Level.Text = val.ToString();
        }

        private void ZipVal_ValueChanged(object sender, EventArgs e)
        {
            Level.Text = ZipVal.Value.ToString();
            DrawHistogram();
        }

        private void BtnAbort_Click(object sender=null, EventArgs e=null)
        {
            iw.setBitmap(prevImg);
            prevImg.Dispose();
            HistogramImage.Dispose();
            Dispose();
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            iw.setPrevBitmap(prevImg);
            iw.updateLUT();
            prevImg.Dispose();
            HistogramImage.Dispose();
            Dispose();
            Close();
        }

    }
}
