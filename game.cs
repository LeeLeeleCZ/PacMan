using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using PAC_MAN.Maps;

namespace PAC_MAN
{
    public partial class game : Form
    {
        private IKeyboardMouseEvents m_GlobalHook;
        public Bitmap btm;
        bool isMoving = false;
        int[,] map;
        List<Label> listLabelu = new List<Label>();
        public int pocetGoldu;
        public int PocetGoldu
        {
            get { return pocetGoldu; }
            set
            {
                if (value == 0)
                {
                    m_GlobalHook.KeyPress -= GlobalHookKeyPress;
                    foreach (ghost g in listDuchu)
                        g.timer.Stop();
                    Pacman.timer.Stop();
                    m_GlobalHook.Dispose();
                    pocetGoldu = value;
                    DateTime dt = DateTime.Now;
                    using (var wingame = new WinGame(parent))
                    {
                        var result = wingame.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            DataSent(this, "Menu");
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            DataSent(this, $"Game;{mapName}");
                        }
                        else if (result == DialogResult.Continue)
                        {
                            wingame.Close();
                            using (var Name = new SaveMap(parent, true))
                            {
                                var result2 = Name.ShowDialog();
                                if (result2 == DialogResult.OK)
                                {
                                    SQLiteCommand command = new SQLiteCommand(Nastavení.m_dbConnection);
                                    TimeSpan ts = dt - startTime;
                                    command.CommandText = $@"INSERT INTO '{mapName}' (name, score, cas, zivoty) VALUES ('{Name.Jmeno}', '{Pacman.score}', '{ts.Minutes}:{ts.Seconds}', '{Pacman.lives}')";
                                    command.ExecuteNonQuery();
                                    this.Close();
                                }
                                else if (result2 == DialogResult.Cancel)
                                {
                                    Name.Close();
                                    DataSent(this, "Menu");
                                }
                            }
                        }
                    }
                }
                else 
                    pocetGoldu = value;
            }
        }
        public event DataSentHandler DataSent;
        List<dynamic> listDuchu = new List<dynamic>();
        Graphics g;
        public pacman Pacman;
        string mapName;
        private Form parent;
        private bool gameOver = false;
        private DateTime startTime;
        public bool GameOver
        {
            get => gameOver;
            set
            {
                if (value)
                {
                    m_GlobalHook.KeyPress -= GlobalHookKeyPress;
                    foreach (ghost g in listDuchu)
                        g.timer.Stop();
                    Pacman.timer.Stop();
                    m_GlobalHook.Dispose();
                    gameOver = value;
                    using (var gameOver = new GameOver(parent))
                    {
                        var result = gameOver.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            DataSent(this,"Menu");
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            DataSent(this, $"Game;{mapName}");
                        }
                    }
                }
                else
                    gameOver = value;
            }
        }

        public game(Form parent, string mapName)
        {
            this.mapName = mapName;
            this.parent = parent;
            parent.Visible = true;
            InitializeComponent();
            btm = new Bitmap(this.Width, this.Height);
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            map = new int[this.Width / 50, this.Height / 50];

            NacistMapu();
            parent.Visible = true;
            m_GlobalHook = Hook.AppEvents();
            m_GlobalHook.KeyPress += GlobalHookKeyPress;

            // save system time
            startTime = DateTime.Now;
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
                    else if (map[x, y] == 2)
                    {
                        Pacman = new pacman(x, y, map, this);
                        Pacman.BringToFront();
                        this.Controls.Add(Pacman);
                    }
                    else if (map[x, y] == 3)
                    {
                        var ghost = new ghost(x, y, map, this);
                        ghost.BringToFront();
                        this.Controls.Add(ghost);
                        listDuchu.Add(ghost);
                    }
                    else if (map[x, y] == 4)
                    {
                        var ghost = new TrackerGhost(x, y, map, this);
                        ghost.BringToFront();
                        this.Controls.Add(ghost);
                        listDuchu.Add(ghost);
                    }
                    else if (map[x, y] == -1)
                    {
                        map[x, y] = -1;
                        pocetGoldu++;
                        g.FillEllipse(new SolidBrush(Color.Gold), x * 50+20, y * 50+20, 10, 10);
                        
                    }
                }
            }
            this.Refresh();
        }

        private void lblPaint(object? sender, PaintEventArgs e)
        {
            Label lbl = (Label)sender;
            
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
            FileStream fs = new FileStream($"..//..//..//Maps//{mapName}.xml", FileMode.Open);
            List<int> list = (List<int>)serializer.Deserialize(fs);
            fs.Close();
            return list;
        }


        private void GlobalHookKeyPress(object? sender, KeyPressEventArgs e)
        {
            if (Nastavení.Controls)
            {
                switch (e.KeyChar)
                {
                    case 'w':
                        Pacman.BudouciSmer = pacman.Smer.Nahoru;
                        break;
                    case 'a':
                        Pacman.BudouciSmer = pacman.Smer.Doleva;
                        break;
                    case 's':
                        Pacman.BudouciSmer = pacman.Smer.Dolu;
                        break;
                    case 'd':
                        Pacman.BudouciSmer = pacman.Smer.Doprava;
                        break;
                    case (char)27:
                        m_GlobalHook.KeyPress -= GlobalHookKeyPress;
                        m_GlobalHook.Dispose();
                        Pacman.Dispose();
                        foreach (var item in listDuchu)
                        {
                            item.timer.Stop();
                            item.Dispose();
                        }
                        this.Close();
                        this.Dispose();
                        GC.Collect();
                        break;
                }
            }
            else
            {
                switch (e.KeyChar)
                {
                    case (char)38:
                        Pacman.BudouciSmer = pacman.Smer.Nahoru;
                        break;
                    case (char)37:
                        Pacman.BudouciSmer = pacman.Smer.Doleva;
                        break;
                    case (char)40:
                        Pacman.BudouciSmer = pacman.Smer.Dolu;
                        break;
                    case (char)39:
                        Pacman.BudouciSmer = pacman.Smer.Doprava;
                        break;
                    case (char)27:
                        m_GlobalHook.KeyPress -= GlobalHookKeyPress;
                        m_GlobalHook.Dispose();
                        Pacman.Dispose();
                        foreach (var item in listDuchu)
                        {
                            item.timer.Stop();
                            item.Dispose();
                        }
                        this.Close();
                        this.Dispose();
                        GC.Collect();
                        break;
                }
            }
        }
        
        
        private void game_Paint(object sender, PaintEventArgs e) => e.Graphics.DrawImage(btm, 0, 0);

        private void game_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void game_Load(object sender, EventArgs e)
        {
            
            
        }
    }
}
