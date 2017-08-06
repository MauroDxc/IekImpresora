using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Impresora.Forms
{
    public partial class ProgressDialog : Form
    {
        public Button CancelButton { get { return button1; } }

        public ProgressDialog()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

        }

        public void SetMessage(string message)
        {
            label1.Text = message;
        }

        public void AddMessage(string message)
        {
            listBox1.Items.Add("   " + message);
        }

        public void MarkMessage(int handle)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString().Contains(handle + "]"))
                {
                    listBox1.Items[i] = " * " + listBox1.Items[i];
                }
            }
        }

        public void ShowProgress(int percentage)
        {
            label2.Text = percentage + "%";
            progressBar1.Value = percentage;
            if (TagManager.Instance.rchek == 0)
                button2.BackColor = Button.DefaultBackColor;
        }

        private void btAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ChangeView()
        {
            btAceptar.Visible = true;
            label3.Visible = true;
            pictureBox1.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            progressBar1.Visible = false;
        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TagManager.Instance.rchek = 1;
            button2.BackColor = Color.Green;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

    }
}
