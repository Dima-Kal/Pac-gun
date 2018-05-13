namespace WindowsFormsApplication1
{
    partial class GameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.DrawingTimer = new System.Windows.Forms.Timer(this.components);
            this.BulletTimer = new System.Windows.Forms.Timer(this.components);
            this.PacmanTimer = new System.Windows.Forms.Timer(this.components);
            this.GhostTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // DrawingTimer
            // 
            this.DrawingTimer.Interval = 40;
            this.DrawingTimer.Tick += new System.EventHandler(this.DrawingTimer_Tick);
            // 
            // BulletTimer
            // 
            this.BulletTimer.Interval = 1;
            this.BulletTimer.Tick += new System.EventHandler(this.BulletTimer_Tick);
            // 
            // PacmanTimer
            // 
            this.PacmanTimer.Interval = 80;
            this.PacmanTimer.Tick += new System.EventHandler(this.PacmanTimer_Tick);
            // 
            // GhostTimer
            // 
            this.GhostTimer.Interval = 120;
            this.GhostTimer.Tick += new System.EventHandler(this.GhostTimer_Tick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(580, 675);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PacGun";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer DrawingTimer;
        private System.Windows.Forms.Timer BulletTimer;
        private System.Windows.Forms.Timer PacmanTimer;
        private System.Windows.Forms.Timer GhostTimer;
    }
}

