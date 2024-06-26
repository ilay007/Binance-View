namespace BinanceAcountViewer
{
    partial class KnowledgeViewer
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            checkBox2 = new System.Windows.Forms.CheckBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            button3 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            panel2 = new System.Windows.Forms.Panel();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            panel3 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            pictureBox6 = new System.Windows.Forms.PictureBox();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            panel5 = new System.Windows.Forms.Panel();
            pictureBox7 = new System.Windows.Forms.PictureBox();
            pictureBox4 = new System.Windows.Forms.PictureBox();
            panel6 = new System.Windows.Forms.Panel();
            pictureBox8 = new System.Windows.Forms.PictureBox();
            pictureBox5 = new System.Windows.Forms.PictureBox();
            button4 = new System.Windows.Forms.Button();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.9899216F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.01008F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 603F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 1, 0);
            tableLayoutPanel1.Controls.Add(panel3, 0, 1);
            tableLayoutPanel1.Controls.Add(panel4, 2, 0);
            tableLayoutPanel1.Controls.Add(panel5, 1, 1);
            tableLayoutPanel1.Controls.Add(panel6, 2, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1389, 758);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(button4);
            panel1.Controls.Add(checkBox2);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(198, 373);
            panel1.TabIndex = 0;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new System.Drawing.Point(9, 136);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(134, 24);
            checkBox2.TabIndex = 4;
            checkBox2.Text = "Buy Knowledge";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(9, 105);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(134, 24);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Sell Knowledge";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(92, 165);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(84, 29);
            button3.TabIndex = 2;
            button3.Text = ">>";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(9, 165);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(77, 29);
            button2.TabIndex = 1;
            button2.Text = "<<";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(9, 9);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(175, 39);
            button1.TabIndex = 0;
            button1.Text = "DownloadKnowledge";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(pictureBox1);
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(207, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(575, 373);
            panel2.TabIndex = 1;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            pictureBox2.Location = new System.Drawing.Point(0, 290);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(575, 83);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(575, 284);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Location = new System.Drawing.Point(3, 382);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(198, 373);
            panel3.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.Controls.Add(pictureBox6);
            panel4.Controls.Add(pictureBox3);
            panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            panel4.Location = new System.Drawing.Point(788, 3);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(598, 373);
            panel4.TabIndex = 3;
            // 
            // pictureBox6
            // 
            pictureBox6.Dock = System.Windows.Forms.DockStyle.Bottom;
            pictureBox6.Location = new System.Drawing.Point(0, 290);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new System.Drawing.Size(598, 83);
            pictureBox6.TabIndex = 1;
            pictureBox6.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Dock = System.Windows.Forms.DockStyle.Top;
            pictureBox3.Location = new System.Drawing.Point(0, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new System.Drawing.Size(598, 284);
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // panel5
            // 
            panel5.Controls.Add(pictureBox7);
            panel5.Controls.Add(pictureBox4);
            panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            panel5.Location = new System.Drawing.Point(207, 382);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(575, 373);
            panel5.TabIndex = 4;
            // 
            // pictureBox7
            // 
            pictureBox7.Dock = System.Windows.Forms.DockStyle.Bottom;
            pictureBox7.Location = new System.Drawing.Point(0, 311);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new System.Drawing.Size(575, 62);
            pictureBox7.TabIndex = 1;
            pictureBox7.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Dock = System.Windows.Forms.DockStyle.Top;
            pictureBox4.Location = new System.Drawing.Point(0, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new System.Drawing.Size(575, 308);
            pictureBox4.TabIndex = 0;
            pictureBox4.TabStop = false;
            // 
            // panel6
            // 
            panel6.Controls.Add(pictureBox8);
            panel6.Controls.Add(pictureBox5);
            panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            panel6.Location = new System.Drawing.Point(788, 382);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(598, 373);
            panel6.TabIndex = 5;
            // 
            // pictureBox8
            // 
            pictureBox8.Dock = System.Windows.Forms.DockStyle.Bottom;
            pictureBox8.Location = new System.Drawing.Point(0, 311);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new System.Drawing.Size(598, 62);
            pictureBox8.TabIndex = 1;
            pictureBox8.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Dock = System.Windows.Forms.DockStyle.Top;
            pictureBox5.Location = new System.Drawing.Point(0, 0);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new System.Drawing.Size(598, 308);
            pictureBox5.TabIndex = 0;
            pictureBox5.TabStop = false;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(9, 54);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(175, 34);
            button4.TabIndex = 5;
            button4.Text = "Delete Selected";
            button4.UseVisualStyleBackColor = true;
            // 
            // KnowledgeViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1389, 758);
            Controls.Add(tableLayoutPanel1);
            Name = "KnowledgeViewer";
            Text = "KnowledgeViewer";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button4;
    }
}