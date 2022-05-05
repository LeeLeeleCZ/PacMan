using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAC_MAN
{
    public partial class Nastaveni : Form
    {
        public event DataSentHandler DataSent;
        public Nastaveni()
        {
            InitializeComponent();
        }
    }
}
