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
    public delegate void DataSentHandler(Form? sender, string? msg);
    public partial class Menu : Form
    {
        SmerPacmana smer = SmerPacmana.dolu;
        public event DataSentHandler DataSent;

        public Menu()
        {
            InitializeComponent();
            PacManPictureBox.Image = Image.FromFile("../../../grafika/pacman(dolu).gif");
            // make the image fit the picturebox
            PacManPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch(smer)
            {
                case SmerPacmana.dolu:
                    PacManPictureBox.Top += 1;
                    if (PacManPictureBox.Top >= panel1.Height - PacManPictureBox.Height)
                    {
                        PacManPictureBox.Image = Image.FromFile("../../../grafika/pacman.gif");
                        smer = SmerPacmana.doprava;
                    }
                    break;
                case SmerPacmana.doprava:
                    PacManPictureBox.Left += 1;
                    if (PacManPictureBox.Left >= panel1.Width - PacManPictureBox.Width)
                    {
                        PacManPictureBox.Image = Image.FromFile("../../../grafika/pacman(nahoru).gif");
                        smer = SmerPacmana.nahoru;
                    }
                    break;
                case SmerPacmana.nahoru:
                    PacManPictureBox.Top -= 1;
                    if (PacManPictureBox.Top <= 0)
                    {
                        PacManPictureBox.Image = Image.FromFile("../../../grafika/pacman(doleva).gif");
                        smer = SmerPacmana.doleva;
                    }
                    break;
                case SmerPacmana.doleva:
                    PacManPictureBox.Left -= 1;
                    if (PacManPictureBox.Left <= 0)
                    {
                        PacManPictureBox.Image = Image.FromFile("../../../grafika/pacman(dolu).gif");
                        smer = SmerPacmana.dolu;
                    }
                    break;
            }
        }

        enum SmerPacmana { dolu, doprava, nahoru, doleva };

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
            for (int i = 22; i <= panel1.Height-24; i+=24)
            {
                e.Graphics.FillEllipse(Brushes.Gold, 24, i, 10, 10);
            }

            for (int i = 24; i <= panel1.Width - 24; i += 24)
            {
                e.Graphics.FillEllipse(Brushes.Gold, i, panel1.Height - 24, 10, 10);
            }

            for (int i = 22; i <= panel1.Height - 24; i += 24)
            {
                e.Graphics.FillEllipse(Brushes.Gold, panel1.Width-28, i, 10, 10);
            }

            for (int i = 24; i <= panel1.Width - 24; i += 24)
            {
                e.Graphics.FillEllipse(Brushes.Gold, i, 24, 10, 10);
            }
            
        }

        private void button1_Click(object sender, EventArgs e) => DataSent(null, "Game");

        private void button2_Click(object sender, EventArgs e) => DataSent(null, "LevelEditor");

        private void button4_Click(object sender, EventArgs e) => DataSent(null, "Settings");

        private void Menu_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e) => DataSent(null, "Score");
    }
}
