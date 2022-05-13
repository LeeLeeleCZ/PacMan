using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace PAC_MAN
{
    public partial class MainForm : Form
    {
        public event DataSentHandler DataSent;
        #region DLL_Imports
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        [DllImport("user32.dll")]
        static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        private const int AW_VER_POSITIVE = 0x00000004;
        private const int AW_VER_NEGATIVE = 0x00000008;
        private const int AW_SLIDE = 0x00040000;
        private const int AW_HIDE = 0x00010000;
        #endregion
        Form activeForm;
        public MainForm()
        {
            InitializeComponent();
            #region PridaniButtonu
            // add close button to panel1
            Button btnClose = new Button();
            btnClose.Size = new Size(39, 39);
            btnClose.Location = new Point(TitleBar.Width - btnClose.Width - 5, 5);
            btnClose.BackColor = Color.Red;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseDownBackColor = Color.DarkRed;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Click += new EventHandler(btnClose_Click);
            TitleBar.Controls.Add(btnClose);

            // add minimize button to panel1
            Button btnMinimize = new Button();
            btnMinimize.Size = new Size(39, 39);
            btnMinimize.Location = new Point(TitleBar.Width - btnMinimize.Width - btnClose.Width - 10, 5);
            btnMinimize.BackColor = Color.Green;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatAppearance.MouseDownBackColor = Color.DarkGreen;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.Click += new EventHandler(btnMinimize_Click);
            TitleBar.Controls.Add(btnMinimize);
            #endregion
            var menu = new Menu();
            otevritForm(menu);
            menu.DataSent += OptionDataSent;
            this.Move += MainForm_Move;

        }

        private void MainForm_Move(object? sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is LevelEditorOptions)
                {
                    LevelEditorOptions editor = (LevelEditorOptions)form;
                    // set editor location to be in center
                    editor.Location = new Point(this.Location.X + this.Width+25, this.Location.Y + this.Height / 2 - editor.Height / 2+25);
                    //editor.Location = new Point(this.Location.X + this.Width, this.Location.Y);
                }
            }
        }

        private void OptionDataSent(Form? sender, string msg)
        {
            if(sender != null)
            {
                sender.Dispose();
                sender = null;
            }  
            GC.Collect();
            switch (msg)
            {
                case "Menu":
                    var menu = new Menu();
                    otevritForm(menu);
                    menu.DataSent += OptionDataSent;
                    break;
                case "Game":
                    //input box which inputs string
                    string mapName = Microsoft.VisualBasic.Interaction.InputBox("Kterou mapu chcete naèíst?", "Naètení mapy", "level1");
                    var game = new game(mapName);
                    otevritForm(game);
                    game.DataSent += OptionDataSent;
                    break;
                case "LevelEditor":
                    var LevelEditor = new LevelEditor(this);
                    otevritForm(LevelEditor);
                    LevelEditor.DataSent += OptionDataSent;
                    break;
                case "Settings":
                    break;
            }
        }

        public static void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void btnMinimize_Click(object? sender, EventArgs e)
        {
            // animace pro minimalizaci
            for (int i = 0; i < 25; i++)
            {
                this.Opacity -= 0.04;
                wait(1);
            }

            this.WindowState = FormWindowState.Minimized;
            Opacity = 1;
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            // posouvaní formu pomoci pretahovani panelu1
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // draw a 1px height line at the bottom of the panel
            e.Graphics.DrawLine(Pens.Black, 0, TitleBar.Height - 1, TitleBar.Width, TitleBar.Height - 1);
        }

        public void otevritForm(Form childForm)
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Name == childForm.Name)
                {
                    childForm.Close();
                    childForm.Dispose();
                    childForm = Application.OpenForms[i];
                }
            }

            activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.None;
            childForm.Dock = DockStyle.Fill;
            zobrazeni.Controls.Add(childForm);
            zobrazeni.Tag = childForm;

            childForm.Visible = false;
            childForm.BringToFront();
            childForm.Show();
            childForm.Visible = true;
            GC.Collect();
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

    }
}