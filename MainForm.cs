using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Media;
using System.Runtime.InteropServices;
using System.Text.Json;
using Timer = System.Windows.Forms.Timer;
using NAudio;
using NAudio.Wave;

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
        AudioFileReader reader = new AudioFileReader("../../../Zvuky/PAC-MAN Theme.mp3");
        WaveOutEvent headphones = new WaveOutEvent();
        private bool HrajeHudba;
        public MainForm()
        {
            InitializeComponent();
            this.Icon = new Icon(@"../../../grafika/pacman.ico");
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

            headphones.PlaybackStopped += (sender, args) =>
            {
                headphones.Dispose();
                headphones = null;
            };
            

            if (Nastaven?.Hudba)
            {
                if (headphones == null)
                    headphones = new WaveOutEvent();
                if (reader == null)
                    reader = new AudioFileReader("../../../Zvuky/PAC-MAN Theme.mp3");
                headphones.Init(reader);
                headphones.Play();
                HrajeHudba = true;
            }

            Nastaven?.SettingsUpdate += () =>
            {
                if (Nastaven?.Hudba)
                {
                    if (!HrajeHudba)
                    {
                        if(headphones == null)
                            headphones = new WaveOutEvent();
                        if (reader == null)
                            reader = new AudioFileReader("../../../Zvuky/PAC-MAN Theme.mp3");
                        headphones.Init(reader);
                        headphones.Play();
                        HrajeHudba = true;
                    }
                }
                else
                {
                    if (HrajeHudba)
                    {
                        headphones.Stop();
                        reader.Dispose();
                        headphones.Dispose();
                        HrajeHudba = false;
                    }
                }
            };
        }

        private void OptionDataSent(Form? sender, string msg)
        {
            if(sender != null)
            {
                sender.Dispose();
                sender = null;
            }
            if (msg.Contains(';'))
            {
                var msgs = msg.Split(';');
                var game = new game(this, msgs[1]);
                otevritForm(game);
                game.DataSent += OptionDataSent;
                return;
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
                    MapSelector selector = new MapSelector();
                    selector.DataSent += (sender, msg) =>
                    {
                        if (sender != null)
                            sender.Close();
                        if (msg != null)
                        {
                            var game = new game(this, msg);
                            otevritForm(game);
                            game.DataSent += OptionDataSent;
                        }
                    };
                    otevritForm(selector);
                    break;
                case "LevelEditor":
                    var ChooseMap = new LevelEditorMapSelect();
                    ChooseMap.DataSent += (sender, msg) =>
                    {
                        if (sender != null)
                            sender.Close();
                        if (msg != null)
                        {
                            if (msg == "Nova")
                            {
                                var LevelEditor = new LevelEditor(this, null);
                                otevritForm(LevelEditor);
                                LevelEditor.DataSent += OptionDataSent;
                            }
                            else if (msg == "Nacist")
                            {
                                var editor = new MapSelector();
                                otevritForm(editor);
                                editor.DataSent += (s, e) =>
                                {
                                    if (sender != null)
                                        sender.Close();
                                    if (e != null)
                                    {
                                        var LevelEditor = new LevelEditor(this, e);
                                        otevritForm(LevelEditor);
                                        LevelEditor.DataSent += OptionDataSent;
                                    }
                                };
                            }
                        }
                    };
                    otevritForm(ChooseMap);
                    break;
                case "Settings":
                    var settings = new Settings();
                    
                    otevritForm(settings);
                    break;
                case "Score":
                    var score = new ScoreForm();
                    otevritForm(score);
                    break;
            }
        }

        private void btnMinimize_Click(object? sender, EventArgs e)
        {
            
            var timer1 = new Timer();
            timer1.Interval = 1;
            timer1.Tick += (s, e) =>
            {
                this.Opacity -= 0.04;
                if (this.Opacity == 0)
                {
                    timer1.Enabled = false;
                    timer1.Stop();
                    this.WindowState = FormWindowState.Minimized;
                    Opacity = 1;
                }
            };
            timer1.Enabled = true;
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            // posouvan? formu pomoci pretahovani panelu1
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e) => e.Graphics.DrawLine(Pens.Black, 0, TitleBar.Height - 1, TitleBar.Width, TitleBar.Height - 1);

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
    }
}