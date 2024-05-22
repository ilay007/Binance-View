
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace AcountViewer
{
    partial class TradingInfo
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listView2 = new BrightIdeasSoftware.DataListView();
            this.ovlColumn3 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn4 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn11 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn5 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn6 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn7 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn8 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn9 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn10 = new BrightIdeasSoftware.OLVColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listView1 = new BrightIdeasSoftware.DataListView();
            this.olvColumn2 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn1 = new BrightIdeasSoftware.OLVColumn();
            this.olvPrice = new BrightIdeasSoftware.OLVColumn();
            this.olvBalance = new BrightIdeasSoftware.OLVColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listView2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.875F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.794948F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.20506F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1037, 673);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(460, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 32);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.listView2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(460, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(574, 629);
            this.panel2.TabIndex = 3;
            // 
            // listView2
            // 
            this.listView2.AllColumns.Add(this.ovlColumn3);
            this.listView2.AllColumns.Add(this.olvColumn4);
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ovlColumn3,
            this.olvColumn11,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn9,
            this.olvColumn10});
            this.listView2.DataSource = null;
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(574, 629);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            // 
            // ovlColumn3
            // 
            this.ovlColumn3.AspectName = "Symbol";
            this.ovlColumn3.Text = "CoinName";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Price";
            this.olvColumn4.Text = "Price";
            // 
            // olvColumn11
            // 
            this.olvColumn11.AspectName = "Date";
            this.olvColumn11.Text = "Data";
            // 
            // ovlColumn5
            // 
            this.olvColumn5.AspectName = "ExecutedQty";
            this.olvColumn5.Text = "ExecutedQty";
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "OrigQty";
            this.olvColumn6.Text = "OrigQty";
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "Type";
            this.olvColumn7.Text = "Type";
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "Side";
            this.olvColumn8.Text = "Sell";
            // 
            // olvColumn9
            // 
            this.olvColumn9.AspectName = "MidlePrice";
            this.olvColumn9.Text = "MidlPrice";
            // 
            // olvColumn10
            // 
            this.olvColumn10.AspectName = "Profit";
            this.olvColumn10.Text = "Profit";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.listView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(451, 629);
            this.panel3.TabIndex = 4;
            // 
            // listView1
            // 
            this.listView1.AllColumns.Add(this.olvColumn2);
            this.listView1.AllColumns.Add(this.olvColumn1);
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvPrice,
            this.olvBalance});
            this.listView1.DataSource = null;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(451, 629);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // ovlColumn2
            // 
            this.olvColumn2.AspectName = "Free";
            this.olvColumn2.Text = "Sum";
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Coin";
            this.olvColumn1.Text = "CoinName";
            // 
            // olvPrice
            // 
            this.olvPrice.AspectName = "Price";
            this.olvPrice.Text = "Price";
            // 
            // olvBalance
            // 
            this.olvBalance.AspectName = "Balance";
            this.olvBalance.Text = "Balance";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(451, 32);
            this.panel4.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(102, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 31);
            this.button2.TabIndex = 1;
            this.button2.Text = "ShowInfoUses2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "ShowInfoUser1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TradingInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TradingInfo";
            this.Text = "TradingInfo";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listView2)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        
        private System.Windows.Forms.Panel panel2;

        //ExecutedQty

        private OLVColumn olvColumn2;
        private OLVColumn olvColumn1;
        private OLVColumn ovlColumn3;
        private OLVColumn olvColumn4;
        private OLVColumn olvColumn5;
        private OLVColumn olvColumn6;
        private OLVColumn olvColumn7;
        private OLVColumn olvColumn8;
        private OLVColumn olvColumn9;
        private OLVColumn olvColumn10;
        private OLVColumn olvColumn11;

        private OLVColumn olvPrice;
        private OLVColumn olvBalance;

        private System.Windows.Forms.Panel panel3;

        private BrightIdeasSoftware.DataListView listView1;
        private BrightIdeasSoftware.DataListView listView2;
        private Panel panel4;
        private Button button2;
    }
}

