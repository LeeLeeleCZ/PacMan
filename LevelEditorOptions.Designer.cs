namespace PAC_MAN
{
    partial class LevelEditorOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.returnBtn = new System.Windows.Forms.Button();
            this.editGhostsBtn = new System.Windows.Forms.Button();
            this.editPacManBtn = new System.Windows.Forms.Button();
            this.editWallsBtn = new System.Windows.Forms.Button();
            this.Ghost1Btn = new System.Windows.Forms.Button();
            this.Ghost2Btn = new System.Windows.Forms.Button();
            this.Ghost3Btn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.editCoinBtn = new System.Windows.Forms.Button();
            this.editCoinsBtn = new System.Windows.Forms.Button();
            this.removeCoinsBtn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // returnBtn
            // 
            this.returnBtn.BackColor = System.Drawing.Color.Black;
            this.returnBtn.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.returnBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.returnBtn.Location = new System.Drawing.Point(0, 275);
            this.returnBtn.Name = "returnBtn";
            this.returnBtn.Size = new System.Drawing.Size(50, 50);
            this.returnBtn.TabIndex = 7;
            this.returnBtn.UseVisualStyleBackColor = false;
            this.returnBtn.Click += new System.EventHandler(this.button1_Click);
            this.returnBtn.Paint += new System.Windows.Forms.PaintEventHandler(this.returnBtn_Paint);
            // 
            // editGhostsBtn
            // 
            this.editGhostsBtn.BackColor = System.Drawing.Color.DarkGray;
            this.editGhostsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editGhostsBtn.Location = new System.Drawing.Point(0, 55);
            this.editGhostsBtn.Name = "editGhostsBtn";
            this.editGhostsBtn.Size = new System.Drawing.Size(50, 50);
            this.editGhostsBtn.TabIndex = 6;
            this.editGhostsBtn.UseVisualStyleBackColor = false;
            this.editGhostsBtn.Click += new System.EventHandler(this.button1_Click);
            this.editGhostsBtn.MouseHover += new System.EventHandler(this.LevelEditorOptions_MouseHover);
            // 
            // editPacManBtn
            // 
            this.editPacManBtn.BackColor = System.Drawing.Color.DarkGray;
            this.editPacManBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editPacManBtn.Location = new System.Drawing.Point(0, 110);
            this.editPacManBtn.Name = "editPacManBtn";
            this.editPacManBtn.Size = new System.Drawing.Size(50, 50);
            this.editPacManBtn.TabIndex = 5;
            this.editPacManBtn.UseVisualStyleBackColor = false;
            this.editPacManBtn.Click += new System.EventHandler(this.button1_Click);
            this.editPacManBtn.MouseHover += new System.EventHandler(this.LevelEditorOptions_MouseHover);
            // 
            // editWallsBtn
            // 
            this.editWallsBtn.BackColor = System.Drawing.Color.Blue;
            this.editWallsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editWallsBtn.Location = new System.Drawing.Point(0, 0);
            this.editWallsBtn.Name = "editWallsBtn";
            this.editWallsBtn.Size = new System.Drawing.Size(50, 50);
            this.editWallsBtn.TabIndex = 4;
            this.editWallsBtn.UseVisualStyleBackColor = false;
            this.editWallsBtn.Click += new System.EventHandler(this.button1_Click);
            this.editWallsBtn.MouseHover += new System.EventHandler(this.LevelEditorOptions_MouseHover);
            // 
            // Ghost1Btn
            // 
            this.Ghost1Btn.BackColor = System.Drawing.Color.DarkGray;
            this.Ghost1Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ghost1Btn.Location = new System.Drawing.Point(56, 55);
            this.Ghost1Btn.Name = "Ghost1Btn";
            this.Ghost1Btn.Size = new System.Drawing.Size(50, 50);
            this.Ghost1Btn.TabIndex = 8;
            this.Ghost1Btn.UseVisualStyleBackColor = false;
            this.Ghost1Btn.Visible = false;
            this.Ghost1Btn.Click += new System.EventHandler(this.button1_Click);
            this.Ghost1Btn.MouseHover += new System.EventHandler(this.LevelEditorOptions_MouseHover);
            // 
            // Ghost2Btn
            // 
            this.Ghost2Btn.BackColor = System.Drawing.Color.DarkGray;
            this.Ghost2Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ghost2Btn.Location = new System.Drawing.Point(112, 55);
            this.Ghost2Btn.Name = "Ghost2Btn";
            this.Ghost2Btn.Size = new System.Drawing.Size(50, 50);
            this.Ghost2Btn.TabIndex = 9;
            this.Ghost2Btn.UseVisualStyleBackColor = false;
            this.Ghost2Btn.Visible = false;
            this.Ghost2Btn.Click += new System.EventHandler(this.button1_Click);
            this.Ghost2Btn.MouseHover += new System.EventHandler(this.LevelEditorOptions_MouseHover);
            // 
            // Ghost3Btn
            // 
            this.Ghost3Btn.BackColor = System.Drawing.Color.DarkGray;
            this.Ghost3Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ghost3Btn.Location = new System.Drawing.Point(168, 55);
            this.Ghost3Btn.Name = "Ghost3Btn";
            this.Ghost3Btn.Size = new System.Drawing.Size(50, 50);
            this.Ghost3Btn.TabIndex = 10;
            this.Ghost3Btn.UseVisualStyleBackColor = false;
            this.Ghost3Btn.Visible = false;
            this.Ghost3Btn.MouseHover += new System.EventHandler(this.LevelEditorOptions_MouseHover);
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = System.Drawing.Color.Black;
            this.saveBtn.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.Location = new System.Drawing.Point(0, 220);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(50, 50);
            this.saveBtn.TabIndex = 11;
            this.saveBtn.UseVisualStyleBackColor = false;
            this.saveBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // editCoinBtn
            // 
            this.editCoinBtn.BackColor = System.Drawing.Color.DarkGray;
            this.editCoinBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editCoinBtn.Location = new System.Drawing.Point(0, 165);
            this.editCoinBtn.Name = "editCoinBtn";
            this.editCoinBtn.Size = new System.Drawing.Size(50, 50);
            this.editCoinBtn.TabIndex = 12;
            this.editCoinBtn.UseVisualStyleBackColor = false;
            this.editCoinBtn.Click += new System.EventHandler(this.button1_Click);
            this.editCoinBtn.Paint += new System.Windows.Forms.PaintEventHandler(this.editCoinBtn_Paint);
            this.editCoinBtn.MouseHover += new System.EventHandler(this.LevelEditorOptions_MouseHover);
            // 
            // editCoinsBtn
            // 
            this.editCoinsBtn.BackColor = System.Drawing.Color.DarkGray;
            this.editCoinsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editCoinsBtn.Location = new System.Drawing.Point(56, 165);
            this.editCoinsBtn.Name = "editCoinsBtn";
            this.editCoinsBtn.Size = new System.Drawing.Size(50, 50);
            this.editCoinsBtn.TabIndex = 13;
            this.editCoinsBtn.UseVisualStyleBackColor = false;
            this.editCoinsBtn.Visible = false;
            this.editCoinsBtn.Click += new System.EventHandler(this.button1_Click);
            this.editCoinsBtn.Paint += new System.Windows.Forms.PaintEventHandler(this.editCoinsBtn_Paint);
            this.editCoinsBtn.MouseHover += new System.EventHandler(this.LevelEditorOptions_MouseHover);
            // 
            // removeCoinsBtn
            // 
            this.removeCoinsBtn.BackColor = System.Drawing.Color.DarkGray;
            this.removeCoinsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeCoinsBtn.Location = new System.Drawing.Point(112, 165);
            this.removeCoinsBtn.Name = "removeCoinsBtn";
            this.removeCoinsBtn.Size = new System.Drawing.Size(50, 50);
            this.removeCoinsBtn.TabIndex = 14;
            this.removeCoinsBtn.UseVisualStyleBackColor = false;
            this.removeCoinsBtn.Visible = false;
            this.removeCoinsBtn.Click += new System.EventHandler(this.button1_Click);
            this.removeCoinsBtn.Paint += new System.Windows.Forms.PaintEventHandler(this.removeCoinsBtn_Paint);
            this.removeCoinsBtn.MouseHover += new System.EventHandler(this.LevelEditorOptions_MouseHover);
            // 
            // LevelEditorOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(220, 325);
            this.Controls.Add(this.editCoinBtn);
            this.Controls.Add(this.removeCoinsBtn);
            this.Controls.Add(this.editCoinsBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.editGhostsBtn);
            this.Controls.Add(this.Ghost3Btn);
            this.Controls.Add(this.Ghost2Btn);
            this.Controls.Add(this.Ghost1Btn);
            this.Controls.Add(this.returnBtn);
            this.Controls.Add(this.editPacManBtn);
            this.Controls.Add(this.editWallsBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(220, 325);
            this.MinimumSize = new System.Drawing.Size(50, 200);
            this.Name = "LevelEditorOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LevelEditorOptions";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.LightGray;
            this.Load += new System.EventHandler(this.LevelEditorOptions_Load);
            this.MouseHover += new System.EventHandler(this.LevelEditorOptions_MouseHover);
            this.ResumeLayout(false);

        }

        #endregion

        private Button returnBtn;
        private Button editGhostsBtn;
        private Button editPacManBtn;
        private Button editWallsBtn;
        private Button Ghost1Btn;
        private Button Ghost2Btn;
        private Button Ghost3Btn;
        private Button saveBtn;
        private Button editCoinBtn;
        private Button editCoinsBtn;
        private Button removeCoinsBtn;
        private ToolTip toolTip1;
    }
}