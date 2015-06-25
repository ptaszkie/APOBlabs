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
    public partial class Gamma: Form
    {
        ImageWindow iw;
        private Bitmap prevImg = null;
        bool dontHandle=false;

        public Gamma(ImageWindow iw)
        {
            InitializeComponent();
            this.iw = iw;
            MdiParent = iw.MdiParent;
            Text = Text + " " + iw.Text;
            prevImg = iw.getBitmap();

            
            iw.Gamma(ZipVal.Value);
            Show();
        }

        private void Value_TextChanged(object sender, EventArgs e)
        {
            if (dontHandle)
            {
                dontHandle = false;
                return;
            }

            double val;
            double.TryParse(Value.Text, out val);
            if(val<0)
                val=0;
            if (val>10) 
                val=10;

            ZipVal.Value = (int)(val*100);

            dontHandle = true;
            Value.Text = val.ToString();
        }

        private void ZipVal_ValueChanged(object sender, EventArgs e)
        {
            double val = (double)ZipVal.Value / 100.0F;
            dontHandle = false;
            Value.Text = val.ToString();
            iw.setBitmap(prevImg);
            iw.Gamma(ZipVal.Value);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            iw.setBitmap(prevImg);
            prevImg.Dispose();
            Dispose();
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            iw.setPrevBitmap(prevImg);
            iw.updateLUT();
            prevImg.Dispose();
            Dispose();
            Close();
        }
    }
}
