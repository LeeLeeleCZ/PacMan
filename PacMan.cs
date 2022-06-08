using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Timer = System.Windows.Forms.Timer;

namespace PAC_MAN
{
    public class PacMan : PictureBox
    {
        IWavePlayer waveOutDevice = new WaveOut();
        AudioFileReader audioFileReader = new AudioFileReader("../../../Zvuky/Pac-Man eating.mp3");
        public int lives = 3;
        int score = 0;
        public int x = 0;
        public int y = 0;
        Graphics g;
        /*
        public int X { 
            get { return x; } 
            set {

                if (x < value)
                {
                    if (Parent.Width <= this.X * 50 + 50) return;
                    if (map[this.X + 1, this.Y] == 1) return;
                    if (map[this.X + 1, this.Y] == -1)
                    {
                        waveOutDevice.Stop();
                        waveOutDevice.Play();
                        score++;
                        Parent.pocetGoldu--;
                        map[this.X + 1, this.Y] = 0;
                        g.FillEllipse(new SolidBrush(Color.Black), this.X * 50 + 20 + 50, this.Y * 50 + 20, 10, 10);
                    }
                    this.smer = Smer.Doprava;
                    PohybujeSe = true;
                    this.Image = Image.FromFile("../../../grafika/pacman.gif");
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
                        waveOutDevice.Play();
                        score++;
                        Parent.pocetGoldu--;
                        map[this.X - 1, this.Y] = 0;
                        g.FillEllipse(new SolidBrush(Color.Black), this.X * 50 + 20 - 50, this.Y * 50 + 20, 10, 10);
                    }
                    this.smer = Smer.Doleva;
                    PohybujeSe = true;
                    this.Image = Image.FromFile("../../../grafika/pacman(doleva).gif");
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
                        waveOutDevice.Play();
                        score++;
                        Parent.pocetGoldu--;
                        map[this.X, this.Y + 1] = 0;
                        g.FillEllipse(new SolidBrush(Color.Black), this.X * 50 + 20, this.Y * 50 + 20 + 50, 10, 10);
                    }
                    this.smer = Smer.Dolu;
                    PohybujeSe = true;
                    this.Image = Image.FromFile("../../../grafika/pacman(dolu).gif");
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
                        waveOutDevice.Play();
                        score++;
                        Parent.pocetGoldu--;
                        map[this.X, this.Y - 1] = 0;
                        g.FillEllipse(new SolidBrush(Color.Black), this.X * 50 + 20, this.Y * 50 + 20 - 50, 10, 10);
                    }
                    this.smer = Smer.Nahoru;
                    PohybujeSe = true;
                    this.Image = Image.FromFile("../../../grafika/pacman(nahoru).gif");
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
        */

        private int[,] map;
        private game Parent;
        public bool pohybujeSe = false;
        public Smer _smer = Smer.Nikam;
        public Smer smer { get { return _smer; } set { _smer = value;}   }
        private Timer timer = new Timer();

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
            this.Image = Image.FromFile("../../../grafika/pacman.gif");
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            waveOutDevice.Init(audioFileReader);

            waveOutDevice.PlaybackStopped += (sender, args) =>
            {
                //waveOutDevice.Play();
                waveOutDevice.Stop();
                //audioFileReader.Dispose();
                //waveOutDevice.Dispose();
                
                //waveOutDevice.Init(audioFileReader);
            };
            timer.Interval = 1;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            if (smer == Smer.Nikam) return;
            switch (smer)
            {
                case Smer.Nahoru:
                    this.y--;
                    break;
                case Smer.Doprava:
                    this.x++;
                    break;
                case Smer.Dolu:
                    this.y++;
                    break;
                case Smer.Doleva:
                    this.x--;
                    break;
            }
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
