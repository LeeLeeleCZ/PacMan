using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAC_MAN
{
    public partial class SaveMap : Form
    {
        public event DataSentHandler DataSent;
        public SaveMap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) => this.Close();

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Red, 2);
            e.Graphics.DrawLine(pen, 0, 0, button1.Width, button1.Height);
            e.Graphics.DrawLine(pen, button1.Width, 0, 0, button1.Height);
        }

        private void button2_Click(object sender, EventArgs e) => DataSent(this, richTextBox1.Text);
    }
}
