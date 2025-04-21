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
    public partial class AdvancedText : Form
    {
        private string filePath = "";
        public AdvancedText(string filePath)
        {
            InitializeComponent();
            this.filePath = filePath;
            if (File.Exists(filePath)) richTextBox.Text = File.ReadAllText(filePath);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            File.WriteAllText(filePath, richTextBox.Text);
            this.Close();
        }

        private void AdvancedText_Load(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
