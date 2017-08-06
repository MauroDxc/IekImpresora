using Impresora.Forms;
using Impresora.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Impresora
{
    public delegate void TextEventHandler(object sender, ObjectEventArgs e);
    public delegate void RefreshHandler(object sender, EventArgs e);
    public delegate void TagMovesEventHandler(object sender, EventArgs e);
    public delegate void TagEnDespEventHandler(object sender, EventArgs e);

    public class TagManager
    {
        public event TagMovesEventHandler TagMoves;
        public event TagEnDespEventHandler TagEnDespChanged;

        private int numeroitems1 = 456;
        private System.Windows.Forms.Timer timer4;
        private List<Text> mTextList;
        public List<Text> SyncArray = new List<Text>(43);
        public List<Text> SyncArrayOff = new List<Text>(43);
        private static TagManager mMe = new TagManager();
        public static bool ftime;
        public static bool ftimeCS;
        int total_EN_DESP;
        public static int drives_rdy = 0;
        public static TagManager Instance { get { return mMe; } }
        public List<Text> TextList { get { return mTextList; } }
        public List<Form> Forms { get; set; }
        public bool StartOk { get; set; }
        string tNamePass = "";
        private int nSync = 0;
        public int rchek { get; set; }
        public int Progess { get; set; }
        public string MovingHandle { get; set; }
        public Queue<Text> Queue = new Queue<Text>();


        private TagManager()
        {
            mTextList = new List<Text>();
            Forms = new List<Form>();
            timer4 = new System.Windows.Forms.Timer();
            this.timer4.Interval = 1500;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            MovingHandle = "";
        }

        public void Init()
        {
            try
            {
                DataTable dt = new DataTable();
                conexionBD cx = new conexionBD();

                dt = cx.selectFrom("a.handle,a.name,b.name,c.name,d.offsetcol,a.status,a.limitof,a.offset_id", "tags a left outer join tcf t on a.handle=t.tags_handle left outer join controls b on t.controls_id=b.id left outer join forms c on t.forms_id=c.id left outer join offset d on a.offset_id=d.idoffset where a.name <>''");
                for (int i = 0; i < numeroitems1; i++)
                {
                    mTextList.Add(new Text());
                }
                SyncArray.Clear();
                Queue.Clear();
                TagEnDespChanged = null;
                TagMoves = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int h = int.Parse(dt.Rows[i].ItemArray[0].ToString());
                    int sta, lo, oid;
                    Text t = mTextList[h];
                    t.Tag = dt.Rows[i].ItemArray[1].ToString();
                    int.TryParse(dt.Rows[i].ItemArray[5].ToString(), out sta);
                    t.Stat = sta;
                    int.TryParse(dt.Rows[i].ItemArray[6].ToString(), out lo);
                    t.LimitOf = lo;
                    int.TryParse(dt.Rows[i].ItemArray[7].ToString(), out oid);
                    t.OffsetId = oid;
                    if (t.Tag != null && t.Tag.Contains("EN_DESP") && AppModule.Instance.DrivesStatus.Select(x => x).Where(w => w.Key == t.OffsetId).First().Value)
                    {
                        SyncArray.Add(t);
                    }
                    else if (t.Tag != null && t.Tag.Contains("EN_DESP") && !AppModule.Instance.DrivesStatus.Select(x => x).Where(w => w.Key == t.OffsetId).First().Value)
                    {
                        SyncArrayOff.Add(t);
                    }
                    string ctrl = dt.Rows[i].ItemArray[2].ToString();
                    string frm = dt.Rows[i].ItemArray[3].ToString();
                    if (!string.IsNullOrEmpty(ctrl) && !string.IsNullOrEmpty(frm))
                    {
                        t.FormControl.Add(new KeyValuePair<string, string>(frm, ctrl));
                    }
                    int offset;
                    int.TryParse(dt.Rows[i].ItemArray[4].ToString(), out offset);
                    t.Offset = offset;
                    t.Handle = h;
                    t.isDWord = false;
                    t.Changed += t_Changed;
                }
                AssignControls(Forms.Select(x => x).FirstOrDefault());
            }
            catch (Exception)
            {
                MessageBox.Show("Error de conexión al servidor de bd");
            }

        }

        private void t_Changed(object sender, ObjectEventArgs e)
        {
            Text t = sender as Text;
            Double dat = Double.Parse(t.Value.ToString());
            dat = t.Tag.Contains("FACTOR") ? Math.Round(dat / 1000, 3) : Math.Round((dat + t.Offset) / 1000, 3);
            if (StartOk && t.Tag.Contains("APOS") && t.LimitOf != 0)
            {
                string slim = "MS.LM_" + (t.LimitOf > 0 ? "P" : "N");
                Text t1 = mTextList.Select(w => w).Where(w => w.Tag != null && w.Tag.Contains(Math.Abs(t.LimitOf) + slim)).FirstOrDefault();
                try
                {
                    double fact_intro = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "INTROFAC").First().Value.ToString());
                    double fact_ran = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "RANFAC").First().Value.ToString());
                    double nlimit;
                    double R12_15 = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "R12_15").First().Value.ToString());
                    double R13_16 = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "R13_16").First().Value.ToString());
                    double R15_12 = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "R15_12").First().Value.ToString());
                    double R16_13 = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "R16_13").First().Value.ToString());
                    double R150_151 = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "R150_151").First().Value.ToString());
                    double R151_150 = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "R151_150").First().Value.ToString());
                    double R152_153 = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "R152_153").First().Value.ToString());
                    double R153_152 = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "R153_152").First().Value.ToString());

                    switch (t.OffsetId)
                    {
                        case 12:
                            nlimit = Math.Round((t.Value * fact_intro) + R12_15, 0);
                            t1.Value = nlimit;
                            break;
                        case 13:
                            nlimit = Math.Round((t.Value * fact_intro) + R13_16, 0);
                            t1.Value = nlimit;
                            break;
                        case 15:
                            nlimit = Math.Round((t.Value / fact_intro) + R15_12, 0);
                            t1.Value = nlimit;
                            break;
                        case 16:
                            nlimit = Math.Round((t.Value / fact_intro) + R16_13, 0);
                            t1.Value = nlimit;
                            break;
                        case 150:
                            nlimit = Math.Round((t.Value * fact_ran) + R150_151, 0);
                            t1.Value = nlimit;
                            break;
                        case 151:
                            nlimit = Math.Round((t.Value / fact_ran) + R151_150, 0);
                            t1.Value = nlimit;
                            break;
                        case 152:
                            nlimit = Math.Round((t.Value / fact_ran) + R152_153, 0);
                            t1.Value = nlimit;
                            break;
                        case 153:
                            nlimit = Math.Round((t.Value * fact_ran) + R153_152, 0);
                            t1.Value = nlimit;
                            break;
                        default:
                            t1.Value = t.Value;
                            break;
                    }
                    actual.escribir(t1.Handle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Se encontró un error inesperado, contacte a su proveedor de servicios");
                    System.Diagnostics.Trace.TraceError("IEK5170 " + ex.Message);
                }
            }
            if (t.Tag.Contains("EN_DESP") && TagEnDespChanged != null)
            {
                TagEnDespChanged(t, null);
            }
            if (t.Tag.Contains("ALARMA_CODE"))
            {
                conexionBD cnn = new conexionBD();
                if (t.Value != 0)
                {
                    if (t.Tag != tNamePass || (t.Tag == tNamePass && PR.alarmP))
                    {
                        string ida = cnn.getNextId("alarmas");
                        string ida1 = cnn.getNextId("historico");
                        string q = "{0},'{1}',(SELECT offset_id FROM tags where handle={2}),{3}";
                        DateTime dt = DateTime.Now;
                        cnn.insertC("alarmas", string.Format(q, ida, dt.ToString("yyyy-MM-dd HH:mm:ss"), t.Handle, t.Value));
                        cnn.insertC("historico", string.Format(q, ida1, dt.ToString("yyyy-MM-dd HH:mm:ss"), t.Handle, t.Value));
                        tNamePass = t.Tag;
                        PR.alarmP = false;
                        timer4.Enabled = true;
                    }



                }
            }

            foreach (var i in t.FormControl)
            {
                Form f = Forms.Find(x => x.Name == i.Key);
                try
                {

                    if (f != null)
                    {
                        Control cl = f.Controls.Find(i.Value, true).FirstOrDefault();
                        string targetProp = "";
                        object val = null;
                        if (i.Value == "timer5" && f.GetType() == typeof(actual))
                        {
                            ((actual)f).Timer5.Enabled = t.Value != 0;
                        }
                        if (cl != null)
                        {
                            if (cl.GetType().Equals(typeof(NumericUpDown)))
                            {
                                switch (t.Stat)
                                {
                                    case 4:
                                        dat = Math.Round(dat, 3);
                                        val = (Decimal)PR.ConvertFrom(dat, PR.FxTypes.Feeder);
                                        break;
                                    case 5:
                                        dat = Math.Round(dat, 3);
                                        val = (Decimal)PR.ConvertFrom(dat, PR.FxTypes.UpperGap);
                                        break;
                                    case 6:
                                        dat = Math.Round(dat, 3);
                                        val = (Decimal)PR.ConvertFrom(dat, PR.FxTypes.UpperDiedCut);
                                        break;
                                    case 7:
                                        dat = Math.Round(dat, 3);
                                        val = (Decimal)PR.ConvertFrom(dat, PR.FxTypes.LowerDiedCut);
                                        break;
                                    case 8:
                                        dat = Math.Round(dat, 3);
                                        val = (Decimal)PR.ConvertFrom(dat, PR.FxTypes.Slotter);
                                        break;
                                    case 9:
                                        dat = Math.Round(dat, 3);
                                        val = (Decimal)PR.ConvertFrom(dat, PR.FxTypes.Slotter53);
                                        break;
                                    case 10:
                                        dat = Math.Round(dat, 3);
                                        val = (Decimal)PR.ConvertFrom(dat, PR.FxTypes.SlotterCab);
                                        break;

                                    default:
                                        val = (decimal)dat;
                                        break;
                                }
                                //if (t.Stat == 5)
                                //{
                                //    Double d = Double.Parse(t.Value.ToString());
                                //    dat = Math.Round(dat, 3);
                                //    val = (Decimal)PR.ConvertFrom(dat, PR.FxTypes.UpperGap);
                                //}
                                //else
                                //{
                                //    val = (decimal)dat;
                                //}
                                targetProp = "Value";
                            }
                            if (cl.GetType().Equals(typeof(TextBox))) { targetProp = "Text"; val = dat.ToString(); }
                            if (cl.GetType().Equals(typeof(Button)))
                            {
                                if (t.Value == 1)
                                {
                                    cl.BackColor = System.Drawing.Color.LimeGreen;
                                }
                                else
                                {
                                    cl.BackColor = Control.DefaultBackColor;
                                }
                                continue;
                            }
                            PropertyInfo[] ps = cl.GetType().GetProperties();
                            PropertyInfo p = ps.Select(x => x).Where(w => w.Name == targetProp).FirstOrDefault();
                            p.SetValue(cl, val, null);
                        }
                    }
                }
                catch (Exception ee)
                {
                    conexionBD cx = new conexionBD();
                    cx.inBitacora(ee.Message, "0", t.Handle);
                }

                if (actual.loading)
                {
                    if (ftime)
                    {
                        total_EN_DESP = SyncArray.Select(x => x).Where(x => x.Value == 1).ToArray().Count();
                        ftime = false;
                        drives_rdy = 0;
                    }
                    //CheckSyncW();
                }


            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            PR.alarmP = true;
            timer4.Enabled = false;
        }

        public void AssignControls(Form f)
        {
            if (f != null)
            {
                foreach (Text t in TextList)
                {
                    foreach (var i in t.FormControl)
                    {
                        if (i.Key == f.Name)
                        {
                            t_Changed(t, null);
                        }
                    }
                }
            }
        }

        int drives_rdyCount = 0;
        int w_pre = 0;

        public int CheckSyncW()
        {
            int syncCount = int.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(z => z.Key.Equals("MAXSYNC_DRIVES")).First().Value.ToString());
            int w = SyncArray.Select(x => x).Where(x => x.Value == 1).ToArray().Count();


            if (ftimeCS)
            {
                drives_rdy = 0;
                for (int i = 0; i < syncCount; i++)
                {
                    MovingHandle = SyncArray[drives_rdy + i].Tag;
                    actual.escribir(SyncArray[drives_rdy + i].Handle);
                    if (TagMoves != null)
                    {
                        TagMoves(SyncArray[drives_rdy + i], null);
                    }
                    drives_rdyCount = i;
                }
                ftimeCS = false;
                nSync++;

            }

            if (w == w_pre - 1)
            {
                drives_rdy = drives_rdyCount;
                for (int i = 0; i < syncCount && i + drives_rdy < SyncArray.Count; i++)
                {
                    MovingHandle = SyncArray[drives_rdy + i].Tag;
                    actual.escribir(SyncArray[drives_rdy + i].Handle);
                    if (TagMoves != null)
                    {
                        TagMoves(SyncArray[drives_rdy + i], null);
                    }
                    drives_rdyCount++;
                }
                ftimeCS = false;
                nSync++;
            }
            else if (w == 0)
            {
                drives_rdy = 0;
                actual.loading = false;
                drives_rdyCount = 0;
            }
            else if (w == w_pre && rchek == 1)
            {
                int drivesim = 0;
                for (int i = 0; i < TagManager.Instance.SyncArray.Count && drivesim < syncCount; i++)
                {
                    if (TagManager.Instance.SyncArray[i].Value == 1)
                    {
                        MovingHandle = SyncArray[i].Tag;
                        actual.escribir(SyncArray[i].Handle);
                        if (TagMoves != null && drives_rdy + i < SyncArray.Count)
                        {
                            TagMoves(SyncArray[drives_rdy + i], null);
                        }
                        drivesim++;
                    }

                }
                rchek = 0;
            }
            w_pre = w;

            int prgss = (int)((double)(SyncArray.Count - w) / SyncArray.Count * 100);
            if (prgss > Progess)
            {
                Progess = prgss;
                return prgss;
            }
            else
            {
                return 0;
            }

        }

        public void InitSyncZ()
        {
            int syncCount = int.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(z => z.Key.Equals("MAXSYNC_DRIVES")).First().Value.ToString());
            var s = SyncArray.Select(x => x).Where(x => x.Value == 1).ToArray();
            foreach (var item in s)
            {
                item.Changed += (object sender, ObjectEventArgs e) =>
                {
                    Text t = sender as Text;
                    if (t.Value == 0 && Queue.Count > 0)
                    {
                        Text tt = Queue.Dequeue();
                        actual.escribir(tt.Handle);
                        if (TagMoves != null)
                        {
                            TagMoves(tt, null);
                        }
                    }
                };
                Queue.Enqueue(item);
            }
            for (int i = 0; i < syncCount; i++)
            {
                Text t = Queue.Dequeue();
                actual.escribir(t.Handle);
                if (TagMoves != null)
                {
                    TagMoves(t, null);
                }
            }
        }


    }


}
