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
    public partial class Median : Form
    {
        ImageWindow iw;
        Bitmap prevImage;
        public Median(ImageWindow iw)
        {
            InitializeComponent();
            MdiParent = iw.MdiParent;
            prevImage = iw.getBitmap();
            this.iw = iw;

            xSize_ValueChanged();
            Show();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            iw.setBitmap(prevImage);
            Dispose();
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            iw.setPrevBitmap(prevImage);
            iw.updateLUT();
            Dispose();
            Close();
        }

        private void xSize_ValueChanged(object sender=null, EventArgs e=null)
        {
            iw.setBitmap(prevImage);
            iw.Median((int)xSize.Value, (int)ySize.Value);
        }
    }
}
