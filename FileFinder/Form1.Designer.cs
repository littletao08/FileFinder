namespace FileFinder
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView = new System.Windows.Forms.TreeView();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btnsetfolder = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.btnquit = new System.Windows.Forms.Button();
            this.checkbox = new System.Windows.Forms.CheckBox();
            this.txtbox = new System.Windows.Forms.TextBox();
            this.txtfolder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.SystemColors.Control;
            this.treeView.Location = new System.Drawing.Point(12, 12);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(123, 460);
            this.treeView.TabIndex = 0;
            this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
            this.treeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDoubleClick);
            // 
            // btnsearch
            // 
            this.btnsearch.Location = new System.Drawing.Point(431, 47);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(75, 23);
            this.btnsearch.TabIndex = 1;
            this.btnsearch.Text = "搜索";
            this.btnsearch.UseVisualStyleBackColor = true;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // btnsetfolder
            // 
            this.btnsetfolder.Location = new System.Drawing.Point(141, 20);
            this.btnsetfolder.Name = "btnsetfolder";
            this.btnsetfolder.Size = new System.Drawing.Size(75, 23);
            this.btnsetfolder.TabIndex = 3;
            this.btnsetfolder.Text = "自定义目录";
            this.btnsetfolder.UseVisualStyleBackColor = true;
            this.btnsetfolder.Click += new System.EventHandler(this.btnsetfolder_Click);
            // 
            // listBox
            // 
            this.listBox.BackColor = System.Drawing.Color.Silver;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 12;
            this.listBox.Location = new System.Drawing.Point(141, 82);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(750, 388);
            this.listBox.TabIndex = 7;
            this.listBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);
            this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // btnquit
            // 
            this.btnquit.Location = new System.Drawing.Point(431, 47);
            this.btnquit.Name = "btnquit";
            this.btnquit.Size = new System.Drawing.Size(75, 23);
            this.btnquit.TabIndex = 8;
            this.btnquit.Text = "取消";
            this.btnquit.UseVisualStyleBackColor = true;
            this.btnquit.Click += new System.EventHandler(this.btnquit_Click);
            // 
            // checkbox
            // 
            this.checkbox.AutoSize = true;
            this.checkbox.Location = new System.Drawing.Point(347, 51);
            this.checkbox.Name = "checkbox";
            this.checkbox.Size = new System.Drawing.Size(84, 16);
            this.checkbox.TabIndex = 9;
            this.checkbox.Text = "大小写敏感";
            this.checkbox.UseVisualStyleBackColor = true;
            // 
            // txtbox
            // 
            this.txtbox.BackColor = System.Drawing.Color.White;
            this.txtbox.Location = new System.Drawing.Point(144, 49);
            this.txtbox.Name = "txtbox";
            this.txtbox.Size = new System.Drawing.Size(200, 21);
            this.txtbox.TabIndex = 10;
            // 
            // txtfolder
            // 
            this.txtfolder.BackColor = System.Drawing.Color.White;
            this.txtfolder.Location = new System.Drawing.Point(220, 21);
            this.txtfolder.Name = "txtfolder";
            this.txtfolder.Size = new System.Drawing.Size(669, 21);
            this.txtfolder.TabIndex = 11;
            this.txtfolder.Text = "E:\\vstsworkspace\\project2009\\source\\as\\activity";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(903, 484);
            this.Controls.Add(this.txtfolder);
            this.Controls.Add(this.txtbox);
            this.Controls.Add(this.checkbox);
            this.Controls.Add(this.btnquit);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.btnsetfolder);
            this.Controls.Add(this.btnsearch);
            this.Controls.Add(this.treeView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnsetfolder;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button btnquit;
        private System.Windows.Forms.CheckBox checkbox;
        private System.Windows.Forms.TextBox txtbox;
        private System.Windows.Forms.TextBox txtfolder;
    }
}

