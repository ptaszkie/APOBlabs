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
    public partial class Histogram : Form
    {
        private Bitmap image = null;
        private UInt32[,] LUT; // R, G, B, Grey
        private float x_scale;
        private float y_scale;
        private int hHeight;
        private int hWidth;
        private UInt32 max;

        public Histogram(ImageWindow iw)
        {
            InitializeComponent();
            MdiParent = iw.MdiParent;
            Text = Text + " " + iw.Text;
            image = iw.getBitmap();
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

            Histogram_ResizeEnd();
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

            for (int i = 0; i < 256; i++)
            {
                if (checkRed.Checked)
                    gr.DrawLine(Pen_R, i * x_scale + 5, hHeight - 10, i * x_scale + 5, (float)(hHeight - 10 - (float)((float)LUT[0, i] * y_scale)));
                if (checkGreen.Checked)
                    gr.DrawLine(Pen_G, i * x_scale + 5, hHeight - 10, i * x_scale + 5, (float)(hHeight - 10 - (float)((float)LUT[1, i] * y_scale)));
                if (checkBlue.Checked)
                    gr.DrawLine(Pen_B, i * x_scale + 5, hHeight - 10, i * x_scale + 5, (float)(hHeight - 10 - (float)((float)LUT[2, i] * y_scale)));

                gr.DrawLine(Pen_Grey, i * x_scale + 5, hHeight - 10, i * x_scale + 5, (float)(hHeight - 10 - (float)((float)LUT[3, i] * y_scale)));
            }

            image.Dispose();
            image=new Bitmap(HistogramImage.Image);
            HistogramImage.Invalidate();
            gr.Dispose();
        }

        private void Histogram_ResizeEnd(object sender=null, EventArgs e=null)
        {
            hHeight = HistogramImage.Height;
            hWidth = HistogramImage.Width;
            x_scale = ((float)hWidth - 10.0F) / 256.0F;
            y_scale = ((float)hHeight - 15.0F) / (float)max;

            DrawHistogram();
        } // change scale of histogram

        private void HistogramImage_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X-5;
            if (x < 0) x = 0;
            if (x > hWidth - 11) x = hWidth - 11;

            x = (int)Math.Floor((double) x / (double)x_scale);
            Value.Text = x.ToString();

            if (checkRed.Checked)
                RValue.Text = LUT[0, x].ToString();
            else
                RValue.Text = "- - -";

            if (checkGreen.Checked)
                GValue.Text = LUT[1, x].ToString();
            else
                GValue.Text = "- - -";

            if (checkBlue.Checked)
                BValue.Text = LUT[2, x].ToString();
            else
                BValue.Text = "- - -";

            GreyValue.Text = LUT[3, x].ToString();


            UInt32 hmax = LUT[3, x];
            if (checkRed.Checked && LUT[0, x] > hmax)
                hmax = LUT[0, x];
            if (checkGreen.Checked && LUT[1, x] > hmax)
                hmax = LUT[1, x];
            if (checkBlue.Checked && LUT[2, x] > hmax)
                hmax = LUT[2, x];

            HistogramImage.Image.Dispose();
            HistogramImage.Image = (Bitmap)image.Clone();
            Graphics gr = Graphics.FromImage(HistogramImage.Image); 
            gr.DrawLine(new Pen(Color.OrangeRed), x * x_scale + 5, hHeight - 10, x * x_scale + 5, (float)(hHeight - 10 - (float)((float)hmax * y_scale)));
        } // update information about desirable data

    }
}
