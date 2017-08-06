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
    public partial class cDrives : Form
    {
        conexionBD cnn = new conexionBD();

        public cDrives()
        {
            InitializeComponent();
        }

        private void CheckBoxes_CheckedChanged(object sender, EventArgs e)
        {
            int tag = int.Parse((sender as CheckBox).Name.Replace("ck", "").Replace("ms", ""));
            for (int i = 0; i < AppModule.Instance.DrivesStatus.Count; i++)
            {
                if (AppModule.Instance.DrivesStatus[i].Key == tag)
                {
                    AppModule.Instance.DrivesStatus[i] = new KeyValuePair<int, bool>(tag, (sender as CheckBox).Checked);
                    StringBuilder up_status = new StringBuilder();

                    up_status.Append("val=").Append(AppModule.Instance.DrivesStatus[i].Value ? 1 : 0).Append(" where idx=").Append(AppModule.Instance.DrivesStatus[i].Key.ToString());
                    if (!cnn.insertC("en_drive(idx,val)", AppModule.Instance.DrivesStatus[i].Key + "," + (AppModule.Instance.DrivesStatus[i].Value ? 1 : 0)))
                    {
                        cnn.update("en_drive", up_status.ToString());
                    }
                    up_status.Clear();
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cDrives_Load(object sender, EventArgs e)
        {
            foreach (TabPage t in this.tabControl3.TabPages)
            {
                foreach (Control ck in t.Controls)
                {
                    if (ck.GetType().Equals(typeof(CheckBox)))
                    {
                        CheckBox cb = ck as CheckBox;
                        cb.Checked = AppModule.Instance.DrivesStatus.Select(x => x).Where(w => w.Key == int.Parse(cb.Name.Replace("ck", "").Replace("ms", ""))).First().Value;
                    }
                }
            }
        }

        private void cDrives_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

    }
}
