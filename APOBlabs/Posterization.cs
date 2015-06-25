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
    public partial class Posterization : Form
    {
        ImageWindow iw;
        private Bitmap prevImg = null;

        public Posterization(ImageWindow iw)
        {
            InitializeComponent();
            this.iw = iw;
            MdiParent = iw.MdiParent;
            Text = Text + " " + iw.Text;
            prevImg = iw.getBitmap();

            iw.Posterize(ZipVal.Value);
            Show();
        }

        private void Value_TextChanged(object sender, EventArgs e)
        {
            int val;
            int.TryParse(Value.Text, out val);
            if(val<0) val=0;
            else if (val>128) val=128;

            ZipVal.Value = val;
            Value.Text = val.ToString();
        }

        private void ZipVal_ValueChanged(object sender, EventArgs e)
        {
            Value.Text = ZipVal.Value.ToString();
            iw.setBitmap(prevImg);
            iw.Posterize(ZipVal.Value);
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
