using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files(*.txt) | *.txt";
            dialog.FilterIndex = 0;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxText.Text = File.ReadAllText(dialog.FileName);
                this.Text = dialog.FileName;
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText(this.Text, textBoxText.Text);
        }

        private void toolStripButtonNewDoc_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files(*.txt) | *.txt";
            dialog.FilterIndex = 0;
            dialog.DefaultExt = "txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Text = dialog.FileName;
                textBoxText.Clear();
            }
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            textBoxText.Copy();
        }

        private void toolStripButtonCut_Click(object sender, EventArgs e)
        {
            textBoxText.Cut();
        }

        private void toolStripButtonPaste_Click(object sender, EventArgs e)
        {
            textBoxText.Paste();
        }

        private void toolStripButtonCancel_Click(object sender, EventArgs e)
        {
            textBoxText.Undo();
        }

        private void toolStripButtonColorText_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxText.ForeColor = dialog.Color;
            }
        }

        private void toolStripButtonBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxText.BackColor = dialog.Color;
            }
        }

        private void toolStripButtonFont_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxText.Font = dialog.Font;
            }
        }


        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonOpen.PerformClick();
        }

        private void NewDocДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonNewDoc.PerformClick();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonSave.PerformClick();
        }

        private void SaveAsКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files(*.txt) | *.txt";
            dialog.FilterIndex = 0;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Text = dialog.FileName;
                toolStripButtonSave.PerformClick();
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxText.Copy();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxText.Cut();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxText.Paste();
        }

        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxText.Undo();
        }

        private void SelectAllВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxText.SelectAll();
        }

        private void ColorTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonColorText.PerformClick();
        }

        private void BackColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonBackColor.PerformClick();
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonFont.PerformClick();
        }
    }
}
