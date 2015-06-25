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
    public partial class Filters : Form
    {
        TextBox[,] b = new TextBox[3,3];
        bool stopEvents;
        ImageWindow iw;

        public Filters(ImageWindow iw)
        {
            InitializeComponent();
            b[0, 0] = t00;
            b[0, 1] = t01;
            b[0, 2] = t02;
            b[1, 0] = t10;
            b[1, 1] = t11;
            b[1, 2] = t12;
            b[2, 0] = t20;
            b[2, 1] = t21;
            b[2, 2] = t22;

            Preset.SelectedIndex = 0;
            stopEvents = false;

            this.iw = iw;
            MdiParent = iw.MdiParent;
            Show();
        }

        private void t00_TextChanged(object sender=null, EventArgs e=null)
        {
            if(stopEvents) return;
            stopEvents = true;
            int pom, wyn = 0;
            foreach (TextBox t in b)
            {
                int.TryParse(t.Text, out pom);
                wyn += pom;
            }
            if (wyn < 1) wyn = 1;
            divider.Text = wyn.ToString();

            stopEvents = false;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            int[,] t = new int[3,3];
            t[0, 0] = int.Parse(t00.Text);
            t[0, 1] = int.Parse(t01.Text);
            t[0, 2] = int.Parse(t02.Text);
            t[1, 0] = int.Parse(t10.Text);
            t[1, 1] = int.Parse(t11.Text);
            t[1, 2] = int.Parse(t12.Text);
            t[2, 0] = int.Parse(t20.Text);
            t[2, 1] = int.Parse(t21.Text);
            t[2, 2] = int.Parse(t22.Text);

            iw.Filter(t, int.Parse(divider.Text));
            Dispose();
            Close();
        }

        private void Preset_SelectedIndexChanged(object sender, EventArgs e)
        {
            stopEvents = true;
            if (Preset.Text.Equals("Custom"))
            {
                b[0, 0].Text = "0";
                b[0, 1].Text = "0";
                b[0, 2].Text = "0";
                b[1, 0].Text = "0";
                b[1, 1].Text = "1";
                b[1, 2].Text = "0";
                b[2, 0].Text = "0";
                b[2, 1].Text = "0";
                b[2, 2].Text = "0";
            }
            else if (Preset.Text.Equals("Gradiant Gx"))
            {
                b[0, 0].Text = "-1";
                b[0, 1].Text = "-2";
                b[0, 2].Text = "-1";
                b[1, 0].Text = "0";
                b[1, 1].Text = "0";
                b[1, 2].Text = "0";
                b[2, 0].Text = "1";
                b[2, 1].Text = "2";
                b[2, 2].Text = "1";
            }
            else if (Preset.Text.Equals("Gradiant Gy"))
            {
                b[0, 0].Text = "-1";
                b[0, 1].Text = "0";
                b[0, 2].Text = "1";
                b[1, 0].Text = "-2";
                b[1, 1].Text = "0";
                b[1, 2].Text = "2";
                b[2, 0].Text = "-1";
                b[2, 1].Text = "0";
                b[2, 2].Text = "1";
            }
            else if (Preset.Text.Equals("Laplasjan"))
            {
                b[0, 0].Text = "0";
                b[0, 1].Text = "1";
                b[0, 2].Text = "0";
                b[1, 0].Text = "1";
                b[1, 1].Text = "-4";
                b[1, 2].Text = "1";
                b[2, 0].Text = "0";
                b[2, 1].Text = "1";
                b[2, 2].Text = "0";
            }
            else if (Preset.Text.Equals("Edge detection #1"))
            {
                b[0, 0].Text = "1";
                b[0, 1].Text = "-2";
                b[0, 2].Text = "1";
                b[1, 0].Text = "-2";
                b[1, 1].Text = "5";
                b[1, 2].Text = "-2";
                b[2, 0].Text = "1";
                b[2, 1].Text = "-2";
                b[2, 2].Text = "-1";
            }
            else if (Preset.Text.Equals("Edge detection #2"))
            {
                b[0, 0].Text = "-1";
                b[0, 1].Text = "-1";
                b[0, 2].Text = "-1";
                b[1, 0].Text = "-1";
                b[1, 1].Text = "9";
                b[1, 2].Text = "-1";
                b[2, 0].Text = "-1";
                b[2, 1].Text = "-1";
                b[2, 2].Text = "-1";
            }
            else if (Preset.Text.Equals("Edge detection #3"))
            {
                b[0, 0].Text = "0";
                b[0, 1].Text = "-1";
                b[0, 2].Text = "0";
                b[1, 0].Text = "-1";
                b[1, 1].Text = "5";
                b[1, 2].Text = "-1";
                b[2, 0].Text = "0";
                b[2, 1].Text = "-1";
                b[2, 2].Text = "0";
            }
            else if (Preset.Text.Equals("Sobel Gx"))
            {
                b[0, 0].Text = "-1";
                b[0, 1].Text = "0";
                b[0, 2].Text = "-1";
                b[1, 0].Text = "-2";
                b[1, 1].Text = "0";
                b[1, 2].Text = "2";
                b[2, 0].Text = "-1";
                b[2, 1].Text = "0";
                b[2, 2].Text = "-1";
            }
            else if (Preset.Text.Equals("Sobel Gy"))
            {
                b[0, 0].Text = "-1";
                b[0, 1].Text = "-2";
                b[0, 2].Text = "-1";
                b[1, 0].Text = "0";
                b[1, 1].Text = "0";
                b[1, 2].Text = "0";
                b[2, 0].Text = "1";
                b[2, 1].Text = "2";
                b[2, 2].Text = "1";
            }

            
            stopEvents = false;
            t00_TextChanged();
        }
    }
}
