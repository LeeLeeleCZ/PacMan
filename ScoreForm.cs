using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAC_MAN
{
    public partial class ScoreForm : Form
    {
        public ScoreForm()
        {
            InitializeComponent();

            CloseBtn.BackgroundImage = Image.FromFile("../../../grafika/return-icon-Red.png");
            CloseBtn.BackgroundImageLayout = ImageLayout.Stretch;

            NactiComboBox();
            comboBox1.SelectedIndex = 0;
            NacistData();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void label1_Paint(object sender, PaintEventArgs e) => e.Graphics.DrawLine(Pens.Blue, 0, label1.Height - 1, label1.Width, label1.Height - 1);

        private void button2_Click(object sender, EventArgs e) => this.Close();

        private void NacistData()
        {
            string tabulka;
            tabulka = comboBox1.SelectedItem.ToString();

            if (tabulka == null)
                return;
            DataSet d = new DataSet();
            Nastavení.NaplnDataset(d, $"SELECT * FROM '{tabulka}' ORDER BY score DESC");

            dataGridView1.DataSource = d.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void NactiComboBox()
        {
            using DataSet d = new DataSet();
            Nastavení.NaplnDataset(d, "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1");

            foreach (DataRow row in d.Tables[0].Rows)
                comboBox1.Items.Add(row[0]);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) => NacistData();
    }
}
