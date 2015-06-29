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
    public partial class Contrast: Form
    {
        ImageWindow iw;
        private Bitmap prevImg = null;
        bool dontHandle=false;

        public Contrast(ImageWindow iw)
        {
            InitializeComponent();
            this.iw = iw;
            MdiParent = iw.MdiParent;
            Text = Text + " " + iw.Text;
            prevImg = iw.getBitmap();

            
            iw.Contrast(ZipVal.Value);
            Show();
        }

        private void Value_TextChanged(object sender, EventArgs e)
        {
            if (dontHandle)
            {
                dontHandle = false;
                return;
            }

            int val;
            int.TryParse(Value.Text, out val);
            if(val<-100)
                val=-100;
            if (val>100) 
                val=100;

            ZipVal.Value = val+100;

            dontHandle = true;
            Value.Text = val.ToString();
        }

        private void ZipVal_ValueChanged(object sender, EventArgs e)
        {
            int val = ZipVal.Value -100;
            dontHandle = false;
            Value.Text = val.ToString();
            iw.setBitmap(prevImg);
            iw.Contrast(val);
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
