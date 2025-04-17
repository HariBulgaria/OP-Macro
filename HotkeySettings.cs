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
    public partial class HotkeySettings : Form
    {
        public HotkeySettings(uint vk)
        {
            InitializeComponent();
            label1.Text = vk.ToString();
        }

        private void HotkeySettings_Load(object sender, EventArgs e)
        {

        }
    }
}
