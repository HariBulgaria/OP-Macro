using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OP_Macro
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(Path.Combine(Application.StartupPath, "info.png"));
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {

        }
    }
}
