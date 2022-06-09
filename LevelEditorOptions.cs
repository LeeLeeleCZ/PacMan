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
    public delegate void EODataSentHandler(Form? sender, LevelEditor.Mode Mode);
    public partial class LevelEditorOptions : Form
    {
        public event EODataSentHandler DataSent;
        LevelEditor parent;
        MainForm mainform;
        private bool ghostAktivni = false;
        public LevelEditorOptions(LevelEditor parent)
        {
            InitializeComponent();
            this.parent = parent;
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Tag == "MainForm")
                {
                    mainform = (MainForm)Application.OpenForms[i];
                    mainform.Move += MFmove;
                }
            }
            
            editGhostsBtn.BackgroundImage = Image.FromFile("../../../grafika/ghost.png");
            editPacManBtn.BackgroundImage = Image.FromFile("../../../grafika/pacman.gif");
            editGhostsBtn.BackgroundImageLayout = ImageLayout.Stretch;
            editPacManBtn.BackgroundImageLayout = ImageLayout.Stretch;

            Ghost1Btn.Location = new Point(0, 55);
            Ghost2Btn.Location = new Point(0, 55);
            Ghost3Btn.Location = new Point(0, 55);
        }

        private void MFmove(object? sender, EventArgs e) => this.Location = new Point(mainform.Location.X + mainform.Width + 25, mainform.Location.Y + mainform.Height / 2 - this.Height / 2 + 25);

        private void LevelEditorOptions_Load(object sender, EventArgs e) => this.Location = new Point(mainform.Location.X + mainform.Width + 25, mainform.Location.Y + mainform.Height / 2 - this.Height / 2 + 25);

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int aktivovat = 0;
            switch (btn.Name)
            {
                case "editGhostsBtn":
                    DataSent(this, LevelEditor.Mode.EditGhosts);
                    if (!Ghost1Btn.Visible)
                        aktivovat = 1;
                    else
                        aktivovat = 2;
                    break;
                case "editPacManBtn":
                    DataSent(this, LevelEditor.Mode.EditPacMan);
                    break;
                case "editWallsBtn":
                    DataSent(this, LevelEditor.Mode.EditWalls);
                    break;
                case "returnBtn":
                    DataSent(this, LevelEditor.Mode.returnBtn);
                    break;
            }
            if(aktivovat == 1)
                zobrazDuchy(true);
            else if (aktivovat == 0)
                zobrazDuchy(false);
        }

        private void zobrazDuchy(bool zobrazit)
        {
            /*
            for (int i = 0; i < 28; i++)
            {
                Ghost1Btn.Location = Ghost1Btn.Location with {X = Ghost1Btn.Location.X+2};
                wait(10);
            }
            wait(50);
            for (int i = 0; i < 56; i++)
            {
                Ghost2Btn.Location = Ghost2Btn.Location with {X = Ghost2Btn.Location.X+2};
                wait(10);
            }
            wait(50);
            for (int i = 0; i < 84; i++)
            {
                Ghost3Btn.Location = Ghost3Btn.Location with {X = Ghost3Btn.Location.X+2};
                wait(10);
            }
            */
            if (zobrazit)
            {
                Ghost1Btn.Visible = true;
                Ghost2Btn.Visible = true;
                Ghost3Btn.Visible = true;
                for (int i = 0; i < 28; i++)
                {
                    Ghost1Btn.Location = Ghost1Btn.Location with { X = Ghost1Btn.Location.X + 2 };
                    Ghost2Btn.Location = Ghost2Btn.Location with { X = Ghost2Btn.Location.X + 4 };
                    Ghost3Btn.Location = Ghost3Btn.Location with { X = Ghost3Btn.Location.X + 6 };
                    wait(10);
                }
            }
            else
            {
                Ghost1Btn.Visible = false;
                Ghost2Btn.Visible = false;
                Ghost3Btn.Visible = false;
                Ghost1Btn.Location = editGhostsBtn.Location;
                Ghost2Btn.Location = editGhostsBtn.Location;
                Ghost3Btn.Location = editGhostsBtn.Location;
            }

        }

        private void returnBtn_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Red, 2);
            e.Graphics.DrawLine(pen, 0, 0, returnBtn.Width, returnBtn.Height);
            e.Graphics.DrawLine(pen, returnBtn.Width, 0, 0, returnBtn.Height);
        }

        //private void Mainform_DataSent(string msg)
        //{
        //    if (msg == "position")
        //    {
        //        this.Location = new Point(mainform.Location.X + mainform.Width / 2 - this.Width / 2, mainform.Location.Y + mainform.Height / 2 - this.Height / 2);
        //    }
        //}

        public static void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
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
