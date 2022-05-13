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
    public partial class LevelEditorOptions : Form
    {
        LevelEditor parent;
        MainForm mainform;
        public event DataSentHandler DataSent;
        public LevelEditorOptions(LevelEditor parent)
        {
            InitializeComponent();
            this.parent = parent;
            // set location to be in middle of parent
            this.Location = new Point(parent.Location.X + parent.Width / 2 - this.Width / 2, parent.Location.Y + parent.Height / 2 - this.Height / 2);
            //parent.DataSent += Parent_DataSent;

            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Tag == "MainForm")
                {
                    mainform = (MainForm)Application.OpenForms[i];
                    this.Location = new Point(mainform.Location.X + mainform.Width, mainform.Location.Y);
                }
            }
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
