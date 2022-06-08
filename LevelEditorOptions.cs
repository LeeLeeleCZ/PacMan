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
        }

        private void MFmove(object? sender, EventArgs e) => this.Location = new Point(mainform.Location.X + mainform.Width + 25, mainform.Location.Y + mainform.Height / 2 - this.Height / 2 + 25);

        private void LevelEditorOptions_Load(object sender, EventArgs e) => this.Location = new Point(mainform.Location.X + mainform.Width + 25, mainform.Location.Y + mainform.Height / 2 - this.Height / 2 + 25);

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "editGhostsBtn":
                    DataSent(this, LevelEditor.Mode.EditGhosts);
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


    }
}
