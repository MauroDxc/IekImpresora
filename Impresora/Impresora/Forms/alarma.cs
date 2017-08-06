using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OPCAutomation;

namespace Impresora
{
    public partial class alarma : Form
    {
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
        Object[] opcItemActiveState = new Control[numeroitems1];
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

        public alarma()
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


            //this.l1.TextChanged += new System.EventHandler(this.label1_TextChanged);
            //this.l2.TextChanged += new System.EventHandler(this.label2_TextChanged);
            //this.l3.TextChanged += new System.EventHandler(this.label3_TextChanged);
            //this.l4.TextChanged += new System.EventHandler(this.label4_TextChanged);
            //this.l5.TextChanged += new System.EventHandler(this.label5_TextChanged);
            //this.l6.TextChanged += new System.EventHandler(this.label6_TextChanged);
            //this.l7.TextChanged += new System.EventHandler(this.label7_TextChanged);
            //this.l8.TextChanged += new System.EventHandler(this.label8_TextChanged);
            //this.l9.TextChanged += new System.EventHandler(this.label9_TextChanged);
            //this.l10.TextChanged += new System.EventHandler(this.label10_TextChanged);
            //this.l11.TextChanged += new System.EventHandler(this.label11_TextChanged);
            //this.l12.TextChanged += new System.EventHandler(this.label12_TextChanged);
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
                button2.Enabled = true;
            }

            catch (Exception ex)
            {
                button2.Enabled = true;
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

            timer1.Enabled = true;
            opcIdItems[1] = "LENZE.10MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[1] = 0;
            clientHandles[1] = 1;
            opcIdItems[2] = "LENZE.11MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[2] = 0;
            clientHandles[2] = 2;
            opcIdItems[3] = "LENZE.12MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[3] = 0;
            clientHandles[3] = 3;
            opcIdItems[4] = "LENZE.13MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[4] = 0;
            clientHandles[4] = 4;
            opcIdItems[5] = "LENZE.14MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[5] = 0;
            clientHandles[5] = 5;
            opcIdItems[6] = "LENZE.15MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[6] = 0;
            clientHandles[6] = 6;
            opcIdItems[7] = "LENZE.16MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[7] = 0;
            clientHandles[7] = 7;
            opcIdItems[8] = "LENZE.20MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[8] = 0;
            clientHandles[8] = 8;
            opcIdItems[9] = "LENZE.21MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[9] = 0;
            clientHandles[9] = 9;
            opcIdItems[10] = "LENZE.22MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[10] = 0;
            clientHandles[10] = 10;
            opcIdItems[11] = "LENZE.23MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[11] = 0;
            clientHandles[11] = 11;
            opcIdItems[12] = "LENZE.24MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[12] = 0;
            clientHandles[12] = 12;
            opcIdItems[13] = "LENZE.25MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[13] = 0;
            clientHandles[13] = 13;
            opcIdItems[14] = "LENZE.30MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[14] = 0;
            clientHandles[14] = 14;
            opcIdItems[15] = "LENZE.31MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[15] = 0;
            clientHandles[15] = 15;
            opcIdItems[16] = "LENZE.32MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[16] = 0;
            clientHandles[16] = 16;
            opcIdItems[17] = "LENZE.33MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[17] = 0;
            clientHandles[17] = 17;
            opcIdItems[18] = "LENZE.34MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[18] = 0;
            clientHandles[18] = 18;
            opcIdItems[19] = "LENZE.35MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[19] = 0;
            clientHandles[19] = 19;
            opcIdItems[20] = "LENZE.120MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[20] = 0;
            clientHandles[20] = 20;
            opcIdItems[21] = "LENZE.121MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[21] = 0;
            clientHandles[21] = 21;
            opcIdItems[22] = "LENZE.122MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[22] = 0;
            clientHandles[22] = 22;
            opcIdItems[23] = "LENZE.123MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[23] = 0;
            clientHandles[23] = 23;
            opcIdItems[24] = "LENZE.124MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[24] = 0;
            clientHandles[24] = 24;
            opcIdItems[25] = "LENZE.125MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[25] = 0;
            clientHandles[25] = 25;
            opcIdItems[26] = "LENZE.130MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[26] = 0;
            clientHandles[26] = 26;
            opcIdItems[27] = "LENZE.131MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[27] = 0;
            clientHandles[27] = 27;
            opcIdItems[28] = "LENZE.132MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[28] = 0;
            clientHandles[28] = 28;
            opcIdItems[29] = "LENZE.133MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[29] = 0;
            clientHandles[29] = 29;
            opcIdItems[30] = "LENZE.134MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[30] = 0;
            clientHandles[30] = 30;
            opcIdItems[31] = "LENZE.135MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[31] = 0;
            clientHandles[31] = 31;
            opcIdItems[32] = "LENZE.40MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[32] = 0;
            clientHandles[32] = 32;
            opcIdItems[33] = "LENZE.41MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[33] = 0;
            clientHandles[33] = 33;
            opcIdItems[34] = "LENZE.42MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[34] = 0;
            clientHandles[34] = 34;
            opcIdItems[35] = "LENZE.50MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[35] = 0;
            clientHandles[35] = 35;
            opcIdItems[36] = "LENZE.51MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[36] = 0;
            clientHandles[36] = 36;
            opcIdItems[37] = "LENZE.52MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[37] = 0;
            clientHandles[37] = 37;
            opcIdItems[38] = "LENZE.53MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[38] = 0;
            clientHandles[38] = 38;
            opcIdItems[39] = "LENZE.54MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[39] = 0;
            clientHandles[39] = 39;
            opcIdItems[40] = "LENZE.150MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[40] = 0;
            clientHandles[40] = 40;
            opcIdItems[41] = "LENZE.151MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[41] = 0;
            clientHandles[41] = 41;
            opcIdItems[42] = "LENZE.152MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[42] = 0;
            clientHandles[42] = 42;
            opcIdItems[43] = "LENZE.153MS.ALARMA_RESET"; // De SW AUTO MAN
            OPCItemIsArray[43] = 0;
            clientHandles[43] = 43;

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

        private bool escribir(int index)
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
                                return true;
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
                                return true;
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
                                return true;
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
                                return true;
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
                                return true;
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
                                return true;
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
                                return true;
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
                                return true;
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
                                return true;
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
                                return true;
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
                }
                return true;
            }
            catch (Exception ex)
            {
                // Error handling
                MessageBox.Show("OPC server write item failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                return false;
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
                        //MessageBox.Show("Vuelve intentar datos inciertos", "Alarma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            string fecha = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string fecha1 = dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd");
            DataTable datos = cnn.selectFrom("a.fecha as Fecha, d.nombre as Equipo, p.descripcion as 'Alarma',p.idal_code as 'Clave'", "al_code as p,historico as a, drive as d where a.fecha between '" + fecha + "' and '" + fecha1 + "'and a.id_al_code = p.idal_code and a.drive = d.iddrive order by fecha desc");
            dataGridView2.DataSource = datos;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            cnn.delete("alarmas");
            dataActualizar();
        }

        private void dataActualizar()
        {
            conexionBD cnn = new conexionBD();
            DataTable datos = cnn.selectFrom("a.fecha as Fecha, d.nombre as Equipo, p.descripcion as 'Alarma',p.idal_code as 'Clave',p.pista as 'Acciones',a.idalarmas", "alarmas as a, al_code as p, drive as d where a.id_al_code = p.idal_code and a.drive = d.iddrive order by fecha desc");
            dataGridView1.DataSource = datos;
            dataGridView1.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.Columns[5].Visible= false;
            string fecha = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string fechai = fecha + " 00:00:00"; 
            string fechaf = fecha + " 23:59:59";
            DataTable datos1 = cnn.selectFrom("a.fecha as Fecha, d.nombre as Equipo, p.descripcion as 'Alarma',p.idal_code as 'Clave'", "al_code as p,historico as a, drive as d where a.fecha> '" + fechai + "' and a.fecha< '" + fechaf + "'and a.id_al_code = p.idal_code and a.drive = d.iddrive order by fecha desc");
            dataGridView2.DataSource = datos1;
            dataGridView2.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

        }

        private void alarma_Load(object sender, EventArgs e)
        {
            dataActualizar();
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                String nombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                int idal = (int)dataGridView1.CurrentRow.Cells[5].Value;
                if (nombre != "" && nombre != null)
                {
                    conexionBD cnn = new conexionBD();
                    String drive = cnn.busca1("idDrive", "drive", "nombre = '" + nombre + "'");
                    String ida = cnn.getNextId("bitacora");
                    DateTime dt = DateTime.Now;
                    cnn.insertC("bitacora", "'" + ida + "','" + drive + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "','reset','" + "common'");
                    switch (drive)
                    {
                        case "10":
                            opcItemValueToWritte[1].Text = "1";
                            if (escribir(1))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "11":
                            opcItemValueToWritte[2].Text = "1";
                            if (escribir(2))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "12":
                            opcItemValueToWritte[3].Text = "1";
                            if (escribir(3))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "13":
                            opcItemValueToWritte[4].Text = "1";
                            if (escribir(4))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "14":
                            opcItemValueToWritte[5].Text = "1";
                            if (escribir(5))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "15":
                            opcItemValueToWritte[6].Text = "1";
                            if (escribir(6))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "16":
                            opcItemValueToWritte[7].Text = "1";
                            if (escribir(7))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "20":
                            opcItemValueToWritte[8].Text = "1";
                            if (escribir(8))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "21":
                            opcItemValueToWritte[9].Text = "1";
                            if (escribir(9))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "22":
                            opcItemValueToWritte[10].Text = "1";
                            if (escribir(10))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "23":
                            opcItemValueToWritte[11].Text = "1";
                            if (escribir(11))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "24":
                            opcItemValueToWritte[12].Text = "1";
                            if (escribir(12))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "25":
                            opcItemValueToWritte[13].Text = "1";
                            if (escribir(13))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "30":
                            opcItemValueToWritte[14].Text = "1";
                            if (escribir(14))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "31":
                            opcItemValueToWritte[15].Text = "1";
                            if (escribir(15))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "32":
                            opcItemValueToWritte[16].Text = "1";
                            if (escribir(16))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "33":
                            opcItemValueToWritte[17].Text = "1";
                            if (escribir(17))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "34":
                            opcItemValueToWritte[18].Text = "1";
                            if (escribir(18))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "35":
                            opcItemValueToWritte[19].Text = "1";
                            if (escribir(19))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "120":
                            opcItemValueToWritte[20].Text = "1";
                            if (escribir(20))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "121":
                            opcItemValueToWritte[21].Text = "1";
                            if (escribir(21))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "122":
                            opcItemValueToWritte[22].Text = "1";
                            if (escribir(22))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "123":
                            opcItemValueToWritte[23].Text = "1";
                            if (escribir(23))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "124":
                            opcItemValueToWritte[24].Text = "1";
                            if (escribir(24))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "125":
                            opcItemValueToWritte[25].Text = "1";
                            if (escribir(25))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "130":
                            opcItemValueToWritte[26].Text = "1";
                            if (escribir(26))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "131":
                            opcItemValueToWritte[27].Text = "1";
                            if (escribir(27))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "132":
                            opcItemValueToWritte[28].Text = "1";
                            if (escribir(28))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "133":
                            opcItemValueToWritte[29].Text = "1";
                            if (escribir(29))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "134":
                            opcItemValueToWritte[30].Text = "1";
                            if (escribir(30))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "135":
                            opcItemValueToWritte[31].Text = "1";
                            if (escribir(31))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "40":
                            opcItemValueToWritte[32].Text = "1";
                            if (escribir(32))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "41":
                            opcItemValueToWritte[33].Text = "1";
                            if (escribir(33))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "42":
                            opcItemValueToWritte[34].Text = "1";
                            if (escribir(34))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "50":
                            opcItemValueToWritte[35].Text = "1";
                            if (escribir(35))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "51":
                            opcItemValueToWritte[36].Text = "1";
                            if (escribir(36))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "52":
                            opcItemValueToWritte[37].Text = "1";
                            if (escribir(37))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "53":
                            opcItemValueToWritte[38].Text = "1";
                            if (escribir(38))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;

                        case "54":
                            opcItemValueToWritte[39].Text = "1";
                            if (escribir(39))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;

                        case "150":
                            opcItemValueToWritte[40].Text = "1";
                            if (escribir(40))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "151":
                            opcItemValueToWritte[41].Text = "1";
                            if (escribir(41))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "152":
                            opcItemValueToWritte[42].Text = "1";
                            if (escribir(42))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        case "153":
                            opcItemValueToWritte[43].Text = "1";
                            if (escribir(43))
                            {
                                cnn.delete("alarmas where idalarmas=" + idal);
                                dataActualizar();
                            }
                            break;
                        // End case

                        default:
                            MessageBox.Show("El tipo de valor que se intenta escribir es desconocido", "Error de argumento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;

                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataActualizar();
        }

        private void Alarmas_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataActualizar();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataActualizar();
        }
    }
}
