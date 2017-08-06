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
    public partial class cPass : Form
    {
        public cPass()
        {
            InitializeComponent();
            textBox10.Focus();
        }

        private void cPass_Load(object sender, EventArgs e)
        {
            textBox10.Focus();
        }

        private void textBox20_Enter(object sender, EventArgs e)
        {
            (sender as TextBox).Text = "";
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == textBox20.Text && textBox10.Text != "" && textBox10.Text.Length > 3)
            {
                conexionBD cnn = new conexionBD();
                if (cnn.update("sysparams", "value = '" + textBox10.Text + "' where name= 'PASS_OP'"))
                    MessageBox.Show("Contraseña actualizada");
            }
            else
            {
                MessageBox.Show("La contraseña debe tener al menos 4 caracteres y conicidir con la confirmación","Revise nueva contraseña",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (textBox11.Text == textBox19.Text && textBox11.Text != "" && textBox11.Text.Length > 3)
            {
                conexionBD cnn = new conexionBD();
                if(cnn.update("sysparams", "value = '" + textBox11.Text + "' where name= 'PASS_MTTO'"))
                    MessageBox.Show("Contraseña actualizada");
            }
            else
            {
                MessageBox.Show("La contraseña debe tener al menos 4 caracteres", "Revise nueva contraseña", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
