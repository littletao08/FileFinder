/* ==============================================================================
 * 功能描述：文件内容查询 
 * 创 建 者：huangliang
 * 创建日期：2017.06.19
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace FileFinder
{
    public partial class Form1 : Form
    {
        private Dictionary<string, int> _fileKeyNum = new Dictionary<string, int>();
        private Dictionary<string, string> _fileName = new Dictionary<string, string>();
        private Dictionary<string, int> _page = new Dictionary<string, int>();
        private bool _isSearching = false;
        private string _targetString;
        private IntKeyGen keyGener = new IntKeyGen();
        private Thread _currentThread;
        private string _initFolder = "";
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.listBox.DrawMode = DrawMode.OwnerDrawFixed;
            TreeNode rootNode = new TreeNode("我的电脑");
            rootNode.Text = "我的电脑";
            this.treeView.Nodes.Add(rootNode);
            //循环遍历计算机所有逻辑驱动器名称(盘符)  
            foreach (string drive in Environment.GetLogicalDrives())
            {
                //实例化DriveInfo对象 命名空间System.IO  
                var dir = new DriveInfo(drive);
                switch (dir.DriveType)           //判断驱动器类型  
                {
                    case DriveType.Fixed:        //仅取固定磁盘盘符 Removable-U盘   
                        {
                            //Split仅获取盘符字母  
                            TreeNode tNode = new TreeNode(dir.Name.Split(':')[0]);
                            tNode.Name = dir.Name;
                            tNode.Tag = tNode.Name;
                            //tNode.ImageIndex = IconIndexes.FixedDrive;         //获取结点显示图片  
                            //tNode.SelectedImageIndex = IconIndexes.FixedDrive; //选择显示图片  
                            treeView.Nodes.Add(tNode);                    //加载驱动节点  
                            tNode.Nodes.Add("");
                        }
                        break;
                }
            }
            rootNode.Expand();
            this.setBtnState();
        }

        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;
            var path = node.Name;
            string[] dics = Directory.GetDirectories(path);
            foreach (var dic in dics)
            {
                TreeNode subNode = new TreeNode(new DirectoryInfo(dic).Name); //实例化  
                subNode.Name = new DirectoryInfo(dic).FullName;               //完整目录  
                subNode.Tag = subNode.Name;
                //subNode.ImageIndex = IconIndexes.ClosedFolder;       //获取节点显示图片  
                //subNode.SelectedImageIndex = IconIndexes.OpenFolder; //选择节点显示图片  
                node.Nodes.Add(subNode);
                subNode.Nodes.Add("");
            }
            this.initListBox();
        }

        private void btnsetfolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "选择目标文件夹";
            if (fb.ShowDialog() == DialogResult.OK)
            {
                this.txtfolder.Text = fb.SelectedPath;
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            this.searchFiles();
        }

        private void searchFiles()
        {
            if(this.txtbox.Text == "")
            {
                MessageBox.Show("输入查找的key");
                return;
            }
            if (!Directory.Exists(this.txtfolder.Text))
            {
                MessageBox.Show("文件目录不存在");
            }
            else
            {
                this.startSearch();
            }
        }

        private void initListBox()
        {
            this.listBox.MultiColumn = false;
        }

        private void startSearch()
        {
            this._fileName.Clear();
            this._fileKeyNum.Clear();
            this._page.Clear();
            this.listBox.Items.Clear();
            this._initFolder = this.txtfolder.Text;
            this._targetString = this.txtbox.Text;
            this._isSearching = true;
            this.setBtnState();
            this._currentThread = new Thread(new ParameterizedThreadStart(enterSearch));
            this._currentThread.Start(_initFolder);
        }

        private void enterSearch(object path)
        {
            findFolder(path);
            this.quitSearch();
        }

        private void quitSearch()
        {
            this._isSearching = false;
            this.txtfolder.Text = _initFolder;
            this.setBtnState();
        }

        private void findFolder(object path)
        {
            var files = Directory.GetFiles(path.ToString());
            foreach (var file in files)
            {
                this.readFileAndSearch(file);
            }
            var folders = Directory.GetDirectories(path.ToString());
            foreach (var folder in folders)
            {
                this.txtfolder.Text = folder;
                findFolder(folder);
            }
        }

        private void readFileAndSearch(string filePath)
        {
            StreamReader reader;
            try
            {
                reader = new StreamReader(filePath);
            }
            catch (Exception)
            {
                return;
            }
            var fileinfo = new FileInfo(filePath);
            foreach(var s in FilterFileExtensions.filter)
            {
                if(fileinfo.Extension.ToUpper().IndexOf(s.ToUpper()) != -1)
                {
                    return;
                }
            }
            this.readFile(reader, filePath);
        }

        private void readFile(StreamReader reader, string filePath)
        {
            var cachList = new List<string>();
            List<int> lineNumber = new List<int>();
            List<int> intKeyList = new List<int>();
            int findNum = 0;
            int intKey;
            var readLine = reader.ReadLine();
            var currentLine = 0;
            while (readLine != null)
            {
                currentLine++;
                readLine = readLine.Trim();
                var findIndex = -1;
                if (this.checkbox.CheckState == CheckState.Checked)
                {
                    findIndex = readLine.IndexOf(_targetString);
                }
                else
                {
                    findIndex = readLine.ToUpper().IndexOf(_targetString.ToUpper());
                }
                if (findIndex != -1)
                {
                    findNum++;
                    intKey = keyGener.genKey();
                    intKeyList.Add(intKey);
                    cachList.Add(readLine);
                    lineNumber.Add(currentLine);
                }
                readLine = reader.ReadLine();
            }
            if (findNum > 0)
            {
                this._fileKeyNum.Add(filePath, findNum);
                this.listBox.Items.Add(filePath);
                var tempKey = "";
                for (var k = 0; k < cachList.Count; k++)
                {
                    tempKey = cachList[k];
                    if (this._fileName.ContainsKey(cachList[k]))
                    {
                        tempKey = cachList[k] + getOnlySplitKey() + lineNumber[k] + getOnlySplitKey() + intKeyList[k];
                    }
                    this._page.Add(tempKey, lineNumber[k]);
                    this.listBox.Items.Add("  " + tempKey);
                    this._fileName.Add(tempKey, filePath);
                }
            }
            reader.Close();
        }

        private string getOnlySplitKey()
        {
            return "Θ";
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            var select = (ListBox)sender;
            var item = select.SelectedItem;
            //MessageBox.Show(item.ToString());
            var filePath = item.ToString(); ;
            var par = "";
            if (!this._fileKeyNum.ContainsKey(item.ToString()))
            {
                var key = item.ToString().Trim();
                filePath = this._fileName[key];
                par += " -n" + this._page[key];
            }
            Process p = new Process();
            p.StartInfo.FileName = "notepad++.exe";
            p.StartInfo.Arguments = filePath + par;
            p.Start(); 
        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var str = listBox.GetItemText(listBox.Items[e.Index]);
            bool isSelected = this._fileKeyNum.ContainsKey(str);
            if (e.Index > -1)
            {
                Color color = isSelected ? SystemColors.Highlight : Color.SandyBrown;
                SolidBrush backgroundBrush = new SolidBrush(color);
                SolidBrush textBrush = new SolidBrush(e.ForeColor);

                var eachStringWidth = 6;
                var posStart = 0;
                if (this.checkbox.Checked)
                {
                    posStart = str.IndexOf(txtbox.Text);
                }
                else
                {
                    posStart = str.ToUpper().IndexOf(txtbox.Text.ToUpper());
                }
                var offX = e.Bounds.X + posStart * eachStringWidth;
                var rec = new Rectangle(new Point(offX, e.Bounds.Y), new Size(txtbox.Text.Length * eachStringWidth, e.Bounds.Height));
                if(isSelected)
                {
                    rec = e.Bounds;
                }
                e.Graphics.FillRectangle(backgroundBrush, rec);
                // Draw the text
                e.Graphics.DrawString(str, e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
                // Clean up
                backgroundBrush.Dispose();
                textBrush.Dispose();
            }
            e.DrawFocusRectangle();
        }

        private void btnquit_Click(object sender, EventArgs e)
        {
            if (this._currentThread.IsAlive)
            {
                this._currentThread.Abort();
            }
            this.quitSearch();
        }

        private void setBtnState()
        {
            this.btnsearch.Visible = !this._isSearching;
            this.btnquit.Visible = this._isSearching;
        }

        private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.txtfolder.Text = treeView.SelectedNode.Name;
        }
    }
}
