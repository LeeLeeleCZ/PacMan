using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace PAC_MAN
{
    public partial class MapSelector : Form
    {
        public event DataSentHandler DataSent;
        string mapName;
        int[,] map;
        public Bitmap btm;
        List<Label> listLabelu = new List<Label>();
        public int pocetGoldu;
        private Graphics g;
        private Button selectedbutton;
        public Button SelectedButton
        {
            get { return selectedbutton; }
            set
            {
                try
                {
                    if (selectedbutton != null)
                    {
                        selectedbutton.BackColor = Color.Black;
                        selectedbutton.ForeColor = Color.Blue;
                    }
                    selectedbutton = value;
                    selectedbutton.BackColor = Color.Blue;
                    selectedbutton.ForeColor = Color.White;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public MapSelector()
        {
            InitializeComponent();

            DirectoryInfo d = new DirectoryInfo(@"..//..//..//Maps");

            btm = new Bitmap(panel2.Width, panel2.Height);
            g = Graphics.FromImage(btm);
            map = new int[panel2.Width / 20, panel2.Height / 20];
            //put every file in the directory to a list

            FileInfo[] Files = d.GetFiles("*.xml");
            List<FileInfo> files = new List<FileInfo>();
            foreach (FileInfo file in Files)
            {
                if (file.Name != "default.xml")
                {
                    files.Add(file);
                }
                
            }
            
            int pocet = 0;

            /*for (int i = 0; i < files.Count; i++)
            {
                if(files[i].Name == "default.xml")
                    files.RemoveAt(i);
            }*/
            
            foreach (FileInfo item in files)
            {
                Button btn = new Button();
                btn.Text = item.Name.Substring(0, item.Name.Length - 4);
                btn.Click += Btn_Click;
                btn.Tag = item.FullName;
                btn.Font = new Font(DefaultBtn.Font.Name, 18, DefaultBtn.Font.Style);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 2;
                btn.FlatAppearance.BorderColor = Color.Blue;
                btn.Margin = new Padding(1);
                btn.Size = new Size(485, 50);
                btn.ForeColor = Color.Blue;
                btn.Location = new Point(1, pocet * (btn.Height + 10));
                panel1.Controls.Add(btn);
                pocet++;
            }
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
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[i, y] = list[p];
                    p++;
                }
            }
            btm = new Bitmap(panel2.Width, panel2.Height);
            Graphics g = Graphics.FromImage(btm);
            panel2.Refresh();

            //replace all 0 in map with -1
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[i, y] == 0)
                    {
                        map[i, y] = -1;
                    }
                }
            }

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[i, y] == 1)
                    {
                        g.FillRectangle(Brushes.Blue, i * 20, y * 20, 20, 20);
                    }
                    else if (map[i, y] == -1)
                    {
                        
                        g.FillEllipse(new SolidBrush(Color.Gold), i * 20+8, y * 20+8, 4, 4);

                    }
                }
            }

            panel2.Invalidate();
            GC.Collect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSent(null, null);
            this.Close();
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Red, 2);
            e.Graphics.DrawLine(pen, 0, 0, button1.Width, button1.Height);
            e.Graphics.DrawLine(pen, button1.Width, 0, 0, button1.Height);
        }

        private void Btn_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            SelectedButton = (Button) sender;
            mapName = btn.Text == DefaultBtn.Text ? "default" : SelectedButton.Text;
            NacistMapu();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 2);
            e.Graphics.DrawRectangle(pen, 0, 0, panel2.Width - 1, panel2.Height - 1);
            e.Graphics.DrawImage(btm, 0, 0, panel2.Width, panel2.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(selectedbutton != null)
                DataSent(this, mapName);
            //this.Close();
        }
    }
}
