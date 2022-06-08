﻿namespace PAC_MAN
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
            this.returnBtn = new System.Windows.Forms.Button();
            this.editGhostsBtn = new System.Windows.Forms.Button();
            this.editPacManBtn = new System.Windows.Forms.Button();
            this.editWallsBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // returnBtn
            // 
            this.returnBtn.BackColor = System.Drawing.Color.Black;
            this.returnBtn.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.returnBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.returnBtn.Location = new System.Drawing.Point(0, 165);
            this.returnBtn.Name = "returnBtn";
            this.returnBtn.Size = new System.Drawing.Size(50, 50);
            this.returnBtn.TabIndex = 7;
            this.returnBtn.UseVisualStyleBackColor = false;
            this.returnBtn.Click += new System.EventHandler(this.button1_Click);
            this.returnBtn.Paint += new System.Windows.Forms.PaintEventHandler(this.returnBtn_Paint);
            // 
            // editGhostsBtn
            // 
            this.editGhostsBtn.BackColor = System.Drawing.Color.LightYellow;
            this.editGhostsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editGhostsBtn.Location = new System.Drawing.Point(0, 55);
            this.editGhostsBtn.Name = "editGhostsBtn";
            this.editGhostsBtn.Size = new System.Drawing.Size(50, 50);
            this.editGhostsBtn.TabIndex = 6;
            this.editGhostsBtn.UseVisualStyleBackColor = false;
            this.editGhostsBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // editPacManBtn
            // 
            this.editPacManBtn.BackColor = System.Drawing.Color.LightYellow;
            this.editPacManBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editPacManBtn.Location = new System.Drawing.Point(0, 110);
            this.editPacManBtn.Name = "editPacManBtn";
            this.editPacManBtn.Size = new System.Drawing.Size(50, 50);
            this.editPacManBtn.TabIndex = 5;
            this.editPacManBtn.UseVisualStyleBackColor = false;
            this.editPacManBtn.Click += new System.EventHandler(this.button1_Click);
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
            // 
            // LevelEditorOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(100, 215);
            this.Controls.Add(this.returnBtn);
            this.Controls.Add(this.editGhostsBtn);
            this.Controls.Add(this.editPacManBtn);
            this.Controls.Add(this.editWallsBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(100, 215);
            this.MinimumSize = new System.Drawing.Size(50, 200);
            this.Name = "LevelEditorOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LevelEditorOptions";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.LevelEditorOptions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button returnBtn;
        private Button editGhostsBtn;
        private Button editPacManBtn;
        private Button editWallsBtn;
    }
}