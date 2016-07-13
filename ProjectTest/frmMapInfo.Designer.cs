namespace ProjectTest
{
    partial class frmMapInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapInfo));
            this.txtFieldDb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtById = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtByTag = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtByClass = new System.Windows.Forms.TextBox();
            this.numByIndex = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numByIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFieldDb
            // 
            this.txtFieldDb.Location = new System.Drawing.Point(12, 26);
            this.txtFieldDb.Name = "txtFieldDb";
            this.txtFieldDb.Size = new System.Drawing.Size(299, 20);
            this.txtFieldDb.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Maping To Field Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Get Content By Id or Name of Element";
            // 
            // txtById
            // 
            this.txtById.Location = new System.Drawing.Point(12, 65);
            this.txtById.Name = "txtById";
            this.txtById.Size = new System.Drawing.Size(299, 20);
            this.txtById.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Get Content By Tag Name of Element";
            // 
            // txtByTag
            // 
            this.txtByTag.Location = new System.Drawing.Point(12, 103);
            this.txtByTag.Name = "txtByTag";
            this.txtByTag.Size = new System.Drawing.Size(299, 20);
            this.txtByTag.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Get Content By Class Name of Element";
            // 
            // txtByClass
            // 
            this.txtByClass.Location = new System.Drawing.Point(12, 143);
            this.txtByClass.Name = "txtByClass";
            this.txtByClass.Size = new System.Drawing.Size(299, 20);
            this.txtByClass.TabIndex = 6;
            // 
            // numByIndex
            // 
            this.numByIndex.Location = new System.Drawing.Point(12, 192);
            this.numByIndex.Name = "numByIndex";
            this.numByIndex.Size = new System.Drawing.Size(86, 20);
            this.numByIndex.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Index of Element";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(152, 216);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(236, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(350, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(244, 78);
            this.label6.TabIndex = 12;
            this.label6.Text = "Extract HTML by DOM object\r\n\r\nContent By Id return single value\r\n\r\nContent By Tag" +
    " and Class Name return multi value\r\nMust using Index of element to choose one";
            // 
            // frmMapInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 262);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numByIndex);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtByClass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtByTag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtById);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFieldDb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMapInfo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Maping Info";
            this.Load += new System.EventHandler(this.frmMapInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numByIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFieldDb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtById;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtByTag;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtByClass;
        private System.Windows.Forms.NumericUpDown numByIndex;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
    }
}