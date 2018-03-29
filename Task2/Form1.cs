using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        ImageList imageList = new ImageList();
        List<string> stringCopy = new List<string>();
        List<string> stringCut = new List<string>();
        public Form1()
        {
            InitializeComponent();
            FillDriveNodes();
            listView1.SmallImageList = imageList;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
          

        }
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if (dirs.Length != 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if (dirs.Length != 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void FillDriveNodes()
        {
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    TreeNode driveNode = new TreeNode { Text = drive.Name };
                    FillTreeNode(driveNode, drive.Name);
                    treeView1.Nodes.Add(driveNode);
                }
            }
            catch (Exception ex) { }
        }
        // получаем дочерние узлы для определенного узла
        private void FillTreeNode(TreeNode driveNode, string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = dir.Remove(0, dir.LastIndexOf("\\") + 1);
                    driveNode.Nodes.Add(dirNode);
                }
            }
            catch (Exception ex) { }
        }
        private const int SHGFI_ICON = 0x100;
        private const int SHGFI_SMALLICON = 0x1;
        private const int SHGFI_LARGEICON = 0x0;
        private struct SHFILEINFO
        {

            public IntPtr hIcon;

            public int iIcon;

            public uint dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };
        [DllImport("Shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);

        public Image GetImage(string path)
        {
            try
            {
                IntPtr hImgLarge;
                SHFILEINFO shinfo = new SHFILEINFO();

                string FileName = path;

                System.Drawing.Icon myIcon;

                hImgLarge = SHGetFileInfo(FileName, 0, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);

                myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);

                return myIcon.ToBitmap();
            }
            catch
            {
                return null;
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            ShowItems(e.Node.FullPath);
        }
        private void ShowItems(string path)
        {
            listView1.Clear();
            foreach (var item in Directory.GetDirectories(path))
            {
                var listViewItem = listView1.Items.Add(new DirectoryInfo(item).Name);
                string newItem = item.Replace(@"\\", @"\");
                try
                {

                    imageList.Images.Add(newItem, GetImage(newItem));
                    listViewItem.ImageKey = newItem;

                }
                catch { }
            }
            foreach (var item in Directory.GetFiles(path))
            {
                var listViewItem = listView1.Items.Add(new DirectoryInfo(item).Name);
                string newItem = item.Replace(@"\\", @"\");
                try
                {
                    imageList.Images.Add(newItem, GetImage(newItem));
                    listViewItem.ImageKey = newItem;
                }
                catch { }
            }
            listView1.LargeImageList = imageList;
            labelCurrentFolder.Text = new DirectoryInfo(path).Name;
            textBox1.Text = path.Replace(@"\\", @"\");
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = listView1.SelectedItems;
                ListViewItem lvItem = items[0];
                string what = lvItem.Text;
                if (Directory.Exists(textBox1.Text + "\\" + what))
                    ShowItems(textBox1.Text + "\\" + what);
                else
                    Process.Start(textBox1.Text + "\\" + what);
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyValue.ToString());
        }
        

        private void listView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MessageBox.Show(e.KeyValue.ToString());
        }

        private void bunifuImageButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButtonClose_MouseMove(object sender, MouseEventArgs e)
        {
            bunifuImageButtonClose.BackColor = Color.Red;
        }

        private void bunifuImageButtonClose_MouseLeave(object sender, EventArgs e)
        {
            bunifuImageButtonClose.BackColor = Color.Transparent;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text.LastIndexOf('\\') >= 0&&textBox1.TextLength>3)
            {
                if (textBox1.Text.LastIndexOf('\\') == 2)
                {
                    ShowItems(textBox1.Text.Substring(0, textBox1.Text.LastIndexOf('\\')+1));
                }
                else
                {
                    ShowItems(textBox1.Text.Substring(0, textBox1.Text.LastIndexOf('\\')));
                }
            }
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                stringCut.Clear();
                stringCopy.Clear();
                foreach(ListViewItem item in listView1.SelectedItems)
                {
                    if(textBox1.TextLength!=3)
                        stringCopy.Add(textBox1.Text+"\\"+item.Text);
                    else
                        stringCopy.Add(textBox1.Text + item.Text);
                }
                return;
            }
            MessageBox.Show("Выберите файл или папку!");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listView1_Leave(object sender, EventArgs e)
        {

            
        }
        public void CreateFolder(string Name)
        {
            Directory.CreateDirectory(textBox1.Text + "\\" + Name);
            ShowItems(textBox1.Text);
        }

        private void toolStripButtonCreate_Click(object sender, EventArgs e)
        {
            FormCreate form = new FormCreate(this);
            form.ShowDialog();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                foreach(ListViewItem item in listView1.SelectedItems)
                {
                    if (Directory.Exists(textBox1.Text + "\\"+item.Text))
                    {
                        Directory.Delete(textBox1.Text + "\\" + item.Text,true);
                    }
                    else
                    {
                        File.Delete(textBox1.Text + "\\" + item.Text);
                    }
                }
                ShowItems(textBox1.Text);
                return;
            }
            MessageBox.Show("Выберите файл или папку!");
        }

        private void toolStripButtonCut_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                stringCut.Clear();
                stringCopy.Clear();
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    if (textBox1.TextLength != 3)
                        stringCut.Add(textBox1.Text + "\\" + item.Text);
                    else
                        stringCut.Add(textBox1.Text + item.Text);
                }
                return;
            }
            MessageBox.Show("Выберите файл или папку!");
        }

        private void toolStripButtonPaste_Click(object sender, EventArgs e)
        {
            if (stringCopy.Count == 0 && stringCut.Count == 0)
            {
                MessageBox.Show("Буфер обмена пуст!");
                return;
            }
            if (stringCopy.Count > 0)
            {
                foreach(var item in stringCopy)
                {
                    if(Directory.Exists(item))
                        Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(item, textBox1.Text + "\\" + new DirectoryInfo(item).Name);
                    else
                        File.Copy(item, textBox1.Text + "\\" + new DirectoryInfo(item).Name);
                }
                ShowItems(textBox1.Text);
                return;
            }
            if (stringCut.Count > 0)
            {
                foreach(var item in stringCut)
                {
                    if (Directory.Exists(item))
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(item, textBox1.Text + "\\" + new DirectoryInfo(item).Name);
                        Directory.Delete(item,true);
                    }
                    else
                    {
                        File.Copy(item, textBox1.Text + "\\" + new DirectoryInfo(item).Name);
                        File.Delete(item);
                    }
                }
                ShowItems(textBox1.Text);
                return;
            }
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonCreate.PerformClick();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonDelete.PerformClick();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonCopy.PerformClick();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonCut.PerformClick();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonPaste.PerformClick();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void ContextDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonDelete.PerformClick();
        }

        private void ContextCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonCopy.PerformClick();
        }

        private void ContextPasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonPaste.PerformClick();
        }

        private void ContextCutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonCut.PerformClick();
        }
    }
}
