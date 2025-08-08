namespace MauiSoft.SRP
{
    partial class Main
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
            components = new System.ComponentModel.Container();
            timerSaitek = new System.Windows.Forms.Timer(components);
            DSP4 = new Label();
            DSP3 = new Label();
            DSP2 = new Label();
            DSP1 = new Label();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // timerSaitek
            // 
            timerSaitek.Enabled = true;
            timerSaitek.Tick += TimerSaitek_Tick;
            // 
            // DSP4
            // 
            DSP4.BackColor = Color.Black;
            DSP4.BorderStyle = BorderStyle.FixedSingle;
            DSP4.Font = new Font("P51LCD", 36F);
            DSP4.ForeColor = Color.Red;
            DSP4.Location = new Point(265, 101);
            DSP4.Name = "DSP4";
            DSP4.Size = new Size(216, 66);
            DSP4.TabIndex = 10;
            DSP4.Text = "110.10";
            DSP4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DSP3
            // 
            DSP3.BackColor = Color.Black;
            DSP3.BorderStyle = BorderStyle.FixedSingle;
            DSP3.Font = new Font("P51LCD", 36F);
            DSP3.ForeColor = Color.Red;
            DSP3.Location = new Point(25, 101);
            DSP3.Name = "DSP3";
            DSP3.Size = new Size(216, 66);
            DSP3.TabIndex = 9;
            DSP3.Text = "110.10";
            DSP3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DSP2
            // 
            DSP2.BackColor = Color.Black;
            DSP2.BorderStyle = BorderStyle.FixedSingle;
            DSP2.Font = new Font("P51LCD", 36F);
            DSP2.ForeColor = Color.Red;
            DSP2.Location = new Point(265, 20);
            DSP2.Name = "DSP2";
            DSP2.Size = new Size(216, 66);
            DSP2.TabIndex = 8;
            DSP2.Text = "110.10";
            DSP2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DSP1
            // 
            DSP1.BackColor = Color.Black;
            DSP1.BorderStyle = BorderStyle.FixedSingle;
            DSP1.Font = new Font("P51LCD", 36F);
            DSP1.ForeColor = Color.Red;
            DSP1.Location = new Point(25, 20);
            DSP1.Name = "DSP1";
            DSP1.Size = new Size(216, 66);
            DSP1.TabIndex = 6;
            DSP1.Text = "110.10";
            DSP1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Black;
            pictureBox1.Location = new Point(8, 7);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(494, 183);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(25, 217);
            button1.Name = "button1";
            button1.Size = new Size(108, 49);
            button1.TabIndex = 11;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 288);
            Controls.Add(button1);
            Controls.Add(DSP4);
            Controls.Add(DSP3);
            Controls.Add(DSP2);
            Controls.Add(DSP1);
            Controls.Add(pictureBox1);
            Margin = new Padding(4);
            Name = "Main";
            Text = "Test";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timerSaitek;
        private Label DSP4;
        private Label DSP3;
        private Label DSP2;
        private Label DSP1;
        private PictureBox pictureBox1;
        private Button button1;
    }
}

