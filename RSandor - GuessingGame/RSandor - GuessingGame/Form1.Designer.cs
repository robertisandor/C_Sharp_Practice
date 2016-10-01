namespace RSandor___GuessingGame
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.UserInputTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HigherOrLower = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // UserInputTextBox
            // 
            this.UserInputTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.UserInputTextBox.Location = new System.Drawing.Point(0, 361);
            this.UserInputTextBox.Margin = new System.Windows.Forms.Padding(50, 100, 50, 50);
            this.UserInputTextBox.Name = "UserInputTextBox";
            this.UserInputTextBox.Size = new System.Drawing.Size(392, 20);
            this.UserInputTextBox.TabIndex = 1;
            this.UserInputTextBox.Text = "Enter # here";
            this.UserInputTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UserInputTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UserInputTextBox_MouseClick);
            this.UserInputTextBox.TextChanged += new System.EventHandler(this.UserInputTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 78);
            this.label2.TabIndex = 2;
            this.label2.Text = "          This is A Guessing Game!\r\nThe computer will think of a number\r\nbetween " +
    "1 and 100 & you\'ll have to guess\r\n          correctly within 4 guesses.\r\n\r\nWhene" +
    "ver you\'re ready, type in your #.\r\n";
            // 
            // HigherOrLower
            // 
            this.HigherOrLower.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.HigherOrLower.AutoSize = true;
            this.HigherOrLower.Location = new System.Drawing.Point(132, 238);
            this.HigherOrLower.Name = "HigherOrLower";
            this.HigherOrLower.Size = new System.Drawing.Size(154, 13);
            this.HigherOrLower.TabIndex = 3;
            this.HigherOrLower.Text = "I\'ll tell you to go higher or lower!";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Number of attempts left: 5";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(168, 295);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Guess!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Location = new System.Drawing.Point(168, 324);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "New Game";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(392, 381);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HigherOrLower);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UserInputTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserInputTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label HigherOrLower;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

