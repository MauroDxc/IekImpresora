//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Impresora
//{
//    class opcconector
//    {
//        //---------------------------------------------------------
//        //TODO: Copiar estas asignaciones de evento en el constructor:
//        // después de InitializeComponents.
//        // Comprobar que los delegados usados son los correctos.
//        //---------------------------------------------------------
//        /*
//        Button1.Click += new System.EventHandler(Button1_Click);
//        Button2.Click += new System.EventHandler(Button2_Click);
//        ConnectedGroup.DataChange += new int(ConnectedGroup_DataChange);
//        Button3.Click += new System.EventHandler(Button3_Click);
//        base.Load += new System.EventHandler(Form1_Load);
//        */
//        //---------------------------------------------------------
//        public class Form1
//        {
//            const int NUMITEMS = 1;

//            OPCAutomation.OPCServer COPCServer;
//            OPCAutomation.OPCServer AnOPCServer;
//            OPCAutomation.OPCServer ConnectedOPCServer;
//            OPCAutomation.OPCGroup ConnectedGroup;

//            // OPC Item related data
//            string[] OPCItemIDs = new string[(NUMITEMS + 1)];
//            Int32[] ClientHandles = new Int32[(NUMITEMS + 1)];
//            System.Array ItemServerHandles;

//            // Arrays are used to provide iterative access to sets of controls
//            object[] OPCItemName = new object[(NUMITEMS + 1)];
//            object[] OPCItemValue = new object[(NUMITEMS + 1)];
//            object[] OPCItemValueToWrite = new object[(NUMITEMS + 1)];
//            object[] OPCItemWriteButton = new object[(NUMITEMS + 1)];
//            object[] OPCItemActiveState = new object[(NUMITEMS + 1)];
//            object[] OPCItemSyncReadButton = new object[(NUMITEMS + 1)];
//            object[] OPCItemQuality = new object[(NUMITEMS + 1)];
//            int[] OPCItemIsArray = new int[(NUMITEMS + 1)];




//            private void Button1_Click(System.Object sender, System.EventArgs e)
//            {


//                COPCServer = new OPCAutomation.OPCServer();
//                COPCServer.Connect("KEPware.KEPServerEx.V4", "");


//                try
//                {
//                    // Set the desire active state for the group
//                    COPCServer.OPCGroups.DefaultGroupIsActive = true;

//                    System.Globalization.NumberFormatInfo nfi_g0 = new System.Globalization.NumberFormatInfo();
//                    nfi_g0.CurrencyGroupSeparator = ".";
//                    //Set the desired percent deadband
//                    COPCServer.OPCGroups.DefaultGroupDeadband = Convert.ToDouble("0", nfi_g0);

//                    // Add the group and set its update rate
//                    ConnectedGroup = COPCServer.OPCGroups.Add("Teca");

//                    System.Globalization.NumberFormatInfo nfi_g1 = new System.Globalization.NumberFormatInfo();
//                    nfi_g1.CurrencyGroupSeparator = ".";
//                    // Set the update rate for the group
//                    ConnectedGroup.UpdateRate = Convert.ToDouble("10", nfi_g1);

//                    // ****************************************************************
//                    // Mark this group to receive asynchronous updates via the DataChange event.
//                    // This setting is IMPORTANT. Without setting '.IsSubcribed' to True your
//                    // VB application will not receive DataChange notifications.  This will
//                    // make it appear that you have not properly connected to the server.
//                    ConnectedGroup.IsSubscribed = true;

//                    //*****************************************************************
//                    // Now that a group has been added disable the Add group Button and enable
//                    // the Remove group Button.  This demo application adds only a single group

//                    // Disable the Disconnect Server button since we now have a group that must be removed first
//                    Button2.Enabled = false;

//                }
//                catch (Exception ex)
//                {
//                    // Error handling
//                    MessageBox.Show("OPC server add group failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
//                }

//            }


//            private void Button2_Click(System.Object sender, System.EventArgs e)
//            {
//                COPCServer.Disconnect();
//            }


//            private void ConnectedGroup_DataChange(int TransactionID, int NumItems, ref System.Array ClientHandles, ref System.Array ItemValues, ref System.Array Qualities, ref System.Array TimeStamps)
//            {
//                // We don't have error handling here since this is an event called from the OPC interface

//                try
//                {
//                    short i;
//                    for (i = 1; i <= NumItems; i++)
//                    {
//                        // Use the 'Clienthandles' array returned by the server to pull out the
//                        // index number of the control to update and load the value.
//                        if (IsArray(ItemValues(i)))
//                        {
//                            Array ItsAnArray;
//                            int x;
//                            string Suffix;

//                            ItsAnArray = ItemValues(i);

//                            // Store the size of array for use by sync write
//                            OPCItemIsArray[ClientHandles[i]] = ItsAnArray.GetUpperBound(0) + 1;

//                            OPCItemValue[ClientHandles[i]].Text = "";
//                            for (x = ItsAnArray.GetLowerBound(0); x <= ItsAnArray.GetUpperBound(0); x++)
//                            {
//                                if (x == ItsAnArray.GetUpperBound(0))
//                                {
//                                    Suffix = "";
//                                }
//                                else
//                                {
//                                    Suffix = ", ";
//                                }
//                                OPCItemValue[ClientHandles[i]].Text = OPCItemValue[ClientHandles[i]].Text + ItsAnArray(x) + Suffix;
//                            }
//                        }
//                        else
//                        {
//                            OPCItemValue[ClientHandles[i]].Text = ItemValues(i);
//                        }

//                        // Check the Qualties for each item retured here.  The actual contents of the
//                        // quality field can contain bit field data which can provide specific
//                        // error conditions.  Normally if everything is OK then the quality will
//                        // contain the 0xC0
//                        if ((Qualities(i) & OPCAutomation.OPCQuality.OPCQualityGood) == OPCAutomation.OPCQuality.OPCQualityGood)
//                        {
//                            OPCItemQuality[ClientHandles[i]].Text = "Good";
//                        }
//                        else if ((Qualities(i) & OPCAutomation.OPCQuality.OPCQualityUncertain) == OPCAutomation.OPCQuality.OPCQualityUncertain)
//                        {
//                            OPCItemQuality[ClientHandles[i]].Text = "Uncertain";
//                        }
//                        else
//                        {
//                            OPCItemQuality[ClientHandles[i]].Text = "Bad";
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    // Error handling
//                    MessageBox.Show("OPC DataChange failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
//                }
//            }


//            private void Button3_Click(System.Object sender, System.EventArgs e)
//            {

//                // Test to see if the OPC Group object is currently available
//                if (ConnectedGroup != null)
//                {
//                    try
//                    {
//                        int ItemCount = NUMITEMS;

//                        // Array for potential error returns.  This example doesn't
//                        // check them but yours should ultimately.
//                        System.Array AddItemServerErrors;

//                        // Load the request OPC Item names and build the ClientHandles list
//                        for (short i = 1; i <= NUMITEMS; i++)
//                        {
//                            // Load the name of then item to be added to this group.  You can add
//                            // as many items as you want to the group in a single call by building these
//                            // arrays as needed.
//                            OPCItemIDs[i] = "Channel_1.Device_1.R0";

//                            // ASSume all aren't an array. If it is, this holds size and is set in
//                            // Data change event.
//                            OPCItemIsArray[i] = 0;

//                            // The client handles are given to the OPC Server for each item you intend
//                            // to add to the group.  The OPC Server will uses these client handles
//                            // by returning them to you in the 'DataChange' event.  You can use the
//                            // client handles as a key to linking each valued returned from the Server
//                            // back to some element in your application.  In this example we are simply
//                            // placing the Index number of each control that will be used to display
//                            // data for the item.  In your application the ClientHandle value you use
//                            // can by whatever you need to best fit your program.  You will see how
//                            // these client handles are used in the 'DataChange' event handler.
//                            ClientHandles[i] = i;

//                            // Make the Items active start control Active, for the demo I want all items to start active
//                            // Your application may need to start the items as inactive.
//                            //OPCItemActiveState(i).CheckState = System.Windows.Forms.CheckState.Checked
//                        }

//                        // Establish a connection to the OPC item interface of the connected group
//                        //                OPCItemCollection = ConnectedGroup.OPCItems

//                        // Setting the '.DefaultIsActive' property forces all items we are about to
//                        // add to the group to be added in an active state.  If you want to add them
//                        // all as inactive simply set this property false, you can always make the
//                        // items active later as needed using each item's own active state property.
//                        // One key distinction to note, the active state of an item is independent
//                        // from the group active state.  If a group is active but the item is
//                        // inactive no data will be received for the item.  Also changing the
//                        // state of the group will not change the state of an item.
//                        ConnectedGroup.OPCItems.DefaultIsActive = true;

//                        // Atempt to add the items,  some may fail so the ItemServerErrors will need
//                        // to be check on completion of the call.  We are adding all item using the
//                        // default data type of VT_EMPTY and letting the server pick the appropriate
//                        // data type.  The ItemServerHandles is an array that the OPC Server will
//                        // return to your application.  This array like your own ClientHandles array
//                        // is used by the server to allow you to reference individual items in an OPC
//                        // group.  When you need to perform an action on a single OPC item you will
//                        // need to use the ItemServerHandles for that item.  With this said you need to
//                        // maintain the ItemServerHandles array for use throughout your application.
//                        // Use of the ItemServerHandles will be demonstrated in other subroutines in
//                        // this example program.
//                        ConnectedGroup.OPCItems.AddItems(ItemCount, OPCItemIDs, ClientHandles, ItemServerHandles, AddItemServerErrors);

//                        // This next step checks the error return on each item we attempted to
//                        // register.  If an item is in error it's associated controls will be
//                        // disabled.  If all items are in error then the Add Item button will
//                        // remain active.
//                        bool AnItemIsGood;
//                        AnItemIsGood = false;
//                        for (short i = 1; i <= NUMITEMS; i++)
//                        {
//                            if (AddItemServerErrors(i) == 0)
//                            { //If the item was added successfully then allow it to be used.
//                                OPCItemValueToWrite[i].Enabled = true;
//                                OPCItemWriteButton[i].Enabled = true;
//                                OPCItemActiveState[i].Enabled = true;
//                                OPCItemSyncReadButton[i].Enabled = true;

//                                AnItemIsGood = true;
//                                OPCItemValue[i].Enabled = true;
//                            }
//                            else
//                            {
//                                ItemServerHandles(i) = 0; // If the handle was bad mark it as empty
//                                OPCItemValueToWrite[i].Enabled = false;
//                                OPCItemWriteButton[i].Enabled = false;
//                                OPCItemActiveState[i].Enabled = false;
//                                OPCItemSyncReadButton[i].Enabled = false;

//                                OPCItemValue[i].Enabled = false;
//                                OPCItemValue[i].Text = "OPC Add Item Fail";
//                            }
//                        }

//                        // Disable the Add OPC item button if any item in the list was good
//                        object Response;
//                        if (AnItemIsGood)
//                        {
//                            Button3.Enabled = false;

//                            for (short i = 1; i <= NUMITEMS; i++)
//                            {
//                                OPCItemName[i].Enabled = false; // Disable the Item Name cotnrols while now that they have been added to the group.
//                            }


//                        }
//                        else
//                        {
//                            // The OPC Server did not accept any of the items we attempted to enter, let the user know to try again.
//                            MessageBox.Show("The OPC Server has not accepted any of the item you have entered, check your item names and try again.", "OPC Add Item", MessageBoxButtons.OK);
//                        }

//                    }
//                    catch (Exception ex)
//                    {
//                        // Error handling
//                        MessageBox.Show("OPC server add items failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
//                    }
//                }
//            }

//            private void Form1_Load(System.Object sender, System.EventArgs e)
//            {
//                OPCItemName[1] = TextBox1;
//            }
//        }
//    }
//}
