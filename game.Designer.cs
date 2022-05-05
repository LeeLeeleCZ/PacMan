namespace PAC_MAN
{
    partial class game
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
            this.PacManPictureBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PacManPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PacManPictureBox
            // 
            this.PacManPictureBox.Location = new System.Drawing.Point(0, 0);
            this.PacManPictureBox.Name = "PacManPictureBox";
            this.PacManPictureBox.Size = new System.Drawing.Size(50, 50);
            this.PacManPictureBox.TabIndex = 0;
            this.PacManPictureBox.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.PacManPictureBox);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "game";
            this.Text = "game";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.game_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.game_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.PacManPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox PacManPictureBox;
        private System.Windows.Forms.Timer timer1;
    }
}