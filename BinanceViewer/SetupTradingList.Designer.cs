using BrightIdeasSoftware;

namespace BinanceAcountViewer
{
    partial class SetupTradingList
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
            listView1 = new DataListView();
            olvColumn2 = new OLVColumn();
            olvColumn1 = new OLVColumn();
            olvColumn3 = new OLVColumn();
            olvColumn4 = new OLVColumn();
            olvColumn5 = new OLVColumn();
            olvColumn6 = new OLVColumn();
            buttonAddCoin = new System.Windows.Forms.Button();
            panel2 = new System.Windows.Forms.Panel();
            button1 = new System.Windows.Forms.Button();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)listView1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.0171661F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.9828329F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.70053F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.2994652F));
            tableLayoutPanel1.Size = new System.Drawing.Size(816, 421);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // panel1
            // 
            panel1.Controls.Add(listView1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 2);
            panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(704, 365);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // listView1
            // 
            listView1.AllColumns.Add(olvColumn2);
            listView1.AllColumns.Add(olvColumn1);
            listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { olvColumn1, olvColumn2, olvColumn3, olvColumn4, olvColumn5, olvColumn6 });
            listView1.DataSource = null;
            listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            listView1.Location = new System.Drawing.Point(0, 0);
            listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(704, 365);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = System.Windows.Forms.View.Details;
            listView1.SelectedIndexChanged += listView2_SelectedIndexChanged;
            // 
            // olvColumn2
            // 
            olvColumn2.AspectName = "NumOfTradingCoins";
            olvColumn2.Text = "NumOfTradingCoins";
            // 
            // olvColumn1
            // 
            olvColumn1.AspectName = "Name";
            olvColumn1.Text = "CoinName";
            // 
            // olvColumn3
            // 
            olvColumn3.AspectName = "BalanceUSDT";
            olvColumn3.Text = "BalanceUSDT";
            // 
            // olvColumn4
            // 
            olvColumn4.AspectName = "Balance";
            olvColumn4.Text = "Balance";
            // 
            // olvColumn5
            // 
            olvColumn5.AspectName = "LastSellPrice";
            olvColumn5.Text = "LastSellPrice";
            // 
            // olvColumn6
            // 
            olvColumn6.AspectName = "LastBuyPrice";
            olvColumn6.Text = "LastBuyPrice";
            // 
            // buttonAddCoin
            // 
            buttonAddCoin.Location = new System.Drawing.Point(3, 8);
            buttonAddCoin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonAddCoin.Name = "buttonAddCoin";
            buttonAddCoin.Size = new System.Drawing.Size(93, 22);
            buttonAddCoin.TabIndex = 1;
            buttonAddCoin.Text = "AddCoin";
            buttonAddCoin.UseVisualStyleBackColor = true;
            buttonAddCoin.Click += buttonAddCoin_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(button1);
            panel2.Controls.Add(buttonAddCoin);
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(713, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(100, 363);
            panel2.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(3, 35);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(93, 23);
            button1.TabIndex = 2;
            button1.Text = "DeleteSelected";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // SetupTradingList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(816, 421);
            Controls.Add(tableLayoutPanel1);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "SetupTradingList";
            Text = "SetupTradingList";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)listView1).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private OLVColumn olvColumn2;
        private OLVColumn olvColumn1;
        private OLVColumn olvColumn3;
        private OLVColumn olvColumn4;
        private OLVColumn olvColumn5;
        private OLVColumn olvColumn6;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private DataListView listView1;
        private System.Windows.Forms.Button buttonAddCoin;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
    }
}