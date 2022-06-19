using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Timer = System.Windows.Forms.Timer;

namespace PAC_MAN
{
    public class pacman : PictureBox
    {
        //WaveOutEvent waveOutDevice = new WaveOut();
        //AudioFileReader audioFileReader = new AudioFileReader("../../../Zvuky/Pac-Man eating.mp3");
        //System.Media.SoundPlayer wplayer = new System.Media.SoundPlayer(@"c:\mywavfile.wav");

        public int lives = 3;
        public int score = 0;
        public int x = 0;
        public int y = 0;
        Graphics g;
        public bool deathCooldown = false;

        public bool DeathCooldown
        {
            get
            {
                return deathCooldown;
            }
            set
            {
                deathCooldown = true;
                var deathTimer = new Timer();
                deathTimer.Interval = 250;
                int delkaTimeru = 0;
                bool nepruhledny = false;
                deathTimer.Tick += (s, e) =>
                {
                    this.Image = nepruhledny ? aktualniImage : SetAlpha((Bitmap)aktualniImage, 50);
                    nepruhledny = !nepruhledny;
                    delkaTimeru++;

                    if (delkaTimeru == 12)
                    {
                        deathCooldown = false;
                        this.Image = aktualniImage;
                        deathTimer.Stop();
                    }
                };
                deathTimer.Start();
            }
        }


        public int X { 
            get { return x; } 
            set
            {
                x = value;
            } 
        }
        
        public int Y { 
            get { return y; }
            set
            {
                y = value;
            }
        }

        private int[,] map;
        private game Parent;
        private Smer _smer = Smer.Nikam;
        private Image aktualniImage = Image.FromFile("../../../grafika/pacman.gif");

        private Smer smer
        {
            get { return _smer; }
            set
            {
                _smer = value;

                switch (value)
                {
                    case Smer.Doleva:
                        this.aktualniImage = Image.FromFile("../../../grafika/pacman(doleva).gif");
                        break;
                    case Smer.Doprava:
                        this.aktualniImage = Image.FromFile("../../../grafika/pacman.gif");
                        break;
                    case Smer.Nahoru:
                        this.aktualniImage = Image.FromFile("../../../grafika/pacman(nahoru).gif");
                        break;
                    case Smer.Dolu:
                        this.aktualniImage = Image.FromFile("../../../grafika/pacman(dolu).gif");
                        break;
                }

                this.Image = aktualniImage;
            }
        }
        private Smer budouciSmer = Smer.Nikam;
        public Smer BudouciSmer
        {
            get { return budouciSmer; }
            set
            {
                if (smer == Smer.Nikam)
                    smer = value;
                else
                {
                    /*
                    switch (value)
                    {
                        case Smer.Doleva:
                            if (smer == Smer.Doprava)
                                smer = value;
                            else
                                budouciSmer = value;
                            break;
                        case Smer.Doprava:
                            if (smer == Smer.Doleva)
                                smer = value;
                            else
                                budouciSmer = value;
                            break;
                        case 
                            Smer.Nahoru:
                            if (smer == Smer.Dolu)
                                smer = value;
                            else
                                budouciSmer = value;
                            break;
                        case Smer.Dolu:
                            if (smer == Smer.Nahoru)
                                smer = value;
                            else
                                budouciSmer = value;
                            break;

                    }*/
                    budouciSmer = value;
                }
            }
        }
        public Timer timer = new Timer();

        public pacman(int x, int y, int[,] map, game parent)
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
            /*waveOutDevice.Init(audioFileReader);

            waveOutDevice.PlaybackStopped += (sender, args) =>
            {
                ZvukHraje = false;
                waveOutDevice.Stop();
                
                //audioFileReader.Dispose();
                waveOutDevice.Dispose();

            };*/
            timer.Interval = 1;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private bool ZvukHraje = false;


        private void timer_Tick(object? sender, EventArgs e)
        {
            if (this.Location.X % 50 == 0 && this.Location.Y % 50 == 0)
            {
                x = this.Location.X / 50;
                y = this.Location.Y / 50;
                if(!MuzeDoSmeru(BudouciSmer))
                    budouciSmer = Smer.Nikam;
                if (BudouciSmer != null && BudouciSmer != Smer.Nikam)
                    smer = BudouciSmer;
            }
            if (smer == Smer.Nikam) return;
            switch (smer)
            {
                case Smer.Doleva:
                    if (MuzeDoSmeru(Smer.Doleva))
                    {
                        SebratCoin();
                        this.Location = new Point(this.Location.X - 2, this.Location.Y);
                        return;
                    }
                    else
                        smer = Smer.Nikam;
                    
                    break;
                case Smer.Nahoru:
                    if (MuzeDoSmeru(Smer.Nahoru))
                    {
                        SebratCoin();
                        this.Location = new Point(this.Location.X, this.Location.Y - 2);
                        return;
                    }
                    else
                        smer = Smer.Nikam;
                    
                    break;
                case Smer.Doprava:
                    if (MuzeDoSmeru(Smer.Doprava))
                    {
                        SebratCoin();
                        this.Location = new Point(this.Location.X + 2, this.Location.Y);
                        return;
                    }
                    else
                        smer = Smer.Nikam;
                    
                    break;
                case Smer.Dolu:
                    if (MuzeDoSmeru(Smer.Dolu))
                    {
                        SebratCoin();
                        this.Location = new Point(this.Location.X, this.Location.Y + 2);
                        return;
                    }
                    else
                        smer = Smer.Nikam;
                    
                    break;
            }
            GC.Collect();
        }

        private void SebratCoin()
        {
            if (this.map[this.X, this.Y] == -1)
            {
                if (Nastavení.Zvuk)
                {
                    if (!ZvukHraje)
                    {
                        ZvukHraje = true;
                        AudioFileReader reader = new AudioFileReader("../../../Zvuky/Pac-Man eating.mp3");
                        var headphones = new WaveOutEvent();
                        headphones.PlaybackStopped += (s, e) =>
                        {
                            headphones.Dispose();
                            reader.Dispose();
                            reader = null;
                            headphones = null;
                        };
                        headphones.Init(reader);
                        headphones.Play();
                    }
                }
                this.map[this.X, this.Y] = 0;
                Parent.PocetGoldu--;
                score+=100;
                Graphics g = Graphics.FromImage(Parent.btm);
                g.FillEllipse(new SolidBrush(Color.Black), x * 50 + 20, y * 50 + 20, 10, 10);
                ZvukHraje = false;
            }
        }

        private bool MuzeDoSmeru(Smer smer_)
        {
            switch (smer_)
            {
                case Smer.Doleva:
                    try
                    {
                        if (this.map[this.X - 1, this.Y] != 1) return true;
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                case Smer.Nahoru:
                    try
                    {
                        if (this.map[this.X, this.Y - 1] != 1) return true;
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                case Smer.Doprava:
                    try
                    {
                        if (this.map[this.X + 1, this.Y] != 1) return true;
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                case Smer.Dolu:
                    try
                    {
                        if (this.map[this.X, this.Y + 1] != 1) return true;
                    }
                    catch
                    {
                        return false;
                    }
                    break;
            }
            return false;
        }

        public enum Smer
        {
            Doleva,
            Nahoru,
            Doprava,
            Dolu,
            Nikam
        }

        static Bitmap SetAlpha(Bitmap bmpIn, int alpha)
        {
            Bitmap bmpOut = new Bitmap(bmpIn.Width, bmpIn.Height);
            float a = alpha / 255f;
            Rectangle r = new Rectangle(0, 0, bmpIn.Width, bmpIn.Height);

            float[][] matrixItems = {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, a, 0},
                new float[] {0, 0, 0, 0, 1}};

            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

            ImageAttributes imageAtt = new ImageAttributes();
            imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            using (Graphics g = Graphics.FromImage(bmpOut))
                g.DrawImage(bmpIn, r, r.X, r.Y, r.Width, r.Height, GraphicsUnit.Pixel, imageAtt);

            return bmpOut;
        }
    }
}
