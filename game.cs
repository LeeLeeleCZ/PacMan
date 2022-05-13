using Gma.System.MouseKeyHook;
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

namespace PAC_MAN
{
    public partial class game : Form
    {
        private IKeyboardMouseEvents m_GlobalHook;
        public Bitmap btm;
        bool isMoving = false;
        int[,] map;
        List<Label> listLabelu = new List<Label>();
        public event DataSentHandler DataSent;
        public int score = 0;
        //public int PacmanX = 0;
        //public int PacmanY = 0;
        Ghost ghost;
        //List<Ghost> listDuchu = new List<Ghost>();
        Graphics g;
        public PacMan pacman;
        string mapName;
        public bool GameOver = false;
        public int pocetGoldu;

        private static void OnInit()
        {
            while (false)
            {
                
            }
            
        }

        public game(string mapName)
        {
            this.mapName = mapName;
            InitializeComponent();
            btm = new Bitmap(this.Width, this.Height);
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            map = new int[this.Width / 50, this.Height / 50];
            NacistMapu();
            //nakreslitGrid();
            //PacManPictureBox.Image = Image.FromFile("pacman.gif");
            // make the image fit the picturebox
            g = Graphics.FromImage(btm);
            //PacManPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            m_GlobalHook = Hook.AppEvents();
            m_GlobalHook.KeyPress += GlobalHookKeyPress;

            pacman = new PacMan(0, 0, map, this);
            pacman.BringToFront();
            this.Controls.Add(pacman);

            ghost = new Ghost(map.GetLength(0)-1,map.GetLength(1)-1, map, this);
            //Ghost ghost2 = new Ghost(map.GetLength(0)-1, 0, map, this);
            
            //ghost2.BringToFront();
            ghost.BringToFront();
            //this.Controls.Add(ghost2);
            this.Controls.Add(ghost);
            
            //listDuchu.Add(ghost);
            //listDuchu.Add(ghost2);
            //timer1.Start();
        }

        private void Ghost_nastavPolohu(Point poloha)
        {
            ghost.Location = new System.Drawing.Point(ghost.x * 50, ghost.y * 50);
        }

        private void NacistMapu()
        {
            List<int> list = DeserializeXml();
            int p = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[i, y] = list[p];
                    p++;
                }
            }

            Graphics g = Graphics.FromImage(btm);
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == 1)
                    {
                        Label lbl = new Label();
                        lbl.Location = new Point(x * 50, y * 50);
                        lbl.Size = new Size(50, 50);
                        lbl.BackColor = Color.Black;
                        lbl.Paint += lblPaint;
                        listLabelu.Add(lbl);
                        this.Controls.Add(lbl);

                    }
                    else
                    {
                        //draw a gold coin on the map using graphics
                        map[x, y] = -1;
                        pocetGoldu++;
                        g.FillEllipse(new SolidBrush(Color.Gold), x * 50+20, y * 50+20, 10, 10);
                        
                    }
                    //g.FillRectangle(Brushes.White, x * 50, y * 50, 50, 50);
                }
            }
        }

        private void lblPaint(object? sender, PaintEventArgs e)
        {
            Label lbl = (Label)sender;

            //get lbl location in the map array
            int x = (lbl.Location.X / 50);
            int y = (lbl.Location.Y / 50);

            int wallLeft = 0;
            int wallTop = 0;
            int wallRight = 0;
            int wallBottom = 0;

            const int sirkaZdi = 8;

            try
            {
                if (map[x - 1, y] != 1)
                    wallLeft = sirkaZdi;
            }
            catch { }

            try
            {
                if (map[x + 1, y] != 1)
                    wallRight = sirkaZdi;
            }
            catch { }

            try
            {
                if (map[x, y - 1] != 1)
                    wallTop = sirkaZdi;
            }
            catch { }

            try
            {
                if (map[x, y + 1] != 1)
                    wallBottom = sirkaZdi;
            }
            catch { }

            try
            {
                ControlPaint.DrawBorder(e.Graphics, lbl.DisplayRectangle,
                Color.Blue, wallLeft, ButtonBorderStyle.Solid,
                Color.Blue, wallTop, ButtonBorderStyle.Solid,
                Color.Blue, wallRight, ButtonBorderStyle.Solid,
                Color.Blue, wallBottom, ButtonBorderStyle.Solid
                );
            }
            catch { }
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


        private void GlobalHookKeyPress(object? sender, KeyPressEventArgs e)
        {
            switch(e.KeyChar)
            {
                case 'w':
                    if (pacman.pohybujeSe)
                        pacman.smer = PacMan.Smer.Nahoru;
                    else
                        pacman.Y--;
                    break;
                case 'a':
                    if (pacman.pohybujeSe)
                        pacman.smer = PacMan.Smer.Doleva;
                    else
                        pacman.X--;
                    break;
                case 's':
                    if (pacman.pohybujeSe)
                        pacman.smer = PacMan.Smer.Dolu;
                    else
                        pacman.Y++;
                    break;
                case 'd':
                    if (pacman.pohybujeSe)
                        pacman.smer = PacMan.Smer.Doprava;
                    else
                        pacman.X++;
                    break;
            }
        }

        void nakreslitGrid()
        {
            // draw a grid on the form, 50 pixels wide and 50 pixels high
            Graphics g = Graphics.FromImage(btm);
            for (int i = 0; i < this.Width; i += 50)
            {
                // horizontální linky
                g.DrawLine(Pens.White, i, 0, i, this.Height);
            }
            for (int i = 0; i < this.Height; i += 50)
            {
                // vertikální linky
                g.DrawLine(Pens.White, 0, i, this.Width, i);
            }
        }
        private void game_Paint(object sender, PaintEventArgs e)
        {
           e.Graphics.DrawImage(btm, 0, 0);   
        }

        private void game_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
    }
}
