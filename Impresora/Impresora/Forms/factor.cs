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
    public partial class factor : Form
    {
        public factor()
        {
            InitializeComponent();
            TagManager.Instance.Forms.Add(this);
            TagManager.Instance.AssignControls(this);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btMov_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            foreach (var i in TagManager.Instance.TextList)
            {
                foreach (var x in i.FormControl)
                {
                    if (b.Name.StartsWith("btMov") && x.Value == b.Name)
                    {
                        if (i.Value == 0)
                        {
                            int w = int.Parse(b.Name.Replace("btMov", ""));
                            NumericUpDown nu = (NumericUpDown)this.Controls.Find("nUD" + w, true).FirstOrDefault();
                            double d = double.Parse(nu.Value.ToString());
                            conexionBD cnn = new conexionBD();
                            int h = int.Parse(cnn.selectVal("handle", string.Format("tags where offset_id = {0} and name like '%.DESP'", w)));
                            Objects.Text t1 = TagManager.Instance.TextList.Select(p => p).Where(p => p.Handle == h).FirstOrDefault();
                            t1.Value = d * 1000;
                            ////
                            Objects.Text t = TagManager.Instance.TextList.Find(z => z.Tag != null && z.Tag.Contains(w + "MS.APOS"));
                            int stat = t.Stat;
                            double xx = d;
                            double npos_conv = 0;
                            double v = 0;

                            switch (stat)
                            {
                                case 0:
                                    v = Math.Round(xx * 1000);
                                    break;
                                case 4:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.Feeder);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 5:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.UpperGap);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 6:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.UpperDiedCut);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 7:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.LowerDiedCut);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 8:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.Slotter);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 9:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.Slotter53);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 10:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.SlotterCab);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                default:
                                    break;
                            }
                            t1.Value = (v) + t.Value;
                            ////
                            if (actual.escribir(h))
                            {
                                i.Value = 1;
                                actual.escribir(i.Handle);
                            }
                        }
                        else
                        {
                            i.Value = 0;
                            actual.escribir(i.Handle);
                        }
                    }
                    else if (x.Value == b.Name)
                    {
                        if (i.Value == 1)
                        {
                            i.Value = 0;
                        }
                        else
                        {
                            i.Value = 1;
                        }
                        actual.escribir(i.Handle);
                    }

                }
            }
        }

        private void btCal_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            double v;
            foreach (var i in TagManager.Instance.TextList)
            {
                foreach (var x in i.FormControl)
                {
                    if (b.Name.StartsWith("btCal") && x.Value == b.Name)
                    {
                        if (i.Value == 0)
                        {
                            int w = int.Parse(b.Name.Replace("btCal", ""));
                            NumericUpDown nu = (NumericUpDown)this.Controls.Find("nUDesr" + w , true).FirstOrDefault();
                            double xx = double.Parse(nu.Value.ToString());
                            if (xx==0 && MessageBox.Show("El valor real se encuentra en 0; ¿Está seguro de querer calibrarlo?", "Por favor confirme", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) return;
                            conexionBD cnn = new conexionBD();
                            int h_dsr = int.Parse(cnn.selectVal("handle", string.Format("tags where offset_id = {0} and name like '%.DESR'", w)));
                            int stat = int.Parse(cnn.selectVal("status", string.Format("tags where offset_id = {0} and name like '%.DESR'", w)));
                            double npos_conv = 0;
                            Objects.Text t1 = TagManager.Instance.TextList.Select(p => p).Where(p => p.Handle == h_dsr).FirstOrDefault();
                           
                            switch (stat)
                            {
                                case 0:
                                    v = Math.Round(xx * 1000);
                                    break;
                                case 4:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.Feeder);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 5:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.UpperGap);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 6:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.UpperDiedCut);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 7:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.LowerDiedCut);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 8:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.Slotter);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 9:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.Slotter53);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                case 10:
                                    npos_conv = PR.ConvertTo(xx, PR.FxTypes.SlotterCab);
                                    v = Math.Round(npos_conv * 1000);
                                    break;
                                default:
                                    v = Math.Round(xx * 1000);
                                    break;
                            }
                            t1.Value = v;
                            if (actual.escribir(h_dsr))
                            {
                                i.Value = 1;
                                actual.escribir(i.Handle);
                                MessageBox.Show("El factor se ha actualizado por favor revise los limites, velocidad y aceleración para el eje correspondiente", "Recuerde", MessageBoxButtons.YesNo);
                            }
                        }
                        else
                        {
                            i.Value = 0;
                            actual.escribir(i.Handle);
                        }
                    }
                    else if (x.Value == b.Name)
                    {
                        if (i.Value == 1)
                        {
                            i.Value = 0;
                        }
                        else
                        {
                            i.Value = 1;
                        }
                        actual.escribir(i.Handle);
                    }

                }
            }
            
        }

        private void factor_FormClosing(object sender, FormClosingEventArgs e)
        {
            TagManager.Instance.Forms.Remove(this);
        }

        private void nUDesr24_Click(object sender, EventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;
            nu.Text = "";
        }

        private void nUD24_Click(object sender, EventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;
            nu.Text = "";
        }

        private void nUDesr14_Click(object sender, EventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;
            nu.Text = "";
        }

        private void nUDesr30_Click(object sender, EventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;
            nu.Text = "";
        }

        private void nUDesr120_Click(object sender, EventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;
            nu.Text = "";
        }

        private void nUDesr130_Click(object sender, EventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;
            nu.Text = "";
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;
            nu.Text = "";
        }

        private void nUDesr50_Click(object sender, EventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;
            nu.Text = "";
        }
    }
}
