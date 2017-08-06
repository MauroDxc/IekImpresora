using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Impresora
{
    public partial class passOperator : Form
    {
        private string cx;
        public passOperator()
        {
            InitializeComponent();
            conexionBD cnn = new conexionBD();
            cx = cnn.selectVal("value", "sysparams where name = 'PASS_OP'");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == cx)
            {
                PR.set_2();
                this.Close();
            }
            else
                MessageBox.Show("Contraseña incorrecta", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                button1_Click(sender, e);

            }
        }
    }
}
