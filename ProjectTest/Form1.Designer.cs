namespace ProjectTest
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
            this.txtCurrentUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReports = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtResultPostToUrl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkMaps = new System.Windows.Forms.CheckedListBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnMapAdd = new System.Windows.Forms.ToolStripButton();
            this.btnAddRemove = new System.Windows.Forms.ToolStripButton();
            this.btnMapView = new System.Windows.Forms.ToolStripButton();
            this.btnTestExt = new System.Windows.Forms.Button();
            this.btnAddAndStart = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInitExtension = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numDeep = new System.Windows.Forms.NumericUpDown();
            this.txtInitUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkScrapers = new System.Windows.Forms.CheckedListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnStartAll = new System.Windows.Forms.ToolStripButton();
            this.btnStopAll = new System.Windows.Forms.ToolStripButton();
            this.btnRemove = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDeep)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCurrentUrl
            // 
            this.txtCurrentUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCurrentUrl.Location = new System.Drawing.Point(78, 3);
            this.txtCurrentUrl.Multiline = true;
            this.txtCurrentUrl.Name = "txtCurrentUrl";
            this.txtCurrentUrl.Size = new System.Drawing.Size(828, 21);
            this.txtCurrentUrl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Link Current ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 200);
            this.label2.TabIndex = 2;
            this.label2.Text = "Reports";
            // 
            // txtReports
            // 
            this.txtReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReports.Location = new System.Drawing.Point(78, 30);
            this.txtReports.Multiline = true;
            this.txtReports.Name = "txtReports";
            this.txtReports.Size = new System.Drawing.Size(828, 194);
            this.txtReports.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtResultPostToUrl);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.btnTestExt);
            this.groupBox1.Controls.Add(this.btnAddAndStart);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtInitExtension);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numDeep);
            this.groupBox1.Controls.Add(this.txtInitUrl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 333);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scraper Config";
            // 
            // txtResultPostToUrl
            // 
            this.txtResultPostToUrl.Location = new System.Drawing.Point(104, 48);
            this.txtResultPostToUrl.Name = "txtResultPostToUrl";
            this.txtResultPostToUrl.Size = new System.Drawing.Size(360, 20);
            this.txtResultPostToUrl.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Result Post to Url";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkMaps);
            this.groupBox4.Controls.Add(this.toolStrip2);
            this.groupBox4.Location = new System.Drawing.Point(9, 183);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(367, 144);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Content maping";
            // 
            // chkMaps
            // 
            this.chkMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMaps.FormattingEnabled = true;
            this.chkMaps.Location = new System.Drawing.Point(3, 41);
            this.chkMaps.Name = "chkMaps";
            this.chkMaps.Size = new System.Drawing.Size(361, 100);
            this.chkMaps.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMapAdd,
            this.btnAddRemove,
            this.btnMapView});
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(361, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnMapAdd
            // 
            this.btnMapAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnMapAdd.Image")));
            this.btnMapAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapAdd.Name = "btnMapAdd";
            this.btnMapAdd.Size = new System.Drawing.Size(49, 22);
            this.btnMapAdd.Text = "Add";
            this.btnMapAdd.Click += new System.EventHandler(this.btnMapAdd_Click);
            // 
            // btnAddRemove
            // 
            this.btnAddRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRemove.Image")));
            this.btnAddRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRemove.Name = "btnAddRemove";
            this.btnAddRemove.Size = new System.Drawing.Size(70, 22);
            this.btnAddRemove.Text = "Remove";
            this.btnAddRemove.Click += new System.EventHandler(this.btnAddRemove_Click);
            // 
            // btnMapView
            // 
            this.btnMapView.Image = ((System.Drawing.Image)(resources.GetObject("btnMapView.Image")));
            this.btnMapView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapView.Name = "btnMapView";
            this.btnMapView.Size = new System.Drawing.Size(52, 22);
            this.btnMapView.Text = "View";
            this.btnMapView.Click += new System.EventHandler(this.btnMapView_Click);
            // 
            // btnTestExt
            // 
            this.btnTestExt.Location = new System.Drawing.Point(382, 168);
            this.btnTestExt.Name = "btnTestExt";
            this.btnTestExt.Size = new System.Drawing.Size(17, 10);
            this.btnTestExt.TabIndex = 7;
            this.btnTestExt.Text = "Test Extension";
            this.btnTestExt.UseVisualStyleBackColor = true;
            this.btnTestExt.Visible = false;
            this.btnTestExt.Click += new System.EventHandler(this.btnTestExt_Click);
            // 
            // btnAddAndStart
            // 
            this.btnAddAndStart.Location = new System.Drawing.Point(379, 234);
            this.btnAddAndStart.Name = "btnAddAndStart";
            this.btnAddAndStart.Size = new System.Drawing.Size(85, 38);
            this.btnAddAndStart.TabIndex = 10;
            this.btnAddAndStart.Text = "Add && Start >>";
            this.btnAddAndStart.UseVisualStyleBackColor = true;
            this.btnAddAndStart.Click += new System.EventHandler(this.btnAddAndStart_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(379, 193);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 35);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add >>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Download include file type by extension";
            // 
            // txtInitExtension
            // 
            this.txtInitExtension.Location = new System.Drawing.Point(9, 129);
            this.txtInitExtension.Multiline = true;
            this.txtInitExtension.Name = "txtInitExtension";
            this.txtInitExtension.Size = new System.Drawing.Size(428, 53);
            this.txtInitExtension.TabIndex = 4;
            this.txtInitExtension.Text = ".txt, .pdf, .doc, .docx, .xls, .xlsx, .png, .jpg, .jpeg, .bmp, .gif, .zip, .rar, " +
    ".7z, .flv, .wa, .mp3, .mov, .flv, .swf";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Deep";
            // 
            // numDeep
            // 
            this.numDeep.Location = new System.Drawing.Point(57, 87);
            this.numDeep.Name = "numDeep";
            this.numDeep.Size = new System.Drawing.Size(87, 20);
            this.numDeep.TabIndex = 2;
            this.numDeep.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // txtInitUrl
            // 
            this.txtInitUrl.Location = new System.Drawing.Point(104, 18);
            this.txtInitUrl.Name = "txtInitUrl";
            this.txtInitUrl.Size = new System.Drawing.Size(360, 20);
            this.txtInitUrl.TabIndex = 1;
            this.txtInitUrl.Text = "http://vnexpress.net";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "UrlInit";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkScrapers);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 333);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scraper(s)";
            // 
            // chkScrapers
            // 
            this.chkScrapers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkScrapers.FormattingEnabled = true;
            this.chkScrapers.Location = new System.Drawing.Point(3, 41);
            this.chkScrapers.Name = "chkScrapers";
            this.chkScrapers.Size = new System.Drawing.Size(435, 289);
            this.chkScrapers.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnStop,
            this.btnStartAll,
            this.btnStopAll,
            this.btnRemove});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(435, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnStart
            // 
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(51, 22);
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(51, 22);
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStartAll
            // 
            this.btnStartAll.Image = ((System.Drawing.Image)(resources.GetObject("btnStartAll.Image")));
            this.btnStartAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartAll.Name = "btnStartAll";
            this.btnStartAll.Size = new System.Drawing.Size(65, 22);
            this.btnStartAll.Text = "StartAll";
            this.btnStartAll.Click += new System.EventHandler(this.btnStartAll_Click);
            // 
            // btnStopAll
            // 
            this.btnStopAll.Image = ((System.Drawing.Image)(resources.GetObject("btnStopAll.Image")));
            this.btnStopAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.Size = new System.Drawing.Size(65, 22);
            this.btnStopAll.Text = "StopAll";
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(70, 22);
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(915, 583);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.TabIndex = 9;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(915, 333);
            this.splitContainer2.SplitterDistance = 470;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(915, 246);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scrape statistic";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.360836F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.63916F));
            this.tableLayoutPanel1.Controls.Add(this.txtReports, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCurrentUrl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.3348F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.6652F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(909, 227);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 583);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Scraper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDeep)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCurrentUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReports;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numDeep;
        private System.Windows.Forms.TextBox txtInitUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInitExtension;
        private System.Windows.Forms.Button btnTestExt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.CheckedListBox chkScrapers;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnStartAll;
        private System.Windows.Forms.ToolStripButton btnStopAll;
        private System.Windows.Forms.ToolStripButton btnRemove;
        private System.Windows.Forms.Button btnAddAndStart;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox chkMaps;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnMapAdd;
        private System.Windows.Forms.ToolStripButton btnAddRemove;
        private System.Windows.Forms.ToolStripButton btnMapView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtResultPostToUrl;
        private System.Windows.Forms.Label label6;
    }
}

