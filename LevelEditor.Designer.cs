namespace PAC_MAN
{
    partial class LevelEditor
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
            this.SuspendLayout();
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LevelEditor";
            this.Text = "LevelEditor";
            this.Load += new System.EventHandler(this.LevelEditor_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LevelEditor_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LevelEditor_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LevelEditor_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LevelEditor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LevelEditor_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LevelEditor_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}