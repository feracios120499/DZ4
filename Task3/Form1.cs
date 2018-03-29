using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                comboBox1.Items.Add(item.Text);
            }
            if (comboBox1.Items.Count != 0)
                comboBox1.SelectedIndex = 0;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem strip = new ToolStripMenuItem();
            strip.Text = textBox1.Text;
            menuStrip1.Items.Add(strip);
            comboBox1.Items.Clear();
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                comboBox1.Items.Add(item.Text);
            }
            if (comboBox1.Items.Count != 0)
                comboBox1.SelectedIndex = 0;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(ToolStripMenuItem item in menuStrip1.Items)
            {
                if (item.Text == comboBox1.SelectedItem.ToString())
                {
                    ToolStripMenuItem strip = new ToolStripMenuItem();
                    strip.Text = textBox2.Text;
                    item.DropDownItems.Add(strip);
                }
            }
        }
    }
}
