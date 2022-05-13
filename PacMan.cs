using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC_MAN
{
    public class PacMan : PictureBox
    {
        public int lives = 3;
        int score = 0;
        private int x = 0;
        Graphics g;
        public int X { 
            get { return x; } 
            set {

                if (x < value)
                {
                    if (Parent.Width <= this.X * 50 + 50) return;
                    if (map[this.X + 1, this.Y] == 1) return;
                    if (map[this.X + 1, this.Y] == -1)
                    {
                        score++;
                        Parent.pocetGoldu--;
                        map[this.X + 1, this.Y] = 0;
                        g.FillEllipse(new SolidBrush(Color.Black), this.X * 50 + 20 + 50, this.Y * 50 + 20, 10, 10);
                    }
                    this.smer = Smer.Doprava;
                    PohybujeSe = true;
                    this.Image = Image.FromFile("pacman.gif");
                    for (int i = 0; i < 25; i++)
                    {
                        this.Left += 2;
                        MainForm.wait(1);
                    }
                    x = value;
                }
                else if (x > value)
                {
                    if (this.X <= 0) return;
                    if (map[this.X - 1, this.Y] == 1) return;
                    if (map[this.X - 1, this.Y] == -1)
                    {
                        score++;
                        Parent.pocetGoldu--;
                        map[this.X - 1, this.Y] = 0;
                        g.FillEllipse(new SolidBrush(Color.Black), this.X * 50 + 20 - 50, this.Y * 50 + 20, 10, 10);
                    }
                    this.smer = Smer.Doleva;
                    PohybujeSe = true;
                    this.Image = Image.FromFile("pacman(doleva).gif");
                    for (int i = 0; i < 25; i++)
                    {
                        this.Left -= 2;
                        MainForm.wait(1);
                    }
                    x = value;
                }
                else if (x == value)
                {
                    return;
                }

                
                PohybujeSe = false;
                GC.Collect();
            } 
        }
        public int y = 0;
        public int Y { 
            get { return y; }
            set
            {
                if (y < value)
                {
                    if (Parent.Height <= this.Y * 50 + 50) return;
                    if (map[this.X, this.Y + 1] == 1) return;
                    if (map[this.X, this.Y + 1] == -1)
                    {
                        score++;
                        Parent.pocetGoldu--;
                        map[this.X, this.Y + 1] = 0;
                        g.FillEllipse(new SolidBrush(Color.Black), this.X * 50 + 20, this.Y * 50 + 20 + 50, 10, 10);
                    }
                    this.smer = Smer.Dolu;
                    PohybujeSe = true;
                    this.Image = Image.FromFile("pacman(dolu).gif");
                    for (int i = 0; i < 25; i++)
                    {
                        this.Top += 2;
                        MainForm.wait(1);
                    }
                    y = value;
                }
                else if (y > value)
                {
                    if (this.Y <= 0) return;
                    if (map[this.X, this.Y - 1] == 1) return;
                    if (map[this.X, this.Y - 1] == -1)
                    {
                        score++;
                        Parent.pocetGoldu--;
                        map[this.X, this.Y - 1] = 0;
                        g.FillEllipse(new SolidBrush(Color.Black), this.X * 50 + 20, this.Y * 50 + 20 - 50, 10, 10);
                    }
                    this.smer = Smer.Nahoru;
                    PohybujeSe = true;
                    this.Image = Image.FromFile("pacman(nahoru).gif");
                    for (int i = 0; i < 25; i++)
                    {
                        this.Top -= 2;
                        MainForm.wait(1);
                    }
                    y = value;
                }
                else if (y == value)
                {
                    return;
                }

                
                PohybujeSe = false;
                GC.Collect();
            }
        }
        private int[,] map;
        private game Parent;
        public bool pohybujeSe = false;

        bool PohybujeSe {
            get { return pohybujeSe; }
            set { pohybujeSe = value;
                if (!value)
                {
                    switch (smer)
                    {
                        case Smer.Doleva:
                            this.X--;
                            break;
                        case Smer.Doprava:
                            this.X++;
                            break;
                        case Smer.Nahoru:
                            this.Y--;
                            break;
                        case Smer.Dolu:
                            this.Y++;
                            break;
                    }
                }

            }
        }

        public Smer _smer = Smer.Nikam;
        public Smer smer { get { return _smer; } set { _smer = value;}   }
        
        public PacMan(int x, int y, int[,] map, game parent)
        {
            g = Graphics.FromImage(parent.btm);
            this.Height = 50;
            this.Width = 50;
            this.map = map;
            this.Parent = parent;
            this.Location = new System.Drawing.Point(x * 50, y * 50);
            this.x = x;
            this.y = y;
            this.Image = Image.FromFile("pacman.gif");
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public enum Smer
        {
            Doleva,
            Nahoru,
            Doprava,
            Dolu,
            Nikam
        }
    }
}
