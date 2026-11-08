namespace ColoriResistenze
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            Btn_Chiudi = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Zucca;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(81, 74);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(92, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(203, 74);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // Btn_Chiudi
            // 
            Btn_Chiudi.Location = new Point(119, 166);
            Btn_Chiudi.Name = "Btn_Chiudi";
            Btn_Chiudi.Size = new Size(75, 23);
            Btn_Chiudi.TabIndex = 2;
            Btn_Chiudi.Text = "Chiudi";
            Btn_Chiudi.UseVisualStyleBackColor = true;
            Btn_Chiudi.Click += Btn_Chiudi_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label1.Location = new Point(42, 93);
            label1.Name = "label1";
            label1.Size = new Size(223, 28);
            label1.TabIndex = 3;
            label1.Text = "Colori resistenze V 1.0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(82, 126);
            label2.Name = "label2";
            label2.Size = new Size(144, 15);
            label2.TabIndex = 4;
            label2.Text = "Software Open Surce 2026";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 144);
            label3.Name = "label3";
            label3.Size = new Size(299, 15);
            label3.TabIndex = 5;
            label3.Text = " GIT-HUB: https://github.com/CZ1968/ColoriResistenze";
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(310, 198);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Btn_Chiudi);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormAbout";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Colori Resistenze V1.0";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button Btn_Chiudi;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
