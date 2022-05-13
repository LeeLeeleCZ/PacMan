using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Gma.System.MouseKeyHook;


namespace PAC_MAN
{
    public partial class LevelEditor : Form
    {
        private readonly IKeyboardMouseEvents KMEvents;
        public event DataSentHandler DataSent;
        Bitmap btm;
        int[,] pole;
        PictureBox[,] zdi;
        LevelEditorOptions EditorOptions;
        public LevelEditor(MainForm parent)
        {
            KMEvents = Hook.AppEvents();
            KMEvents.KeyPress += KMEvents_KeyPress;
            InitializeComponent();
            btm = new Bitmap(this.Width, this.Height);
            nakreslitGrid();
            pole = new int[btm.Width / 50, btm.Height / 50];
            zdi = new PictureBox[btm.Width / 50, btm.Height / 50];
            // fill every filed in pole with 0
            for (int i = 0; i < pole.GetLength(0); i++)
            {
                for (int j = 0; j < pole.GetLength(1); j++)
                {
                    pole[i, j] = 0;
                }
            }

            EditorOptions = new LevelEditorOptions(this);
            EditorOptions.Show();
            
        }


        private void editOptions_DataSent(string msg)
        {
            throw new NotImplementedException();
        }

        private void KMEvents_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                saveLevel(pole);
                //DataSent("Menu");
                DataSent(this, "Menu");
                this.Close();
                this.Dispose();
                EditorOptions.Dispose();
                //this = null;
            }

        }

        void nakreslitGrid()
        {
            Graphics g = Graphics.FromImage(btm);
            for (int i = 0; i < this.Width; i += 50)
            {
                // draw a horizontal line
                g.DrawLine(Pens.DarkGray, i, 0, i, this.Height);
            }
            for (int i = 0; i < this.Height; i += 50)
            {
                // draw a vertical line
                g.DrawLine(Pens.DarkGray, 0, i, this.Width, i);
            }
        }

        private void LevelEditor_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(btm, 0, 0);
        }

        private void LevelEditor_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / 50;
            int y = e.Y / 50;
            Graphics g = Graphics.FromImage(btm);

            if (pole[x, y] == 1)
            {
                pole[x, y] = 0;
                g.FillRectangle(Brushes.Black, x * 50 + 1, y * 50 + 1, 49, 49);
            }
            else
            {
                pole[x, y] = 1;
                g.FillRectangle(Brushes.Red, x * 50 + 1, y * 50 + 1, 49, 49);
            }

            this.Invalidate();

        }

        public void saveLevel(int[,] pole)            
        {
            List<int> list = new List<int>();
            //for (int i = 0; i < pole.GetLength(0); i++)
            //{
            //    for (int j = 0; j < pole.GetLength(1); j++)
            //    {
            //        list.Add(pole[i, j]);
            //    }
            //}

            foreach (int item in pole)
            {
                list.Add(item);
            }
            
            string filename = Microsoft.VisualBasic.Interaction.InputBox("Zadejte jméno levelu:", "Uložení mapy", "level1");
            XmlSerializer serializer = new XmlSerializer(typeof(List<int>));

            using (FileStream fs = new FileStream($"..//..//..//Maps//{filename}.xml", FileMode.Create))
            {
                serializer.Serialize(fs, list);
            }
        }

        private void LevelEditor_Load(object sender, EventArgs e)
        {

        }

        private void LevelEditor_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
