using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private bool PacmanExists = false;
        private MainForm parent;

        public Mode ModEditu
        {
            get
            {
                return modEditu;
            }
            set
            {
                if (value == Mode.ReturnBtn)
                {
                    this.Close();
                    EditorOptions.Close();
                }
                else if (value == Mode.EditCoins)
                {
                    PridejCoiny(true);
                    modEditu = Mode.EditCoin;
                }
                else if (value == Mode.RemoveCoins)
                {
                    PridejCoiny(false);
                    modEditu = Mode.EditCoin;
                }
                else if (value == Mode.SaveBtn)
                    saveLevel(pole);
                else
                {
                    modEditu = value;
                }
            }
        }
        
        public LevelEditor(MainForm parent, string? mapName)
        {
            this.parent = parent;
            this.mapName = mapName;
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
        /*
        {
            ModEditu = mode;
            /*
            if (mode == Mode.EditCoins)
            {
                PridejCoiny(true);
                modEditu = Mode.EditCoin;
            }
            else if (mode == Mode.RemoveCoins)
            {
                PridejCoiny(false);
                modEditu = Mode.EditCoin;
            }
            else
                ModEditu = mode;
            
        }*/

        private void PridejCoiny(bool pridat)
        {
            // replace every int in pole with 1
            for (int i = 0; i < pole.GetLength(0); i++)
            {
                for (int j = 0; j < pole.GetLength(1); j++)
                {
                    if (pole[i, j] == 0 || pole[i, j] == -1)
                    {
                        pole[i, j] = pridat ? -1 : 0;
                        g.FillEllipse(pridat ? Brushes.Yellow : Brushes.Black, i * 50 + 20, j * 50 + 20, 10, 10);
                    }
                }
            }
            Refresh();
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
            if (ModEditu == Mode.EditGhosts)
            {
                int x = e.X / 50;
                int y = e.Y / 50;

                if (x > btm.Width / 50 - 1 || y > btm.Height / 50 - 1 || x < 0 || y < 0)
                    return;

                if (pole[x, y] != 0)
                    return;

                pole[x, y] = 3;
                PictureBox Pbox = new PictureBox();
                Pbox.Size = new Size(50, 50);
                Pbox.Location = new Point(x * 50, y * 50);
                Pbox.Image = Image.FromFile("../../../grafika/ghost.png");
                Pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                Pbox.BackColor = Color.Transparent;
                Pbox.Tag = "ghost";
                Controls.Add(Pbox);
                Pbox.Click += PboxClick;
            }
            else if (ModEditu == Mode.EditPacMan)
            {
                if (PacmanExists)
                    return;
                else
                    PacmanExists = true;
                int x = e.X / 50;
                int y = e.Y / 50;

                if (x > btm.Width / 50 - 1 || y > btm.Height / 50 - 1 || x < 0 || y < 0)
                    return;

                if (pole[x, y] != 0)
                    return;

                pole[x, y] = 2;
                PictureBox Pbox = new PictureBox();
                Pbox.Size = new Size(50, 50);
                Pbox.Location = new Point(x * 50, y * 50);
                Pbox.Image = Image.FromFile("../../../grafika/pacman.gif");
                Pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                Pbox.BackColor = Color.Transparent;
                Pbox.Tag = "Pacman";
                Controls.Add(Pbox);
                Pbox.Click += PboxClick;
            }
        }

        private void PboxClick(object? sender, EventArgs e)
        {
            PictureBox Pbox = (PictureBox)sender;
            if (ModEditu == Mode.EditGhosts && Pbox.Tag == "ghost")
            {
                int x = Pbox.Location.X / 50;
                int y = Pbox.Location.Y / 50;
                pole[x, y] = 0;
                Controls.Remove(Pbox);
                GC.Collect();
            }
            else if (ModEditu == Mode.EditPacMan && Pbox.Tag == "Pacman")
            {
                PacmanExists = false;
                int x = Pbox.Location.X / 50;
                int y = Pbox.Location.Y / 50;
                pole[x, y] = 0;
                Controls.Remove(Pbox);
                GC.Collect();
            }
        }

        public void saveLevel(int[,] pole)
        {
            if (!PacmanExists)
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }
            using (var Ulozeni = new SaveMap(parent))
            {
                var result = Ulozeni.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (Ulozeni.JmenoMapy == "")
                        return;
                    List<int> list = new List<int>();
                    foreach (int item in pole)
                    {
                        list.Add(item);
                    }
                    XmlSerializer serializer = new XmlSerializer(typeof(List<int>));

                    using (FileStream fs = new FileStream($"..//..//..//Maps//{Ulozeni.JmenoMapy}.xml", FileMode.Create))
                    {
                        serializer.Serialize(fs, list);
                    }
                    
                    EditorOptions.Close();
                    EditorOptions.Dispose();
                    Close();
                    Dispose();
                }
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
                    else if (pole[i, y] == 2)
                    {
                        PictureBox Pbox = new PictureBox();
                        Pbox.Size = new Size(50, 50);
                        Pbox.Location = new Point(i * 50, y * 50);
                        Pbox.Image = Image.FromFile("../../../grafika/pacman.gif");
                        Pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                        Pbox.BackColor = Color.Transparent;
                        Pbox.Tag = "Pacman";
                        Controls.Add(Pbox);
                        Pbox.Click += PboxClick;
                    }
                    else if (pole[i, y] == 3)
                    {
                        PictureBox Pbox = new PictureBox();
                        Pbox.Size = new Size(50, 50);
                        Pbox.Location = new Point(i * 50, y * 50);
                        Pbox.Image = Image.FromFile("../../../grafika/ghost.png");
                        Pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                        Pbox.BackColor = Color.Transparent;
                        Pbox.Tag = "ghost";
                        Controls.Add(Pbox);
                        Pbox.Click += PboxClick;
                    }
                    else if (pole[i, y] == -1)
                    {
                        pole[i, y] = -1;
                        g.FillEllipse(new SolidBrush(Color.Gold), i * 50 + 20, y * 50 + 20, 10, 10);
                    }
                    p++;
                }
            }
        }

        private void LevelEditor_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / 50;
            int y = e.Y / 50;
            
            if (pole[x, y] == 1 || pole[x, y] == -1)
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
            if(!(ModEditu == Mode.EditWalls || ModEditu == Mode.EditCoin)) return;
            if (HoldingMouse)
            {
                if(modEditu == Mode.EditWalls)
                {
                    int x = e.X / 50;
                    int y = e.Y / 50;

                    if (x > btm.Width / 50 - 1 || y > btm.Height / 50 - 1 || x < 0 || y < 0)
                        return;

                    if (!(pole[x, y] == 0 || pole[x, y] == 1))
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
                else if (modEditu == Mode.EditCoin)
                {
                    int x = e.X / 50;
                    int y = e.Y / 50;

                    if (x > btm.Width / 50 - 1 || y > btm.Height / 50 - 1 || x < 0 || y < 0)
                        return;

                    if (!(pole[x, y] == 0 || pole[x, y] == -1))
                        return;


                    if (KliknutoNaZed)
                    {
                        pole[x, y] = 0;
                        g.FillEllipse(Brushes.Black, x * 50 + 20, y * 50 + 20, 10, 10);
                    }
                    else
                    {
                        pole[x, y] = -1;
                        g.FillEllipse(Brushes.Yellow, x * 50 + 20, y * 50 + 20, 10, 10);
                    }

                    this.Invalidate();
                }
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
            ReturnBtn,
            SaveBtn,
            EditCoins,
            EditCoin,
            RemoveCoins
        }
    }
}
