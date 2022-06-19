using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace PAC_MAN
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            Nastavení.SettingsUpdate += settings_update;
            settings_update();

            
            button1.BackgroundImage = Image.FromFile("../../../grafika/return-icon-Red.png");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void settings_update()
        {
            pictureBox1.Image = Nastavení.Hudba ? Image.FromFile("../../../grafika/CheckMark-Blue.png") : null;
            pictureBox2.Image = Nastavení.Zvuk ? Image.FromFile("../../../grafika/CheckMark-Blue.png") : null;
            pictureBox3.Image = Nastavení.Controls ? Image.FromFile("../../../grafika/CheckMark-Blue.png") : null;
            pictureBox4.Image = !Nastavení.Controls ? Image.FromFile("../../../grafika/CheckMark-Blue.png") : null;
            GC.Collect();
        }

        private void label3_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = (Label) sender;
            ControlPaint.DrawBorder(e.Graphics, lbl.ClientRectangle,
                Color.Blue, 2, ButtonBorderStyle.Solid,
                Color.Blue, 2, ButtonBorderStyle.Solid,
                Color.Blue, 2, ButtonBorderStyle.Solid,
                Color.Blue, 2, ButtonBorderStyle.Solid
            );
        }

        private void Paint(object sender, PaintEventArgs e)
        {
            PictureBox lbl = (PictureBox)sender;
            Rectangle rect = new Rectangle(0, 0, lbl.Width, lbl.Height);
            ControlPaint.DrawBorder(e.Graphics, lbl.ClientRectangle,
                Color.Blue, 2, ButtonBorderStyle.Solid,
                Color.Blue, 2, ButtonBorderStyle.Solid,
                Color.Blue, 2, ButtonBorderStyle.Solid,
                Color.Blue, 2, ButtonBorderStyle.Solid
            );
        }

        private void label3_Click(object sender, EventArgs e)
        {
            switch ((Label)sender == label3 ? "Music" : "Sound")
            {
                case "Music":
                    Nastavení.Hudba = !Nastavení.Hudba;
                    break;
                case "Sound":
                    Nastavení.Zvuk = !Nastavení.Zvuk;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e) => this.Close();
        
        
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            switch ((PictureBox)sender == pictureBox3 ? "wasd" : "šipky")
            {
                case "wasd":
                    Nastavení.Controls = true;
                    break;
                case "šipky":
                    Nastavení.Controls = false;
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) => Nastavení.Hudba = !Nastavení.Hudba;

        private void pictureBox2_Click(object sender, EventArgs e) => Nastavení.Zvuk = !Nastavení.Zvuk;

        private void Settings_Load(object sender, EventArgs e)
        {
            
            List<PictureBox> list = new List<PictureBox>();
            foreach (Control c in this.Controls)
            {
                if (c is PictureBox)
                {
                    list.Add((PictureBox)c);
                }
            }

            list.Reverse();

            foreach (PictureBox box in list)
            {
                for (int i = 0; i < 30; i++)
                {
                    box.Left -= 10;
                    wait(1);
                }
            }
        }

        public static void wait(int milliseconds)
        {
            var timer1 = new Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
    }
}
