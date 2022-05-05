using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC_MAN
{
    internal class PacMan : PictureBox
    {
        public event Action OnInit;
        private int x = 0;
        public int X { 
            get { return x; } 
            set {

                if (x < value)
                {
                    this.smer = Smer.Doprava;
                    pohybujeSe = true;
                    for (int i = 0; i < 25; i++)
                    {
                        this.Left += 2;
                        MainForm.wait(1);
                    }
                }
                else if (x > value)
                {
                    this.smer = Smer.Doleva;
                    pohybujeSe = true;
                    for (int i = 0; i < 25; i++)
                    {
                        this.Left -= 2;
                        MainForm.wait(1);
                    }
                }
                else if (x == value)
                {
                    return;
                }

                x = value;
                pohybujeSe = false;
            } 
        }
        public int y = 0;
        public int Y { 
            get { return y; }
            set
            {
                if (y < value)
                {
                    this.smer = Smer.Dolu;
                    pohybujeSe = true;
                    for (int i = 0; i < 25; i++)
                    {
                        this.Top += 2;
                        MainForm.wait(1);
                    }
                }
                else if (y > value)
                {
                    this.smer = Smer.Nahoru;
                    pohybujeSe = true;
                    for (int i = 0; i < 25; i++)
                    {
                        this.Top -= 2;
                        MainForm.wait(1);
                    }
                }
                else if (y == value)
                {
                    return;
                }

                y = value;
                pohybujeSe = false;
            }
        }
        private int[,] map;
        private game Parent;
        public bool pohybujeSe = false;

        public Smer _smer = Smer.Doprava;
        public Smer smer { 
            get { return _smer; } 
            set { 
                _smer = value;
                switch(value)
                {
                    case Smer.Doleva:
                        this.Image = Image.FromFile("pacman(doleva).gif");
                        break;
                    case Smer.Doprava:
                        this.Image = Image.FromFile("pacman.gif");
                        break;
                    case Smer.Nahoru:
                        this.Image = Image.FromFile("pacman(nahoru).gif");
                        break;
                    case Smer.Dolu:
                        this.Image = Image.FromFile("pacman(dolu).gif");
                        break;
                }
            } 
                    
        }
        
        public PacMan(int x, int y, int[,] map, Action callBack, game parent)
        {
            this.Height = 50;
            this.Width = 50;
            this.map = map;
            this.Parent = parent;
            this.Location = new System.Drawing.Point(x * 50, y * 50);
            this.x = x;
            this.y = y;
            this.Image = Image.FromFile("pacman.gif");
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.OnInit += callBack;
            if (OnInit != null) OnInit();
        }

        public enum Smer
        {
            Doleva,
            Nahoru,
            Doprava,
            Dolu
        }
    }
}
