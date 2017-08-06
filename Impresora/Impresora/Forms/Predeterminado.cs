using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;


namespace Impresora.Forms
{
    public partial class Predeterminado : Form
    {
        public Predeterminado()
        {
            InitializeComponent();
        }

        private void numericUpDown25_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label77_Click(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void Predeterminado_Load(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            DataTable dataB = cnn.selectFrom("*", "pflauta where idpflauta='B'");
            DataTable dataC = cnn.selectFrom("*", "pflauta where idpflauta='C'");
            DataTable dataBC = cnn.selectFrom("*", "pflauta where idpflauta='BC'");

            foreach (TabPage t in this.tabControl3.TabPages)
            {
                try
                {
                    if (t.Name == "B")
                    {
                        foreach (Control ck in t.Controls)
                        {
                            if (ck.GetType().Equals(typeof(NumericUpDown)))
                            {
                                NumericUpDown nu = ck as NumericUpDown;
                                if(nu.Name.Contains("BA"))
                                   nu.Value = decimal.Parse(dataB.Rows[0][dataC.Columns["V" + nu.Name.Replace("BA", "")].Ordinal].ToString());
                            }
                        }
                    }
                    if (t.Name == "C")
                    {
                        foreach (Control ck in t.Controls)
                        {
                            if (ck.GetType().Equals(typeof(NumericUpDown)))
                            {
                                NumericUpDown nu = ck as NumericUpDown;
                                if (nu.Name.Contains("CA"))
                                    nu.Value = decimal.Parse(dataC.Rows[0][dataC.Columns["V" + nu.Name.Replace("CA", "")].Ordinal].ToString());
                            }
                        }
                    }
                    if (t.Name == "BC")
                    {
                        foreach (Control ck in t.Controls)
                        {
                            if (ck.GetType().Equals(typeof(NumericUpDown)))
                            {
                                NumericUpDown nu = ck as NumericUpDown;
                                if (nu.Name.Contains("BCA"))
                                    nu.Value = decimal.Parse(dataBC.Rows[0][dataC.Columns["V" + nu.Name.Replace("BCA", "")].Ordinal].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void BN122_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown nu = sender as NumericUpDown;
            string flauta = nu.Name.Substring(0,1);
            string var = nu.Name.Replace("BN","");
            conexionBD cnn = new conexionBD();
            if(e.KeyChar==(char)Keys.Enter)
            {
                if (cnn.update("pflauta", "V" + var + "= " + nu.Value.ToString() + " where idpflauta='" + flauta + "'"))
                {
                    NumericUpDown n = Controls.Find("BA" + var, true).FirstOrDefault() as NumericUpDown;
                    n.Value = nu.Value;
                }
                nu.Value = 0;
            }
        }

        private void BN153_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown nu = sender as NumericUpDown;
            string flauta = nu.Name.Substring(0, 1);
            string var = nu.Name.Replace("BN", "");
            conexionBD cnn = new conexionBD();
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (cnn.update("pflauta", "V" + var + "= " + nu.Value.ToString() + " where idpflauta='" + flauta + "'"))
                {
                    NumericUpDown n = Controls.Find("BA" + var, true).FirstOrDefault() as NumericUpDown;
                    n.Value = nu.Value;
                }
                nu.Value = 0;
            }
        
        }

        private void BN122_Enter(object sender, EventArgs e)
        {
            NumericUpDown n = sender as NumericUpDown;
            n.Text = "";
        }

        private void BN153_Enter(object sender, EventArgs e)
        {
            NumericUpDown n = sender as NumericUpDown;
            n.Text = "";
        }

        private void numericUpDown43_Enter(object sender, EventArgs e)
        {
            NumericUpDown n = sender as NumericUpDown;
            n.Text = "";
        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CN122_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown nu = sender as NumericUpDown;
            string flauta = nu.Name.Substring(0, 1);
            string var = nu.Name.Replace("CN", "");
            conexionBD cnn = new conexionBD();
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (cnn.update("pflauta", "V" + var + "= " + nu.Value.ToString() + " where idpflauta='" + flauta + "'"))
                {
                    NumericUpDown n = Controls.Find("CA" + var, true).FirstOrDefault() as NumericUpDown;
                    n.Value = nu.Value;
                }
                nu.Value = 0;
            }
        }

        private void CN153_ValueChanged(object sender, EventArgs e)
        {

        }

        private void BCN122_Enter(object sender, EventArgs e)
        {
            NumericUpDown n = sender as NumericUpDown;
            n.Text = "";
        }

        private void BCN122_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown nu = sender as NumericUpDown;
            string flauta = nu.Name.Substring(0, 2);
            string var = nu.Name.Replace("BCN", "");
            conexionBD cnn = new conexionBD();
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (cnn.update("pflauta", "V" + var + "= " + nu.Value.ToString() + " where idpflauta='" + flauta + "'"))
                {
                    NumericUpDown n = Controls.Find("BCA" + var, true).FirstOrDefault() as NumericUpDown;
                    n.Value = nu.Value;
                }
                nu.Value = 0;
            }
        }


    }
}

