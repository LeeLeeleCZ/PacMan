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

            returnBtn.BackgroundImage = Image.FromFile("../../../grafika/return-icon-Red.png");
            returnBtn.BackgroundImageLayout = ImageLayout.Stretch;

            saveBtn.BackgroundImage = Image.FromFile("../../../grafika/CheckMark-Green.png");
            saveBtn.BackgroundImageLayout = ImageLayout.Stretch;

            editGhostsBtn.BackgroundImage = Image.FromFile("../../../grafika/ghost.png");
            Ghost1Btn.BackgroundImage = Image.FromFile("../../../grafika/ghost.png");
            Ghost2Btn.BackgroundImage = Image.FromFile("../../../grafika/ghost-teal.png");
            editPacManBtn.BackgroundImage = Image.FromFile("../../../grafika/pacman.gif");
            editGhostsBtn.BackgroundImageLayout = ImageLayout.Stretch;
            editPacManBtn.BackgroundImageLayout = ImageLayout.Stretch;
            Ghost1Btn.BackgroundImageLayout = ImageLayout.Stretch;
            Ghost2Btn.BackgroundImageLayout = ImageLayout.Stretch;

            Ghost1Btn.Location = new Point(0, 55);
            Ghost2Btn.Location = new Point(0, 55);
            Ghost3Btn.Location = new Point(0, 55);
        }

        private void MFmove(object? sender, EventArgs e) => this.Location = new Point(mainform.Location.X + mainform.Width + 25, mainform.Location.Y + mainform.Height / 2 - this.Height / 2 + 25);

        private void LevelEditorOptions_Load(object sender, EventArgs e) => this.Location = new Point(mainform.Location.X + mainform.Width + 25, mainform.Location.Y + mainform.Height / 2 - this.Height / 2 + 25);

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Show? zobrazit = Show.Nic;
            switch (btn.Name)
            {
                case "editGhostsBtn":
                    DataSent(this, LevelEditor.Mode.EditGhosts);
                    if (!Ghost1Btn.Visible)
                        zobrazit = Show.Duchove;
                    else
                        zobrazit = null;
                    break;
                case "Ghost1Btn":
                    DataSent(this, LevelEditor.Mode.EditGhosts);
                    if (!Ghost1Btn.Visible)
                        zobrazit = Show.Duchove;
                    else
                        zobrazit = null;
                    break;
                case "Ghost2Btn":
                    DataSent(this, LevelEditor.Mode.EditTrackers);
                    if (!Ghost1Btn.Visible)
                        zobrazit = Show.Duchove;
                    else
                        zobrazit = null;
                    break;                
                case "editPacManBtn":
                    DataSent(this, LevelEditor.Mode.EditPacMan);
                    break;
                case "editWallsBtn":
                    DataSent(this, LevelEditor.Mode.EditWalls);
                    break;
                case "editCoinBtn":
                    if (!editCoinsBtn.Visible)
                        zobrazit = Show.Coiny;
                    else
                        zobrazit = null;
                    DataSent(this, LevelEditor.Mode.EditCoin);
                    break;
                case "editCoinsBtn":
                    zobrazit = null;
                    DataSent(this, LevelEditor.Mode.EditCoins);
                    break;
                case "returnBtn":
                    DataSent(this, LevelEditor.Mode.ReturnBtn);
                    break;
                case "saveBtn":
                    DataSent(this, LevelEditor.Mode.SaveBtn);
                    break;
                case "removeCoinsBtn":
                    if (!editCoinsBtn.Visible)
                        zobrazit = Show.Coiny;
                    else
                        zobrazit = null;
                    DataSent(this, LevelEditor.Mode.RemoveCoins);
                    break;
            }
            if (zobrazit != null)
                AnimacePosunout((Show)zobrazit);
        }

        private void AnimacePosunout(Show zobrazit)
        {
            if (zobrazit == Show.Duchove)
            {
                SchovatButtony();
                Ghost1Btn.Visible = true;
                Ghost2Btn.Visible = true;
                Ghost3Btn.Visible = false;
                for (int i = 0; i < 28; i++)
                {
                    Ghost1Btn.Location = Ghost1Btn.Location with { X = Ghost1Btn.Location.X + 2 };
                    Ghost2Btn.Location = Ghost2Btn.Location with { X = Ghost2Btn.Location.X + 4 };
                    Ghost3Btn.Location = Ghost3Btn.Location with { X = Ghost3Btn.Location.X + 6 };
                    wait(10);
                }
            }
            else if(zobrazit == Show.Nic)
            {
                SchovatButtony();
            }
            else if (zobrazit == Show.Coiny)
            {
                SchovatButtony();
                editCoinsBtn.Visible = true;
                removeCoinsBtn.Visible = true;
                for (int i = 0; i < 28; i++)
                {
                    editCoinsBtn.Location = editCoinsBtn.Location with { X = editCoinsBtn.Location.X + 2 };
                    removeCoinsBtn.Location = removeCoinsBtn.Location with { X = removeCoinsBtn.Location.X + 4 };
                    wait(10);
                }
            }

        }

        void SchovatButtony()
        {
            Ghost1Btn.Visible = false;
            Ghost2Btn.Visible = false;
            Ghost3Btn.Visible = false;
            removeCoinsBtn.Visible = false;
            editCoinsBtn.Visible = false;
            Ghost1Btn.Location = editGhostsBtn.Location;
            Ghost2Btn.Location = editGhostsBtn.Location;
            Ghost3Btn.Location = editGhostsBtn.Location;
            editCoinsBtn.Location = editCoinBtn.Location;
            removeCoinsBtn.Location = editCoinBtn.Location;
        }

        private void returnBtn_Paint(object sender, PaintEventArgs e)
        {
            /*Pen pen = new Pen(Color.Red, 2);
            e.Graphics.DrawLine(pen, 0, 0, returnBtn.Width, returnBtn.Height);
            e.Graphics.DrawLine(pen, returnBtn.Width, 0, 0, returnBtn.Height);*/
        }


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


        enum Show
        {
            Nic,
            Duchove,
            Coiny
        }

        private void editCoinBtn_Paint(object sender, PaintEventArgs e)
        {
            //_sender.Text = "Coin";
            e.Graphics.FillEllipse(new SolidBrush(Color.Gold), 15, 15, 20, 20);
        }

        private void editCoinsBtn_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(Color.Gold), 10, 10, 10, 10);
            e.Graphics.FillEllipse(new SolidBrush(Color.Gold), 30, 10, 10, 10);
            e.Graphics.FillEllipse(new SolidBrush(Color.Gold), 10, 30, 10, 10);
            e.Graphics.FillEllipse(new SolidBrush(Color.Gold), 30, 30, 10, 10);
        }

        private void removeCoinsBtn_Paint(object sender, PaintEventArgs e)
        {
            Button _sender = (Button) sender;
            e.Graphics.FillEllipse(new SolidBrush(Color.Gold), 15, 15, 20, 20);

            //draw a red line from left upper corner to right bottom corner
            e.Graphics.DrawLine(new Pen(Color.Red, 5), 10, 10, _sender.Width-10, _sender.Height-10);
        }

        private void LevelEditorOptions_MouseHover(object sender, EventArgs e)
        {
            Button _sender = (Button)sender;
            switch (_sender.Name)
            {
                case "editGhostsBtn":
                    //toolTip1.Show("Edit ghosts", _sender);
                    break;
                case "editCoinBtn":
                    toolTip1.Show("Edit coins", _sender);
                    break;
                case "editCoinsBtn":
                    toolTip1.Show("Place coins to empty spaces", _sender);
                    break;
                case "removeCoinsBtn":
                    toolTip1.Show("Remove coins", _sender);
                    break;
                case "Ghost1Btn":
                    toolTip1.Show("Basic ghost", _sender);
                    break;
                case "Ghost2Btn":
                    toolTip1.Show("Tracker ghost", _sender);
                    break;
                case "Ghost3Btn":
                    toolTip1.Show("Smart ghost", _sender);
                    break;
                case "saveBtn":
                    toolTip1.Show("Save map", _sender);
                    break;
                case "returnBtn":
                    toolTip1.Show("Return to main menu", _sender);
                    break;
            }
        }

    }
}
