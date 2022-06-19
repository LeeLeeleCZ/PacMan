namespace PAC_MAN
{
    partial class MapSelector
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
            this.button1 = new System.Windows.Forms.Button();
            this.DefaultBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1050, 526);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.Paint += new System.Windows.Forms.PaintEventHandler(this.button1_Paint);
            // 
            // DefaultBtn
            // 
            this.DefaultBtn.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.DefaultBtn.FlatAppearance.BorderSize = 2;
            this.DefaultBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DefaultBtn.Font = new System.Drawing.Font("Bauhaus 93", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DefaultBtn.ForeColor = System.Drawing.Color.Blue;
            this.DefaultBtn.Location = new System.Drawing.Point(101, 175);
            this.DefaultBtn.Margin = new System.Windows.Forms.Padding(50, 3, 50, 3);
            this.DefaultBtn.Name = "DefaultBtn";
            this.DefaultBtn.Size = new System.Drawing.Size(485, 50);
            this.DefaultBtn.TabIndex = 1;
            this.DefaultBtn.Text = "Default Map";
            this.DefaultBtn.UseVisualStyleBackColor = true;
            this.DefaultBtn.Click += new System.EventHandler(this.Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(100, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Load default map:";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(100, 260);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(486, 235);
            this.panel1.TabIndex = 3;
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(101, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Load custom map:";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(620, 175);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(480, 320);
            this.panel2.TabIndex = 5;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Bauhaus 93", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.Lime;
            this.button2.Location = new System.Drawing.Point(100, 526);
            this.button2.Margin = new System.Windows.Forms.Padding(50, 3, 50, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(938, 50);
            this.button2.TabIndex = 6;
            this.button2.Text = "Load map..";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(58, 260);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(26, 235);
            this.vScrollBar1.TabIndex = 7;
            this.vScrollBar1.Visible = false;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // MapSelector
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.ControlBox = false;
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DefaultBtn);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MapSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapSelector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Button DefaultBtn;
        private Label label1;
        private Panel panel1;
        private Label label2;
        private Panel panel2;
        private Button button2;
        private VScrollBar vScrollBar1;
    }
}