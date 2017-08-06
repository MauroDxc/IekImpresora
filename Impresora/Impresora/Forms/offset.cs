using Impresora.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Threading;

namespace Impresora
{
    public partial class offset : Form
    {
        static int numeroitems = 450;
        static int numeroitems1 = numeroitems + 1;
        List<Text> mOpcTextList;

        public offset()
        {
            InitializeComponent();

            TagManager.Instance.Forms.Add(this);
            TagManager.Instance.AssignControls(this);
            mOpcTextList = TagManager.Instance.TextList;
        }


        private void aplica(NumericUpDown a1, NumericUpDown a2, int id)
        {
            conexionBD cnn = new conexionBD();

            try
            {
                Text t = TagManager.Instance.TextList.Find(x => x.Tag != null && x.Tag.Contains(id + "MS.APOS"));

                int stat = t.Stat;
                double xx = (double)a1.Value;
                double npos_conv = 0;
                double v = 0;

                switch (stat)
                {
                    case 0:
                        v = Math.Round(xx * 1000);
                        a2.Value = a1.Value;
                        a1.Text = a1.Value.ToString();
                        break;
                    case 4:
                        npos_conv = PR.ConvertTo(xx, PR.FxTypes.Feeder);
                        v = Math.Round(npos_conv * 1000);
                        a2.Value = (Decimal)PR.ConvertFrom(npos_conv, PR.FxTypes.Feeder);
                        a1.Text = a1.Value.ToString();
                        break;
                    case 5:
                        npos_conv = PR.ConvertTo(xx, PR.FxTypes.UpperGap);
                        v = Math.Round(npos_conv * 1000);
                        a2.Value = (Decimal)PR.ConvertFrom(npos_conv, PR.FxTypes.UpperGap);
                        a1.Text = a1.Value.ToString();
                        break;
                    case 6:
                        npos_conv = PR.ConvertTo(xx, PR.FxTypes.UpperDiedCut);
                        v = Math.Round(npos_conv * 1000);
                        a2.Value = (Decimal)PR.ConvertFrom(npos_conv, PR.FxTypes.UpperDiedCut);
                        a1.Text = a1.Value.ToString();
                        break;
                    case 7:
                        npos_conv = PR.ConvertTo(xx, PR.FxTypes.LowerDiedCut);
                        v = Math.Round(npos_conv * 1000);
                        a2.Value = (Decimal)PR.ConvertFrom(npos_conv, PR.FxTypes.LowerDiedCut);
                        a1.Text = a1.Value.ToString();
                        break;
                    case 8:
                        npos_conv = PR.ConvertTo(xx, PR.FxTypes.Slotter);
                        v = Math.Round(npos_conv * 1000);
                        a2.Value = (Decimal)PR.ConvertFrom(npos_conv, PR.FxTypes.Slotter);
                        a1.Text = a1.Value.ToString();
                        break;
                    case 9:
                        npos_conv = PR.ConvertTo(xx, PR.FxTypes.Slotter53);
                        v = Math.Round(npos_conv * 1000);
                        a2.Value = (Decimal)PR.ConvertFrom(npos_conv, PR.FxTypes.Slotter53);
                        a1.Text = a1.Value.ToString();
                        break;
                    case 10:
                        npos_conv = PR.ConvertTo(xx, PR.FxTypes.SlotterCab);
                        v = Math.Round(npos_conv * 1000);
                        a2.Value = (Decimal)PR.ConvertFrom(npos_conv, PR.FxTypes.SlotterCab);
                        a1.Text = a1.Value.ToString();
                        break;
                    default:
                        break;
                }
                cnn.update("offset", "offsetcol='" + v + "' where idoffset=" + id);
                t.Offset = v;
                t.ForceChange();
                List<Text> ls = TagManager.Instance.TextList.Select(x => x).Where(x => x.OffsetId == t.OffsetId).ToList();
                foreach (var item in ls)
                {
                    item.Offset = v;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void button24_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown21, numericUpDown27, 10);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown20, numericUpDown26, 11);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button48_Click(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            string s = (numericUpDown19.Value * 1000).ToString();
            if (!cnn.insertC("offset", "11," + s))
            {
                if (cnn.update("offset", "offsetcol='" + s + "' where idoffset=12"))
                {
                    numericUpDown25.Value = numericUpDown19.Value;
                    numericUpDown19.Value = 0;

                }
            }

        }

        private void button31_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown19, numericUpDown25, 12);


        }


        private void button30_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown18, numericUpDown24, 13);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown2, numericUpDown3, 14);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown17, numericUpDown23, 15);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown16, numericUpDown22, 16);
        }

        private void button52_Click(object sender, EventArgs e)
        {

        }

        private void button51_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown47, numericUpDown53, 21);
        }

        private void button61_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown46, numericUpDown52, 22);
        }

        private void button60_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown45, numericUpDown51, 23);
        }

        private void button59_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown29, numericUpDown30, 24);
        }

        private void button58_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown44, numericUpDown50, 25);
        }

        private void button83_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown75, numericUpDown81, 30);
        }

        private void button82_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown74, numericUpDown80, 31);
        }

        private void button90_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown73, numericUpDown79, 32);
        }

        private void button89_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown72, numericUpDown78, 33);
        }

        private void button88_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown56, numericUpDown57, 34);
        }

        private void button87_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown71, numericUpDown77, 35);
        }

        private void button110_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown102, numericUpDown108, 120);
        }

        private void button109_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown101, numericUpDown107, 121);
        }

        private void button117_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown100, numericUpDown106, 122);
        }

        private void button116_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown99, numericUpDown105, 123);
        }

        private void button115_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown83, numericUpDown84, 124);
        }

        private void button114_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown98, numericUpDown104, 125);
        }

        private void button137_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown129, numericUpDown135, 130);
        }

        private void button136_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown128, numericUpDown134, 131);
        }

        private void button144_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown127, numericUpDown133, 132);
        }

        private void button143_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown126, numericUpDown132, 133);
        }

        private void button142_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown110, numericUpDown111, 134);
        }

        private void button141_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown125, numericUpDown131, 135);
        }

        private void button164_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown156, numericUpDown162, 40);
        }

        private void button163_Click(object sender, EventArgs e)
        {
            //aplica(numericUpDown155, numericUpDown161, 41);
        }

        private void button171_Click(object sender, EventArgs e)
        {
            //aplica(numericUpDown154, numericUpDown160, 42);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown221, numericUpDown233, 50);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown220, numericUpDown226, 51);
        }

        private void button63_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown219, numericUpDown225, 52);
        }

        private void button62_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown218, numericUpDown224, 53);
        }

        private void button55_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown191, numericUpDown192, 54);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown217, numericUpDown223, 150);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown216, numericUpDown222, 151);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown199, numericUpDown201, 152);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            aplica(numericUpDown198, numericUpDown200, 153);
        }

        private void offset_Load(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            DataTable dat = cnn.selectFrom("o.idoffset, o.offsetcol, t.status", "offset o inner join tags t on o.idoffset = t.offset_id where t.name like '%MS.APOS'");

            double dvalor;
            decimal valor;
            foreach (DataRow row in dat.Rows)
            {
                double.TryParse(row[1].ToString(), out dvalor);
                dvalor /= 1000;
                int sta = int.Parse(row[2].ToString());
                switch (sta)
                {
                    case 4:
                        dvalor = PR.ConvertFrom(dvalor, PR.FxTypes.Feeder);
                        break;
                    case 5:
                        //dat = Math.Round(dat, 3);
                        dvalor = PR.ConvertFrom(dvalor, PR.FxTypes.UpperGap);
                        break;
                    case 6:
                        //dat = Math.Round(dat, 3);
                        dvalor = PR.ConvertFrom(dvalor, PR.FxTypes.UpperDiedCut);
                        break;
                    case 7:
                        //dat = Math.Round(dat, 3);
                        dvalor = PR.ConvertFrom(dvalor, PR.FxTypes.LowerDiedCut);
                        break;
                    case 8:
                        //dat = Math.Round(dat, 3);
                        dvalor = PR.ConvertFrom(dvalor, PR.FxTypes.Slotter);
                        break;
                    case 9:
                        //dat = Math.Round(dat, 3);
                        dvalor = PR.ConvertFrom(dvalor, PR.FxTypes.Slotter53);
                        break;
                    case 10:
                        //dat = Math.Round(dat, 3);
                        dvalor = PR.ConvertFrom(dvalor, PR.FxTypes.SlotterCab);
                        break;
                    default:
                        break;
                }
                
                valor = (decimal)dvalor;
                switch (int.Parse(row[0].ToString()))
                {
                    case 10:
                        numericUpDown27.Value = valor;
                        break;
                    case 11:
                        numericUpDown26.Value = valor;
                        break;
                    case 12:
                        numericUpDown25.Value = valor;
                        break;
                    case 13:
                        numericUpDown24.Value = valor;
                        break;
                    case 14:
                        numericUpDown3.Value = valor;
                        break;
                    case 15:
                        numericUpDown23.Value = valor;
                        break;
                    case 16:
                        numericUpDown22.Value = valor;
                        break;
                    case 20:
                        numericUpDown54.Value = valor;
                        break;
                    case 21:
                        numericUpDown53.Value = valor;
                        break;
                    case 22:
                        numericUpDown52.Value = valor;
                        break;
                    case 23:
                        numericUpDown51.Value = valor;
                        break;
                    case 24:
                        numericUpDown30.Value = valor;
                        break;
                    case 25:
                        numericUpDown50.Value = valor;
                        break;
                    case 30:

                        numericUpDown81.Value = valor;
                        break;
                    case 31:

                        numericUpDown80.Value = valor;
                        break;
                    case 32:

                        numericUpDown79.Value = valor;
                        break;
                    case 33:

                        numericUpDown78.Value = valor;
                        break;
                    case 34:

                        numericUpDown57.Value = valor;
                        break;
                    case 35:

                        numericUpDown77.Value = valor;
                        break;
                    case 120:

                        numericUpDown108.Value = valor;
                        break;
                    case 121:

                        numericUpDown107.Value = valor;
                        break;
                    case 122:

                        numericUpDown106.Value = valor;
                        break;
                    case 123:

                        numericUpDown105.Value = valor;
                        break;
                    case 124:

                        numericUpDown84.Value = valor;
                        break;
                    case 125:

                        numericUpDown104.Value = valor;
                        break;
                    case 130:

                        numericUpDown135.Value = valor;
                        break;
                    case 131:

                        numericUpDown134.Value = valor;
                        break;
                    case 132:

                        numericUpDown133.Value = valor;
                        break;
                    case 133:

                        numericUpDown132.Value = valor;
                        break;
                    case 134:

                        numericUpDown111.Value = valor;
                        break;
                    case 135:

                        numericUpDown131.Value = valor;
                        break;
                    case 40:

                        numericUpDown162.Value = valor;
                        break;
                    case 41:

                        numericUpDown161.Value = valor;
                        break;
                    case 42:

                        numericUpDown160.Value = valor;
                        break;
                    case 50:

                        numericUpDown233.Value = valor;
                        break;
                    case 51:

                        numericUpDown226.Value = valor;
                        break;
                    case 52:

                        numericUpDown225.Value = valor;
                        break;
                    case 53:

                        numericUpDown224.Value = valor;
                        break;
                    case 54:

                        numericUpDown192.Value = valor;
                        break;
                    case 150:

                        numericUpDown223.Value = valor;
                        break;
                    case 151:

                        numericUpDown222.Value = valor;
                        break;
                    case 152:

                        numericUpDown201.Value = valor;
                        break;
                    case 153:

                        numericUpDown200.Value = valor;
                        break;
                    case 154:

                        numericUpDown78.Value = valor;
                        break;
                    default:
                        break;
                }

            }

        }

        private void offset_FormClosed(object sender, FormClosedEventArgs e)
        {
            TagManager.Instance.Forms.Remove(this);
        }

        private void clear(object sender, EventArgs e)
        {
            var x = sender as NumericUpDown;
            x.Text = "";

        }

        private void xbtHM_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Aplica(b);
            // this.Invoke(new EventHandler(rf));
        }

        private void rf(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            if (TagManager.Instance.TextList[446].Value == 0)
                TagManager.Instance.TextList[446].Value = 1;
            else
                TagManager.Instance.TextList[446].Value = 0;
            actual.escribir(446);
        }

        private void Aplica(Button b)
        {
            foreach (var i in mOpcTextList)
            {
                foreach (var x in i.FormControl)
                {
                    if (x.Value == b.Name)
                    {
                        if (i.Value == 1)
                        {
                            i.Value = 0;
                        }
                        else
                        {
                            i.Value = 1;
                            if (!actual.escribir(i.Handle))
                            {
                                MessageBox.Show("Valor no escrito");
                            }

                        }

                    }
                }
            }
        }

    }
}
















