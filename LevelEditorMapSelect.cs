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
    
    public partial class LevelEditorMapSelect : Form
    {
        public event DataSentHandler DataSent;
        public LevelEditorMapSelect()
        {
            InitializeComponent();

            button3.BackgroundImage = Image.FromFile("../../../grafika/return-icon-Red.png");
            button3.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e) => DataSent(this, "Nova");

        private void button2_Click(object sender, EventArgs e) => DataSent(this, "Nacist");

        private void button3_Click(object sender, EventArgs e) => this.Close();

        private void button3_Paint(object sender, PaintEventArgs e)
        {
            /*Pen pen = new Pen(Color.Red, 2);
            e.Graphics.DrawLine(pen, 0, 0, button3.Width, button3.Height);
            e.Graphics.DrawLine(pen, button3.Width, 0, 0, button3.Height);*/
        }
    }
}
