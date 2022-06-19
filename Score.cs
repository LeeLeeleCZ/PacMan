using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAC_MAN
{
    public partial class Score : Form
    {
        public Score()
        {
            InitializeComponent();

            DataSet d = new DataSet();
            Nastavení.NaplnDataset(d, "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1");

            foreach (DataRow row in d.Tables[0].Rows)
            {
                comboBox1.Items.Add(row[0]);
            }
            button2.BackgroundImage = Image.FromFile("../../../grafika/return-icon-Red.png");
            button2.BackgroundImageLayout = ImageLayout.Stretch;

            comboBox1.SelectedIndex = 0;
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            /*DataGridView lbl = (DataGridView)sender;
            ControlPaint.DrawBorder(e.Graphics, lbl.ClientRectangle,
                Color.Blue, 2, ButtonBorderStyle.Solid,
                Color.Blue, 2, ButtonBorderStyle.Solid,
                Color.Blue, 2, ButtonBorderStyle.Solid,
                Color.Blue, 2, ButtonBorderStyle.Solid
            );
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tabulka = comboBox1.SelectedItem.ToString();
            DataSet d = new DataSet();
            Nastavení.NaplnDataset(d, "SELECT * FROM " + tabulka + " ORDER BY score DESC");

            dataGridView1.DataSource = d.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button2_Click(object sender, EventArgs e) => this.Close();
    }
}
