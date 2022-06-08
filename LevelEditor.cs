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
        private string mapName;
        LevelEditorOptions EditorOptions;
        private bool HoldingMouse = false;
        private Graphics g;
        private bool KliknutoNaZed;
        private Mode modEditu = Mode.EditWalls;

        public Mode ModEditu
        {
            get
            {
                return modEditu;
            }
            set
            {
                if (value == Mode.returnBtn)
                {
                    this.Close();
                    EditorOptions.Close();
                }
                else
                {
                    modEditu = value;
                }
            }
        }
        
        public LevelEditor(MainForm parent, string? mapName)
        {
            this.mapName = mapName;
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
            EditorOptions.DataSent += EODataSent;

            if (mapName != null)
            {
                NacistMapu();
            }

            g = Graphics.FromImage(btm);

        }

        private void EODataSent(Form? sender, Mode mode) => ModEditu = mode;

        private void KMEvents_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                var Ulozeni = new SaveMap();
                Ulozeni.DataSent += (sender, name) =>
                {
                    if(sender!=null)
                        sender.Close();
                    if (name != null)
                    {

                    }
                };
                Ulozeni.ShowDialog();
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
        {/*
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
            */
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
        private List<int> DeserializeXml()
        {
            // deserialize xml
            XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
            FileStream fs = new FileStream($"..//..//..//Maps//{mapName}.xml", FileMode.Open);
            List<int> list = (List<int>)serializer.Deserialize(fs);
            fs.Close();
            return list;
        }

        private void NacistMapu()
        {
            List<int> list = DeserializeXml();
            int p = 0;
            Graphics g = Graphics.FromImage(btm);
            for (int i = 0; i < pole.GetLength(0); i++)
            {
                for (int y = 0; y < pole.GetLength(1); y++)
                {
                    pole[i, y] = list[p];
                    if (pole[i, y] == 1)
                    {
                        g.FillRectangle(Brushes.Blue, i * 50 + 1, y * 50 + 1, 49, 49);
                    }
                    p++;
                }
            }
        }

        private void LevelEditor_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / 50;
            int y = e.Y / 50;
            
            if (pole[x, y] == 1)
            {
                KliknutoNaZed = true;
            }
            else
            {
                KliknutoNaZed = false;
            }

            HoldingMouse = true;
        }

        private void LevelEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if(ModEditu != Mode.EditWalls) return;
            if (HoldingMouse)
            {
                int x = e.X / 50;
                int y = e.Y / 50;

                if (x > btm.Width / 50 - 1 || y > btm.Height / 50 - 1 || x < 0 || y < 0)
                    return;

                if (KliknutoNaZed)
                {
                    pole[x, y] = 0;
                    g.FillRectangle(Brushes.Black, x * 50 + 1, y * 50 + 1, 49, 49);
                }
                else
                {
                    pole[x, y] = 1;
                    g.FillRectangle(Brushes.Blue, x * 50 + 1, y * 50 + 1, 49, 49);
                }

                this.Invalidate();
            }
        }

        private void LevelEditor_MouseUp(object sender, MouseEventArgs e)
        {
            HoldingMouse = false;
            GC.Collect();
        }

        public enum Mode
        {
            EditWalls,
            EditGhosts,
            EditPacMan,
            returnBtn,
        }
    }
}
