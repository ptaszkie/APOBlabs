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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void otworzStrone(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(link.Text);
            //new ProcessStartInfo(link.Text).Start();
        }
    }
}
