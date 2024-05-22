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
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)listView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.0171661F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.9828329F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.70053F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.2994652F));
            tableLayoutPanel1.Size = new System.Drawing.Size(932, 561);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(listView1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(805, 485);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // listView1
            // 
            listView1.AllColumns.Add(olvColumn2);
            listView1.AllColumns.Add(olvColumn1);
            listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { olvColumn1, olvColumn2, olvColumn3, olvColumn4 });
            listView1.DataSource = null;
            listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            listView1.Location = new System.Drawing.Point(0, 0);
            listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(805, 485);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = System.Windows.Forms.View.Details;

            //[{"Name":"LTC","Balance":0.710498,"StartBalanceUSDT":40.0,"BalanceUSDT":0.0,"LastPriceCoin":0.0}]
            // 
            // olvColumn2
            // 
            olvColumn2.AspectName = "Free";
            olvColumn2.Text = "Sum";
            // 
            // olvColumn1
            // 
            olvColumn1.AspectName = "Name";
            olvColumn1.Text = "CoinName";
            // 
            // olvColumn3
            // 
            olvColumn3.AspectName = "StartBalanceUSDT";
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
            // SetupTradingList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(932, 561);
            Controls.Add(tableLayoutPanel1);
            Name = "SetupTradingList";
            Text = "SetupTradingList";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)listView1).EndInit();
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
    }
}