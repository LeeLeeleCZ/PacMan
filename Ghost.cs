using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PAC_MAN
{
    delegate void NastavPolohu();
    internal class Ghost : PictureBox
    {
        //public event NastavPolohu nastavPolohu;
        public Smer smer;
        public int x;
        public int y;
        Thread thread;
        int[,] map;
        bool pohybujeSe = false;
        game parent;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public Ghost(int x, int y, int[,] map, game parent)
        {
            parent = parent;
            this.map = map;
            this.x = x;
            this.y = y;
            this.Location = new System.Drawing.Point(x*50, y*50);
            this.Image = Image.FromFile("ghost.png");
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Height = 50;
            this.Width = 50;
            timer.Tick += TimerTick;
            timer.Interval = 10;
            timer.Enabled = true;

            thread = new Thread(new ThreadStart(this.Pohyb));
            thread.Start();

            bool doleva = false;
            bool nahoru = false;
            bool doprava = false;
            bool dolu = false;

            try
            {
                doleva = map[this.x - 1, this.y] == 0 || map[this.x - 1, this.y] == -1 ? true : false;
            }
            catch { }

            try
            {
                nahoru = map[this.x, this.y - 1] == 0 || map[this.x, this.y - 1] == -1 ? true : false;
            }
            catch { }

            try
            {
                doprava = map[this.x + 1, this.y] == 0 || map[this.x + 1, this.y] == -1 ? true : false;
            }
            catch { }

            try
            {
                dolu = map[this.x, this.y + 1] == 0 || map[this.x, this.y + 1] == -1 ? true : false;
            }
            catch { }

            this.smer = this.VybratSmer(doleva, nahoru, doprava, dolu);
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            zobraz();
        }

        void zobraz()
        {
            this.Location = new System.Drawing.Point(this.x * 50, this.y * 50);
            //MainForm.wait(1000);
            //if (this.x == parent.PacmanX && this.y == parent.PacmanY)
            //{
            //    MessageBox.Show("Prohráli jste");
            //    Environment.Exit(0);
            //}
            
            pohybujeSe = false;
        }

        public void Pohyb()
        {
            while (true)
            {
                if(!pohybujeSe)
                {
                    switch (this.smer)
                    {
                        case Smer.Nahoru:
                            this.y--;
                            // move the picturebox 50px from top
                            //this.Location = new System.Drawing.Point(this.x, this.y + 50);
                            //for (int i = 0; i < 25; i++)
                            //{
                            //    this.Top -= 2;
                            //    MainForm.wait(1);
                            //}
                            //NastavPolohu d = NastavPolohu(zobraz);
                            MainForm.wait(500);
                            break;
                        case Smer.Dolu:
                            //for (int i = 0; i < 25; i++)
                            //{
                            //    this.Top += 2;
                            //    MainForm.wait(1);
                            //}
                            this.y++;
                            MainForm.wait(500);
                            break;
                        case Smer.Doleva:
                            //for (int i = 0; i < 25; i++)
                            //{
                            //    this.Left -= 2;
                            //    MainForm.wait(1);
                            //}
                            this.x--;
                            MainForm.wait(500);
                            break;
                        case Smer.Doprava:
                            //for (int i = 0; i < 25; i++)
                            //{
                            //    this.Left += 2;
                            //    MainForm.wait(1);
                            //}
                            this.x++;
                            MainForm.wait(500);
                            break;
                    }

                    bool doleva = false;
                    bool nahoru = false;
                    bool doprava = false;
                    bool dolu = false;

                    try
                    {
                        doleva = map[this.x - 1, this.y] == 0 || map[this.x - 1, this.y] == -1 ? true : false;
                    }
                    catch { }

                    try
                    {
                        nahoru = map[this.x, this.y - 1] == 0 || map[this.x, this.y - 1] == -1 ? true : false;
                    }
                    catch { }

                    try
                    {
                        doprava = map[this.x + 1, this.y] == 0 || map[this.x + 1, this.y] == -1 ? true : false;
                    }
                    catch { }

                    try
                    {
                        dolu = map[this.x, this.y + 1] == 0 || map[this.x, this.y + 1] == -1 ? true : false;
                    }
                    catch { }

                    this.smer = this.VybratSmer(doleva, nahoru, doprava, dolu);
                    pohybujeSe = true;
                }
            }
        }

        public enum Smer
        {
            Doleva,
            Nahoru,
            Doprava,
            Dolu
        }

        public Smer VybratSmer(bool doleva, bool nahoru, bool doprava, bool dolu)
        {

            List<Smer> mozneSmery = new List<Smer>();
            if (doleva && this.smer != Smer.Doprava)
                mozneSmery.Add(Smer.Doleva);
            if (nahoru && this.smer != Smer.Dolu)
                mozneSmery.Add(Smer.Nahoru);
            if (doprava && this.smer != Smer.Doleva)
                mozneSmery.Add(Smer.Doprava);
            if (dolu && this.smer != Smer.Nahoru)
                mozneSmery.Add(Smer.Dolu);
            
            if (mozneSmery.Count == 0)
            {
                switch (smer)
                {
                    case Smer.Doleva:
                        return Smer.Doprava;
                        break;
                    case Smer.Nahoru:
                        return Smer.Dolu;
                        break;
                    case Smer.Doprava:
                        return Smer.Doleva;
                        break;
                    case Smer.Dolu:
                        return Smer.Nahoru;
                        break;
                        
                }
            }
            else
            {
                Random rnd = new Random();
                int index = rnd.Next(mozneSmery.Count);
                return mozneSmery[index];
            }

            return smer;
        }
    }
}
