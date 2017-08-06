
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Management;
using MySql;
using MySql.Data;
using OPCAutomation;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using Impresora.Objects;
using System.Reflection;
using Impresora.Forms;
using System.Threading.Tasks;



namespace Impresora
{
    public partial class actual : Form
    {
        [DllImport("user32.dll")]
        private static extern int FindWindow(string lpszClassName, string lpszWindowName);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hWnd, int nCmdShow);
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        String PersonalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Starts Here */
        // Structure contain information about low-level keyboard input event 
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }
        //System level functions to be used for hook and unhook keyboard input  
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);
        //Declaring Global objects     
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;

        public Timer Timer5 { get { return timer5; } }

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                // Disabling Windows keys 

                if (objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin || objKeyInfo.key == Keys.Tab && HasAltModifier(objKeyInfo.flags) || objKeyInfo.key == Keys.Escape && (ModifierKeys & Keys.Control) == Keys.Control || objKeyInfo.key == Keys.F4 && HasAltModifier(objKeyInfo.flags))
                //if (objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin  || objKeyInfo.key == Keys.Escape && (ModifierKeys & Keys.Control) == Keys.Control || objKeyInfo.key == Keys.F4 && HasAltModifier(objKeyInfo.flags))
                {
                    return (IntPtr)0; // if 0 is returned then All the above keys will be enabled  
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        bool HasAltModifier(int flags)
        {
            return (flags & 0x20) == 0x20;
        }

        /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Ends Here */


        string path;
        public static bool loading = false;
        static int numeroitems = 455;
        static int numeroitems1 = numeroitems + 1;
        OPCServer servidorOpc;
        public static OPCGroup grupoConectado;

        String[] opcIdItems = new String[numeroitems1];
        Int32[] clientHandles = new Int32[numeroitems1];
        public static Array itemServerHandles;
        static int actual_array_size = numeroitems + 1;
        Object[] opcItemNames = new Object[numeroitems1];
        public static Control[] opcItemValueToWritte = new Control[numeroitems1];
        public static List<Text> mOpcTextList;
        Object[] opcItemButtonToWritte = new Object[numeroitems1];
        Object[] opcItemActiveState = new Control[numeroitems1];
        Control[] opcItemQuality = new Control[numeroitems1];
        public static int[] OPCItemIsArray = new int[numeroitems1];
        static int lsigno = 7;
        int[] signo = new int[lsigno];
        private ProgressDialog mProgressDialog;
        private int mWorkProgress = 0;
        public int WorkProgress
        {
            get { return mWorkProgress; }
            set
            {
                mWorkProgress = value;
                mProgressDialog.ShowProgress(value);
            }
        }



        enum Limit
        {
            P, N
        }
        private Box mBox = new Box();


        public actual()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            path = PersonalFolder + "\\Kinex\\Cajas\\";
            escribirFichero("s");
            timer3.Enabled = true;
            TagManager.Instance.Forms.Add(this);
            TagManager.Instance.Init();
            mOpcTextList = TagManager.Instance.TextList.Where(x => x.Tag != "").ToList();
            while (mOpcTextList.Count < numeroitems1)
            {
                mOpcTextList.Add(new Objects.Text());
            }

            DateTime tiempo = new DateTime();
            DateTimeFormatInfo myDTFI = new CultureInfo("es-MX", false).DateTimeFormat;

            tiempo = DateTime.Now;
            label8.Text = tiempo.ToString("D", myDTFI);
            label8.Location = new Point((363 - label8.Width) / 2, 314);
            tiempo = DateTime.Now;
            label3.Text = tiempo.ToString(" h:mm:ss tt");
            label3.Location = new Point((313 - label13.Width) / 2, 348);
            String nombreOPCServer = "KEPware.KEPServerEx.V4";
            textBox1.CharacterCasing = CharacterCasing.Upper;
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox4.CharacterCasing = CharacterCasing.Upper;
            textBox5.CharacterCasing = CharacterCasing.Upper;
            textBox6.CharacterCasing = CharacterCasing.Upper;
            textBox7.CharacterCasing = CharacterCasing.Upper;
            textBox8.CharacterCasing = CharacterCasing.Upper;
            textBox9.CharacterCasing = CharacterCasing.Upper;
            textBox1.CharacterCasing = CharacterCasing.Upper;


            try
            {
                servidorOpc = new OPCAutomation.OPCServer();
                servidorOpc.Connect(nombreOPCServer, "");
            }

            catch (Exception ex)
            {
                servidorOpc = null;
                button1.Enabled = false;
                MessageBox.Show("OPC server connect failed with exception: " +
                    ex.Message, "OPCInterface Exception", MessageBoxButtons.OK);
            }

            try
            {
                servidorOpc.OPCGroups.DefaultGroupIsActive = true;
                servidorOpc.OPCGroups.DefaultGroupDeadband = 0;
                grupoConectado = servidorOpc.OPCGroups.Add("G1");
                grupoConectado.UpdateRate = 250;
                grupoConectado.IsSubscribed = true;
                grupoConectado.DataChange += grupoConectado_DataChange;


            }

            catch (Exception ex)
            {
                MessageBox.Show("OPC server fallo al agregar grupo con: " + ex.Message, "Conexion Exception", MessageBoxButtons.OK);
            }

            try
            {
                int ItemCount = numeroitems;
                Array addItemServerErrors;
                Array tagList = mOpcTextList.Select(x => x.Tag).ToArray();
                Array handleList = mOpcTextList.Select(x => x.Handle).ToArray();
                //grupoConectado.OPCItems.DefaultIsActive = true;
                grupoConectado.OPCItems.AddItems(ItemCount, tagList, handleList, out itemServerHandles, out addItemServerErrors);

                bool itemgood;
                itemgood = false;
                for (int i = 1; i <= numeroitems; i++)
                {
                    Int32 ab = (Int32)addItemServerErrors.GetValue(i);
                    if (ab == 0)
                    {
                        itemgood = true;

                    }
                }
                if (!itemgood)
                {
                    MessageBox.Show("OPC server no encontro ningun item con el nombre espeficado ", "Conexion Exception", MessageBoxButtons.OK);
                }

                toolStripStatusLabel4.Text = "Enlace de red en línea";
            }
            catch (Exception ex)
            {
                MessageBox.Show("OPC server fallo al agregar los items solicitados al grupo con: " + ex.Message, "Conexion Exception", MessageBoxButtons.OK);
            }


            //conexionBD cnn = new conexionBD();

            //string clave1 = cnn.selectPass("1");
            //string clave2 = cnn.selectPass("2");
            //string maestra = clave1 + clave2;
            //string a1 = "", a2 = "";
            //int f1 = 0;
            //Char[] ch = { ' ' };
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_BIOS");
            //ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("select * from " + "Win32_DiskDrive");

            //foreach (ManagementObject share in searcher.Get())
            //{
            //    foreach (PropertyData PC in share.Properties)
            //    {
            //        if (PC.Name == "SerialNumber")
            //            a1 = PC.Value.ToString();
            //    }
            //}

            //foreach (ManagementObject share1 in searcher1.Get())
            //{
            //    foreach (PropertyData PC in share1.Properties)
            //    {
            //        if (PC.Name == "Caption")
            //        {
            //            if (PC.Value.ToString() == "ST310005 24AS SATA Disk Device")
            //                f1 = 1;
            //        }

            //        if (PC.Name == "SerialNumber" && f1 == 1)
            //        {
            //            a2 = PC.Value.ToString();
            //            a2 = a2.TrimEnd(ch);

            //            f1 = 0;
            //        }
            //    }
            //}
            //DateTime fc = new DateTime(2016, 02, 15);
            //DateTime rg = DateTime.Now;

            //if ((maestra == (a1 + a2)))
            //{
            //    if ((rg < fc))
            //    {
            //        f1 = 0;
            //        //MessageBox.Show("Licencia Valida");
            //    }
            //    else
            //    {
                    
                 
            //        cnn.delete("licence");
                 


            //        if (System.IO.File.Exists(@"C:\Users\Public\impiekv5_3s\OPF\lenze_kepserverV4_2.opf"))
            //        {
            //            try
            //            {
            //                System.IO.File.Delete(@"C:\Users\Public\impiekv5_3s\OPF\lenze_kepserverV4_2.opf");
            //            }
            //            catch (System.IO.IOException e)
            //            {
            //            }
            //        }


            //        MessageBox.Show("Sentimos las inconveniencias, un archivo se ha dañado o no se encuentra, contacte a su proveedor de servicios. \n\nIngeniería Electrónica Kinex S.A. de C.V. \nwww.iekinex.com.mx \nRef. IMP_IEK_V5_4", "Error 05297", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            //        this.Close();
            //        return;
            //    }
            //}
            //else
            //{
            //    //        cnn.delete("tcf");
            //    //        cnn.delete("tags");
            //    //        cnn.delete("licence");
            //    //        cnn.delete("controls");
            //    //        cnn.delete("forms");


            //    //        if (System.IO.File.Exists(@"C:\Users\Public\impIEK_V5\Data\lenze_kepserverV4_2.opf"))
            //    //        {
            //    //            try
            //    //            {
            //    //                System.IO.File.Delete(@"C:\Users\Public\impIEK_V5\Data\lenze_kepserverV4_2.opf");
            //    //            }
            //    //            catch (System.IO.IOException e)
            //    //            {
            //    //            }
            //    //        }
            //    MessageBox.Show("Sentimos las inconveniencias, un archivo se ha dañado o no se encuentra, contacte a su proveedor de servicios. \n\nIngeniería Electrónica Kinex S.A. de C.V. \nwww.iekinex.com.mx \nRef. IMP_IEK_V5_4", "Error 05298", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            //    this.Close();
            //}
        }
        




        /////////////////////////////////////////////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\




        private void Aplica(int id, double v, Limit lim)
        {
            conexionBD cnn = new conexionBD();
            string s = v.ToString();
            string q1 = "offset (idoffset,{0})";
            string q2 = id + "," + s;
            string q3 = "{0}='{1}' where idoffset={2}";

            if (lim == Limit.N)
            {
                q1 = string.Format(q1, "LN");
                q3 = string.Format(q3, "LN", s, id);
            }
            else
            {
                q1 = string.Format(q1, "LP");
                q3 = string.Format(q3, "LP", s, id);
            }
            if (!cnn.insertC(q1, q2))
            {
                if (cnn.update("offset", q3))
                {

                }
            }
        }




        private void escribirFichero(string texto)
        {

            try
            {
                //si no existe la carpeta temporal la creamos 
                if (!(Directory.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                }

                if (Directory.Exists(path))
                {
                    //Creamos el fichero temporal y 
                    //añadimos el texto pasado como parámetro 
                    //System.IO.StreamWriter ficheroTemporal =
                    //    new System.IO.StreamWriter(rutaFichero);
                    //ficheroTemporal.WriteLine(texto);
                    //ficheroTemporal.Close();
                }
            }
            catch (Exception errorC)
            {
                MessageBox.Show("No se puede crear la carpeta" + errorC.ToString());
            }
        }

        private void label1_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox10.Text = dat.ToString();
            numericUpDown53.Value = (Decimal)dat;


        }

        private void label2_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox11.Text = dat.ToString();
            numericUpDown82.Value = (Decimal)dat;
        }

        private void label3_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox12.Text = dat.ToString();
            numericUpDown112.Value = (Decimal)dat;

        }
        private void label4_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox13.Text = dat.ToString();
            numericUpDown142.Value = (Decimal)dat;

        }
        private void label5_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox14.Text = dat.ToString();
            numericUpDown172.Value = (Decimal)dat;

        }
        private void label6_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox2.Text = dat.ToString();
            numericUpDown202.Value = (Decimal)dat;

        }
        private void label7_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox26.Text = dat.ToString();


        }
        private void label8_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox25.Text = dat.ToString();

        }
        private void label9_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox24.Text = dat.ToString();

        }
        private void label10_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox23.Text = dat.ToString();

        }
        private void label11_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox22.Text = dat.ToString();

        }
        private void label12_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            textBox21.Text = dat.ToString();

        }

        private void label19_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',20," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',20," + info.Text);
                timer5.Enabled = true;
            }

        }
        private void label20_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',30," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',30," + info.Text);
                timer5.Enabled = true;
            }

        }
        private void label21_TextChanged(Object sender, EventArgs e)
        {

            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',120," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',120," + info.Text);
                timer5.Enabled = true;
            }

        }
        private void label22_TextChanged(Object sender, EventArgs e)
        {

            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',130," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',130," + info.Text);
                timer5.Enabled = true;
            }

        }
        private void label23_TextChanged(Object sender, EventArgs e)
        {

            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',40," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',40," + info.Text);
                timer5.Enabled = true;
            }

        }
        private void label24_TextChanged(Object sender, EventArgs e)
        {

            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',50," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',50," + info.Text);
                timer5.Enabled = true;
            }
        }

        private void label39_TextChanged(Object sender, EventArgs e)
        {

            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',12," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',12," + info.Text);
                timer5.Enabled = true;
            }
        }

        private void label40_TextChanged(Object sender, EventArgs e)
        {

            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',13," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',13," + info.Text);
                timer5.Enabled = true;
            }
        }

        private void label41_TextChanged(Object sender, EventArgs e)
        {

            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',15," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',15," + info.Text);
                timer5.Enabled = true;
            }
        }

        private void label42_TextChanged(Object sender, EventArgs e)
        {

            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',16," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',16," + info.Text);
                timer5.Enabled = true;
            }
        }

        double fact_intro = 1.042116713;

        private void label31_TextChanged(Object sender, EventArgs e)
        {
            Label info = (Label)sender;
            if (info.Text != "")
            {
                double nlimit = Math.Round((long.Parse(info.Text) * fact_intro) - 24000, 0);
                mOpcTextList[37].Value = nlimit;
                escribir(37);
            }

        }
        private void label32_TextChanged(Object sender, EventArgs e)
        {

            Label info = (Label)sender;
            if (info.Text != "")
            {
                double nlimit = Math.Round((long.Parse(info.Text) * fact_intro) - 50000, 0);
                mOpcTextList[38].Value = nlimit;
                escribir(38);
            }

        }
        private void label33_TextChanged(Object sender, EventArgs e)
        {
            Label info = (Label)sender;
            if (info.Text != "")
            {
                double nlimit = Math.Round((long.Parse(info.Text) / fact_intro) + 24000, 0);
                mOpcTextList[35].Value = nlimit;
                escribir(35);
            }

        }
        private void label34_TextChanged(Object sender, EventArgs e)
        {
            Label info = (Label)sender;
            if (info.Text != "")
            {
                double nlimit = Math.Round((long.Parse(info.Text) / fact_intro) + 50000, 0);
                mOpcTextList[36].Value = nlimit;
                escribir(36);
            }
        }
        private void label43_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',14," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',14," + info.Text);
                timer5.Enabled = true;
            }

        }
        private void label44_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',21," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',21," + info.Text);
                timer5.Enabled = true;
            }

        }
        private void label45_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',22," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',22," + info.Text);
                timer5.Enabled = true;
            }

        }
        private void label46_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',23," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',23," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label47_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',24," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',24," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label48_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',25," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',25," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label49_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',31," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',31," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label50_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',32," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',32," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label51_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',33," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',33," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label52_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',34," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',34," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label53_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',35," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',35," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label54_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',121," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',121," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label55_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',122," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',122," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label56_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',123," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',123," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label57_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',124," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',124," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label58_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',125," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',125," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label59_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',131," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',131," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label60_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',132," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',132," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label61_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',133," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',133," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label62_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',134," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',134," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label63_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',135," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',135," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label64_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',41," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',41," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label65_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',42," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',42," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label66_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',51," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',51," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label67_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',52," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',52," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label68_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',53," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',53," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label69_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',54," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',54," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label70_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',150," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',150," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label71_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',151," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',151," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label72_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',152," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',152," + info.Text);
                timer5.Enabled = true;
            }
        }
        private void label73_TextChanged(Object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            Label info = (Label)sender;
            if (info.Text != "0")
            {
                string ida = cnn.getNextId("alarmas");
                string ida1 = cnn.getNextId("historico");
                DateTime dt = DateTime.Now;
                cnn.insertC("alarmas", "'" + ida + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',153," + info.Text);
                cnn.insertC("historico", "'" + ida1 + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',153," + info.Text);
                timer5.Enabled = true;
            }
        }

        private void label139_TextChanged(Object sender, EventArgs e)
        {


            Label info = (Label)sender;
            if (info.Text != "")
            {
                double nlimit = Math.Round((long.Parse(info.Text) * fact_intro) - 24000, 0);
                mOpcTextList[145].Value = nlimit;
                escribir(145);
            }

        }
        private void label140_TextChanged(Object sender, EventArgs e)
        {

            Label info = (Label)sender;
            if (info.Text != "")
            {
                double nlimit = Math.Round((long.Parse(info.Text) * fact_intro) - 59000, 0);
                mOpcTextList[146].Value = nlimit;
                escribir(146);
            }

        }

        private void label141_TextChanged(Object sender, EventArgs e)
        {
            Label info = (Label)sender;
            if (info.Text != "")
            {
                double nlimit = Math.Round((long.Parse(info.Text) / fact_intro) + 24000, 0);
                mOpcTextList[143].Value = nlimit;
                escribir(143);
            }

        }
        private void label142_TextChanged(Object sender, EventArgs e)
        {
            Label info = (Label)sender;
            if (info.Text != "")
            {
                double nlimit = Math.Round((long.Parse(info.Text) / fact_intro) + 59000, 0);
                mOpcTextList[144].Value = nlimit;
                escribir(144);
            }
        }

        private void label146_TextChanged(Object sender, EventArgs e)
        {

            //Label d = sender as Label;
            //Double dat = Double.Parse(d.Text.ToString());
            //dat = Math.Round(dat / 1000, 3);
            //numericUpDown7.Value = (Decimal)dat;

        }

        private void label147_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown19.Value = (Decimal)dat;

        }
        private void label148_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown26.Value = (Decimal)dat;

        }
        private void label149_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown17.Value = (Decimal)dat;

        }
        private void label150_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown25.Value = (Decimal)dat;

        }
        private void label35_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown24.Value = (Decimal)dat;

        }
        private void label36_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown23.Value = (Decimal)dat;

        }
        private void label151_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown16.Value = (Decimal)dat;

        }
        private void label152_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown15.Value = (Decimal)dat;

        }
        private void label153_TextChanged(Object sender, EventArgs e)
        {
            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown14.Value = (Decimal)dat;

        }
        private void label37_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown13.Value = (Decimal)dat;

        }
        private void label38_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown12.Value = (Decimal)dat;

        }
        private void label154_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown22.Value = (Decimal)dat;

        }
        private void label155_TextChanged(Object sender, EventArgs e)
        {
            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown21.Value = (Decimal)dat;
        }
        private void label156_TextChanged(Object sender, EventArgs e)
        {
            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown20.Value = (Decimal)dat;
        }
        private void label157_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown46.Value = (Decimal)dat;

        }
        private void label158_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown45.Value = (Decimal)dat;

        }
        private void label159_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown44.Value = (Decimal)dat;

        }
        private void label160_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown43.Value = (Decimal)dat;

        }
        private void label161_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown42.Value = (Decimal)dat;

        }
        private void label162_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown41.Value = (Decimal)dat;

        }
        private void label163_TextChanged(Object sender, EventArgs e)
        {
            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown11.Value = (Decimal)dat;

        }
        private void label164_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown52.Value = (Decimal)dat;

        }
        private void label165_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown51.Value = (Decimal)dat;

        }
        private void label166_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown50.Value = (Decimal)dat;

        }
        private void label167_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown49.Value = (Decimal)dat;

        }
        private void label168_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown48.Value = (Decimal)dat;

        }
        private void label169_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown75.Value = (Decimal)dat;

        }
        private void label170_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown68.Value = (Decimal)dat;

        }
        private void label171_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown61.Value = (Decimal)dat;

        }
        private void label172_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown54.Value = (Decimal)dat;

        }
        private void label173_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown47.Value = (Decimal)dat;

        }
        private void label174_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown111.Value = (Decimal)dat;

        }
        private void label175_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown110.Value = (Decimal)dat;

        }
        private void label176_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown109.Value = (Decimal)dat;

        }
        private void label177_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown108.Value = (Decimal)dat;

        }
        private void label178_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown107.Value = (Decimal)dat;

        }
        private void label179_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown141.Value = (Decimal)dat;

        }
        private void label180_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown140.Value = (Decimal)dat;

        }
        private void label181_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown139.Value = (Decimal)dat;

        }
        private void label182_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown138.Value = (Decimal)dat;

        }
        private void label183_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown137.Value = (Decimal)dat;

        }
        private void label184_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown171.Value = (Decimal)dat;

        }
        private void label185_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown170.Value = (Decimal)dat;

        }
        private void label186_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown201.Value = (Decimal)dat;

        }
        private void label187_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown200.Value = (Decimal)dat;

        }
        private void label188_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown199.Value = (Decimal)dat;

        }
        private void label189_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown198.Value = (Decimal)dat;

        }
        private void label190_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown197.Value = (Decimal)dat;

        }
        private void label191_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown168.Value = (Decimal)dat;

        }
        private void label192_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown167.Value = (Decimal)dat;

        }
        private void label193_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown186.Value = (Decimal)dat;

        }
        private void label194_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown80.Value = (Decimal)dat;

        }
        private void label195_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown73.Value = (Decimal)dat;

        }
        private void label196_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown79.Value = (Decimal)dat;

        }
        private void label197_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown72.Value = (Decimal)dat;

        }
        private void label198_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown78.Value = (Decimal)dat;

        }
        private void label199_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown71.Value = (Decimal)dat;

        }
        private void label200_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown77.Value = (Decimal)dat;

        }
        private void label201_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown70.Value = (Decimal)dat;

        }
        private void label202_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown76.Value = (Decimal)dat;

        }
        private void label203_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown69.Value = (Decimal)dat;

        }
        private void label204_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown105.Value = (Decimal)dat;

        }
        private void label205_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown99.Value = (Decimal)dat;

        }
        private void label206_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown104.Value = (Decimal)dat;

        }

        private void label207_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown98.Value = (Decimal)dat;

        }
        private void label208_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown103.Value = (Decimal)dat;

        }
        private void label209_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown97.Value = (Decimal)dat;

        }
        private void label210_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown96.Value = (Decimal)dat;

        }
        private void label211_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown102.Value = (Decimal)dat;

        }
        private void label212_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown101.Value = (Decimal)dat;

        }
        private void label213_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown95.Value = (Decimal)dat;

        }
        private void label214_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown135.Value = (Decimal)dat;

        }
        private void label215_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown129.Value = (Decimal)dat;

        }
        private void label216_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown134.Value = (Decimal)dat;

        }
        private void label217_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown128.Value = (Decimal)dat;

        }
        private void label218_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown133.Value = (Decimal)dat;

        }
        private void label219_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown127.Value = (Decimal)dat;

        }
        private void label220_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown132.Value = (Decimal)dat;

        }
        private void label221_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown126.Value = (Decimal)dat;

        }
        private void label222_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown131.Value = (Decimal)dat;

        }
        private void label223_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown125.Value = (Decimal)dat;

        }
        private void label224_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown165.Value = (Decimal)dat;

        }
        private void label225_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown159.Value = (Decimal)dat;

        }
        private void label226_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown164.Value = (Decimal)dat;

        }
        private void label227_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown158.Value = (Decimal)dat;

        }
        private void label228_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown163.Value = (Decimal)dat;

        }
        private void label229_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown157.Value = (Decimal)dat;

        }
        private void label230_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown162.Value = (Decimal)dat;

        }
        private void label231_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown156.Value = (Decimal)dat;

        }
        private void label232_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown161.Value = (Decimal)dat;

        }
        private void label233_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown155.Value = (Decimal)dat;

        }
        private void label234_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown195.Value = (Decimal)dat;

        }
        private void label235_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown189.Value = (Decimal)dat;

        }
        private void label236_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown194.Value = (Decimal)dat;

        }

        private void label237_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown188.Value = (Decimal)dat;

        }
        private void label238_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown225.Value = (Decimal)dat;

        }
        private void label239_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown219.Value = (Decimal)dat;

        }
        private void label240_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown224.Value = (Decimal)dat;

        }
        private void label241_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown218.Value = (Decimal)dat;

        }
        private void label242_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown223.Value = (Decimal)dat;

        }
        private void label243_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown217.Value = (Decimal)dat;

        }
        private void label244_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown193.Value = (Decimal)dat;

        }
        private void label245_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown192.Value = (Decimal)dat;

        }
        private void label246_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown222.Value = (Decimal)dat;

        }
        private void label247_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown216.Value = (Decimal)dat;

        }
        private void label248_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown221.Value = (Decimal)dat;

        }
        private void label249_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown215.Value = (Decimal)dat;

        }
        private void label250_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown185.Value = (Decimal)dat;

        }
        private void label251_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown180.Value = (Decimal)dat;

        }
        private void label252_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown181.Value = (Decimal)dat;

        }
        private void label253_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown179.Value = (Decimal)dat;

        }
        private void label254_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown74.Value = (Decimal)dat;

        }
        private void label255_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown100.Value = (Decimal)dat;

        }
        private void label256_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown130.Value = (Decimal)dat;

        }
        private void label257_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown160.Value = (Decimal)dat;

        }
        private void label258_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown190.Value = (Decimal)dat;

        }
        private void label259_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round(dat / 1000, 3);
            numericUpDown220.Value = (Decimal)dat;

        }
        //private void label260_TextChanged(Object sender, EventArgs e)
        //{

        //    Label d = sender as Label;
        //    Double dat = Double.Parse(d.Text.ToString());
        //    dat = Math.Round(dat / 1000, 3);
        //    numericUpDown23.Value = (Decimal)dat;

        //}
        //private void label261_TextChanged(Object sender, EventArgs e)
        //{

        //    Label d = sender as Label;
        //    Double dat = Double.Parse(d.Text.ToString());
        //    dat = Math.Round(dat / 1000, 3);
        //    numericUpDown23.Value = (Decimal)dat;

        //}
        //private void label262_TextChanged(Object sender, EventArgs e)
        //{

        //    Label d = sender as Label;
        //    Double dat = Double.Parse(d.Text.ToString());
        //    dat = Math.Round(dat / 1000, 3);
        //    numericUpDown23.Value = (Decimal)dat;

        //}
        //private void label263_TextChanged(Object sender, EventArgs e)
        //{

        //    Label d = sender as Label;
        //    Double dat = Double.Parse(d.Text.ToString());
        //    dat = Math.Round(dat / 1000, 3);
        //    numericUpDown23.Value = (Decimal)dat;

        //}
        //private void label264_TextChanged(Object sender, EventArgs e)
        //{

        //    Label d = sender as Label;
        //    Double dat = Double.Parse(d.Text.ToString());
        //    dat = Math.Round(dat / 1000, 3);
        //    numericUpDown23.Value = (Decimal)dat;

        //}
        //private void label265_TextChanged(Object sender, EventArgs e)
        //{

        //    Label d = sender as Label;
        //    Double dat = Double.Parse(d.Text.ToString());
        //    dat = Math.Round(dat / 1000, 3);
        //    numericUpDown23.Value = (Decimal)dat;

        //}
        //private void label266_TextChanged(Object sender, EventArgs e)
        //{

        //    Label d = sender as Label;
        //    Double dat = Double.Parse(d.Text.ToString());
        //    dat = Math.Round(dat / 1000, 3);
        //    numericUpDown23.Value = (Decimal)dat;

        //}
        private void full_maximize(object sender, EventArgs e)
        {
            // First, Hide the taskbar

            int hWnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hWnd, SW_HIDE);

            // Then, format and size the window. 
            // Important: Borderstyle -must- be first, 
            // if placed after the sizing functions, 
            // it'll strangely firm up the taskbar distance.

            FormBorderStyle = FormBorderStyle.None;
            this.Location = new Point(0, 0);
            this.WindowState = FormWindowState.Maximized;

            //        The following is optional, but worth knowing.

            //        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //        this.TopMost = true;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Important, re-show Show Window, you know, you should probably
            // Also add a handler if the app is minimized, or loses focus, otherwise
            // Your users will be taskbarless.

            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    break;
                case CloseReason.ApplicationExitCall:
                    if ((servidorOpc != null))
                    {
                        try
                        {
                            //Disconnect from the server, This should only occur after the items and group
                            // have been removed
                            servidorOpc.Disconnect();
                        }
                        catch (Exception ex)
                        {
                            // Error handling
                            MessageBox.Show("OPC server fallo al desconectarse con excepcion: " + ex.Message, "Conexion Exception", MessageBoxButtons.OK);
                        }
                        finally
                        {
                            // Release the old instance of the OPC Server object and allow the resources
                            // to be freed
                            servidorOpc = null;
                            // Allow a reconnect once the disconnect completes      
                        }
                    }
                    Application.Exit();
                    break;
            }


            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_SHOW);

        }

        public static void ToggleTaskManager(bool toggle)
        {
            Microsoft.Win32.RegistryKey HKCU = Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey key = HKCU.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            key.SetValue("DisableTaskMgr", toggle ? 0 : 1, Microsoft.Win32.RegistryValueKind.DWord);
        }

        private void actual_Load(object sender, EventArgs e)
        {
            TagManager.Instance.StartOk = false;

            full_maximize(sender, e);
            button33.BackColor = Button.DefaultBackColor;
            timer2.Enabled = false;
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
            for (int a = 0; a < lsigno; a++)
            {
                signo[a] = 1;
            }
            String nombreOPCServer = "KEPware.KEPServerEx.V4";
            try
            {
                servidorOpc = new OPCAutomation.OPCServer();
                servidorOpc.Connect(nombreOPCServer, "");
                //Button1.Enabled = false;
                //Button2.Enabled = true;
                //Button3.Enabled = true;
                toolStripStatusLabel1.Text = "Conectado con servidor KINEX";
            }
            catch (Exception ex)
            {
                //Button2.Enabled = false;
                servidorOpc = null;
                //Button1.Enabled = true;
                MessageBox.Show("OPC server connect failed with exception: " +
                    ex.Message, "OPCInterface Exception", MessageBoxButtons.OK);
            }



            cargaUsuarios(comboBox1);
            cargaClientes(comboBox2);

            carga(comboBox6, "nombre", "resistencia");
            carga(comboBox3, "idflauta", "flauta");
            carga(comboBox8, "color where idcolor != 1");
            carga(comboBox5, "idoperardor,nombre", "operador");
            carga(comboBox4, "idcaja,clave", "caja");
            carga(comboBox7, "idcliente,nombre", "cliente");

            try
            {
                conexionBD cx = new conexionBD();
                numericUpDown227.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='40ms.pressure.protection'"));
                numericUpDown235.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='R12_15'"));
                numericUpDown228.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='R13_16'"));
                numericUpDown229.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='R15_12'"));
                numericUpDown230.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='R16_13'"));
                numericUpDown234.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='R150_151'"));
                numericUpDown233.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='R151_150'"));
                numericUpDown232.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='R152_153'"));
                numericUpDown231.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='R153_152'"));
                numericUpDown236.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='INTROFAC'"));
                numericUpDown237.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='RANFAC'"));
                numericUpDown237.Value = decimal.Parse(cx.selectVal("value", "sysparams where name='MAXSYNC_DRIVES'"));
            }
            catch (Exception exc)
            {

                MessageBox.Show("Ocurrio un error:" + exc.Message);
            }



            if (rkApp.GetValue("Impresora.exe") == null)
            {
                // The value doesn't exist, the application is not set to run at startup
                checkBox1.Checked = false;
            }
            else
            {
                // The value exists, the application is set to run at startup
                checkBox1.Checked = true;
            }

            TagManager.Instance.StartOk = true;
            foreach (var item in TagManager.Instance.TextList.Select(x => x).Where(x => x != null && x.LimitOf != 0))
            {
                item.ForceChange();
            }
        }

        private void grupoConectado_DataChange(int TransactionID, int NumItems, ref System.Array ClientHandles, ref System.Array ItemValues, ref System.Array Qualities, ref System.Array TimeStamps)
        {
            // We don't have error handling here since this is an event called from the OPC interface
            try
            {

                for (int i = 1; i <= NumItems; i++)
                {
                    // Use the 'Clienthandles' array returned by the server to pull out the
                    // index number of the control to update and load the value.
                    Type a = ItemValues.GetValue(i).GetType();
                    if (a.IsArray)
                    {
                        Array ItsAnArray = null;
                        int x = 0;
                        string Suffix = null;

                        ItsAnArray = (Array)ItemValues.GetValue(i);

                        // Store the size of array for use by sync write
                        OPCItemIsArray[int.Parse(ClientHandles.GetValue(i).ToString())] = ItsAnArray.GetUpperBound(0) + 1;
                        mOpcTextList[int.Parse(ClientHandles.GetValue(i).ToString())].Value = 0;
                        for (x = ItsAnArray.GetLowerBound(0); x <= ItsAnArray.GetUpperBound(0); x++)
                        {
                            if (x == ItsAnArray.GetUpperBound(0))
                            {
                                Suffix = "";
                            }
                            else
                            {
                                Suffix = ", ";
                            }


                            mOpcTextList[int.Parse(ClientHandles.GetValue(i).ToString())].Value = double.Parse(mOpcTextList[int.Parse(ClientHandles.GetValue(i).ToString())].Value + ItsAnArray.GetValue(x).ToString() + Suffix);


                        }
                    }
                    else
                    {
                        int ii = (int)ClientHandles.GetValue(i);
                        mOpcTextList[ii].Value = double.Parse(ItemValues.GetValue(i).ToString());

                    }

                    // Check the Qualties for each item retured here.  The actual contents of the
                    // quality field can contain bit field data which can provide specific
                    // error conditions.  Normally if everything is OK then the quality will
                    // contain the 0xC0
                    int good = (int)OPCAutomation.OPCQuality.OPCQualityGood;


                    if (!Qualities.GetValue(i).Equals(good))
                    {
                        //MessageBox.Show("Vuelve intentar datos inciertos", "Alarma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        toolStripStatusLabel4.Text = "Error de comunicación, datos corruptos";
                    }

                }
            }
            catch (Exception ex)
            {
                // Error handling
                MessageBox.Show("OPC DataChange failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
            }

            try
            {
                if (mOpcTextList[13].Value == 1)
                {
                    label45.ForeColor = Color.Lime;
                    label71.ForeColor = Color.Lime;
                    label54.ForeColor = Color.Lime;
                    label61.ForeColor = Color.Lime;
                    textBox26.BackColor = Color.Lime;
                    textBox10.BackColor = Color.Lime;
                    textBox20.BackColor = Color.Lime;
                    button34.BackColor = Color.Lime;
                    button34.Enabled = false;
                    textBox20.Enabled = false;
                }
                else
                {
                    label45.ForeColor = Color.MidnightBlue;
                    label71.ForeColor = Color.MidnightBlue;
                    label54.ForeColor = Color.MidnightBlue;
                    label61.ForeColor = Color.MidnightBlue;
                    textBox26.BackColor = TextBox.DefaultBackColor;
                    textBox10.BackColor = TextBox.DefaultBackColor;
                    textBox20.BackColor = TextBox.DefaultBackColor;
                    button34.BackColor = Button.DefaultBackColor;
                    button34.Enabled = true;
                    textBox20.Enabled = true;
                }

                if (mOpcTextList[14].Value == 1)
                {
                    label46.ForeColor = Color.Lime;
                    label70.ForeColor = Color.Lime;
                    label55.ForeColor = Color.Lime;
                    label60.ForeColor = Color.Lime;
                    textBox25.BackColor = Color.Lime;
                    textBox11.BackColor = Color.Lime;
                    textBox19.BackColor = Color.Lime;
                    button35.BackColor = Color.Lime;
                    button35.Enabled = false;
                    textBox19.Enabled = false;
                }
                else
                {
                    label46.ForeColor = Color.MidnightBlue;
                    label70.ForeColor = Color.MidnightBlue;
                    label55.ForeColor = Color.MidnightBlue;
                    label60.ForeColor = Color.MidnightBlue;
                    textBox25.BackColor = TextBox.DefaultBackColor;
                    textBox11.BackColor = TextBox.DefaultBackColor;
                    textBox19.BackColor = TextBox.DefaultBackColor;
                    button35.BackColor = Button.DefaultBackColor;
                    button35.Enabled = true;
                    textBox19.Enabled = true;
                }

                if (mOpcTextList[15].Value == 1)
                {
                    label47.ForeColor = Color.Lime;
                    label69.ForeColor = Color.Lime;
                    label56.ForeColor = Color.Lime;
                    label62.ForeColor = Color.Lime;
                    textBox24.BackColor = Color.Lime;
                    textBox12.BackColor = Color.Lime;
                    textBox18.BackColor = Color.Lime;
                    button36.BackColor = Color.Lime;
                    button36.Enabled = false;
                    textBox18.Enabled = false;
                }
                else
                {
                    label47.ForeColor = Color.MidnightBlue;
                    label69.ForeColor = Color.MidnightBlue;
                    label56.ForeColor = Color.MidnightBlue;
                    label62.ForeColor = Color.MidnightBlue;
                    textBox24.BackColor = TextBox.DefaultBackColor;
                    textBox12.BackColor = TextBox.DefaultBackColor;
                    textBox18.BackColor = TextBox.DefaultBackColor;
                    button36.BackColor = Button.DefaultBackColor;
                    button36.Enabled = true;
                    textBox18.Enabled = true;
                }

                if (mOpcTextList[16].Value == 1)
                {
                    label48.ForeColor = Color.Lime;
                    label68.ForeColor = Color.Lime;
                    label57.ForeColor = Color.Lime;
                    label63.ForeColor = Color.Lime;
                    textBox23.BackColor = Color.Lime;
                    textBox13.BackColor = Color.Lime;
                    textBox17.BackColor = Color.Lime;
                    button37.BackColor = Color.Lime;
                    button37.Enabled = false;
                    textBox17.Enabled = false;
                }
                else
                {
                    label48.ForeColor = Color.MidnightBlue;
                    label68.ForeColor = Color.MidnightBlue;
                    label57.ForeColor = Color.MidnightBlue;
                    label63.ForeColor = Color.MidnightBlue;
                    textBox23.BackColor = TextBox.DefaultBackColor;
                    textBox13.BackColor = TextBox.DefaultBackColor;
                    textBox17.BackColor = TextBox.DefaultBackColor;
                    button37.BackColor = Button.DefaultBackColor;
                    button37.Enabled = true;
                    textBox17.Enabled = true;
                }

                if (mOpcTextList[17].Value == 1)
                {
                    label49.ForeColor = Color.Lime;
                    label67.ForeColor = Color.Lime;
                    label58.ForeColor = Color.Lime;
                    label64.ForeColor = Color.Lime;
                    textBox22.BackColor = Color.Lime;
                    textBox14.BackColor = Color.Lime;
                    textBox16.BackColor = Color.Lime;
                    button38.BackColor = Color.Lime;
                    button38.Enabled = false;
                    textBox16.Enabled = false;
                }
                else
                {
                    label49.ForeColor = Color.MidnightBlue;
                    label67.ForeColor = Color.MidnightBlue;
                    label58.ForeColor = Color.MidnightBlue;
                    label64.ForeColor = Color.MidnightBlue;
                    textBox22.BackColor = TextBox.DefaultBackColor;
                    textBox14.BackColor = TextBox.DefaultBackColor;
                    textBox16.BackColor = TextBox.DefaultBackColor;
                    button38.BackColor = Button.DefaultBackColor;
                    button38.Enabled = true;
                    textBox16.Enabled = true;
                }

                if (mOpcTextList[18].Value == 1)
                {
                    label51.ForeColor = Color.Lime;
                    label66.ForeColor = Color.Lime;
                    label59.ForeColor = Color.Lime;
                    label65.ForeColor = Color.Lime;
                    textBox21.BackColor = Color.Lime;
                    textBox2.BackColor = Color.Lime;
                    textBox15.BackColor = Color.Lime;
                    button39.BackColor = Color.Lime;
                    button39.Enabled = false;
                    textBox15.Enabled = false;
                }
                else
                {
                    label51.ForeColor = Color.MidnightBlue;
                    label66.ForeColor = Color.MidnightBlue;
                    label59.ForeColor = Color.MidnightBlue;
                    label65.ForeColor = Color.MidnightBlue;
                    textBox21.BackColor = TextBox.DefaultBackColor;
                    textBox2.BackColor = TextBox.DefaultBackColor;
                    textBox15.BackColor = TextBox.DefaultBackColor;
                    button39.BackColor = Button.DefaultBackColor;
                    button39.Enabled = true;
                    textBox15.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                // Error handling
                MessageBox.Show("OPC Labels failed with exception: " + ex.Message, "Exception", MessageBoxButtons.OK);
            }

        }

        public static bool LoadArray(ref System.Array AnArray, int CanonDT, string wrTxt)
        {
            int ii = 0;
            int loc = 0;
            int Wlen = 0;
            int start = 0;

            try
            {
                start = 1;
                Wlen = wrTxt.Length;
                for (ii = AnArray.GetLowerBound(0); ii <= AnArray.GetUpperBound(0); ii++)
                {
                    loc = wrTxt.IndexOf(",", 0);
                    if (ii < AnArray.GetUpperBound(0))
                    {
                        if (loc == 0)
                        {
                            MessageBox.Show("Valor escrito: Numero incorrecto de digitos para el tamaño del arreglo?", "Error de argumento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }
                    else
                    {
                        loc = Wlen + 1;
                    }

                    switch (CanonDT)
                    {
                        case (int)CanonicalDataTypes.CanonDtByte:
                            AnArray.SetValue(Convert.ToByte((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtChar:
                            AnArray.SetValue(Convert.ToSByte((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case


                        case (int)CanonicalDataTypes.CanonDtWord:
                            AnArray.SetValue(Convert.ToUInt16((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtShort:
                            AnArray.SetValue(Convert.ToInt16((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtDWord:
                            AnArray.SetValue(Convert.ToInt32((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtLong:
                            AnArray.SetValue(Convert.ToInt32((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtFloat:
                            AnArray.SetValue(Convert.ToSingle((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtDouble:
                            AnArray.SetValue(Convert.ToDouble((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtBool:
                            AnArray.SetValue(Convert.ToBoolean((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtString:
                            AnArray.SetValue(Convert.ToString((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        default:
                            MessageBox.Show("El tipo de valor que se intenta escribir es desconocido", "Error de argumento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                    }

                    start = loc + 1;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Al intentar escribir el vaor se genero la siguiente excepción: " + ex.Message, "Excepción de OPC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public enum CanonicalDataTypes
        {
            CanonDtByte = 17,
            CanonDtChar = 16,
            CanonDtWord = 18,
            CanonDtShort = 2,
            CanonDtDWord = 19,
            CanonDtLong = 3,
            CanonDtFloat = 4,
            CanonDtDouble = 5,
            CanonDtBool = 11,
            CanonDtString = 8,
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Form a = new PrimerColor();
            a.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form a = new modulo1("Introductor");
            a.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (numericUpDown8.Value == 130464)
            {
                timer2.Enabled = false;
                if ((servidorOpc != null))
                {
                    try
                    {
                        //Disconnect from the server, This should only occur after the items and group
                        // have been removed
                        servidorOpc.Disconnect();
                    }
                    catch (Exception ex)
                    {
                        // Error handling
                        MessageBox.Show("OPC server fallo al desconectarse con excepcion: " + ex.Message, "Conexion Exception", MessageBoxButtons.OK);
                    }
                    finally
                    {
                        // Release the old instance of the OPC Server object and allow the resources
                        // to be freed
                        servidorOpc = null;
                        // Allow a reconnect once the disconnect completes      
                    }

                }

                this.Dispose();
                Application.Exit();

            }
            else
            {
                if (MessageBox.Show(" ¿ Estas seguro que deseas apagar ?", "El sistema se apagará", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    if ((servidorOpc != null))
                    {
                        try
                        {
                            //Disconnect from the server, This should only occur after the items and group
                            // have been removed
                            servidorOpc.Disconnect();
                        }
                        catch (Exception ex)
                        {
                            // Error handling
                            MessageBox.Show("OPC server fallo al desconectarse con excepcion: " + ex.Message, "Conexion Exception", MessageBoxButtons.OK);
                        }
                        finally
                        {
                            // Release the old instance of the OPC Server object and allow the resources
                            // to be freed
                            servidorOpc = null;
                            // Allow a reconnect once the disconnect completes      
                        }

                    }

                    this.Dispose();
                    Process proceso = new Process();
                    proceso.StartInfo.UseShellExecute = false;
                    proceso.StartInfo.RedirectStandardOutput = true;
                    proceso.StartInfo.FileName = "shutdown.exe";
                    proceso.StartInfo.Arguments = "/p";
                    proceso.Start();

                }
            }

            // Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            double maxt = 7776000;
            DateTime tiempo = new DateTime();
            DateTimeFormatInfo myDTFI = new CultureInfo("es-MX", false).DateTimeFormat;
            tiempo = DateTime.Now;
            label8.Text = tiempo.ToString("D", myDTFI);
            label8.Location = new Point((355 - label8.Width) / 2, 358);
            tiempo = DateTime.Now;
            label3.Text = tiempo.ToString(" h:mm:ss tt");
            label3.Location = new Point((305 - label13.Width) / 2, 401);
            //DateTime fc = new DateTime(2015, 04, 30, 07, 15, 09);
            //double sec = double.Parse(cnn.selectVal("descripcion", "al_code where idal_code = 50"));
            //sec = sec + 1;
            //    if ((tiempo < fc) && (sec < maxt))
            //    {
            //        //MessageBox.Show("Licencia Valida");
            //        cnn.update("al_code", "descripcion ='" + sec + "' where idal_code = 50");
            //    }
            //    else
            //    {

            //        cnn.delete("tcf");
            //        cnn.delete("tags");
            //        cnn.delete("licence");
            //        cnn.delete("controls");
            //        cnn.delete("forms");


            //        if (System.IO.File.Exists(@"C:\Users\Public\impIEK_V5\Data\lenze_kepserverV4_2.opf"))
            //        {
            //            try
            //            {
            //                System.IO.File.Delete(@"C:\Users\Public\impIEK_V5\Data\lenze_kepserverV4_2.opf");
            //            }
            //            catch (System.IO.IOException em)
            //            {
            //            }
            //        }
            //        timer1.Enabled = false;
            //        MessageBox.Show("Sentimos las inconveniencias, la licencia a expirado o un archivo se ha deñado, contacte a su proveedor de servicios. \n\nIngeniería Electrónica Kinex S.A. de C.V. \nwww.iekinex.com.mx \nRef. IMP_IEK_V5_1", "Error 00097", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        this.Close();
            //        throw new System.Exception();
            //    }

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Paro de Emergencia", "Paro Total", MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK)
            { this.Enabled = true; }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            Form ncaja = new nuevaCaja();
            ncaja.ShowDialog();

        }

        private void actual_KeyPress(object sender, KeyPressEventArgs e)
        {

        }



        private void button23_Click(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            string id = cnn.getNextId("operador");
            if (cnn.insert("operador", id + ",'" + textBox3.Text + "'"))
            {
                textBox3.Text = "";
                cargaUsuarios(comboBox1);
                cargaUsuarios(comboBox5);
                button23.Enabled = false;
            }


        }

        private void cargaUsuarios(ComboBox cb)
        {
            cb.Items.Clear();
            try
            {
                conexionBD con = new conexionBD();
                DataTable Reader;
                Reader = con.selectFrom("nombre", "operador");
                string sResultado = "";
                //Reader.
                for (int i = 0; i < Reader.Rows.Count; i++)
                {
                    sResultado = Reader.Rows[i][0].ToString();
                    cb.Items.Add(sResultado);
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("Error al conectar a servidor de datos");
            }
        }

        private void cargaClientes(ComboBox cb)
        {
            cb.Items.Clear();
            try
            {
                conexionBD con = new conexionBD();
                DataTable Reader;
                Reader = con.selectFrom("nombre", "cliente");
                string sResultado = "";
                //Reader.
                for (int i = 0; i < Reader.Rows.Count; i++)
                {
                    sResultado = Reader.Rows[i][0].ToString();
                    cb.Items.Add(sResultado);
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("Error al conectar a servidor de datos");
            }
        }

        private void carga(ComboBox cb, string atributo, string tabla)
        {
            cb.Items.Clear();
            try
            {
                conexionBD con = new conexionBD();
                DataTable Reader;
                Reader = con.selectFrom(atributo, tabla);
                for (int i = 0; i < Reader.Rows.Count; i++)
                {
                    if (cb.Name == "comboBox4" || cb.Name == "comboBox5" || cb.Name == "comboBox7")
                    {
                        KeyValuePair<int, string> kv = new KeyValuePair<int, string>(int.Parse(Reader.Rows[i][0].ToString()), Reader.Rows[i][1].ToString());
                        cb.Items.Add(kv);
                    }
                    else
                    {
                        cb.Items.Add(Reader.Rows[i][0].ToString());
                    }
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("Error al conectar a servidor de datos");
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                conexionBD cnn = new conexionBD();
                if (cnn.deleteFrom("operador", "nombre = '" + comboBox1.Text + "'"))
                    comboBox1.Text = "";

                cargaUsuarios(comboBox1);
                cargaUsuarios(comboBox5);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            string id = cnn.getNextId("cliente");
            if (cnn.insert("cliente", id + ",'" + textBox4.Text + "'"))
            {
                textBox4.Text = "";
                cargaClientes(comboBox2);
                cargaClientes(comboBox7);
                button26.Enabled = false;
            }


        }

        private void carga(ComboBox cb, string tabla)
        {
            cb.Items.Clear();
            try
            {
                conexionBD con = new conexionBD();
                DataTable Reader;
                Reader = con.selectFrom("*", tabla);
                string sResultado = "";
                //Reader.
                for (int i = 0; i < Reader.Rows.Count; i++)
                {
                    sResultado = Reader.Rows[i][1].ToString() + " " + Reader.Rows[i][2].ToString() + " " + Reader.Rows[i][3].ToString();
                    cb.Items.Add(sResultado);
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("Error al conectar a servidor de datos");
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                conexionBD cnn = new conexionBD();
                if (cnn.deleteFrom("cliente", "nombre = '" + comboBox2.Text + "'"))
                    comboBox2.Text = "";

                cargaClientes(comboBox2);
                cargaClientes(comboBox7);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();

            if (textBox5.Text != "")
            {
                if (cnn.busca("idflauta", "flauta", textBox5.Text) == "")
                {
                    cnn.insert("flauta", "'" + textBox5.Text + "','" + numericUpDown1.Value + "'");
                    carga(comboBox3, "idflauta", "flauta");
                    textBox5.Text = "";
                    button28.Enabled = false;
                    numericUpDown1.Value = 0;
                    numericUpDown1.Enabled = false;
                }
                else
                {
                    MessageBox.Show("La clave de flauta que se intenta dar de alta ya existe, intenta con otra o primero borra la existente.", "Intenta de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox5.Text = "";
                    button28.Enabled = false;
                    numericUpDown1.Value = 0;
                    numericUpDown1.Enabled = false;
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = true;

        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                conexionBD cnn = new conexionBD();
                if (cnn.deleteFrom("flauta", "idflauta = '" + comboBox3.Text + "'"))
                    comboBox3.Text = "";
                carga(comboBox3, "idflauta", "flauta");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            string id = cnn.getNextId("resistencia");
            if (textBox6.Text != "")
            {
                if (cnn.busca("nombre", "resistencia", textBox6.Text) == "")
                {
                    cnn.insert("resistencia", id + ",'" + textBox6.Text + "','0','0','0','0','0','0','0','0'");

                    carga(comboBox6, "nombre", "resistencia");
                    textBox6.Text = "";
                    button30.Enabled = false;
                    numericUpDown2.Value = 0;
                    numericUpDown2.Enabled = false;
                    numericUpDown3.Value = 0;
                    numericUpDown3.Enabled = false;
                    numericUpDown4.Value = 0;
                    numericUpDown4.Enabled = false;
                    numericUpDown5.Value = 0;
                    numericUpDown5.Enabled = false;
                    numericUpDown6.Value = 0;
                    numericUpDown6.Enabled = false;
                    numericUpDown7.Value = 0;
                    numericUpDown7.Enabled = false;
                    numericUpDown9.Value = 0;
                    numericUpDown9.Enabled = false;
                    numericUpDown10.Value = 0;
                    numericUpDown10.Enabled = false;
                }
                else
                {
                    MessageBox.Show("La clave para la resistencia que se intenta dar de alta ya existe, intenta con otra o primero borra la existente.", "Intenta de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox5.Text = "";
                    button28.Enabled = false;
                    numericUpDown1.Value = 0;
                    numericUpDown1.Enabled = false;
                }
            }
        }

        public static bool escribir(int index)
        {
            if (index == 17 && mOpcTextList[index].Value.ToString() != "0")
            {
                Text tx = TagManager.Instance.TextList.Select(x => x).Where(x => x != null && x.Handle == 185).FirstOrDefault();
                double trest = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "40ms.pressure.protection").First().Value.ToString());
                if (tx.Value <= trest)
                {
                    MessageBox.Show("No se puede mover el registro del troquel por exceso de presión", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            try
            {
                // Write only 1 item
                short ItemCount = 1;

                // Create some local scope variables to hold the value to be sent.
                // These arrays could just as easily contain all of the item we have added.
                int[] SyncItemServerHandles = new int[2];
                object[] SyncItemValues = new object[2];
                System.Array SyncItemServerErrors = null;
                OPCAutomation.OPCItem AnOpcItem = default(OPCAutomation.OPCItem);

                // Get the Servers handle for the desired item.  The server handles
                // were returned in add item subroutine.
                SyncItemServerHandles[1] = (int)itemServerHandles.GetValue(index);
                AnOpcItem = grupoConectado.OPCItems.GetOPCItem((int)itemServerHandles.GetValue(index));

                // Load the value to be written using Item's Canonical Data Type to
                // convert to correct type. 
                // See Kepware Application note on Canonical Data Types
                Array ItsAnArray = null;
                short CanonDT = 0;
                short vbArray = 8192;

                CanonDT = AnOpcItem.CanonicalDataType;


                // If it is an array, figure out the base type
                if (CanonDT > vbArray)
                {
                    CanonDT -= vbArray;
                }

                switch (CanonDT)
                {
                    case (short)CanonicalDataTypes.CanonDtByte:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(byte), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToByte(opcItemValueToWritte[index].Text);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtChar:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(SByte), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToSByte(opcItemValueToWritte[index].Text);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtWord:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(UInt16), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToUInt16(opcItemValueToWritte[index].Text);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtShort:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(Int16), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToInt16(opcItemValueToWritte[index].Text);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtDWord:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(UInt32), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToUInt32(opcItemValueToWritte[index].Text);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtLong:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(Int32), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToInt32(mOpcTextList[index].Value);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtFloat:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(float), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToSingle(opcItemValueToWritte[index].Text);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtDouble:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(double), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToDouble(opcItemValueToWritte[index].Text);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtBool:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(bool), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToBoolean(opcItemValueToWritte[index].Text);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtString:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(string), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToString(opcItemValueToWritte[index].Text);
                        }
                        break;
                    // End case

                    default:
                        MessageBox.Show("OPCItemWriteButton Unknown data type", "Error al escribir", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    // End case
                }

                // Invoke the SyncWrite operation.  Remember this call will wait until completion
                grupoConectado.SyncWrite(ItemCount, SyncItemServerHandles, SyncItemValues, out SyncItemServerErrors);

                if ((int)SyncItemServerErrors.GetValue(1) != 0)
                {
                    MessageBox.Show("SyncItemServerError: " + SyncItemServerErrors.GetValue(1));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // Error handling
                MessageBox.Show("OPC server write item failed with exception: " + ex.Message + " INDEX = " + index.ToString() + " c=" + opcItemValueToWritte[index].Text + "/", "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                return false;
            }


        }


        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                numericUpDown1.Focus();
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button28.Focus();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button30.Enabled = true;
                button30.Focus();
            }
        }

        private void numericUpDown2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                numericUpDown9.Enabled = true;
                numericUpDown9.Focus();
            }
        }

        private void numericUpDown3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                numericUpDown4.Enabled = true;
                numericUpDown4.Focus();
            }
        }

        private void numericUpDown4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                numericUpDown5.Enabled = true;
                numericUpDown5.Focus();
            }
        }

        private void numericUpDown5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                numericUpDown6.Enabled = true;
                numericUpDown6.Focus();
            }
        }

        private void numericUpDown6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                numericUpDown7.Enabled = true;
                numericUpDown7.Focus();
            }
        }

        private void numericUpDown7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                numericUpDown10.Enabled = true;
                numericUpDown10.Focus();
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (comboBox6.Text != "")
            {
                conexionBD cnn = new conexionBD();
                if (cnn.deleteFrom("resistencia", "nombre = '" + comboBox6.Text + "'"))
                    comboBox6.Text = "";
                carga(comboBox6, "nombre", "resistencia");
            }
        }

        private void button23_TextChanged(object sender, EventArgs e)
        {
            button23.Enabled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            button26.Enabled = true;
            if (textBox4.Text == "")
                button26.Enabled = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            button23.Enabled = true;
            if (textBox3.Text == "")
                button23.Enabled = false;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox8.Enabled = true;
                textBox8.Focus();
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                textBox9.Enabled = true;
                textBox9.Focus();
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                button19.Enabled = true;
                button19.Focus();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            string id = cnn.getNextId("color");
            Color cl = button19.BackColor;
            int cl1 = cl.ToArgb();
            if ((textBox7.Text != "" && textBox7.Text != " ") && (textBox8.Text != "" && textBox8.Text != " ") && (textBox9.Text != "" && textBox9.Text != " "))
            {
                if (cnn.insert("color", id + ",'" + textBox7.Text.Replace(" ", "") + "'" + ",'" + textBox8.Text.Replace(" ", "") + "'" + ",'" + textBox9.Text.Replace(" ", "") + "'"
                    + ",'" + cl1.ToString() + "'"))
                {
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox8.Enabled = false;
                    textBox9.Enabled = false;
                    button19.BackColor = Button.DefaultBackColor;
                    button19.Enabled = false;
                    carga(comboBox8, "color where idcolor != 1");
                    button15.Enabled = false;

                }
            }
            else MessageBox.Show("Datos de color incorrecto, revise Tinta, Catalogo, GCMI", "Error datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (comboBox8.Text != "" && comboBox8.Text != "null")
            {
                conexionBD cnn = new conexionBD();
                int idesp = comboBox8.Text.IndexOf(" ", 0);
                string[] tx = comboBox8.Text.Split(' ');
                if (cnn.deleteFrom("color", "Tinta ='" + tx[0] + "'" + " and Catalogo='" + tx[1] + "' and GCMI='" + tx[2] + "'"))
                {
                    comboBox8.Text = "";
                    carga(comboBox8, "color where idcolor != 1");
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button19.BackColor = colorDialog1.Color;
                button15.Enabled = true;
                button15.Focus();
            }
            else
            {
                MessageBox.Show("Selecciona un color semejante, para completar el alta de la nueva Tinta.", "Selecciona un color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {
            numericUpDown8.DownButton();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            numericUpDown8.UpButton();
        }

        private void numericUpDown9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                numericUpDown3.Enabled = true;
                numericUpDown3.Focus();
            }
        }

        private void numericUpDown10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button30.Enabled = true;
                button30.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form a = new SegundoColor();
            a.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form a = new TercerColor();
            a.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form a = new CuartoColor();
            a.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form a = new troquel();
            a.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form a = new Ranurador();
            a.ShowDialog();
            //t19.Text = "7";
            //escribir(19);
            //label19_TextChanged(sender, e);
            //Form b = new Form1();
            //b.ShowDialog();
        }

        private void actual_Enter(object sender, EventArgs e)
        {
            actualizar_datos();
        }

        private void actualizar_datos()
        {

            cargaUsuarios(comboBox1);
            cargaClientes(comboBox2);
            carga(comboBox6, "nombre", "resistencia");
            carga(comboBox3, "idflauta", "flauta");
            carga(comboBox8, "tinta", "color");
            //carga(comboBox5, "nombre", "operador");
            carga(comboBox4, "idcaja,clave", "caja");
            carga(comboBox7, "idcliente,nombre", "cliente");
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button33_Click(object sender, EventArgs e)
        {
            passOperator p = new passOperator();
            p.ShowDialog();
            if (PR.get_o() == 1)
            {
                timer5.Enabled = false;
                Form a = new alarma();
                a.ShowDialog();
                button33.BackColor = Button.DefaultBackColor;
            }
            PR.set_o(0);
        }

        private void comboBox4_Enter(object sender, EventArgs e)
        {
            carga(comboBox4, "idcaja,clave", "caja");
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            string res = cnn.busca("clave", "caja", comboBox4.Text);
            try
            {
                if (res == "") pictureBox2.Image = null;
                else
                    pictureBox2.Image = Image.FromFile(path + comboBox4.Text + ".bmp");
                if (comboBox4.Text == "")
                    pictureBox2.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontró imagen de caja." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            comboBox4.Text = "";
            comboBox4.SelectedItem = null;
            comboBox5.Text = "";
            comboBox5.SelectedItem = null;
            comboBox7.Text = "";
            comboBox7.SelectedItem = null;
            pictureBox2.Image = null;
            textBox1.Text = "";
            numericUpDown8.Value = 0;
            carga(comboBox5, "idoperardor,nombre", "operador");
            carga(comboBox7, "idcliente,nombre", "cliente");
            button1.BackColor = Button.DefaultBackColor;
            button2.BackColor = Button.DefaultBackColor;
            button3.BackColor = Button.DefaultBackColor;
            button4.BackColor = Button.DefaultBackColor;
            button5.BackColor = Button.DefaultBackColor;
        }

        private void button17_Click(object sender, EventArgs e)
        {

            if (comboBox4.Text != "" && comboBox5.Text != "" && comboBox7.Text != "" && textBox1.Text != "")
            {
                conexionBD cnn = new conexionBD();
                if (numericUpDown8.Value != 0)
                {
                    string res = cnn.busca("idorden", "orden", textBox3.Text);
                    if (res != "0")
                    {
                        if (res == "")
                        {

                            string res1 = cnn.busca("clave", "caja", textBox3.Text);
                            if (res1 != "0")
                            {
                                if (res1 == "")
                                {
                                    DateTime dt = DateTime.Now;

                                    string orden = textBox1.Text;
                                    string idoperardor = cnn.getId("operador", "idoperardor", "nombre", comboBox5.Text);
                                    string idcliente = cnn.getId("cliente", "idcliente", "nombre", comboBox7.Text);
                                    string idcaja = cnn.getId("caja", "idcaja", "clave", comboBox4.Text);
                                    if (cnn.insert("orden", "'" + orden + "','" + numericUpDown8.Value.ToString() + "','" + "NO LISTA" + "','" + idcaja + "','" + idoperardor + "','" + idcliente + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'"))
                                    {
                                        comboBox4.Text = "";
                                        comboBox5.Text = "";
                                        comboBox7.Text = "";
                                        pictureBox2.Image = null;
                                        textBox1.Text = "";
                                        numericUpDown8.Value = 0;
                                        carga(comboBox5, "idoperardor,nombre", "operador");
                                        carga(comboBox7, "idcliente,nombre", "cliente");
                                    }


                                }
                                else
                                {
                                    MessageBox.Show("La clave de caja no es correcta, revisa tus datos.", "Intenta de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //throw new InvalidOperationException();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("La clave ya esta dada de alta, utiliza otra.", "Intenta de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Número de cajas no valido por favor revisalo.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Falta uno o mas campos para ingresar la orden correctamente.", "Revisa la información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private bool saveOrder()
        {
            if (comboBox4.Text != "" && comboBox5.Text != "" && comboBox7.Text != "" && textBox1.Text != "")
            {
                conexionBD cnn = new conexionBD();
                if (numericUpDown8.Value != 0)
                {
                    string res = cnn.busca("idorden", "orden", textBox1.Text);
                    if (res != "0")
                    {
                        if (res == "")
                        {

                            string res1 = cnn.busca("clave", "caja", comboBox4.Text);
                            if (res1 != "0")
                            {
                                if (res1 != "")
                                {
                                    DateTime dt = DateTime.Now;

                                    string orden = textBox1.Text;
                                    string idoperardor = cnn.getId("operador", "idoperardor", "nombre", comboBox5.Text);
                                    string idcliente = cnn.getId("cliente", "idcliente", "nombre", comboBox7.Text);
                                    string idcaja = cnn.getId("caja", "idcaja", "clave", comboBox4.Text);
                                    if (cnn.insertC("orden", "'" + orden + "','" + numericUpDown8.Value.ToString() + "','" + "NO LISTA" + "','" + idcaja + "','" + idoperardor + "','" + idcliente + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'"))
                                    {
                                        comboBox4.Text = "";
                                        comboBox5.Text = "";
                                        comboBox7.Text = "";
                                        pictureBox2.Image = null;
                                        textBox1.Text = "";
                                        numericUpDown8.Value = 0;
                                        carga(comboBox5, "idoperardor,nombre", "operador");
                                        carga(comboBox7, "idcliente,nombre", "cliente");
                                        return true;
                                    }
                                    else return false;
                                }
                                else
                                {
                                    MessageBox.Show("La clave de caja no es correcta, revisa tus datos.", "Intenta de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                    //throw new InvalidOperationException();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Numero de orden existente, utiliza otro.", "Intenta de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Número de cajas no valido por favor revisalo.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Falta uno o mas campos para ingresar la orden correctamente.", "Revisa la información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            consultas a = new consultas();
            a.ShowDialog();
            if (AppModule.Instance.Object != null)
            {
                foreach (KeyValuePair<int, string> item in comboBox4.Items)
                {
                    if (item.Key == ((Objects.Order)AppModule.Instance.Object).IdCaja)
                    {
                        comboBox4.SelectedItem = item;
                    }
                }
                foreach (KeyValuePair<int, string> item in comboBox7.Items)
                {
                    if (item.Key == ((Objects.Order)AppModule.Instance.Object).IdCliente)
                    {
                        comboBox7.SelectedItem = item;
                    }
                }
                foreach (KeyValuePair<int, string> item in comboBox5.Items)
                {
                    if (item.Key == ((Objects.Order)AppModule.Instance.Object).IdOperador)
                    {
                        comboBox5.SelectedItem = item;
                    }
                }
                numericUpDown8.Value = ((Objects.Order)AppModule.Instance.Object).Volumen;
                button138.Enabled = true;
                mBox.ID = ((KeyValuePair<int, string>)(comboBox4.SelectedItem)).Key;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (textBox20.Text != "-" && textBox20.Text != "-." && textBox20.Text != "" && textBox20.Text != "." && textBox20.Text != ".-")
            {
                double pos = double.Parse(textBox10.Text);
                double ajus = double.Parse(textBox20.Text) * signo[1];
                if (ajus != 0)
                {
                    double npos = (pos + ajus) * 1000;
                    mOpcTextList[7].Value = npos;
                    escribir(7);
                    mOpcTextList[13].Value = 1;
                    escribir(13);
                    textBox20.Text = "";
                }
            }

        }

        private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            bool punto = t.Text.Contains(".");
            //double nv = double.Parse(t.Text.ToString());
            int g = 45;
            if (e.KeyChar == (char)g)
                button46_Click(button46, e);

            if (e.KeyChar == (char)Keys.Enter)
                button34_Click(button34, e);

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }
            else if (e.KeyChar == '.' && !punto)
            {
                textBox20.MaxLength = t.Text.Length + 3;
            }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            bool punto = t.Text.Contains(".");

            int g = 45;
            if (e.KeyChar == (char)g)
                button47_Click(button47, e);

            if (e.KeyChar == (char)Keys.Enter)
                button35_Click(button35, e);

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }
            else if (e.KeyChar == '.' && !punto)
            {
                textBox19.MaxLength = t.Text.Length + 3;
            }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            bool punto = t.Text.Contains(".");
            //double nv = double.Parse(t.Text.ToString());
            if (e.KeyChar == (char)Keys.Enter)
                button36_Click(button36, e);

            int g = 45;
            if (e.KeyChar == (char)g)
                button48_Click(button48, e);

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }
            else if (e.KeyChar == '.' && !punto)
            {
                textBox18.MaxLength = t.Text.Length + 3;
            }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            bool punto = t.Text.Contains(".");
            int g = 45;
            if (e.KeyChar == (char)g)
                button49_Click(button49, e);
            if (e.KeyChar == (char)Keys.Enter)
                button37_Click(button37, e);

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }
            else if (e.KeyChar == '.' && !punto)
            {
                textBox17.MaxLength = t.Text.Length + 3;
            }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            bool punto = t.Text.Contains(".");

            int g = 45;
            if (e.KeyChar == (char)g)
                button50_Click(button50, e);

            if (e.KeyChar == (char)Keys.Enter)
                button38_Click(button38, e);

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }
            else if (e.KeyChar == '.' && !punto)
            {
                textBox16.MaxLength = t.Text.Length + 3;
            }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            bool punto = t.Text.Contains(".");

            int g = 45;
            if (e.KeyChar == (char)g)
                button51_Click(button51, e);

            if (e.KeyChar == (char)Keys.Enter)
                button39_Click(button39, e);

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }
            else if (e.KeyChar == '.' && !punto)
            {
                textBox15.MaxLength = t.Text.Length + 3;
            }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (textBox19.Text != "-" && textBox19.Text != "-." && textBox19.Text != "" && textBox19.Text != "." && textBox19.Text != ".-")
            {
                double pos = double.Parse(textBox11.Text);
                double ajus = double.Parse(textBox19.Text) * signo[2];
                if (ajus != 0)
                {
                    double npos = (pos + ajus) * 1000;
                    mOpcTextList[8].Value = npos;
                    escribir(8);
                    mOpcTextList[14].Value = 1;
                    escribir(14);
                    textBox19.Text = "";
                }
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (textBox18.Text != "-" && textBox18.Text != "-." && textBox18.Text != "" && textBox18.Text != "." && textBox18.Text != ".-")
            {
                double pos = double.Parse(textBox12.Text);
                double ajus = double.Parse(textBox18.Text) * signo[3];
                if (ajus != 0)
                {
                    double npos = (pos + ajus) * 1000;
                    mOpcTextList[9].Value = npos;
                    escribir(9);
                    mOpcTextList[15].Value = 1;
                    escribir(15);
                    textBox18.Text = "";
                }
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (textBox17.Text != "-" && textBox17.Text != "-." && textBox17.Text != "" && textBox17.Text != "." && textBox17.Text != ".-")
            {
                double pos = double.Parse(textBox13.Text);
                double ajus = double.Parse(textBox17.Text) * signo[4];
                if (ajus != 0)
                {
                    double npos = (pos + ajus) * 1000;
                    mOpcTextList[10].Value = npos;
                    escribir(10);
                    mOpcTextList[16].Value = 1;
                    escribir(16);
                    textBox17.Text = "";
                }
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (textBox16.Text != "-" && textBox16.Text != "-." && textBox16.Text != "" && textBox16.Text != "." && textBox16.Text != ".-")
            {

                double pos = double.Parse(textBox14.Text);
                double ajus = double.Parse(textBox16.Text) * signo[5];
                if (ajus != 0)
                {
                    double npos = (pos + ajus) * 1000;
                    mOpcTextList[11].Value = npos;
                    escribir(11);
                    mOpcTextList[17].Value = 1;
                    escribir(17);
                    textBox16.Text = "";
                }
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (textBox15.Text != "-" && textBox15.Text != "-." && textBox15.Text != "" && textBox15.Text != "." && textBox15.Text != ".-")
            {
                double pos = double.Parse(textBox2.Text);
                double ajus = double.Parse(textBox15.Text) * signo[6];
                if (ajus != 0)
                {
                    double npos = (pos + ajus) * 1000;
                    mOpcTextList[12].Value = npos;
                    escribir(12);
                    mOpcTextList[18].Value = 1;
                    escribir(18);
                    textBox15.Text = "";
                }
            }

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            //TextBox20  sender as TextBox;
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void button44_Click(object sender, EventArgs e)
        {
            if (mOpcTextList[17].Value == 1)
            {
                mOpcTextList[17].Value = 0;
                escribir(17);
            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void button51_Click(object sender, EventArgs e)
        {
            signo[6] = signo[6] * -1;
            if (signo[6] == 1)
            {
                button51.Image = Impresora.Resources.Images.Add1;
            }
            else if (signo[6] == -1)
            {
                button51.Image = Impresora.Resources.Images.Remove;
            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            signo[5] = signo[5] * -1;
            if (signo[5] == 1)
            {
                button50.Image = Impresora.Resources.Images.Add1;
            }
            else if (signo[5] == -1)
            {
                button50.Image = Impresora.Resources.Images.Remove;
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            signo[4] = signo[4] * -1;
            if (signo[4] == 1)
            {
                button49.Image = Impresora.Resources.Images.Add1;
            }
            else if (signo[4] == -1)
            {
                button49.Image = Impresora.Resources.Images.Remove;
            }

        }

        private void button48_Click(object sender, EventArgs e)
        {
            signo[3] = signo[3] * -1;
            if (signo[3] == 1)
            {
                button48.Image = Impresora.Resources.Images.Add1;
            }
            else if (signo[3] == -1)
            {
                button48.Image = Impresora.Resources.Images.Remove;
            }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            signo[2] = signo[2] * -1;
            if (signo[2] == 1)
            {
                button47.Image = Impresora.Resources.Images.Add1;
            }
            else if (signo[2] == -1)
            {
                button47.Image = Impresora.Resources.Images.Remove;
            }

        }

        private void button46_Click(object sender, EventArgs e)
        {
            signo[1] = signo[1] * -1;
            if (signo[1] == 1)
            {
                button46.Image = Impresora.Resources.Images.Add1;
            }
            else if (signo[1] == -1)
            {
                button46.Image = Impresora.Resources.Images.Remove;
            }
        }

        private void textBox15_Enter(object sender, EventArgs e)
        {
            textBox15.Text = "";
        }

        private void textBox16_Enter(object sender, EventArgs e)
        {
            textBox16.Text = "";
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox17_Enter(object sender, EventArgs e)
        {
            textBox17.Text = "";
        }

        private void textBox18_Enter(object sender, EventArgs e)
        {
            textBox18.Text = "";
        }

        private void textBox19_Enter(object sender, EventArgs e)
        {
            textBox19.Text = "";
        }

        private void textBox20_Enter(object sender, EventArgs e)
        {
            textBox20.Text = "";
        }

        private void button45_Click(object sender, EventArgs e)
        {

            mOpcTextList[18].Value = 0;
            escribir(18);

        }

        private void button43_Click(object sender, EventArgs e)
        {

            mOpcTextList[16].Value = 0;
            escribir(16);

        }

        private void button42_Click(object sender, EventArgs e)
        {

            mOpcTextList[15].Value = 0;
            escribir(15);

        }

        private void button41_Click(object sender, EventArgs e)
        {

            mOpcTextList[14].Value = 0;
            escribir(14);

        }

        private void button40_Click(object sender, EventArgs e)
        {

            mOpcTextList[13].Value = 0;
            escribir(13);

        }

        private void actual_Leave(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            SetForegroundWindow(this.Handle);

        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            if (button33.BackColor == Color.Red)
                button33.BackColor = Button.DefaultBackColor;
            else
            {
                button33.BackColor = Color.Red;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button52_Click(object sender, EventArgs e)
        {
            //conexionBD cnn = new conexionBD();
            //int h_al;
            /////////////////////////////////////////////
            //int.TryParse(cnn.selectVal("handle", string.Format("tags where offset_id = {0} and name like '%.ALARMA_CODE'", numericUpDown18.Value)), out h_al);
            //Objects.Text t2 = TagManager.Instance.TextList.Select(p => p).Where(p => p.Handle == h_al).FirstOrDefault();
            //t2.Value = 7;
            //escribir(t2.Handle);

            ////////////////////////////////////////////

            //int.TryParse(cnn.selectVal("handle", string.Format("tags where offset_id = {0} and name like '%.EN_DESP'", numericUpDown18.Value)), out h_al);
            //Objects.Text t2 = TagManager.Instance.TextList.Select(p => p).Where(p => p.Handle == h_al).FirstOrDefault();
            //t2.Value = 0;
            //escribir(t2.Handle);
            ////////////////////////////////////////////
            if (numericUpDown18.Value == 8989)
            {
                if (checkBox1.Checked)
                {
                    // Add the value in the registry so that the application runs at startup
                    rkApp.SetValue("Impresora.exe", Application.ExecutablePath.ToString());
                }
                else
                {
                    // Remove the value from the registry so that the application doesn't start
                    rkApp.DeleteValue("Impresora.exe", false);
                }
            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            Action(10, numericUpDown40, 147, Limit.P);
        }

        private void button56_Click(object sender, EventArgs e)
        {
            Action(11, numericUpDown39, 149, Limit.P);
        }

        private void button57_Click(object sender, EventArgs e)
        {
            Action(12, numericUpDown38, 151, Limit.P);
        }

        private void button58_Click(object sender, EventArgs e)
        {
            Action(13, numericUpDown37, 152, Limit.P);
        }

        private void button59_Click(object sender, EventArgs e)
        {
            Action(14, numericUpDown36, 153, Limit.P);
        }

        private void button60_Click(object sender, EventArgs e)
        {
            Action(15, numericUpDown35, 37, Limit.P);
        }

        private void button61_Click(object sender, EventArgs e)
        {
            Action(16, numericUpDown34, 38, Limit.P);
        }



        private void button63_Click(object sender, EventArgs e)
        {
            Action(10, numericUpDown33, 148, Limit.N);
        }

        private void Action(int offsetId, NumericUpDown ctrl, int tagHandle, Limit lim)
        {
            string s = "'{0}',{1},'{2}','Cambio de límite {3}: {4}','" + "common'";
            pass p = new pass();
            p.ShowDialog();
            if (PR.get_p() == 1)
            {
                Text t = TagManager.Instance.TextList.Find(x => x.Tag != null && x.Tag.Contains(offsetId + "MS.APOS"));
                int stat = t.Stat;
                double xx = (double)ctrl.Value;
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
                mOpcTextList[tagHandle].Value = (v) - t.Offset;
                escribir(tagHandle);

                Aplica(offsetId, v, lim);
                conexionBD cnn = new conexionBD();
                String ida = cnn.getNextId("bitacora");
                DateTime dt = DateTime.Now;
                if (lim == Limit.P)
                {
                    cnn.update("offset", " LPABS = '" + xx + "' where idoffset = " + offsetId);
                }
                if (lim == Limit.N)
                {
                    cnn.update("offset", " LNABS = '" + xx + "' where idoffset = " + offsetId);
                }

                cnn.insertC("bitacora", string.Format(s, ida, offsetId, dt.ToString("yyyy-MM-dd HH:mm:ss"), lim == Limit.N ? "N" : "P", mOpcTextList[tagHandle].Value));
                PR.set_0();
            }
            else
                PR.set_0();
        }

        private void button62_Click(object sender, EventArgs e)
        {
            Action(11, numericUpDown32, 150, Limit.N);
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Action(12, numericUpDown31, 35, Limit.N);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            Action(13, numericUpDown30, 36, Limit.N);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Action(14, numericUpDown29, 154, Limit.N);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Action(15, numericUpDown28, 155, Limit.N);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Action(16, numericUpDown27, 156, Limit.N);
        }

        private void button105_Click(object sender, EventArgs e)
        {

        }

        private void button104_Click(object sender, EventArgs e)
        {
            Action(131, numericUpDown153, 225, Limit.N);
        }

        private void button70_Click(object sender, EventArgs e)
        {
            Action(20, numericUpDown67, 35, Limit.N);
        }

        private void button77_Click(object sender, EventArgs e)
        {
            Action(20, numericUpDown60, 254, Limit.P);
        }

        private void button69_Click(object sender, EventArgs e)
        {
            Action(21, numericUpDown66, 195, Limit.N);
        }

        private void button76_Click(object sender, EventArgs e)
        {
            Action(21, numericUpDown59, 194, Limit.P);
        }

        private void button68_Click(object sender, EventArgs e)
        {
            Action(22, numericUpDown65, 197, Limit.N);
        }

        private void button75_Click(object sender, EventArgs e)
        {
            Action(22, numericUpDown58, 196, Limit.P);
        }

        private void button67_Click(object sender, EventArgs e)
        {
            Action(23, numericUpDown64, 199, Limit.N);
        }

        private void button74_Click(object sender, EventArgs e)
        {
            Action(23, numericUpDown57, 198, Limit.P);
        }

        private void button66_Click(object sender, EventArgs e)
        {
            Action(24, numericUpDown63, 201, Limit.N);
        }

        private void button73_Click(object sender, EventArgs e)
        {
            Action(24, numericUpDown56, 200, Limit.P);
        }

        private void button72_Click(object sender, EventArgs e)
        {
            Action(25, numericUpDown55, 202, Limit.P);
        }

        private void button65_Click(object sender, EventArgs e)
        {
            Action(25, numericUpDown62, 203, Limit.N);
        }

        private void button87_Click(object sender, EventArgs e)
        {
            Action(30, numericUpDown88, 255, Limit.P);
        }

        private void button86_Click(object sender, EventArgs e)
        {
            Action(31, numericUpDown87, 204, Limit.P);
        }

        private void button80_Click(object sender, EventArgs e)
        {
            Action(31, numericUpDown93, 205, Limit.N);
        }

        private void button85_Click(object sender, EventArgs e)
        {
            Action(32, numericUpDown86, 206, Limit.P);
        }

        private void button79_Click(object sender, EventArgs e)
        {
            Action(32, numericUpDown92, 207, Limit.N);
        }

        private void button84_Click(object sender, EventArgs e)
        {
            Action(33, numericUpDown85, 208, Limit.P);
        }

        private void button78_Click(object sender, EventArgs e)
        {
            Action(33, numericUpDown91, 209, Limit.N);
        }

        private void button83_Click(object sender, EventArgs e)
        {
            Action(34, numericUpDown84, 210, Limit.P);
        }

        private void button71_Click(object sender, EventArgs e)
        {
            Action(34, numericUpDown90, 211, Limit.N);
        }

        private void button82_Click(object sender, EventArgs e)
        {
            Action(35, numericUpDown83, 212, Limit.P);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            Action(35, numericUpDown89, 213, Limit.N);
        }

        private void button99_Click(object sender, EventArgs e)
        {
            Action(120, numericUpDown118, 256, Limit.P);
        }

        private void button98_Click(object sender, EventArgs e)
        {
            Action(121, numericUpDown117, 214, Limit.P);
        }

        private void button92_Click(object sender, EventArgs e)
        {
            Action(121, numericUpDown123, 215, Limit.N);
        }

        private void button97_Click(object sender, EventArgs e)
        {
            Action(122, numericUpDown116, 216, Limit.P);
        }

        private void button91_Click(object sender, EventArgs e)
        {
            Action(122, numericUpDown122, 217, Limit.N);
        }

        private void button96_Click(object sender, EventArgs e)
        {
            Action(123, numericUpDown115, 218, Limit.P);
        }

        private void button90_Click(object sender, EventArgs e)
        {
            Action(123, numericUpDown121, 219, Limit.N);
        }

        private void button95_Click(object sender, EventArgs e)
        {
            Action(124, numericUpDown114, 220, Limit.P);
        }

        private void button89_Click(object sender, EventArgs e)
        {
            Action(124, numericUpDown120, 221, Limit.N);
        }

        private void button94_Click(object sender, EventArgs e)
        {
            Action(125, numericUpDown113, 222, Limit.P);
        }

        private void button88_Click(object sender, EventArgs e)
        {
            Action(125, numericUpDown119, 223, Limit.N);
        }

        private void button111_Click(object sender, EventArgs e)
        {
            Action(130, numericUpDown148, 257, Limit.P);
        }

        private void button110_Click(object sender, EventArgs e)
        {
            Action(131, numericUpDown147, 224, Limit.P);
        }

        private void button109_Click(object sender, EventArgs e)
        {
            Action(132, numericUpDown146, 226, Limit.P);
        }

        private void button103_Click(object sender, EventArgs e)
        {
            Action(132, numericUpDown152, 227, Limit.N);
        }

        private void button108_Click(object sender, EventArgs e)
        {
            Action(133, numericUpDown145, 228, Limit.P);
        }

        private void button102_Click(object sender, EventArgs e)
        {
            Action(133, numericUpDown151, 229, Limit.N);
        }

        private void button107_Click(object sender, EventArgs e)
        {
            Action(134, numericUpDown144, 230, Limit.P);
        }

        private void button101_Click(object sender, EventArgs e)
        {
            Action(134, numericUpDown150, 231, Limit.N);
        }

        private void button106_Click(object sender, EventArgs e)
        {
            Action(135, numericUpDown143, 232, Limit.P);
        }

        private void button100_Click(object sender, EventArgs e)
        {
            Action(135, numericUpDown149, 233, Limit.N);
        }

        private void button123_Click(object sender, EventArgs e)
        {
            Action(40, numericUpDown178, 258, Limit.P);
        }

        private void button122_Click(object sender, EventArgs e)
        {
            Action(41, numericUpDown177, 234, Limit.P);
        }

        private void button116_Click(object sender, EventArgs e)
        {
            Action(41, numericUpDown183, 235, Limit.N);
        }

        private void button121_Click(object sender, EventArgs e)
        {
            Action(42, numericUpDown176, 236, Limit.P);
        }

        private void button115_Click(object sender, EventArgs e)
        {
            Action(42, numericUpDown182, 237, Limit.N);
        }

        private void button135_Click(object sender, EventArgs e)
        {
            Action(50, numericUpDown208, 259, Limit.P);
        }

        private void button134_Click(object sender, EventArgs e)
        {
            Action(51, numericUpDown207, 238, Limit.P);
        }

        private void button128_Click(object sender, EventArgs e)
        {
            Action(51, numericUpDown213, 239, Limit.N);
        }

        private void button133_Click(object sender, EventArgs e)
        {
            Action(52, numericUpDown206, 240, Limit.P);
        }

        private void button127_Click(object sender, EventArgs e)
        {
            Action(52, numericUpDown212, 241, Limit.N);
        }

        private void button132_Click(object sender, EventArgs e)
        {
            Action(53, numericUpDown205, 242, Limit.P);
        }

        private void button126_Click(object sender, EventArgs e)
        {
            Action(53, numericUpDown211, 243, Limit.N);
        }

        private void button120_Click(object sender, EventArgs e)
        {
            Action(54, numericUpDown187, 244, Limit.P);
        }

        private void button119_Click(object sender, EventArgs e)
        {
            Action(54, numericUpDown191, 245, Limit.N);
        }

        private void button131_Click(object sender, EventArgs e)
        {
            Action(150, numericUpDown204, 246, Limit.P);
        }

        private void button125_Click(object sender, EventArgs e)
        {
            Action(150, numericUpDown210, 247, Limit.N);
        }

        private void button130_Click(object sender, EventArgs e)
        {
            Action(151, numericUpDown203, 248, Limit.P);
        }

        private void button124_Click(object sender, EventArgs e)
        {
            Action(151, numericUpDown209, 249, Limit.N);
        }

        private void button118_Click(object sender, EventArgs e)
        {
            Action(152, numericUpDown173, 250, Limit.P);
        }

        private void button113_Click(object sender, EventArgs e)
        {
            Action(152, numericUpDown175, 251, Limit.N);
        }

        private void button114_Click(object sender, EventArgs e)
        {
            Action(153, numericUpDown169, 252, Limit.P);
        }

        private void button112_Click(object sender, EventArgs e)
        {
            Action(153, numericUpDown174, 253, Limit.N);
        }

        private void tabPage10_Click(object sender, EventArgs e)
        {

        }

        private void button129_Click(object sender, EventArgs e)
        {

        }

        private void button136_Click(object sender, EventArgs e)
        {
            pass p = new pass();
            p.ShowDialog();
            if (PR.get_p() == 1)
            {
                conexionBD cnn = new conexionBD();
                DataTable dt1 = cnn.selectFrom("handle, status", "tags where offset_id between 10 and 155 and (name like '%HOMM%')");
                int itemsNumber = dt1.Rows.Count;
                int[] handels_to_SA = new int[itemsNumber];
                for (int i = 1; i < itemsNumber; i++)
                {
                    handels_to_SA[i] = (int)itemServerHandles.GetValue(int.Parse(dt1.Rows[i][0].ToString()));
                }
                Array ErrorSetActive;
                offset os = new offset();
                os.ShowInTaskbar = false;
                grupoConectado.OPCItems.SetActive(itemsNumber - 1, handels_to_SA, true, out ErrorSetActive);

                os.ShowDialog(this);

                grupoConectado.OPCItems.SetActive(itemsNumber - 1, handels_to_SA, false, out ErrorSetActive);

                PR.set_0();
            }
            else
                PR.set_0();
        }

        private void button137_Click(object sender, EventArgs e)
        {
            pass p = new pass();
            p.ShowDialog();
            if (PR.get_p() == 1)
            {
                conexionBD cnn = new conexionBD();
                DataTable dt1 = cnn.selectFrom("handle, status", "tags where offset_id between 10 and 155 and (name like '%FACTOR%' or name like '%EN_FAC%')");
                int itemsNumber = dt1.Rows.Count;
                int[] handels_to_SA = new int[itemsNumber];
                for (int i = 1; i < itemsNumber; i++)
                {
                    handels_to_SA[i] = (int)itemServerHandles.GetValue(int.Parse(dt1.Rows[i][0].ToString()));
                }
                Array ErrorSetActive;
                factor f = new factor();
                f.ShowInTaskbar = false;
                grupoConectado.OPCItems.SetActive(itemsNumber - 1, handels_to_SA, true, out ErrorSetActive);

                f.ShowDialog(this);

                grupoConectado.OPCItems.SetActive(itemsNumber - 1, handels_to_SA, false, out ErrorSetActive);
                PR.set_0();
            }
            else
                PR.set_0();
        }

        private void numericUpDown33_Click(object sender, EventArgs e)
        {
            var x = sender as NumericUpDown;
            x.Text = "";
        }

        Text tx2 = null;
        Text tx;
        private void button32_Click(object sender, EventArgs e)
        {
            tx = TagManager.Instance.TextList.Select(x => x).Where(x => x != null && x.Handle == 185).FirstOrDefault();
            Text tx1 = TagManager.Instance.TextList.Select(x => x).Where(x => x != null && x.Handle == 120).FirstOrDefault();
            tx2 = TagManager.Instance.TextList.Select(x => x).Where(x => x != null && x.Handle == 121).FirstOrDefault();
            tx1.Value = 0;
            tx2.Value = 1;
            tx2.Changed += tx2_Changed;
            passOperator p = new passOperator();
            PR.set_o(0);
            p.ShowDialog();
            if (PR.get_o() == 1)
            {
                try
                {
                    label7.Text = textBox1.Text;
                    requisicionL.Text = comboBox4.Text;
                    operadorL.Text = comboBox5.Text;
                    cantidadRL.Text = numericUpDown8.Value.ToString();
                    clienteL.Text = comboBox7.Text;
                    if (saveOrder())
                    {
                        try
                        {
                            conexionBD cx = new conexionBD();
                            DataTable dt = cx.selectFrom("color1,color2,color3,color4", "caja where clave='" + ((KeyValuePair<int, string>)comboBox4.SelectedItem).Value + "'");
                            int color1, color2, color3, color4;
                            color1 = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                            color2 = int.Parse(dt.Rows[0].ItemArray[1].ToString());
                            color3 = int.Parse(dt.Rows[0].ItemArray[2].ToString());
                            color4 = int.Parse(dt.Rows[0].ItemArray[3].ToString());
                            button2.BackColor = Color.FromArgb(int.Parse(cx.selectVal("argb", " color where idcolor=" + color1)));
                            button3.BackColor = Color.FromArgb(int.Parse(cx.selectVal("argb", " color where idcolor=" + color2)));
                            button4.BackColor = Color.FromArgb(int.Parse(cx.selectVal("argb", " color where idcolor=" + color3)));
                            button5.BackColor = Color.FromArgb(int.Parse(cx.selectVal("argb", " color where idcolor=" + color4)));
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        escribir(tx1.Handle);
                        escribir(tx2.Handle);
                        PR.set_o(0);
                        mProgressDialog = new ProgressDialog();
                        mProgressDialog.CancelButton.Click += CancelButton_Click;
                        mProgressDialog.FormClosing += (object sender2, FormClosingEventArgs ec) =>
                        {
                            TagManager.Instance.TagMoves -= TagManager_TagMoves;
                            TagManager.Instance.TagEnDespChanged -= Instance_TagEnDespChanged;
                        };
                        mProgressDialog.SetMessage("Ajustando presión del troquelador");
                        mProgressDialog.TopMost = true;
                        mProgressDialog.BringToFront();
                        mProgressDialog.Show();
                    }
                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }


        }

        void tx2_Changed(object sender, ObjectEventArgs e)
        {
            if (e.Text == "0")
            {
                tx2.Changed -= tx2_Changed;
                double trest = double.Parse(AppModule.Instance.SysParams.Parameters.Select(x => x).Where(x => x.Key == "40ms.pressure.protection").First().Value.ToString());
                if (tx.Value <= trest)
                {
                    MessageBox.Show("No se puede mover el registro del troquel por exceso de presión", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    mProgressDialog.SetMessage("Ejecutando");
                    RunWorkerAsync();
                }
            }
        }

        void CancelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Cancelar operación?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                backgroundWorker1.CancelAsync();
                TagManager.Instance.Queue.Clear();
                TagManager.Instance.SyncArray.Clear();
                mProgressDialog.Close();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            mProgressDialog.ShowProgress(e.ProgressPercentage);
        }

        private List<int> CheckList;
        private void RunWorkerAsync()
        {
            if (comboBox4.SelectedItem != null)
            {
                mBox.ID = ((KeyValuePair<int, string>)(comboBox4.SelectedItem)).Key;
                int drvTot = typeof(Box).GetProperties().Count() - 1;
                TagManager.Instance.Init();
                foreach (var p in typeof(Box).GetProperties())
                {
                    if (p.Name == "ID") continue;

                    try
                    {
                        string s = p.Name.Replace("P", "");
                        Text t = TagManager.Instance.TextList.Select(x => x).Where(x => x.Tag != null && x.Tag.Contains(s + "MS.DESP")).FirstOrDefault();
                        t.Value = double.Parse(p.GetValue(mBox, null).ToString());
                        Text t1 = TagManager.Instance.TextList.Select(x => x).Where(x => x.Tag != null && x.Tag.Contains(s + "MS.EN_DESP")).FirstOrDefault();
                        t1.Value = 1;
                        escribir(t.Handle);
                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("La configuración es inválida", "Error de ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                }

                TagManager.ftime = true;
                actual.loading = true;
                TagManager.ftimeCS = true;
                int prgrss = 0;


                if (syncarray)
                {
                    SyncArrayInit();
                    syncarray = false;
                }
                TagManager.Instance.TagMoves += TagManager_TagMoves;
                TagManager.Instance.TagEnDespChanged += Instance_TagEnDespChanged;
                CheckList = new List<int>();
                TagManager.Instance.InitSyncZ();

            }
            else
            {
                MessageBox.Show("Seleccione una caja");
            }
        }

        private bool syncarray = true;

        private void SyncArrayInit()
        {
            Text apos12, apos15, apos13, apos16, apos152, apos151, apos150, apos153;
            Text desp12, desp15, desp13, desp16, desp152, desp151, desp150, desp153;

            apos12 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 12 && w.Tag.Contains("MS.APOS")).First();
            apos13 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 13 && w.Tag.Contains("MS.APOS")).First();
            apos15 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 15 && w.Tag.Contains("MS.APOS")).First();
            apos16 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 16 && w.Tag.Contains("MS.APOS")).First();
            apos150 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 150 && w.Tag.Contains("MS.APOS")).First();
            apos151 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 151 && w.Tag.Contains("MS.APOS")).First();
            apos152 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 152 && w.Tag.Contains("MS.APOS")).First();
            apos153 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 153 && w.Tag.Contains("MS.APOS")).First();
            desp12 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 12 && w.Tag.Contains("MS.DESP")).First();
            desp13 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 13 && w.Tag.Contains("MS.DESP")).First();
            desp15 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 15 && w.Tag.Contains("MS.DESP")).First();
            desp16 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 16 && w.Tag.Contains("MS.DESP")).First();
            desp150 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 150 && w.Tag.Contains("MS.DESP")).First();
            desp151 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 151 && w.Tag.Contains("MS.DESP")).First();
            desp152 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 152 && w.Tag.Contains("MS.DESP")).First();
            desp153 = TagManager.Instance.TextList.Select(x => x).Where(w => w.OffsetId == 153 && w.Tag.Contains("MS.DESP")).First();
            if (apos12.Value < desp12.Value)
            {
                Text t12 = null;
                for (int i = 0; i < TagManager.Instance.SyncArray.Count; i++)
                {
                    if (TagManager.Instance.SyncArray[i].OffsetId == 12)
                    {
                        t12 = TagManager.Instance.SyncArray[i];
                        TagManager.Instance.SyncArray.RemoveAt(i);
                        break;
                    }
                }
                if (t12 != null)
                {
                    TagManager.Instance.SyncArray.Add(t12);
                }
            }

            if (apos13.Value < desp13.Value)
            {
                Text t13 = null;
                for (int i = 0; i < TagManager.Instance.SyncArray.Count; i++)
                {
                    if (TagManager.Instance.SyncArray[i].OffsetId == 13)
                    {
                        t13 = TagManager.Instance.SyncArray[i];
                        TagManager.Instance.SyncArray.RemoveAt(i);
                        break;
                    }
                }
                if (t13 != null)
                {
                    TagManager.Instance.SyncArray.Add(t13);
                }
            }
            Text t42 = null;
            for (int i = 0; i < TagManager.Instance.SyncArray.Count; i++)
            {
                if (TagManager.Instance.SyncArray[i].OffsetId == 42)
                {
                    t42 = TagManager.Instance.SyncArray[i];
                    TagManager.Instance.SyncArray.RemoveAt(i);
                    break;
                }
            }
            if (t42 != null)
            {
                TagManager.Instance.SyncArray.Add(t42);
            }

            if (apos15.Value < desp15.Value)
            {
                Text tmov = null;
                for (int i = 0; i < TagManager.Instance.SyncArray.Count; i++)
                {
                    if (TagManager.Instance.SyncArray[i].OffsetId == 15)
                    {
                        tmov = TagManager.Instance.SyncArray[i];
                        TagManager.Instance.SyncArray.RemoveAt(i);
                        break;
                    }
                }
                if (tmov != null)
                {
                    TagManager.Instance.SyncArray.Add(tmov);
                }
            }

            if (apos16.Value < desp16.Value)
            {
                Text tmov = null;
                for (int i = 0; i < TagManager.Instance.SyncArray.Count; i++)
                {
                    if (TagManager.Instance.SyncArray[i].OffsetId == 16)
                    {
                        tmov = TagManager.Instance.SyncArray[i];
                        TagManager.Instance.SyncArray.RemoveAt(i);
                        break;
                    }
                }
                if (tmov != null)
                {
                    TagManager.Instance.SyncArray.Add(tmov);
                }
            }

            if (apos151.Value < desp151.Value)
            {
                Text t151 = null;
                for (int i = 0; i < TagManager.Instance.SyncArray.Count; i++)
                {
                    if (TagManager.Instance.SyncArray[i].OffsetId == 151)
                    {
                        t151 = TagManager.Instance.SyncArray[i];
                        TagManager.Instance.SyncArray.RemoveAt(i);
                        break;
                    }
                }
                if (t151 != null)
                {
                    TagManager.Instance.SyncArray.Add(t151);
                }
            }

            if (apos152.Value < desp152.Value)
            {
                Text t152 = null;
                for (int i = 0; i < TagManager.Instance.SyncArray.Count; i++)
                {
                    if (TagManager.Instance.SyncArray[i].OffsetId == 152)
                    {
                        t152 = TagManager.Instance.SyncArray[i];
                        TagManager.Instance.SyncArray.RemoveAt(i);
                        break;
                    }
                }
                if (t152 != null)
                {
                    TagManager.Instance.SyncArray.Add(t152);
                }
            }

            if (apos150.Value > desp150.Value)
            {
                Text tmov = null;
                for (int i = 0; i < TagManager.Instance.SyncArray.Count; i++)
                {
                    if (TagManager.Instance.SyncArray[i].OffsetId == 150)
                    {
                        tmov = TagManager.Instance.SyncArray[i];
                        TagManager.Instance.SyncArray.RemoveAt(i);
                        break;
                    }
                }
                if (tmov != null)
                {
                    TagManager.Instance.SyncArray.Add(tmov);
                }
            }

            if (apos153.Value > desp153.Value)
            {
                Text tmov = null;
                for (int i = 0; i < TagManager.Instance.SyncArray.Count; i++)
                {
                    if (TagManager.Instance.SyncArray[i].OffsetId == 153)
                    {
                        tmov = TagManager.Instance.SyncArray[i];
                        TagManager.Instance.SyncArray.RemoveAt(i);
                        break;
                    }
                }
                if (tmov != null)
                {
                    TagManager.Instance.SyncArray.Add(tmov);
                }
            }

            for (int i = 0; i < TagManager.Instance.SyncArrayOff.Count; i++)
            {
                TagManager.Instance.SyncArrayOff[i].Value = 0;
                actual.escribir(TagManager.Instance.SyncArrayOff[i].Handle);
            }
        }

        void Instance_TagEnDespChanged(object sender, EventArgs e)
        {
            Text t = sender as Text;
            if (t.Value == 0)
            {
                int i = CheckList.Select(x => x).Where(x => x == t.Handle).Count();
                if (i == 0)
                {
                    CheckList.Add(t.Handle);
                    mProgressDialog.MarkMessage(t.Handle);
                }
                if (TagManager.Instance.SyncArray.Count == CheckList.Count)
                {
                    mProgressDialog.ChangeView();
                }
            }

        }

        void TagManager_TagMoves(object sender, EventArgs e)
        {
            Text t = sender as Text;
            mProgressDialog.AddMessage("[" + t.Handle.ToString().PadLeft(3, ' ') + "] " + t.Tag);
            int pr = (int)((1 - ((double)TagManager.Instance.Queue.Count / (double)TagManager.Instance.SyncArray.Count)) * 100);
            mProgressDialog.ShowProgress(pr);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void numericUpDown18_Enter(object sender, EventArgs e)
        {
            NumericUpDown n = sender as NumericUpDown;
            n.Text = "";
        }

        private void numericUpDown18_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button52_Click(sender, null);
                numericUpDown18.Text = "";
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //conexionBD cnn = new conexionBD();

            //DataTable dt1 = cnn.selectFrom("handle, status", "tags where offset_id between 10 and 155 and (name like '%LM_N%' or name like '%LM_P%' or name like '%DESR%' or name like '%FACTOR%' or name like '%EN_CAL%' or name like '%HOMMINING%' or name like '%EN_FAC%' or name like '%HA%')");
            //int itemsNumber = dt1.Rows.Count;
            //int[] handels_to_SA = new int[itemsNumber];

            //for (int i = 1; i < itemsNumber; i++)
            //{
            //    handels_to_SA[i] = (int)itemServerHandles.GetValue(int.Parse(dt1.Rows[i][0].ToString()));

            //}
            //Array ErrorSetActive;
            //grupoConectado.OPCItems.SetActive(itemsNumber - 1, handels_to_SA, false, out ErrorSetActive);

            //dt1 = cnn.selectFrom("handle, status", "tags where  offset_id not in (10,11,12,13,14,15,16,20,30,120,130,40,50) and name like '%APOS%'");
            //itemsNumber = dt1.Rows.Count;
            //handels_to_SA = new int[itemsNumber];

            //for (int i = 1; i < itemsNumber; i++)
            //{
            //    handels_to_SA[i] = (int)itemServerHandles.GetValue(int.Parse(dt1.Rows[i][0].ToString()));

            //}

            //grupoConectado.OPCItems.SetActive(itemsNumber - 1, handels_to_SA, false, out ErrorSetActive);

            //DataTable dt2 = cnn.selectFrom("handle, status", "tags where (name like '%LM_N%' or name like '%LM_P%') and offset_id in (12,13,15,16,150,151,152,153)");
            //itemsNumber = dt2.Rows.Count;
            //handels_to_SA = new int[itemsNumber];

            //for (int i = 1; i < itemsNumber; i++)
            //{
            //    handels_to_SA[i] = (int)itemServerHandles.GetValue(int.Parse(dt2.Rows[i][0].ToString()));

            //}
            //grupoConectado.OPCItems.SetActive(itemsNumber - 1, handels_to_SA, true, out ErrorSetActive);


            //timer3.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            requisicionL.Text = "";
            cantidadRL.Text = "";
            clienteL.Text = "";
            operadorL.Text = "";

        }

        private void button12_Click(object sender, EventArgs e)
        {
            passOperator p = new passOperator();
            p.ShowDialog();
            if (PR.get_o() == 1)
            {
                if (comboBox4.SelectedItem == null) return;
                conexionBD cx = new conexionBD();
                try
                {
                    string path = cx.selectVal("imgpath", "caja where idcaja=" + ((KeyValuePair<int, string>)comboBox4.SelectedItem).Key);
                    string actualval = cx.selectVal("valoresactuales_idvaloresactuales", "caja where idcaja=" + ((KeyValuePair<int, string>)comboBox4.SelectedItem).Key);
                    cx.delete("caja where idcaja=" + ((KeyValuePair<int, string>)comboBox4.SelectedItem).Key);
                    cx.delete("valoresp where idvaloresp=" + actualval);
                    File.Delete(path);
                    comboBox4.Text = "";
                    comboBox4.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error de actualización");
                }
                PR.set_o(0);
            }
        }

        private void button138_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("El sistema tomará todas las posiciones actuales de los ejes como referencia reemplazando las anteriores para éste modelo de caja: " + comboBox4.Text + " \n ¿Deseas continuar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                passOperator p = new passOperator();
                p.ShowDialog();
                if (PR.get_o() == 1)
                {
                    conexionBD cx = new conexionBD();
                    string idval = cx.selectVal("valoresactuales_idvaloresActuales", "caja where clave='" + comboBox4.Text + "'");
                    if (idval != "")
                    {
                        try
                        {
                            List<Text> lText = TagManager.Instance.TextList.Select(x => x).Where(x => x.Tag != null && x.Tag.Contains("APOS")).ToList();

                            string updStr = "";
                            foreach (var i in lText)
                            {
                                int x = int.Parse(i.Tag.Split(new Char[1] { '.' })[1].Replace("MS", ""));
                                if (x != 23 && x != 33 && x != 123 && x != 133 && x != 42)
                                {
                                    updStr += "P" + x + "=" + Math.Round(i.Value / 1000) + ",";
                                }
                            }
                            string ustring = updStr.Substring(0, updStr.LastIndexOf(","));
                            cx.update("valoresp", ustring + " where idValoresP =" + idval);
                            MessageBox.Show("Actualización completada", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Error en la actualización", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Es necesario tener seleccionado un modelo de caja");
                    }
                }
            }
            PR.set_o(0);
        }

        private void button139_Click(object sender, EventArgs e)
        {
            string p = "40ms.pressure.protection";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown227.Value + " where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown227.Value);
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void label232_Click(object sender, EventArgs e)
        {

        }

        private void button147_Click(object sender, EventArgs e)
        {
            string p = "R12_15";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown235.Value + "where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown235.Value);
        }

        private void numericUpDown235_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button140_Click(object sender, EventArgs e)
        {
            string p = "R15_12";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown228.Value + "where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown228.Value);
        }

        private void button141_Click(object sender, EventArgs e)
        {
            string p = "R13_16";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown229.Value + "where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown229.Value);
        }

        private void button142_Click(object sender, EventArgs e)
        {
            string p = "R16_13";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown230.Value + "where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown230.Value);
        }

        private void button146_Click(object sender, EventArgs e)
        {
            string p = "R150_151";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown234.Value + "where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown234.Value);
        }

        private void button145_Click(object sender, EventArgs e)
        {
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown233.Value + "where id=7");
        }

        private void button144_Click(object sender, EventArgs e)
        {
            string p = "R152_153";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown232.Value + "where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown232.Value);
        }

        private void button143_Click(object sender, EventArgs e)
        {
            string p = "R153_152";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown231.Value + "where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown231.Value);
        }

        private void button148_Click(object sender, EventArgs e)
        {
            string p = "INTROFAC";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown236.Value + "where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown236.Value);
        }

        private void button149_Click(object sender, EventArgs e)
        {
            string p = "RANFAC";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown237.Value + "where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown234.Value);
        }

        private void button150_Click(object sender, EventArgs e)
        {
            pass p = new pass();
            p.ShowDialog();
            if (PR.get_p() == 1)
            {
                Form cdrives = new cDrives();
                cdrives.ShowDialog();
            }
            PR.set_0();
        }

        private void button151_Click(object sender, EventArgs e)
        {
            string p = "MAXSYNC_DRIVES";
            conexionBD cx = new conexionBD();
            cx.update("sysparams", "value= " + numericUpDown238.Value + " where name='" + p + "'");
            AppModule.Instance.SysParams.UpdateParameter(p, numericUpDown238.Value);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            pass p = new pass();
            p.ShowDialog();
            if (PR.get_p() == 1)
            {
                conexionBD cx = new conexionBD();
                try
                {
                    List<Text> lText = TagManager.Instance.TextList.Select(x => x).Where(x => x.Tag != null && x.Tag.Contains("0MS.APOS")).ToList();

                    string updStr = "";
                    for (int i = 0; i <= lText.Count; i++)
                    {
                        if (lText[i].OffsetId == 10)
                        {
                            lText.RemoveAt(i);
                            break;
                        }
                    }
                    for (int i = 0; i <= lText.Count; i++)
                    {
                        if (lText[i].OffsetId == 150)
                        {
                            lText.RemoveAt(i);
                            break;
                        }
                    }
                    foreach (var i in lText)
                    {
                        int x = int.Parse(i.Tag.Split(new Char[1] { '.' })[1].Replace("MS", ""));
                        updStr += "P" + x + "=" + Math.Round(i.Value / 1000) + ",";
                    }
                    string ustring = updStr.Substring(0, updStr.LastIndexOf(","));
                    cx.update("drivescero", ustring + " where idp = 1");
                    MessageBox.Show("Actualización completada");
                }
                catch
                {
                    MessageBox.Show("Error en la actualización", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            PR.set_0();
        }

        private void button152_Click(object sender, EventArgs e)
        {
            passOperator p = new passOperator();
            p.ShowDialog();
            if (PR.get_o() == 1)
            {
                conexionBD cnn = new conexionBD();
                DataTable Reader = cnn.selectFrom("P20,P30,P120,P130,P40,P50", "drivescero where idp = 1");
                int indexD = 7;
                int indexE = 13;
                for (int i = 0; i < 6; i++)
                {
                    mOpcTextList[indexD].Value = Double.Parse(Reader.Rows[0][i].ToString());
                    escribir(indexD);
                    mOpcTextList[indexE].Value = 1;
                    escribir(indexE);
                    indexD++;
                    indexE++;
                }
            }
            PR.set_o(0);

        }

        private void button155_Click(object sender, EventArgs e)
        {
            passOperator p = new passOperator();
            p.ShowDialog();
            if (PR.get_o() == 1)
            {
                conexionBD cx = new conexionBD();
                try
                {
                    List<Text> lText = TagManager.Instance.TextList.Select(x => x).Where(x => x.Tag != null && x.Tag.Contains("0MS.APOS")).ToList();

                    string updStr = "";
                    foreach (var i in lText)
                    {
                        int x = int.Parse(i.Tag.Split(new Char[1] { '.' })[1].Replace("MS", ""));
                        updStr += "P" + x + "=" + Math.Round(i.Value / 1000) + ",";
                    }
                    string ustring = updStr.Substring(0, updStr.LastIndexOf(",P10"));
                    cx.update("drivescero", ustring + " where idp = 2");
                    label237.Text = "Último registro " + DateTime.Now.ToString();
                }
                catch
                {
                }
            }
            PR.set_o(0);
        }

        private void button154_Click(object sender, EventArgs e)
        {
            passOperator p = new passOperator();
            p.ShowDialog();
            if (PR.get_o() == 1)
            {
                conexionBD cnn = new conexionBD();
                DataTable Reader = cnn.selectFrom("P20,P30,P120,P130,P40,P50", "drivescero");
                int indexD = 7;
                int indexE = 13;
                List<Text> lText = TagManager.Instance.TextList.Select(x => x).Where(x => x.Tag != null && x.Tag.Contains("0MS.APOS")).ToList();

                double[] updStr = new double[lText.Count + 1];
                int j = 0;
                foreach (var i in lText)
                {
                    updStr[j] = Math.Round(i.Value / 1000);
                    j++;
                }
                for (int i = 0; i < 6; i++)
                {
                    if (Double.Parse(Reader.Rows[1][i].ToString()) != updStr[i])
                    {
                        mOpcTextList[indexD].Value = Double.Parse(Reader.Rows[1][i].ToString()) * 1000;
                        escribir(indexD);
                        mOpcTextList[indexE].Value = 1;
                        escribir(indexE);
                    }
                    indexD++;
                    indexE++;
                }
            }
            PR.set_o(0);
        }

        private void button153_Click(object sender, EventArgs e)
        {
            int indexE = 13;
            for (int i = 0; i < 6; i++)
            {
                mOpcTextList[indexE].Value = 0;
                escribir(indexE);
                indexE++;
            }
        }

        private void button156_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.ShowDialog();
        }

        private void numericUpDown9_Enter(object sender, EventArgs e)
        {
            NumericUpDown cb = (NumericUpDown)sender;
            cb.Text = "";
        }

        private void numericUpDown2_Enter(object sender, EventArgs e)
        {
            NumericUpDown cb = (NumericUpDown)sender;
            cb.Text = "";
        }

        private void button157_Click(object sender, EventArgs e)
        {
            pass p = new pass();
            p.ShowDialog();
            if (PR.get_p() == 1)
            {
                Predeterminado f = new Predeterminado();
                f.ShowDialog();
            }
            PR.set_0();
        }

        private void button81_Click(object sender, EventArgs e)
        {

        }

        private void button93_Click(object sender, EventArgs e)
        {

        }

        private void button158_Click(object sender, EventArgs e)
        {
            pass p = new pass();
            p.ShowDialog();
            if (PR.get_p() == 1)
            {
                Form cpass = new cPass();
                cpass.ShowDialog();
            }
            PR.set_0();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
                button30.Enabled = false;
        }



    }
}
