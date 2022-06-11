﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PAC_MAN
{
    delegate void NastavPolohu();
    internal class ghost : PictureBox
    {
        public int x;
        public int y;
        Smer smer;
        game parent;
        int[,] map;
        public System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        
        public ghost(int x, int y, int[,] map, game parent)
        {
            this.x = x;
            this.y = y;
            this.map = map;
            this.parent = parent;
            this.Width = 50;
            this.Height = 50;
            this.Image = Image.FromFile("../../../grafika/ghost.png");
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Location = new Point(x * 50, y * 50);
            timer.Tick += tick;
            timer.Interval = 1;
            smer = VybratSmer();
            timer.Start();


        }

        private void tick(object? sender, EventArgs e)
        {

            if (this.Location.X % 50 == 0 && this.Location.Y % 50 == 0)
            {
                x = this.Location.X / 50;
                y = this.Location.Y / 50;
                smer = VybratSmer();
            }
            switch (smer)
            {
                case Smer.Doleva:
                    this.Location = new Point(this.Location.X-2, this.Location.Y) ;
                    break;
                case Smer.Nahoru:
                    this.Location = new Point(this.Location.X, this.Location.Y-2);
                    break;
                case Smer.Doprava:
                    this.Location = new Point(this.Location.X + 2, this.Location.Y);
                    break;
                case Smer.Dolu:
                    this.Location = new Point(this.Location.X, this.Location.Y+2);
                    break;
            }
            if (this.Bounds.IntersectsWith(new Rectangle(parent.Pacman.x * 50, parent.Pacman.y * 50, 50, 50)) && parent.GameOver == false)
            {
                parent.GameOver = true;
                timer.Stop();
                //MessageBox.Show("umřel jsi");
            }
        }

        public Smer VybratSmer()
        {
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
                nahoru = map[this.x , this.y - 1] == 0 || map[this.x, this.y - 1] == -1 ? true : false;
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

        public enum Smer
        {
            Doleva,
            Nahoru,
            Doprava,
            Dolu
        }
    }
}
