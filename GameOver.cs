namespace PAC_MAN.Maps
{
    public partial class GameOver : Form
    {
        private Form parent;
        public GameOver(Form parent)
        {
            this.parent = parent;
            InitializeComponent();
            this.Load += GameOver_Load;
            
        }

        private void GameOver_Load(object? sender, EventArgs e) => this.Location = new Point(parent.Location.X + parent.Width / 2 - this.Width / 2, parent.Location.Y + parent.Height / 2 - this.Height / 2);

        private void GameOver_Paint(object? sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 5);
            e.Graphics.DrawLine(pen, 0, 0, this.Width, 0);
            e.Graphics.DrawLine(pen, 0, 0, 0, this.Height);
            e.Graphics.DrawLine(pen, 0, this.Height, this.Width, this.Height);
            e.Graphics.DrawLine(pen, this.Width, 0, this.Width, this.Height);
        }

        private void button1_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            Label Sender = (Label) sender;
            Pen pen = new Pen(Color.Blue, 5);
            e.Graphics.DrawLine(pen, 0, 0, Sender.Width, 0);
            e.Graphics.DrawLine(pen, 0, Sender.Height, Sender.Width, Sender.Height);
            e.Graphics.DrawLine(pen, 0, 0, 0, Sender.Height);
            e.Graphics.DrawLine(pen, Sender.Width, 0, Sender.Width, Sender.Height);
        }

        private void button2_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.Cancel;
    }
}
