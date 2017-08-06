using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Management;
using OPCAutomation;

namespace Impresora
{
    public partial class SegundoColor : Form
    {
     RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        static int numeroitems = 43;
        static int numeroitems1 = numeroitems + 1;
        OPCServer servidorOpc;
        OPCGroup grupoConectado;


        String[] opcIdItems = new String[numeroitems1];
        Int32[] clientHandles = new Int32[numeroitems1];
        public Array itemServerHandles;
        static int actual_array_size = numeroitems + 1;
        Object[] opcItemNames = new Object[numeroitems1];
        Control[] opcItemValue = new Control[numeroitems1];
        Control[] opcItemValueToWritte = new Control[numeroitems1];
        Object[] opcItemButtonToWritte = new Object[numeroitems1];
        Boolean[] opcItemActiveState = new Boolean[numeroitems1];
        Control[] opcItemQuality = new Control[numeroitems1];
        int[] OPCItemIsArray = new int[numeroitems1];

        TextBox cont = new TextBox();
        Label L201 = new Label();

        Label l1 = new Label();
        Label l2 = new Label();
        Label l3 = new Label();
        Label l4 = new Label();
        Label l5 = new Label();
        Label l6 = new Label();
        Label l7 = new Label();
        Label l8 = new Label();
        Label l9 = new Label();
        Label l10 = new Label();
        Label l11 = new Label();
        Label l12 = new Label();
        Label l13 = new Label();
        Label l14 = new Label();
        Label l15 = new Label();
        Label l16 = new Label();
        Label l17 = new Label();
        Label l18 = new Label();
        Label l19 = new Label();
        Label l20 = new Label();
        Label l21 = new Label();
        Label l22 = new Label();
        Label l23 = new Label();
        Label l24 = new Label();
        Label l25 = new Label();
        Label l26 = new Label();
        Label l27 = new Label();
        Label l28 = new Label();
        Label l29 = new Label();
        Label l30 = new Label();
        Label l31 = new Label();
        Label l32 = new Label();
        Label l33 = new Label();
        Label l34 = new Label();
        Label l35 = new Label();
        Label l36 = new Label();
        Label l37 = new Label();
        Label l38 = new Label();
        Label l39 = new Label();
        Label l40 = new Label();
        Label l41 = new Label();
        Label l42 = new Label();
        Label l43 = new Label();
        Label l44 = new Label();
        Label l45 = new Label();
        Label l46 = new Label();

        TextBox t01 = new TextBox();
        TextBox t02 = new TextBox();
        TextBox t03 = new TextBox();
        TextBox t04 = new TextBox();
        TextBox t05 = new TextBox();
        TextBox t06 = new TextBox();
        TextBox t07 = new TextBox();
        TextBox t08 = new TextBox();
        TextBox t09 = new TextBox();
        TextBox t10 = new TextBox();
        TextBox t11 = new TextBox();
        TextBox t12 = new TextBox();
        TextBox t13 = new TextBox();
        TextBox t14 = new TextBox();
        TextBox t15 = new TextBox();
        TextBox t16 = new TextBox();
        TextBox t17 = new TextBox();
        TextBox t18 = new TextBox();
        TextBox t19 = new TextBox();
        TextBox t20 = new TextBox();
        TextBox t21 = new TextBox();
        TextBox t22 = new TextBox();
        TextBox t23 = new TextBox();
        TextBox t24 = new TextBox();
        TextBox t25 = new TextBox();
        TextBox t26 = new TextBox();
        TextBox t27 = new TextBox();
        TextBox t28 = new TextBox();
        TextBox t29 = new TextBox();
        TextBox t30 = new TextBox();
        TextBox t31 = new TextBox();
        TextBox t32 = new TextBox();
        TextBox t33 = new TextBox();
        TextBox t34 = new TextBox();
        TextBox t35 = new TextBox();
        TextBox t36 = new TextBox();
        TextBox t37 = new TextBox();
        TextBox t38 = new TextBox();
        TextBox t39 = new TextBox();
        TextBox t40 = new TextBox();
        TextBox t41 = new TextBox();
        TextBox t42 = new TextBox();
        TextBox t43 = new TextBox();


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

        
        
        public SegundoColor()
        {
            
            InitializeComponent();
            

            opcItemValue[1] = l1;     //PRIMER COLOR APOS
            opcItemValue[2] = l2;     //SEGUNDO COLOR APOS
            opcItemValue[3] = l3;     //TERCER COLOR APOS
            opcItemValue[4] = l4;     //CUARTO COLOR APOS
            opcItemValue[5] = l5;     //TROQUEL APOS
            opcItemValue[6] = l6;      //SLOTER APOS
            opcItemValue[7] = l7;     //20MS.DESP
            opcItemValue[8] = l8;     //30MS.DESP
            opcItemValue[9] = l9;     //120MS.DESP
            opcItemValue[10] = l10;    //130MS.DESP
            opcItemValue[11] = l11;    //40MS.DESP
            opcItemValue[12] = l12;    //50MS.DESP
            opcItemValue[13] = l13;          //20MS.DESP_EN 
            opcItemValue[14] = l14;          //30MS.DESP_EN 
            opcItemValue[15] = l15;          //120MS.DESP_EN
            opcItemValue[16] = l16;          //130MS.DESP_EN
            opcItemValue[17] = l17;          //40MS.DESP_EN
            opcItemValue[18] = l18;          //50MS.DESP_EN
            opcItemValue[19] = l19;     //120MS.DESP
            opcItemValue[20] = l20;    //130MS.DESP
            opcItemValue[21] = l21;    //40MS.DESP
            opcItemValue[22] = l22;    //50MS.DESP
            opcItemValue[23] = l23;          //20MS.DESP_EN 
            opcItemValue[24] = l24;          //
            opcItemValue[25] = l25;          //120MS.DESP_EN
            opcItemValue[26] = l26;          //130MS.DESP_EN
            opcItemValue[27] = l27;          //40MS.DESP_EN
            opcItemValue[28] = l28;          //50MS.DESP_EN
            opcItemValue[29] = l29;     //120MS.DESP
            opcItemValue[30] = l30;
            opcItemValue[31] = l31;    //40MS.DESP
            opcItemValue[32] = l32;    //50MS.DESP
            opcItemValue[33] = l33;          //20MS.DESP_EN 
            opcItemValue[34] = l34;          //
            opcItemValue[35] = l35;          //120MS.DESP_EN
            opcItemValue[36] = l36;          //130MS.DESP_EN
            opcItemValue[37] = l37;          //40MS.DESP_EN
            opcItemValue[38] = l38;          //50MS.DESP_EN
            opcItemValue[39] = l39;     //13 ALARMA
            opcItemValue[40] = l40;
            opcItemValue[41] = l41;     //13 ALARMA
            opcItemValue[42] = l42;
            opcItemValue[43] = l43;     //13 ALARMA
            //opcItemValue[44] = l44;
            //opcItemValue[45] = l45;     //13 ALARMA
            //opcItemValue[46] = l46;

            opcItemValueToWritte[1] = t01;
            opcItemValueToWritte[2] = t02;
            opcItemValueToWritte[3] = t03;
            opcItemValueToWritte[4] = t04;
            opcItemValueToWritte[5] = t05;
            opcItemValueToWritte[6] = t06;
            opcItemValueToWritte[7] = t07;
            opcItemValueToWritte[8] = t08;
            opcItemValueToWritte[9] = t09;
            opcItemValueToWritte[10] = t10;
            opcItemValueToWritte[11] = t11;
            opcItemValueToWritte[12] = t12;
            opcItemValueToWritte[13] = t13;
            opcItemValueToWritte[14] = t14;
            opcItemValueToWritte[15] = t15;
            opcItemValueToWritte[16] = t16;
            opcItemValueToWritte[17] = t17;
            opcItemValueToWritte[18] = t18;
            opcItemValueToWritte[19] = t19;
            opcItemValueToWritte[20] = t20;
            opcItemValueToWritte[21] = t21;
            opcItemValueToWritte[22] = t22;
            opcItemValueToWritte[23] = t23;
            opcItemValueToWritte[24] = t24;
            opcItemValueToWritte[25] = t25;
            opcItemValueToWritte[26] = t26;
            opcItemValueToWritte[27] = t27;
            opcItemValueToWritte[28] = t28;
            opcItemValueToWritte[29] = t29;
            opcItemValueToWritte[30] = t30;
            opcItemValueToWritte[31] = t31;
            opcItemValueToWritte[32] = t32;
            opcItemValueToWritte[33] = t33;
            opcItemValueToWritte[34] = t34;
            opcItemValueToWritte[35] = t35;
            opcItemValueToWritte[36] = t36;
            opcItemValueToWritte[37] = t37;
            opcItemValueToWritte[38] = t38;
            opcItemValueToWritte[39] = t39;
            opcItemValueToWritte[40] = t40;
            opcItemValueToWritte[41] = t41;
            opcItemValueToWritte[42] = t42;
            opcItemValueToWritte[43] = t43;
            //opcItemValueToWritte[44] = t44;
            //opcItemValueToWritte[45] = t45;
            //opcItemValueToWritte[46] = t46;
            //opcItemValueToWritte[47] = t47;
            //opcItemValueToWritte[48] = t48;
            //opcItemValueToWritte[49] = t49;
            //opcItemValueToWritte[50] = t50;


            this.l1.TextChanged += new System.EventHandler(this.label1_TextChanged);
            this.l2.TextChanged += new System.EventHandler(this.label2_TextChanged);
            this.l3.TextChanged += new System.EventHandler(this.label3_TextChanged);
            this.l4.TextChanged += new System.EventHandler(this.label4_TextChanged);
            this.l5.TextChanged += new System.EventHandler(this.label5_TextChanged);
            this.l6.TextChanged += new System.EventHandler(this.label6_TextChanged);
            this.l7.TextChanged += new System.EventHandler(this.label7_TextChanged);
            this.l8.TextChanged += new System.EventHandler(this.label8_TextChanged);
            this.l9.TextChanged += new System.EventHandler(this.label9_TextChanged);
            this.l10.TextChanged += new System.EventHandler(this.label10_TextChanged);
            this.l11.TextChanged += new System.EventHandler(this.label11_TextChanged);
            this.l12.TextChanged += new System.EventHandler(this.label12_TextChanged);
            this.l13.TextChanged += new System.EventHandler(this.label13_TextChanged);
            this.l14.TextChanged += new System.EventHandler(this.label14_TextChanged);
            this.l15.TextChanged += new System.EventHandler(this.label15_TextChanged);
            this.l16.TextChanged += new System.EventHandler(this.label16_TextChanged);
            this.l17.TextChanged += new System.EventHandler(this.label17_TextChanged);
            this.l18.TextChanged += new System.EventHandler(this.label18_TextChanged);
            //this.l19.TextChanged += new System.EventHandler(this.label19_TextChanged);
            //this.l20.TextChanged += new System.EventHandler(this.label20_TextChanged);
            //this.l21.TextChanged += new System.EventHandler(this.label21_TextChanged);
            //this.l22.TextChanged += new System.EventHandler(this.label22_TextChanged);
            //this.l23.TextChanged += new System.EventHandler(this.label23_TextChanged);
            //this.l24.TextChanged += new System.EventHandler(this.label24_TextChanged);
            //this.l31.TextChanged += new System.EventHandler(this.label31_TextChanged);
            //this.l32.TextChanged += new System.EventHandler(this.label32_TextChanged);
            //this.l33.TextChanged += new System.EventHandler(this.label33_TextChanged);
            //this.l34.TextChanged += new System.EventHandler(this.label34_TextChanged);
            //this.l39.TextChanged += new System.EventHandler(this.label39_TextChanged);
            //this.l40.TextChanged += new System.EventHandler(this.label40_TextChanged);
            //this.l41.TextChanged += new System.EventHandler(this.label41_TextChanged);
            //this.l42.TextChanged += new System.EventHandler(this.label42_TextChanged);


            String nombreOPCServer = "KEPware.KEPServerEx.V4";

            try
            {
                servidorOpc = new OPCAutomation.OPCServer();
                servidorOpc.Connect(nombreOPCServer, "");
                button1.Enabled = true;
                
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
                grupoConectado = servidorOpc.OPCGroups.Add("LENZE");
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
                                
                opcIdItems[1] = "LENZE.30MS.APOS"; // De SW AUTO MAN
                OPCItemIsArray[1] = 0;
                clientHandles[1] = 1;
                opcIdItems[2] = "LENZE.30MS.DESP"; // De SW AUTO MAN
                OPCItemIsArray[2] = 0;
                clientHandles[2] = 2;
                opcIdItems[3] = "LENZE.30MS.EN_DESP"; // De SW AUTO MAN
                OPCItemIsArray[3] = 0;
                clientHandles[3] = 3;
                opcIdItems[4] = "LENZE.31MS.APOS"; // De SW AUTO MAN
                OPCItemIsArray[4] = 0;
                clientHandles[4] = 4;
                opcIdItems[5] = "LENZE.31MS.DESP"; // De SW AUTO MAN
                OPCItemIsArray[5] = 0;
                clientHandles[5] = 5;
                opcIdItems[6] = "LENZE.31MS.EN_DESP"; // De SW AUTO MAN
                OPCItemIsArray[6] = 0;
                clientHandles[6] = 6;
                opcIdItems[7] = "LENZE.32MS.APOS"; // De SW AUTO MAN
                OPCItemIsArray[7] = 0;
                clientHandles[7] = 7;
                opcIdItems[8] = "LENZE.32MS.DESP"; // De SW AUTO MAN
                OPCItemIsArray[8] = 0;
                clientHandles[8] = 8;
                opcIdItems[9] = "LENZE.32MS.EN_DESP"; // De SW AUTO MAN
                OPCItemIsArray[9] = 0;
                clientHandles[9] = 9;
                opcIdItems[10] = "LENZE.33MS.APOS"; // De SW AUTO MAN
                OPCItemIsArray[10] = 0;
                clientHandles[10] = 10;
                opcIdItems[11] = "LENZE.33MS.DESP"; // De SW AUTO MAN
                OPCItemIsArray[11] = 0;
                clientHandles[11] = 11;
                opcIdItems[12] = "LENZE.33MS.EN_DESP"; // De SW AUTO MAN
                OPCItemIsArray[12] = 0;
                clientHandles[12] = 12;
                opcIdItems[13] = "LENZE.34MS.APOS"; // De SW AUTO MAN
                OPCItemIsArray[13] = 0;
                clientHandles[13] = 13;
                opcIdItems[14] = "LENZE.34MS.DESP"; // De SW AUTO MAN
                OPCItemIsArray[14] = 0;
                clientHandles[14] = 14;
                opcIdItems[15] = "LENZE.34MS.EN_DESP"; // De SW AUTO MAN
                OPCItemIsArray[15] = 0;
                clientHandles[15] = 15;
                opcIdItems[16] = "LENZE.35MS.APOS"; // De SW AUTO MAN
                OPCItemIsArray[16] = 0;
                clientHandles[16] = 16;
                opcIdItems[17] = "LENZE.35MS.DESP"; // De SW AUTO MAN
                OPCItemIsArray[17] = 0;
                clientHandles[17] = 17;
                opcIdItems[18] = "LENZE.35MS.EN_DESP"; // De SW AUTO MAN
                OPCItemIsArray[18] = 0;
                clientHandles[18] = 18;
                //opcIdItems[19] = "LENZE.16MS.APOS"; // De SW AUTO MAN
                //OPCItemIsArray[19] = 0;
                //clientHandles[19] = 19;
                //opcIdItems[20] = "LENZE.16MS.DESP"; // De SW AUTO MAN
                //OPCItemIsArray[20] = 0;
                //clientHandles[20] = 20;
                //opcIdItems[21] = "LENZE.16MS.EN_DESP"; // De SW AUTO MAN
                //OPCItemIsArray[21] = 0;
                //clientHandles[21] = 21;
                //opcIdItems[22] = "LENZE.122MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[22] = 0;
                //clientHandles[22] = 22;
                //opcIdItems[23] = "LENZE.123MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[23] = 0;
                //clientHandles[23] = 23;
                //opcIdItems[24] = "LENZE.124MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[24] = 0;
                //clientHandles[24] = 24;
                //opcIdItems[25] = "LENZE.125MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[25] = 0;
                //clientHandles[25] = 25;
                //opcIdItems[26] = "LENZE.130MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[26] = 0;
                //clientHandles[26] = 26;
                //opcIdItems[27] = "LENZE.131MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[27] = 0;
                //clientHandles[27] = 27;
                //opcIdItems[28] = "LENZE.132MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[28] = 0;
                //clientHandles[28] = 28;
                //opcIdItems[29] = "LENZE.133MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[29] = 0;
                //clientHandles[29] = 29;
                //opcIdItems[30] = "LENZE.134MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[30] = 0;
                //clientHandles[30] = 30;
                //opcIdItems[31] = "LENZE.135MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[31] = 0;
                //clientHandles[31] = 31;
                //opcIdItems[32] = "LENZE.40MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[32] = 0;
                //clientHandles[32] = 32;
                //opcIdItems[33] = "LENZE.41MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[33] = 0;
                //clientHandles[33] = 33;
                //opcIdItems[34] = "LENZE.42MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[34] = 0;
                //clientHandles[34] = 34;
                //opcIdItems[35] = "LENZE.50MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[35] = 0;
                //clientHandles[35] = 35;
                //opcIdItems[36] = "LENZE.51MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[36] = 0;
                //clientHandles[36] = 36;
                //opcIdItems[37] = "LENZE.52MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[37] = 0;
                //clientHandles[37] = 37;
                //opcIdItems[38] = "LENZE.53MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[38] = 0;
                //clientHandles[38] = 38;
                //opcIdItems[39] = "LENZE.54MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[39] = 0;
                //clientHandles[39] = 39;
                //opcIdItems[40] = "LENZE.150MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[40] = 0;
                //clientHandles[40] = 40;
                //opcIdItems[41] = "LENZE.151MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[41] = 0;
                //clientHandles[41] = 41;
                //opcIdItems[42] = "LENZE.152MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[42] = 0;
                //clientHandles[42] = 42;
                //opcIdItems[43] = "LENZE.153MS.ALARMA_RESET"; // De SW AUTO MAN
                //OPCItemIsArray[43] = 0;
                //clientHandles[43] = 43;

                grupoConectado.OPCItems.DefaultIsActive = true;
                grupoConectado.OPCItems.AddItems(ItemCount, opcIdItems, clientHandles, out itemServerHandles, out addItemServerErrors);

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


            }
            catch (Exception ex)
            {
                MessageBox.Show("OPC server fallo al agregar los items solicitados al grupo con: " + ex.Message, "Conexion Exception", MessageBoxButtons.OK);
            }
         
        }



        private void label1_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            conexionBD cnn = new conexionBD();
           
            dat = Math.Round((dat + PR.getOffset("30")) / 1000, 3);
            numericUpDown7.Value = (Decimal)dat;

        }

        private void label2_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("30")) / 1000, 3);
            numericUpDown18.Value = (Decimal)dat;

        }
        private void label3_TextChanged(Object sender, EventArgs e)
        {
            Label text = new Label();
            text = (Label)sender;
            if (text.Text == "1")
            {
                label2.ForeColor = Color.Lime;
                numericUpDown18.BackColor = Color.Lime;
                label13.ForeColor = Color.Lime;
                numericUpDown7.BackColor = Color.Lime;
                label26.ForeColor = Color.Lime;
                numericUpDown21.BackColor = Color.Lime;
                label33.ForeColor = Color.Lime;
                button17.BackColor = Color.Lime;
                button46.BackColor = Color.Lime;
                button17.Enabled = false;
                button46.Enabled = false;
                numericUpDown21.Enabled = false;
            }
            else
            {
                label2.ForeColor = Label.DefaultForeColor;
                numericUpDown18.BackColor = NumericUpDown.DefaultBackColor;
                label13.ForeColor = Label.DefaultForeColor;
                numericUpDown7.BackColor = NumericUpDown.DefaultBackColor;
                label26.ForeColor = Label.DefaultForeColor;
                numericUpDown21.BackColor = Color.White;
                label33.ForeColor = Label.DefaultForeColor;
                button17.BackColor = Button.DefaultBackColor;
                button46.BackColor = Button.DefaultBackColor;
                button17.Enabled = true;
                button46.Enabled = true;
                numericUpDown21.Enabled = true;
            }

        }
        private void label4_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("31"))/ 1000, 3);
            numericUpDown6.Value = (Decimal)((0.000000000078953 * Math.Pow(dat, 5)) + (-0.000000039888 * Math.Pow(dat, 4)) + (0.0000018011 * Math.Pow(dat, 3)) + (0.00094145 * Math.Pow(dat, 2)) + (0.000049497 * (dat)) - 0.0062);

        }
        private void label5_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("31")) / 1000, 3);
            numericUpDown13.Value = (Decimal)((0.000000000078953 * Math.Pow(dat, 5)) + (-0.000000039888 * Math.Pow(dat, 4)) + (0.0000018011 * Math.Pow(dat, 3)) + (0.00094145 * Math.Pow(dat, 2)) + (0.000049497 * (dat)) - 0.0062);

        }
        private void label6_TextChanged(Object sender, EventArgs e)
        {

            Label text = new Label();
            text = (Label)sender;
            if (text.Text == "1")
            {
                label3.ForeColor = Color.Lime;
                numericUpDown13.BackColor = Color.Lime;
                label14.ForeColor = Color.Lime;
                numericUpDown6.BackColor = Color.Lime;
                label25.ForeColor = Color.Lime;
                numericUpDown20.BackColor = Color.Lime;
                label32.ForeColor = Color.Lime;
                button1.BackColor = Color.Lime;
                button47.BackColor = Color.Lime;
                button1.Enabled = false;
                button47.Enabled = false;
                numericUpDown20.Enabled = false;
            }
            else
            {
                label3.ForeColor = Label.DefaultForeColor;
                numericUpDown13.BackColor = NumericUpDown.DefaultBackColor;
                label14.ForeColor = Label.DefaultForeColor;
                numericUpDown6.BackColor = NumericUpDown.DefaultBackColor;
                label25.ForeColor = Label.DefaultForeColor;
                numericUpDown20.BackColor = Color.White;
                label32.ForeColor = Label.DefaultForeColor;
                button1.BackColor = Button.DefaultBackColor;
                button47.BackColor = Button.DefaultBackColor;
                button1.Enabled = true;
                button47.Enabled = true;
                numericUpDown20.Enabled = true;
            }

        }
        private void label7_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("32")) / 1000, 3);
            numericUpDown5.Value = (Decimal)((0.000000000078953 * Math.Pow(dat, 5)) + (-0.000000039888 * Math.Pow(dat, 4)) + (0.0000018011 * Math.Pow(dat, 3)) + (0.00094145 * Math.Pow(dat, 2)) + (0.000049497 * (dat)) - 0.0062); 

        }
        private void label8_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("32")) / 1000, 3);
            numericUpDown14.Value = (Decimal)((0.000000000078953 * Math.Pow(dat, 5)) + (-0.000000039888 * Math.Pow(dat, 4)) + (0.0000018011 * Math.Pow(dat, 3)) + (0.00094145 * Math.Pow(dat, 2)) + (0.000049497 * (dat)) - 0.0062);

        }
        private void label9_TextChanged(Object sender, EventArgs e)
        {
            Label text = new Label();
            text = (Label)sender;
            if (text.Text == "1")
            {
                label4.ForeColor = Color.Lime;
                numericUpDown14.BackColor = Color.Lime;
                label15.ForeColor = Color.Lime;
                numericUpDown5.BackColor = Color.Lime;
                label24.ForeColor = Color.Lime;
                numericUpDown12.BackColor = Color.Lime;
                label31.ForeColor = Color.Lime;
                button4.BackColor = Color.Lime;
                button48.BackColor = Color.Lime;
                button4.Enabled = false;
                button48.Enabled = false;
                numericUpDown12.Enabled = false;
            }
            else
            {
                label4.ForeColor = Label.DefaultForeColor;
                numericUpDown14.BackColor = NumericUpDown.DefaultBackColor;
                label15.ForeColor = Label.DefaultForeColor;
                numericUpDown5.BackColor = NumericUpDown.DefaultBackColor;
                label24.ForeColor = Label.DefaultForeColor;
                numericUpDown12.BackColor = Color.White;
                label31.ForeColor = Label.DefaultForeColor;
                button4.BackColor = Button.DefaultBackColor;
                button48.BackColor = Button.DefaultBackColor;
                button4.Enabled = true;
                button48.Enabled = true;
                numericUpDown12.Enabled = true;
            }
         
        }
        private void label10_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("33")) / 1000, 3);
            numericUpDown4.Value = (Decimal)dat;

        }
        private void label11_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("33")) / 1000, 3);
            numericUpDown15.Value = (Decimal)dat;

        }
        private void label12_TextChanged(Object sender, EventArgs e)
        {
            Label text = new Label();
            text = (Label)sender;
            if (text.Text == "1")
            {
                label5.ForeColor = Color.Lime;
                numericUpDown15.BackColor = Color.Lime;
                label16.ForeColor = Color.Lime;
                numericUpDown4.BackColor = Color.Lime;
                label23.ForeColor = Color.Lime;
                numericUpDown11.BackColor = Color.Lime;
                label30.ForeColor = Color.Lime;
                button7.BackColor = Color.Lime;
                button49.BackColor = Color.Lime;
                button7.Enabled = false;
                button49.Enabled = false;
                numericUpDown11.Enabled = false;
            }
            else
            {
                label5.ForeColor = Label.DefaultForeColor;
                numericUpDown15.BackColor = NumericUpDown.DefaultBackColor;
                label16.ForeColor = Label.DefaultForeColor;
                numericUpDown4.BackColor = NumericUpDown.DefaultBackColor;
                label23.ForeColor = Label.DefaultForeColor;
                numericUpDown11.BackColor = Color.White;
                label30.ForeColor = Label.DefaultForeColor;
                button7.BackColor = Button.DefaultBackColor;
                button49.BackColor = Button.DefaultBackColor;
                button7.Enabled = true;
                button49.Enabled = true;
                numericUpDown11.Enabled = true;
            }
        }
        private void label13_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("34")) / 1000, 3);
            numericUpDown3.Value = (Decimal)dat;

        }
        private void label14_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("34")) / 1000, 3);
            numericUpDown16.Value = (Decimal)dat;

        }
        private void label15_TextChanged(Object sender, EventArgs e)
        {

            Label text = new Label();
            text = (Label)sender;
            if (text.Text == "1")
            {
                label6.ForeColor = Color.Lime;
                numericUpDown16.BackColor = Color.Lime;
                label17.ForeColor = Color.Lime;
                numericUpDown3.BackColor = Color.Lime;
                label22.ForeColor = Color.Lime;
                numericUpDown10.BackColor = Color.Lime;
                label29.ForeColor = Color.Lime;
                button50.BackColor = Color.Lime;
                button10.BackColor = Color.Lime;
                button50.Enabled = false;
                button10.Enabled = false;
                numericUpDown10.Enabled = false;
            }
            else
            {
                label6.ForeColor = Label.DefaultForeColor;
                numericUpDown16.BackColor = NumericUpDown.DefaultBackColor;
                label17.ForeColor = Label.DefaultForeColor;
                numericUpDown3.BackColor = NumericUpDown.DefaultBackColor;
                label22.ForeColor = Label.DefaultForeColor;
                numericUpDown10.BackColor = Color.White;
                label29.ForeColor = Label.DefaultForeColor;
                button50.BackColor = Button.DefaultBackColor;
                button10.BackColor = Button.DefaultBackColor;
                button50.Enabled = true;
                button10.Enabled = true;
                numericUpDown10.Enabled = true;
            }

        }
        private void label16_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("35")) / 1000, 3);
            numericUpDown2.Value = (Decimal)dat;

        }
        private void label17_TextChanged(Object sender, EventArgs e)
        {

            Label d = sender as Label;
            Double dat = Double.Parse(d.Text.ToString());
            dat = Math.Round((dat + PR.getOffset("35")) / 1000, 3);
            numericUpDown17.Value = (Decimal)dat;

        }
        private void label18_TextChanged(Object sender, EventArgs e)
        {

            Label text = new Label();
            text = (Label)sender;
            if (text.Text == "1")
            {
                label7.ForeColor = Color.Lime;
                numericUpDown17.BackColor = Color.Lime;
                label18.ForeColor = Color.Lime;
                numericUpDown2.BackColor = Color.Lime;
                label21.ForeColor = Color.Lime;
                numericUpDown9.BackColor = Color.Lime;
                label28.ForeColor = Color.Lime;
                button51.BackColor = Color.Lime;
                button13.BackColor = Color.Lime;
                button51.Enabled = false;
                button13.Enabled = false;
                numericUpDown9.Enabled = false;
            }
            else
            {
                label7.ForeColor = Label.DefaultForeColor;
                numericUpDown17.BackColor = NumericUpDown.DefaultBackColor;
                label18.ForeColor = Label.DefaultForeColor;
                numericUpDown2.BackColor = NumericUpDown.DefaultBackColor;
                label21.ForeColor = Label.DefaultForeColor;
                numericUpDown9.BackColor = Color.White;
                label28.ForeColor = Label.DefaultForeColor;
                button51.BackColor = Button.DefaultBackColor;
                button13.BackColor = Button.DefaultBackColor;
                button51.Enabled = true;
                button13.Enabled = true;
                numericUpDown9.Enabled = true;
            }

        }

        private void escribir(int index)
        {
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
                                return;
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
                                return;
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
                                return;
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
                                return;
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
                                return;
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
                                return;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToInt32(opcItemValueToWritte[index].Text);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtFloat:
                        if ((int)OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(float), (int)OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, opcItemValueToWritte[index].Text))
                            {
                                return;
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
                                return;
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
                                return;
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
                                return;
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
                        return;
                    // End case
                }

                // Invoke the SyncWrite operation.  Remember this call will wait until completion
                grupoConectado.SyncWrite(ItemCount, SyncItemServerHandles, SyncItemValues, out SyncItemServerErrors);

                if ((int)SyncItemServerErrors.GetValue(1) != 0)
                {
                    MessageBox.Show("SyncItemServerError: " + SyncItemServerErrors.GetValue(1));
                }
            }
            catch (Exception ex)
            {
                // Error handling
                MessageBox.Show("OPC server write item failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
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
                        opcItemValue[int.Parse(ClientHandles.GetValue(i).ToString())].Text = "";
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


                            opcItemValue[int.Parse(ClientHandles.GetValue(i).ToString())].Text = opcItemValue[int.Parse(ClientHandles.GetValue(i).ToString())].Text + ItsAnArray.GetValue(x).ToString() + Suffix;


                        }
                    }
                    else
                    {
                        int ii = (int)ClientHandles.GetValue(i);
                        opcItemValue[ii].Text = ItemValues.GetValue(i).ToString();

                    }

                    // Check the Qualties for each item retured here.  The actual contents of the
                    // quality field can contain bit field data which can provide specific
                    // error conditions.  Normally if everything is OK then the quality will
                    // contain the 0xC0
                    int good = (int)OPCAutomation.OPCQuality.OPCQualityGood;


                    if (!Qualities.GetValue(i).Equals(good))
                    {
                        MessageBox.Show("Vuelve intentar datos inciertos", "Alarma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            }
            catch (Exception ex)
            {
                // Error handling
                MessageBox.Show("OPC Labels failed with exception: " + ex.Message, "Exception", MessageBoxButtons.OK);
            }

        }

        private bool LoadArray(ref System.Array AnArray, int CanonDT, string wrTxt)
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

          private void button14_Click(object sender, EventArgs e)
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
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
                t12.Text = "0";
                escribir(12);
          
        }

        private void button18_Click(object sender, EventArgs e)
        {
                           
            
        }

        private void OPCItemActiveState_CheckedChanged(int index)
        {
            
                try
                {
                    // Change only 1 item
                    short ItemCount = 1;

                    // Dim local arrays to pass desired item for state change
                    int[] ActiveItemServerHandles = new int[2];
                    bool ActiveState = false;
                    System.Array ActiveItemErrors = null;

                    // Get the desired state from the control.
                    ActiveState = opcItemActiveState[index];
                    
                    // Get the Servers handle for the desired item.  The server handles
                    // were returned in add item subroutine.
                    ActiveItemServerHandles[1] = (int)itemServerHandles.GetValue(index);

                    // Invoke the SetActive operation on the OPC item collection interface
                    grupoConectado.OPCItems.SetActive(ItemCount, ActiveItemServerHandles, ActiveState, out ActiveItemErrors);

                }
                catch (Exception ex)
                {
                    // Error handling
                    MessageBox.Show("OPC server set active state failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            double pos = (double)numericUpDown7.Value - PR.getOffset("30") / 1000;
            double ajus = (double)numericUpDown21.Value;
            if (ajus != 0)
            {
                double npos = (pos + ajus) * 1000;
                t02.Text = npos.ToString();
                escribir(2);
                t03.Text = "1";
                escribir(3);
               // numericUpDown21.Value = 0;
            }
        }

        private void numericUpDown21_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            if(num.Value>=0)
            {
                button46.Image = Impresora.Resources.Images.Add1;
            }
            else 
            {
                button46.Image = Impresora.Resources.Images.Remove;
            }
        }

        private void numericUpDown20_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            if (num.Value >= 0)
            {
                button47.Image = Impresora.Resources.Images.Add1;
            }
            else
            {
                button47.Image = Impresora.Resources.Images.Remove;
            }
        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            if (num.Value >= 0)
            {
                button48.Image = Impresora.Resources.Images.Add1;
            }
            else
            {
                button48.Image = Impresora.Resources.Images.Remove;
            }
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            if (num.Value >= 0)
            {
                button49.Image = Impresora.Resources.Images.Add1;
            }
            else
            {
                button49.Image = Impresora.Resources.Images.Remove;
            }
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            if (num.Value >= 0)
            {
                button50.Image = Impresora.Resources.Images.Add1;
            }
            else
            {
                button50.Image = Impresora.Resources.Images.Remove;
            }
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            if (num.Value >= 0)
            {
                button51.Image = Impresora.Resources.Images.Add1;
            }
            else
            {
                button51.Image = Impresora.Resources.Images.Remove;
            }
        }
                
        private void button46_Click(object sender, EventArgs e)
        {
            numericUpDown21.Value = numericUpDown21.Value * -1;
        }

        private void button47_Click(object sender, EventArgs e)
        {
            numericUpDown20.Value = numericUpDown20.Value * -1;
        }

        private void button48_Click(object sender, EventArgs e)
        {
            numericUpDown12.Value = numericUpDown12.Value * -1;
        }

        private void button49_Click(object sender, EventArgs e)
        {
            numericUpDown11.Value = numericUpDown11.Value * -1;
        }

        private void button50_Click(object sender, EventArgs e)
        {
            numericUpDown10.Value = numericUpDown10.Value * -1;
        }

        private void button51_Click(object sender, EventArgs e)
        {
            numericUpDown9.Value = numericUpDown9.Value * -1;
        }

        
        private void numericUpDown21_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button17_Click(button17, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double pos = (double)numericUpDown6.Value;
            double ajus = (double)numericUpDown20.Value;
            if (ajus != 0)
            {
                double x = (pos + ajus);
                double npos_conv = PR.ConvertTo(x, PR.FxTypes.UpperGap);
                t05.Text = Math.Round(npos_conv * 1000).ToString();
                escribir(5);
                t06.Text = "1";
                escribir(6);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double pos = (double)numericUpDown5.Value;// -PR.getOffset("22") / 1000;
            double ajus = (double)numericUpDown12.Value;
            if (ajus != 0)
            {
                try
                {
                    double x = (pos + ajus);
                    double npos_conv = PR.ConvertTo(x, PR.FxTypes.UpperGap);
                    t08.Text = Math.Round(npos_conv * 1000).ToString();
                    escribir(8);
                    t09.Text = "1";
                    escribir(9);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Excepcion" + ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            double pos = (double)numericUpDown4.Value;// - PR.getOffset("23") / 1000;
            double ajus = (double)numericUpDown11.Value;
            if (ajus != 0)
            {
                double npos = Math.Round((pos + ajus) * 1000, 5);
                t11.Text = npos.ToString();
                escribir(11);
                t12.Text = "1";
                escribir(12);

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            double pos = (double)numericUpDown3.Value;// -PR.getOffset("34") / 1000;
            double ajus = (double)numericUpDown10.Value;
            if (ajus != 0)
            {
                double npos = (pos + ajus) * 1000;
                t14.Text = npos.ToString();
                escribir(14);
                t15.Text = "1";
                escribir(15);
                //numericUpDown10.Value = 0;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            double pos = (double)numericUpDown2.Value - PR.getOffset("35") / 1000;
            double ajus = (double)numericUpDown9.Value;
            if (ajus != 0)
            {
                double npos = (pos + ajus) * 1000;
                t17.Text = npos.ToString();
                escribir(17);
                t18.Text = "1";
                escribir(18);
                //numericUpDown9.Value = 0;
            }
        }

        
        private void button40_Click(object sender, EventArgs e)
        {
           
                t03.Text = "0";
                escribir(3);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                t06.Text = "0";
                escribir(6);
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
                t09.Text = "0";
                escribir(9);
            
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
          
                t12.Text = "0";
                escribir(12);
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
                t15.Text = "0";
                escribir(15);
        }

        private void button9_Click(object sender, EventArgs e)
        {
                t18.Text = "0";
                escribir(18);
        }

        private void button11_Click(object sender, EventArgs e)
        {
           
                t21.Text = "0";
                escribir(21);
            
        }

        private void numericUpDown20_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button1_Click(button1, e);
        }

        private void numericUpDown12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button4_Click(button4, e);
        }

        private void numericUpDown11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button7_Click(button7, e);
        }

        private void numericUpDown10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button10_Click(button10, e);
        }

        private void numericUpDown9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button13_Click(button13, e);
        }

        private void SegundoColor_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown21_Click(object sender, EventArgs e)
        {
            var x = sender as NumericUpDown;
            x.Text = "";
        }

        private void numericUpDown20_Click(object sender, EventArgs e)
        {
            var x = sender as NumericUpDown;
            x.Text = "";
        }

        private void numericUpDown12_Click(object sender, EventArgs e)
        {
            var x = sender as NumericUpDown;
            x.Text = "";
        }

        private void numericUpDown11_Click(object sender, EventArgs e)
        {
            var x = sender as NumericUpDown;
            x.Text = "";
        }

        private void numericUpDown10_Click(object sender, EventArgs e)
        {
            var x = sender as NumericUpDown;
            x.Text = "";
        }

        private void numericUpDown9_Click(object sender, EventArgs e)
        {
            var x = sender as NumericUpDown;
            x.Text = "";
        }

        
    }
}
