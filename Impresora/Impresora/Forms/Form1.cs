using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql;
using OPCAutomation;
using MySql.Data;
using System.Globalization;



namespace Impresora
{
    public partial class Form1 : Form
    {
        static int numeroitems = 3;
        static int numeroitems1 = numeroitems+1;
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

        public Form1()
        {
            InitializeComponent();
            Button2.Enabled = false;
            Button3.Enabled = false;
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String nombreOPCServer = "KEPware.KEPServerEx.V4";
            try
            {
                servidorOpc = new OPCAutomation.OPCServer();
                servidorOpc.Connect(nombreOPCServer, "");
                Button1.Enabled = false;
                Button2.Enabled = true;
                Button3.Enabled = true;
                toolStripStatusLabel1.Text = "conectado keperverEx";
            }
            catch (Exception ex)
            {
                Button2.Enabled = false;
                servidorOpc = null;
                Button1.Enabled = true;
                MessageBox.Show("OPC server connect failed with exception: " +
                    ex.Message, "OPCInterface Exception", MessageBoxButtons.OK);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
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
                    Button1.Enabled = true;
                    // Don't alllow the Disconnect to be issued now that the connection is closed
                    Button2.Enabled = false;
                    // Disable the group controls now that we no longer have a server connection
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            opcItemNames[1] = textBox1;
            opcItemNames[2] = textBox2;
            opcItemNames[3] = textBox3;

            opcItemValue[1] = textBox4;
            opcItemValue[2] = textBox5;
            opcItemValue[3] = textBox6;

            opcItemValueToWritte[1] = textBox9;
            opcItemValueToWritte[2] = textBox8;
            opcItemValueToWritte[3] = textBox7;

            opcItemQuality[1] = textBox12;
            opcItemQuality[2] = textBox11;
            opcItemQuality[3] = textBox10;

            opcItemButtonToWritte[1] = button4;
            opcItemButtonToWritte[2] = button5;
            opcItemButtonToWritte[3] = button6;

            opcItemActiveState[1] = checkBox1;
            opcItemActiveState[2] = checkBox2;
            opcItemActiveState[3] = checkBox3;

            button4.Click += new EventHandler(OPCItemWriteButton_Click);
            button5.Click += new EventHandler(OPCItemWriteButton_Click);
            button6.Click += new EventHandler(OPCItemWriteButton_Click); 
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                
                servidorOpc.OPCGroups.DefaultGroupIsActive = false;
                servidorOpc.OPCGroups.DefaultGroupDeadband = 0;
                grupoConectado = servidorOpc.OPCGroups.Add("dor");
                grupoConectado.UpdateRate = 500;
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
                TextBox temp;
                for (int i = 1; i <= numeroitems; i++)
                {
                    temp = (TextBox)opcItemNames[i];
                    opcIdItems[i] = temp.Text;
                    OPCItemIsArray[i] = 0;
                    clientHandles[i] = i;
                    CheckBox ch = (CheckBox)opcItemActiveState[i];
                    ch.Checked = true;
                }

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
                if (itemgood)
                {
                    toolStripStatusLabel2.Text = "Items agregados";
                }
                else
                    MessageBox.Show("OPC server no encontro ningun item con el nombre espeficado ", "Conexion Exception", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OPC server fallo al agregar los items solicitados al grupo con: " + ex.Message, "Conexion Exception", MessageBoxButtons.OK);
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
                    int uncertain = (int)OPCAutomation.OPCQuality.OPCQualityUncertain;
                    if (Qualities.GetValue(i).Equals(good))
                    {
                        opcItemQuality[int.Parse(ClientHandles.GetValue(i).ToString())].Text = "Good";
                    }
                    else if (Qualities.GetValue(i).Equals(uncertain))
                    {
                        opcItemQuality[int.Parse(ClientHandles.GetValue(i).ToString())].Text = "Uncertain";
                    }
                    else
                    {
                        opcItemQuality[int.Parse(ClientHandles.GetValue(i).ToString())].Text = "Bad";
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                MessageBox.Show("OPC DataChange failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
            }

        }

        private bool LoadArray(ref System.Array AnArray, int CanonDT,  string wrTxt)
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
                    loc = wrTxt.IndexOf(",",0);
                    if (ii < AnArray.GetUpperBound(0))
                    {
                        if (loc == 0)
                        {
                            MessageBox.Show("Valor escrito: Numero incorrecto de digitos para el tamaño del arreglo?", "Error de argumento",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
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
                            AnArray.SetValue(Convert.ToByte((wrTxt.Substring(start, loc - start))),ii);
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
                            MessageBox.Show("El tipo de valor que se intenta escribir es desconocido","Error de argumento",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            return false;
                    }

                    start = loc + 1;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Al intentar escribir el vaor se genero la siguiente excepción: "+ex.Message, "Excepción de OPC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void OPCItemWriteButton_Click(System.Object sender1, System.EventArgs e)
        {
            if ((grupoConectado != null))
            {
                // Get control index from name
                short index = -1;
                Control sender = (Control)(sender1);
                

                if (sender.Name == "button4")
                {
                    index = 1;
                }
                else if (sender.Name == "button5")
                {
                    index = 2;
                }
                else if (sender.Name == "button6")
                {
                    index = 3;
                }
                else if (sender.Name == "_OPCItemWriteButton_3")
                {
                    index = 4;
                }
                else if (sender.Name == "_OPCItemWriteButton_4")
                {
                    index = 5;
                }
                else if (sender.Name == "_OPCItemWriteButton_5")
                {
                    index = 6;
                }
                else if (sender.Name == "_OPCItemWriteButton_6")
                {
                    index = 7;
                }
                else if (sender.Name == "_OPCItemWriteButton_7")
                {
                    index = 8;
                }
                else if (sender.Name == "_OPCItemWriteButton_8")
                {
                    index = 9;
                }
                else
                {
                    index = 10;
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
                                if (!LoadArray( ref ItsAnArray, CanonDT,  opcItemValueToWritte[index].Text))
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
                            MessageBox.Show("OPCItemWriteButton Unknown data type", "Error al escribir" ,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
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
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form actual = new actual();
            actual.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
           
    }
}