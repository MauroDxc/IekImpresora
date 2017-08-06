using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Impresora
{
    public partial class nuevaCaja : Form
    {
        Bitmap bt1;
        Graphics grafico;
        Double escalag = 1;
        string estilo = "";
        Char[] ch = { ' ' };
        bool istroq = false;

        String PersonalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string path;

        int xA;
        int xB;
        int xC;
        int xD;
        int xE;
        int yF;
        int yG;
        int yH;

        int A;
        int B;
        int C;
        int D;
        int E;
        int F;
        int G;
        int H;


        int i;
        int[] meds;
        int largoS;
        int anchoS;
        int ptw1;

        public nuevaCaja()
        {
            InitializeComponent();
            i = 0;
            path = PersonalFolder + "\\Kinex\\Cajas\\";
            escribirFichero("S");

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

        private void button17_Click(object sender, EventArgs e)
        {
            textBox8.Enabled = false;
            string color1 = "1";
            string color2 = "1";
            string color3 = "1";
            string color4 = "1";
            string v10;
            string v11;
            string v12;
            string v13;
            string v14;
            string v15;
            string v16;
            string v20;
            string v21;
            string v22;
            string v23;
            string v24;
            string v25;
            string v30;
            string v31;
            string v32;
            string v33;
            string v34;
            string v35;
            string v120;
            string v121;
            string v122;
            string v123;
            string v124;
            string v125;
            string v130;
            string v131;
            string v132;
            string v133;
            string v134;
            string v135;
            string v40;
            string v41;
            string v42;
            string v50;
            string v51;
            string v52;
            string v53;
            string v54;
            string v150;
            string v151;
            string v152;
            string v153;


            if (istroq)
            {
                A = 0;
                B = 0;
                C = 0;
                D = 0;
                E = 0;
                F = 0;
                G = 0;
                H = 0;
            }

            if (comboBox1.Text != "")
            {
                if (comboBox2.Text != "")
                {
                    if (comboBox3.Text != "" && comboBox4.Text != "")
                    {
                        int i = 0;

                        conexionBD cnn = new conexionBD();
                        string id = cnn.getNextId("caja");
                        string idmedidas = cnn.getNextId("medidas");
                        string idflauta = comboBox2.Text;
                        string idresistencia = cnn.getId("resistencia", "idresistencia", "nombre", comboBox4.Text);
                        if (!cnn.insertC("medidas", idmedidas + "," + A.ToString() + "," + B.ToString() + "," + C.ToString() + "," +
                             D.ToString() + "," + E.ToString() + "," + F.ToString() + "," + G.ToString() + "," + H.ToString()))
                            i = 1;

                        if (checkBox1.Checked == true)
                        {
                            string[] tx = comboBox6.Text.Split(' ');
                            color1 = cnn.getId("color", "idcolor", "Tinta ='" + tx[0] + "'" + " and Catalogo='" + tx[1] + "' and GCMI='" + tx[2] + "'");
                        }
                        if (checkBox2.Checked == true)
                        {
                            string[] tx = comboBox7.Text.Split(' ');
                            color2 = cnn.getId("color", "idcolor", "Tinta ='" + tx[0] + "'" + " and Catalogo='" + tx[1] + "' and GCMI='" + tx[2] + "'");
                        }
                        if (checkBox3.Checked == true)
                        {
                            string[] tx = comboBox8.Text.Split(' ');
                            color3 = cnn.getId("color", "idcolor", "Tinta ='" + tx[0] + "'" + " and Catalogo='" + tx[1] + "' and GCMI='" + tx[2] + "'");
                        }
                        if (checkBox4.Checked == true)
                        {
                            string[] tx = comboBox9.Text.Split(' ');
                            color4 = cnn.getId("color", "idcolor", "Tinta ='" + tx[0] + "'" + " and Catalogo='" + tx[1] + "' and GCMI='" + tx[2] + "'");
                        }
                        double largo = (double)numericUpDown1.Value;
                        double ancho = (double)numericUpDown2.Value;
                        double espesor = double.Parse(cnn.selectVal("espesor", "resistencia where idResistencia =" + idresistencia));
                        double fe = double.Parse(cnn.selectVal("compensancion_FE", "resistencia where idResistencia =" + idresistencia));
                        double c1 = double.Parse(cnn.selectVal("compensancion_PR1", "resistencia where idResistencia =" + idresistencia));
                        double c2 = double.Parse(cnn.selectVal("compensancion_PR2", "resistencia where idResistencia =" + idresistencia));
                        double c3 = double.Parse(cnn.selectVal("compensancion_PR3", "resistencia where idResistencia =" + idresistencia));
                        double c4 = double.Parse(cnn.selectVal("compensancion_PR4", "resistencia where idResistencia =" + idresistencia));
                        double tr = double.Parse(cnn.selectVal("compensacion_DC", "resistencia where idResistencia =" + idresistencia));
                        double ra = double.Parse(cnn.selectVal("compensacion_SL", "resistencia where idResistencia =" + idresistencia));
                        double borde = 10;
                        DataTable dto = cnn.selectFrom("idoffset,offsetcol,LPABS,LNABS", "offset where idoffset<>0");
                        DataTable d;


                        switch (idflauta)
                        {
                            case "B":
                                d = cnn.selectFrom("*", "pflauta where idpflauta='" + idflauta + "'");
                                break;
                            case "C":
                                d = cnn.selectFrom("*", "pflauta where idpflauta='" + idflauta + "'");
                                break;
                            case "BC":
                                d = cnn.selectFrom("*", "pflauta where idpflauta='" + idflauta + "'");
                                break;
                            default:
                                d = cnn.selectFrom("*", "pflauta where idpflauta='C'");
                                break;
                        }
                        try
                        {
                            double[] dd = new double[154];

                            for (int j = 1; j < d.Columns.Count; j++)
                            {
                                int id_dd = int.Parse(d.Columns[j].ColumnName.Replace("V", ""));
                                dd[id_dd] = double.Parse(d.Rows[0][j].ToString());
                            }

                            string iddrive = "";
                            PR.FxTypes fxtype = PR.FxTypes.Feeder;
                            espesor = dd[10];
                            iddrive = "10";
                            v10 = espesor > (double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? espesor.ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) + .1).ToString();
                            v10 = espesor > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString()) - .1)).ToString() : espesor.ToString();
                            espesor = dd[11];
                            iddrive = "11";
                            v11 = espesor > (double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? espesor.ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) + .1).ToString();
                            v11 = espesor > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString()) - .1)).ToString() : espesor.ToString();
                            espesor = dd[12];
                            iddrive = "12";
                            v12 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 2) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v12 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v12;
                            espesor = dd[13];
                            iddrive = "13";
                            v13 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 2) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v13 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v13;
                            espesor = dd[14];
                            iddrive = "14";
                            fxtype = PR.FxTypes.Feeder;
                            v14 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? (PR.ConvertTo(espesor, fxtype)).ToString() : PR.ConvertTo(Math.Abs((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) + .1), fxtype).ToString();
                            v14 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(Math.Abs((double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) - .1), fxtype).ToString() : v14;
                            espesor = dd[15];
                            iddrive = "15";
                            v15 = ((ancho / 4) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 4) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v15 = ((ancho / 4) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v15;
                            espesor = dd[16];
                            iddrive = "16";
                            v16 = ((ancho / 4) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 4) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v16 = ((ancho / 4) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v16;
                            v20 = dd[20].ToString();
                            espesor = dd[21];
                            iddrive = "21";
                            fxtype = PR.FxTypes.UpperGap;
                            v21 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v21 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v21;
                            espesor = dd[22];
                            iddrive = "22";
                            fxtype = PR.FxTypes.UpperGap;
                            v22 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v22 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v22;
                            v23 = dd[23].ToString();
                            espesor = dd[24];
                            iddrive = "24";
                            v24 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 2) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v24 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v24;
                            espesor = dd[25];
                            iddrive = "25";
                            v25 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 2) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v25 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v25;
                            v30 = dd[30].ToString();
                            espesor = dd[31];
                            iddrive = "31";
                            fxtype = PR.FxTypes.UpperGap;
                            v31 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v31 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v31;
                            espesor = dd[32];
                            iddrive = "32";
                            fxtype = PR.FxTypes.UpperGap;
                            v32 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v32 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v32;
                            v33 = dd[33].ToString();
                            espesor = dd[34];
                            iddrive = "34";
                            v34 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 2) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v34 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v34;
                            espesor = dd[35];
                            iddrive = "35";
                            v35 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 2) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v35 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v35;
                            v120 = dd[120].ToString();
                            espesor = dd[121];
                            iddrive = "121";
                            fxtype = PR.FxTypes.UpperGap;
                            v121 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v121 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v121;
                            espesor = dd[122];
                            iddrive = "122";
                            fxtype = PR.FxTypes.UpperGap;
                            v122 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v122 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v122;
                            v123 = dd[123].ToString();
                            espesor = dd[124];
                            iddrive = "124";
                            v124 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 2) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v124 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v124;
                            espesor = dd[125];
                            iddrive = "125";
                            v125 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 2) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v125 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v125;
                            v130 = dd[130].ToString();
                            espesor = dd[131];
                            iddrive = "131";
                            fxtype = PR.FxTypes.UpperGap;
                            v131 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v131 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v131;
                            espesor = dd[132];
                            iddrive = "132";
                            fxtype = PR.FxTypes.UpperGap;
                            v132 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v132 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v132;
                            v133 = dd[133].ToString();
                            espesor = dd[134];
                            iddrive = "134";
                            v134 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 2) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v134 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v134;
                            espesor = dd[135];
                            iddrive = "135";
                            v135 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) ? ((ancho / 2) + espesor).ToString() : ((double.Parse(dto.Select("idoffset = " + iddrive)[0][1].ToString()) / 1000) + 1).ToString();
                            v135 = ((ancho / 2) + espesor) > (double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? ((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - 1).ToString() : v135;
                            v40 = dd[40].ToString();
                            espesor = dd[41];
                            iddrive = "41";
                            fxtype = PR.FxTypes.UpperDiedCut;
                            v41 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v41 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v41;
                            espesor = dd[42];
                            iddrive = "42";
                            fxtype = PR.FxTypes.LowerDiedCut;
                            v42 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())-.9) ? (PR.ConvertTo(-espesor, fxtype)).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString()) + .1, fxtype).ToString();
                            v42 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) - .1, fxtype).ToString() : v42;
                            v50 = dd[50].ToString();
                            espesor = dd[51];
                            iddrive = "51";
                            fxtype = PR.FxTypes.Slotter;
                            v51 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v51 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v51;
                            espesor = dd[52];
                            iddrive = "52";
                            fxtype = PR.FxTypes.Slotter;
                            v52 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v52 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v52;
                            espesor = dd[53];
                            iddrive = "53";
                            fxtype = PR.FxTypes.Slotter53;
                            v53 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v53 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v53;
                            espesor = dd[54];
                            iddrive = "54";
                            fxtype = PR.FxTypes.SlotterCab;
                            v54 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString())) ? PR.ConvertTo(espesor, fxtype).ToString() : PR.ConvertTo(double.Parse(dto.Select("idoffset = " + iddrive)[0][3].ToString()) + .1, fxtype).ToString();
                            v54 = espesor > Math.Abs(double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) ? PR.ConvertTo((double.Parse(dto.Select("idoffset = " + iddrive)[0][2].ToString())) - .1, fxtype).ToString() : v54;
                            if (istroq)
                            {
                                v150 = (ancho / 2 - borde) > double.Parse(dto.Select("idoffset = 150")[0][1].ToString()) / 1000 ? ((ancho / 2) + dd[150]).ToString() : (double.Parse(dto.Select("idoffset = 150")[0][1].ToString()) / 1000 + 1).ToString();
                                v151 = (ancho / 4 - borde) > double.Parse(dto.Select("idoffset = 151")[0][1].ToString()) / 1000 ? ((ancho / 4) + dd[151]).ToString() : (double.Parse(dto.Select("idoffset = 151")[0][1].ToString()) / 1000 + 1).ToString();
                                v152 = (ancho / 4 - borde) > double.Parse(dto.Select("idoffset = 152")[0][1].ToString()) / 1000 ? ((ancho / 4) + dd[151]).ToString() : (double.Parse(dto.Select("idoffset = 152")[0][1].ToString()) / 1000 + 1).ToString();
                                v153 = (ancho / 2 - borde) > double.Parse(dto.Select("idoffset = 153")[0][1].ToString()) / 1000 ? ((ancho / 2) + dd[151]).ToString() : (double.Parse(dto.Select("idoffset = 153")[0][1].ToString()) / 1000 + 1).ToString();
                            }
                            else
                            {
                                v153 = (ancho - double.Parse(C.ToString())).ToString();
                                v152 = (ancho - double.Parse(D.ToString()) - double.Parse(C.ToString())).ToString();
                                v151 = (ancho - double.Parse(B.ToString()) - double.Parse(A.ToString())).ToString();
                                v150 = (ancho - double.Parse(A.ToString())).ToString();
                                v153 = double.Parse(v150) > double.Parse(dto.Select("idoffset = 150")[0][1].ToString()) / 1000 ? (double.Parse(dto.Select("idoffset = 150")[0][1].ToString()) / 1000 + 1).ToString() : v153;
                                v152 = double.Parse(v151) > double.Parse(dto.Select("idoffset = 151")[0][1].ToString()) / 1000 ? (double.Parse(dto.Select("idoffset = 151")[0][1].ToString()) / 1000 + 1).ToString() : v152;
                                v151 = double.Parse(v152) > double.Parse(dto.Select("idoffset = 152")[0][1].ToString()) / 1000 ? (double.Parse(dto.Select("idoffset = 152")[0][1].ToString()) / 1000 + 1).ToString() : v151;
                                v150 = double.Parse(v153) > double.Parse(dto.Select("idoffset = 153")[0][1].ToString()) / 1000 ? (double.Parse(dto.Select("idoffset = 153")[0][1].ToString()) / 1000 + 1).ToString() : v150;
                            }
                            string idvalact = cnn.getNextId("valoresp");
                            if (!cnn.insertC("valoresp", idvalact + ",'" + v10 + "','" + v11 + "','" + v12 + "','" + v13 + "','" + v14 + "','" + v15 + "','" + v16 + "','" + v20 + "','" + v21 + "','" + v22 + "','" + v23 + "','" + v24 + "','" + v25 + "','" + v30 + "','" + v31 + "','" + v32 + "','" + v33 + "','" + v34 + "'," +
                                "'" + v35 + "','" + v120 + "','" + v121 + "','" + v122 + "','" + v123 + "','" + v124 + "','" + v125 + "','" + v130 + "','" + v131 + "','" + v132 + "','" + v133 + "','" + v134 + "','" + v135 + "','" + v40 + "','" + v41 + "','" + v42 + "','" + v50 + "','" + v51 + "','" + v52 + "','" + v53 + "','" +
                                v54 + "','" + v150 + "','" + v151 + "','" + v152 + "','" + v153 + "'"))
                            {
                                i = 1;
                            }
                            else
                            {
                                if (!cnn.insertC("caja", id + ",'" + textBox3.Text + "'," + idmedidas + ",'" + idflauta + "'," +
                                    idresistencia + ",'" + comboBox1.Text + "'," + numericUpDown1.Value + "," + numericUpDown2.Value
                                    + ",'" + comboBox3.Text + "'," + color1 + "," + color2 + "," + color3 + "," + color4 + ",'" + estilo + "'," + idvalact
                                    + ",'" + path + textBox3.Text + ".bmp'"))
                                {
                                    i = 1;
                                }
                                else
                                {
                                    try
                                    {
                                        if (istroq)
                                        {
                                            Rectangle region = new Rectangle(0, 0, 949, 574);
                                            Bitmap bitmap = new Bitmap(region.Width, region.Height, PixelFormat.Format32bppPArgb);
                                            Graphics graphic = Graphics.FromImage(bitmap);
                                            graphic.CopyFromScreen(this.pictureBox1.Location.X + this.Location.X + 15, this.pictureBox1.Location.Y + this.Location.Y + 15, 0, 0, region.Size);
                                            bitmap.Save(path + textBox3.Text + ".bmp");
                                        }
                                        else
                                        {
                                            Rectangle region = new Rectangle(0, 0, largoS + textBox1.Width + 15, anchoS + textBox1.Height + 15);
                                            Bitmap bitmap = new Bitmap(region.Width, region.Height, PixelFormat.Format32bppPArgb);
                                            Graphics graphic = Graphics.FromImage(bitmap);
                                            graphic.CopyFromScreen(textBox6.Location.X + this.Location.X, textBox1.Location.Y + this.Location.Y, 0, 0, region.Size);
                                            bitmap.Save(path + textBox3.Text + ".bmp");
                                        }
                                    }
                                    catch (Exception exc)
                                    {
                                        MessageBox.Show("Error al guardar imagen e = " + exc.Message);
                                    }
                                
                                }
                            }
                        }
                        catch (SystemException exc1)
                        {
                            MessageBox.Show("Error al calcular pos= " + exc1.Message);
                        }


                        if (i == 1)
                            MessageBox.Show("Por algun dato incorrecto no fue exitosa la alta, revisa los datos e intenta de nuevo", "Vuelve a intentar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            if (MessageBox.Show("Registro Exitoso,¿Quieres registar una nueva caja?", "Exito", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                comboBox6.Enabled = false;
                                comboBox6.SelectedItem = null;
                                comboBox7.Enabled = false;
                                comboBox7.SelectedItem = null;
                                comboBox8.Enabled = false;
                                comboBox8.SelectedItem = null;
                                comboBox9.Enabled = false;
                                comboBox9.SelectedItem = null;
                                checkBox1.Checked = false;
                                checkBox2.Checked = false;
                                checkBox3.Checked = false;
                                checkBox4.Checked = false;
                                textBox1.Enabled = false;
                                textBox2.Enabled = false;
                                textBox4.Enabled = false;
                                textBox5.Enabled = false;
                                textBox6.Enabled = false;
                                textBox7.Enabled = false;
                                textBox8.Enabled = false;
                                textBox9.Enabled = false;
                                numericUpDown1.Value = numericUpDown1.Minimum;
                                numericUpDown1.Enabled = false;
                                numericUpDown2.Value = numericUpDown2.Minimum;
                                numericUpDown2.Enabled = false;
                                numericUpDown3.Value = numericUpDown3.Minimum;
                                numericUpDown3.Enabled = false;

                                comboBox4.SelectedItem = null;
                                comboBox4.Enabled = false;
                                comboBox1.SelectedItem = null;
                                comboBox1.Enabled = false;
                                comboBox2.SelectedItem = null;
                                comboBox2.Enabled = false;
                                comboBox3.SelectedItem = null;
                                comboBox3.Enabled = false;
                                textBox3.Text = "";
                                borrar();
                                refsh();
                                textBox1.Location = new Point(346, 396);
                                textBox2.Location = new Point(392, 396);
                                textBox4.Location = new Point(438, 396);
                                textBox5.Location = new Point(484, 396);
                                textBox9.Location = new Point(531, 396);
                                textBox6.Location = new Point(567, 396);
                                textBox7.Location = new Point(613, 396);
                                textBox8.Location = new Point(659, 396);
                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox4.Text = "";
                                textBox5.Text = "";
                                textBox9.Text = "";
                                textBox6.Text = "";
                                textBox7.Text = "";
                                textBox8.Text = "";
                                button5.Enabled = false;
                                button4.Enabled = false;
                                button1.Enabled = false;
                                button17.Enabled = false;
                                label11.Visible = false;
                                label12.Visible = false;
                                label13.Visible = false;
                            }
                            else
                            {
                                this.Dispose();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor completa todos los datos de la caja", "Ingresa todos los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor selecciona la un tipo de Flauta para la caja", "Ingresa todos los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Por favor selecciona el tipo de caja", "Ingresa todos los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void refsh()
        {
            pictureBox1.Image = bt1;
        }

        private void nuevaCaja_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'impiekDataSet1.color' Puede moverla o quitarla según sea necesario.

            // TODO: esta línea de código carga datos en la tabla 'impiekDataSet.color' Puede moverla o quitarla según sea necesario.

            borrar();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            carga(comboBox2, "idflauta", "flauta");
            carga(comboBox4, "nombre", "resistencia");
            carga(comboBox6, "color where idcolor != 1");
            carga(comboBox7, "color where idcolor != 1");
            carga(comboBox8, "color where idcolor != 1");
            carga(comboBox9, "color where idcolor != 1");
            textBox3.CharacterCasing = CharacterCasing.Upper;

        }

        private void carga(ComboBox cb, string atributo, string tabla)
        {
            cb.Items.Clear();
            try
            {
                conexionBD con = new conexionBD();
                DataTable Reader;
                Reader = con.selectFrom(atributo, tabla);
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

        private void borrar()
        {
            bt1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            grafico = Graphics.FromImage(bt1);
        }

        private int[] escalar(int largo, int ancho, int limitx, int limity)
        {
            int[] res = new int[2];
            res[0] = largo;
            res[1] = ancho;
            for (double escala = 1.5; !(res[0] < limitx && res[1] < limity); escala = escala + 0.1)
            {
                res[0] = (int)(largo / escala);
                res[1] = (int)(ancho / escala);
                escalag = escala;
            }
            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            borrar();
            estilo = "es";
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            int largo = (int)numericUpDown2.Value;
            int ancho = (int)numericUpDown1.Value;
            bool a = true;

            textBox1.Visible = a;
            textBox2.Visible = a;
            textBox4.Visible = a;
            textBox5.Visible = a;
            textBox9.Visible = a;
            textBox6.Visible = a;
            textBox7.Visible = a;
            textBox8.Visible = a;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            int limitex = pictureBox1.Width, limitey = pictureBox1.Height;
            int espaciocotas = 20;
            meds = escalar(largo + espaciocotas, ancho + espaciocotas, limitex - 80, limitey - espaciocotas - 50);
            largoS = meds[0];
            anchoS = meds[1];
            int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);


            int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
            float[] dashValues = { 5, 2 };
            Pen blackPen = new Pen(Color.Black, 1);
            blackPen.DashPattern = dashValues;
            Pen cafe = new Pen(Color.Chocolate, 3);

            int largoC = largoS - 20;
            int segy = anchoS / 3;
            int segx = largoC / 4;

            int cxseg = segx - 10;
            int cxseg1 = segx - 5;
            int cyseg = segy;
            int pyseg = segy / 6;

            int a1x = x0;
            int a1y = y0;
            int Ax = x0;
            int Ay = segy + y0;
            int e1x = x0;
            int e1y = 2 * segy + y0;
            int e3x = x0;
            int e3y = 3 * segy;

            int m1x = segx + x0;
            int m1y = segy + y0;
            int m2x = segx + x0;
            int m2y = 2 * segy + y0;

            int b1x = m1x + 5;
            int b1y = y0;
            int f1x = m2x + 5;
            int f1y = m2y;

            int m3x = 2 * segx + x0;
            int m3y = segy + y0;
            int m4x = 2 * segx + x0;
            int m4y = 2 * segy + y0;

            int c1x = m3x + 5;
            int c1y = y0;
            int g1x = m4x + 5;
            int g1y = m4y;

            int m5x = 3 * segx + x0;
            int m5y = segy + y0;
            int m6x = 3 * segx + x0;
            int m6y = 2 * segy + y0;

            int d1x = m5x + 5;
            int d1y = y0;
            int h1x = m6x + 5;
            int h1y = m6y;

            int r1x = m5x + segx;
            int r1y = m5y;
            int j1x = largoS + x0;
            int j1y = m5y + pyseg;

            int l1x = largoS + x0;
            int l1y = m6y - pyseg;
            int k1x = m5x + segx;
            int k1y = m6y;
            xE = 0;

            grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
            grafico.DrawRectangle(cafe, Ax, Ay, segx * 4, segy);
            grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
            grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

            grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
            grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
            grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
            grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

            grafico.DrawRectangle(cafe, a1x, a1y, cxseg1, cyseg);
            grafico.DrawRectangle(cafe, e1x, e1y, cxseg1, cyseg);
            grafico.DrawRectangle(cafe, b1x, b1y, cxseg, cyseg);
            grafico.DrawRectangle(cafe, f1x, f1y, cxseg, cyseg);

            grafico.DrawRectangle(cafe, c1x, c1y, cxseg, cyseg);
            grafico.DrawRectangle(cafe, g1x, g1y, cxseg, cyseg);
            grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
            grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

            TextBox tA = new TextBox();
            Point pb = pictureBox1.Location;
            int ptw = textBox1.Width;
            int pth = textBox1.Height;
            ptw1 = textBox9.Width;

            textBox1.Location = new Point(x0 + pb.X + (segx / 2) - (ptw / 2), y0 - 30 + pb.Y);

            textBox2.Location = new Point(b1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

            textBox4.Location = new Point(c1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

            textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

            textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

            textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

            textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

            textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);

            textBox1.Enabled = true;

            textBox1.Text = "   A";
            textBox2.Text = "   B";
            textBox4.Text = "   C";
            textBox5.Text = "   D";
            textBox6.Text = "   F";
            textBox7.Text = "   G";
            textBox8.Text = "   H";
            textBox9.Text = "   E";
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            button17.Enabled = false;
            textBox8.Show();

            refsh();
            textBox1.Focus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter && textBox1.Text != "")
                {
                    int nAncho = int.Parse(textBox1.Text);
                    A = nAncho;
                    int nAnchoS = (int)(nAncho / escalag);
                    xA = nAnchoS;

                    borrar();
                    int largo = (int)numericUpDown2.Value;
                    int ancho = (int)numericUpDown1.Value;
                    int limitex = pictureBox1.Width, limitey = pictureBox1.Height;

                    int espaciocotas = 20;
                    int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);
                    int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
                    float[] dashValues = { 5, 2 };
                    Pen blackPen = new Pen(Color.Black, 1);
                    blackPen.DashPattern = dashValues;
                    Pen cafe = new Pen(Color.Chocolate, 3);

                    int largoC = largoS - 20;
                    int segy = (anchoS) / 3;
                    int segx = ((largoC - xA) / 3);

                    if (nAnchoS <= (largoC - xA))
                    {
                        if (estilo == "es")
                        {
                            int cxseg = segx - 10;
                            int cxseg1 = segx - 5;
                            int cyseg = segy;
                            int pyseg = segy / 6;

                            int a1x = x0;
                            int a1y = y0;
                            int Ax = x0;
                            int Ay = segy + y0;
                            int e1x = x0;
                            int e1y = 2 * segy + y0;
                            int e3x = x0;
                            int e3y = 3 * segy;

                            int m1x = xA + x0;
                            int m1y = segy + y0;
                            int m2x = xA + x0;
                            int m2y = 2 * segy + y0;

                            int b1x = m1x + 5;
                            int b1y = y0;
                            int f1x = m2x + 5;
                            int f1y = m2y;

                            int m3x = m1x + segx;
                            int m3y = segy + y0;
                            int m4x = m2x + segx;
                            int m4y = 2 * segy + y0;

                            int c1x = m3x + 5;
                            int c1y = y0;
                            int g1x = m4x + 5;
                            int g1y = m4y;

                            int m5x = m3x + segx;
                            int m5y = segy + y0;
                            int m6x = m4x + segx;
                            int m6y = 2 * segy + y0;

                            int d1x = m5x + 5;
                            int d1y = y0;
                            int h1x = m6x + 5;
                            int h1y = m6y;

                            int r1x = m5x + segx;
                            int r1y = m5y;
                            int j1x = largoS + x0;
                            int j1y = m5y + pyseg;

                            int l1x = largoS + x0;
                            int l1y = m6y - pyseg;
                            int k1x = m5x + segx;
                            int k1y = m6y;

                            grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                            grafico.DrawRectangle(cafe, Ax, Ay, xA + 3 * segx, segy);
                            grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                            grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                            grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                            grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                            grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                            grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                            grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                            grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                            grafico.DrawRectangle(cafe, b1x, b1y, cxseg, cyseg);
                            grafico.DrawRectangle(cafe, f1x, f1y, cxseg, cyseg);

                            grafico.DrawRectangle(cafe, c1x, c1y, cxseg, cyseg);
                            grafico.DrawRectangle(cafe, g1x, g1y, cxseg, cyseg);
                            grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
                            grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

                            TextBox tA = new TextBox();
                            Point pb = pictureBox1.Location;
                            int ptw = textBox1.Width;
                            int pth = textBox1.Height;

                            textBox1.Location = new Point(x0 + pb.X + (nAnchoS / 2) - (ptw / 2), y0 - 30 + pb.Y);

                            textBox2.Location = new Point(b1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

                            textBox4.Location = new Point(c1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

                            textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

                            textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                            textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

                            textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                            textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                        }

                        if (estilo == "es_tap")
                        {

                            segy = (anchoS) / 2;
                            segx = ((largoC - xA) / 3);
                            int cxseg = segx - 10;
                            int cxseg1 = segx - 5;
                            int cyseg = segy;
                            int pyseg = segy / 6;

                            int a1x = x0;
                            int a1y = y0;
                            int Ax = x0;
                            int Ay = segy + y0;
                            int e1x = x0;
                            int e1y = 2 * segy + y0;
                            int e3x = x0;
                            int e3y = 3 * segy;


                            int m1x = xA + x0;
                            int m1y = segy + y0;
                            int m2x = xA + x0;
                            int m2y = 2 * segy + y0;

                            int b1x = m1x + 5;
                            int b1y = y0;
                            int f1x = m2x + 5;
                            int f1y = m2y;

                            int m3x = m1x + segx;
                            int m3y = segy + y0;
                            int m4x = m2x + segx;
                            int m4y = 2 * segy + y0;

                            int c1x = m3x + 5;
                            int c1y = y0;
                            int g1x = m4x + 5;
                            int g1y = m4y;

                            int m5x = m3x + segx;
                            int m5y = segy + y0;
                            int m6x = m4x + segx;
                            int m6y = 2 * segy + y0;

                            int d1x = m5x + 5;
                            int d1y = y0;
                            int h1x = m6x + 5;
                            int h1y = m6y;

                            int r1x = m5x + segx;
                            int r1y = m5y;
                            int j1x = largoS + x0;
                            int j1y = m5y + pyseg;

                            int l1x = largoS + x0;
                            int l1y = m6y - pyseg;
                            int k1x = m5x + segx;
                            int k1y = m6y;

                            grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                            grafico.DrawRectangle(cafe, Ax, Ay, xA + 3 * segx, segy);
                            grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                            grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                            grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                            grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                            grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                            grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                            grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                            //grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                            grafico.DrawRectangle(cafe, b1x, b1y, cxseg, cyseg);
                            //grafico.DrawRectangle(cafe, f1x, f1y, cxseg, cyseg);

                            grafico.DrawRectangle(cafe, c1x, c1y, cxseg, cyseg);
                            //grafico.DrawRectangle(cafe, g1x, g1y, cxseg, cyseg);
                            grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
                            //grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

                            TextBox tA = new TextBox();
                            Point pb = pictureBox1.Location;
                            int ptw = textBox1.Width;
                            int pth = textBox1.Height;

                            textBox1.Location = new Point(x0 + pb.X + (nAnchoS / 2) - (ptw / 2), y0 - 30 + pb.Y);

                            textBox2.Location = new Point(b1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

                            textBox4.Location = new Point(c1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

                            textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

                            textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                            textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

                            textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                            textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                        }

                        if (estilo == "es_fon")
                        {
                            largoC = largoS - 20;
                            segy = anchoS / 2;
                            segx = (largoC - xA) / 3;

                            int cxseg = segx - 10;
                            int cxseg1 = segx - 5;
                            int cyseg = segy;
                            int pyseg = segy / 6;
                            int a1x = x0;
                            int a1y = y0;
                            int Ax = x0;
                            int Ay = y0;
                            int e1x = x0;
                            int e1y = segy + y0;
                            int e3x = x0;
                            int e3y = 3 * segy;

                            int m1x = xA + x0;
                            int m1y = y0;
                            int m2x = xA + x0;
                            int m2y = segy + y0;

                            int b1x = m1x + 5;
                            int b1y = y0;
                            int f1x = m2x + 5;
                            int f1y = m2y;

                            int m3x = m1x + segx;
                            int m3y = y0;
                            int m4x = m2x + segx;
                            int m4y = segy + y0;

                            int c1x = m3x + 5;
                            int c1y = y0;
                            int g1x = m4x + 5;
                            int g1y = m4y;

                            int m5x = m3x + segx;
                            int m5y = y0;
                            int m6x = m4x + segx;
                            int m6y = segy + y0;

                            int d1x = m5x + 5;
                            int d1y = y0;
                            int h1x = m6x + 5;
                            int h1y = m6y;

                            int r1x = m5x + segx;
                            int r1y = m5y;
                            int j1x = largoS + x0;
                            int j1y = m5y + pyseg;

                            int l1x = largoS + x0;
                            int l1y = m6y - pyseg;
                            int k1x = m5x + segx;
                            int k1y = m6y;

                            grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                            grafico.DrawRectangle(cafe, Ax, Ay, xA + 3 * segx, segy);
                            grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                            grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                            grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                            grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                            grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                            grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                            //grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                            grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                            //grafico.DrawRectangle(cafe, b1x, b1y, cxseg, cyseg);
                            grafico.DrawRectangle(cafe, f1x, f1y, cxseg, cyseg);

                            //grafico.DrawRectangle(cafe, c1x, c1y, cxseg, cyseg);
                            grafico.DrawRectangle(cafe, g1x, g1y, cxseg, cyseg);
                            //grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
                            grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

                            TextBox tA = new TextBox();
                            Point pb = pictureBox1.Location;
                            int ptw = textBox1.Width;
                            int pth = textBox1.Height;

                            textBox1.Location = new Point(x0 + pb.X + (nAnchoS / 2) - (ptw / 2), y0 - 30 + pb.Y);

                            textBox2.Location = new Point(b1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

                            textBox4.Location = new Point(c1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

                            textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

                            textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                            textBox7.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                            textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                            textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);


                        }

                        textBox1.Enabled = true;
                        textBox2.Enabled = true;
                        textBox2.Focus();


                        refsh();
                    }
                    else
                        MessageBox.Show("Error: La medida introducida supera las dimensiones de la hoja.", "Fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                if (char.IsDigit(e.KeyChar) == true)

                { }

                else if (e.KeyChar == '\b')

                { }

                else
                {
                    ToolTip tp = new ToolTip();
                    tp.ToolTipIcon = ToolTipIcon.Info;
                    tp.SetToolTip(this.textBox1, " Introduce solo números.");
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && textBox2.Text != "")
            {
                int nAncho = int.Parse(textBox2.Text);
                B = nAncho;
                int nAnchoS = (int)(nAncho / escalag);
                xB = nAnchoS;

                borrar();
                int largo = (int)numericUpDown2.Value;
                int ancho = (int)numericUpDown1.Value;
                int limitex = pictureBox1.Width, limitey = pictureBox1.Height;

                int espaciocotas = 20;
                int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);
                int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
                float[] dashValues = { 5, 2 };
                Pen blackPen = new Pen(Color.Black, 1);
                blackPen.DashPattern = dashValues;
                Pen cafe = new Pen(Color.Chocolate, 3);


                int largoC = largoS - 20;
                int segy = (anchoS) / 3;
                int segx = ((largoC - xA - xB) / 2);
                if (nAnchoS <= (largoC - xA))
                {
                    if (estilo == "es")
                    {
                        int cxseg = segx - 10;
                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = segy / 6;

                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = segy + y0;
                        int e1x = x0;
                        int e1y = 2 * segy + y0;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = segy + y0;
                        int m2x = xA + x0;
                        int m2y = 2 * segy + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = segy + y0;
                        int m4x = m2x + xB;
                        int m4y = 2 * segy + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + segx;
                        int m5y = segy + y0;
                        int m6x = m4x + segx;
                        int m6y = 2 * segy + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + segx;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + segx;
                        int k1y = m6y;

                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + 2 * segx, segy);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                        grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);

                        grafico.DrawRectangle(cafe, c1x, c1y, cxseg, cyseg);
                        grafico.DrawRectangle(cafe, g1x, g1y, cxseg, cyseg);
                        grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
                        grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                    }

                    if (estilo == "es_tap")
                    {

                        largoC = largoS - 20;
                        segy = (anchoS) / 2;
                        segx = ((largoC - xA - xB) / 2);
                        int cxseg = segx - 10;
                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = segy / 6;

                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = segy + y0;
                        int e1x = x0;
                        int e1y = 2 * segy + y0;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = segy + y0;
                        int m2x = xA + x0;
                        int m2y = 2 * segy + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = segy + y0;
                        int m4x = m2x + xB;
                        int m4y = 2 * segy + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + segx;
                        int m5y = segy + y0;
                        int m6x = m4x + segx;
                        int m6y = 2 * segy + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + segx;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + segx;
                        int k1y = m6y;


                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + 2 * segx, segy);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                        //grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                        //grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);

                        grafico.DrawRectangle(cafe, c1x, c1y, cxseg, cyseg);
                        //grafico.DrawRectangle(cafe, g1x, g1y, cxseg, cyseg);
                        grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
                        //grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                    }

                    if (estilo == "es_fon")
                    {

                        largoC = largoS - 20;
                        segy = (anchoS) / 2;
                        segx = ((largoC - xA - xB) / 2);
                        int cxseg = segx - 10;

                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = segy / 6;
                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = y0;
                        int e1x = x0;
                        int e1y = segy + y0;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = y0;
                        int m2x = xA + x0;
                        int m2y = segy + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = y0;
                        int m4x = m2x + xB;
                        int m4y = segy + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + segx;
                        int m5y = y0;
                        int m6x = m4x + segx;
                        int m6y = segy + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + segx;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + segx;
                        int k1y = m6y;

                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + 2 * segx, segy);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        //grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                        //grafico.DrawRectangle(cafe, b1x, b1y, cxseg, cyseg);
                        grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);
                        //grafico.DrawRectangle(cafe, c1x, c1y, cxseg, cyseg);
                        grafico.DrawRectangle(cafe, g1x, g1y, cxseg, cyseg);
                        //grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
                        grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);

                    }

                    textBox1.Enabled = false;
                    textBox2.Enabled = true;
                    textBox4.Enabled = true;
                    textBox4.Focus();
                    refsh();
                }
                else
                    MessageBox.Show("Error: La medida introducida supera las dimensiones de la hoja.", "Fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter && textBox4.Text != "")
            {
                int nAncho = int.Parse(textBox4.Text);
                C = nAncho;
                int nAnchoS = (int)(nAncho / escalag);
                xC = nAnchoS;

                borrar();
                int largo = (int)numericUpDown2.Value;
                int ancho = (int)numericUpDown1.Value;
                int limitex = pictureBox1.Width, limitey = pictureBox1.Height;

                int espaciocotas = 22;
                int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);
                int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
                float[] dashValues = { 5, 2 };
                Pen blackPen = new Pen(Color.Black, 1);
                blackPen.DashPattern = dashValues;
                Pen cafe = new Pen(Color.Chocolate, 3);

                int largoC = largoS - 20;
                int segy = (anchoS) / 3;
                int segx = (largoC - xA - xB - xC);
                if (nAnchoS <= largoC - xA - xB)
                {
                    if (estilo == "es")
                    {
                        int cxseg = segx - 10;
                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = segy / 6;

                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = segy + y0;
                        int e1x = x0;
                        int e1y = 2 * segy + y0;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = segy + y0;
                        int m2x = xA + x0;
                        int m2y = 2 * segy + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = segy + y0;
                        int m4x = m2x + xB;
                        int m4y = 2 * segy + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = segy + y0;
                        int m6x = m4x + xC;
                        int m6y = 2 * segy + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + segx;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + segx;
                        int k1y = m6y;

                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + segx, segy);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                        grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);

                        grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, cyseg);
                        grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, cyseg);
                        grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
                        grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                    }
                    if (estilo == "es_tap")
                    {
                        largoC = largoS - 20;
                        segy = (anchoS) / 2;
                        segx = (largoC - xA - xB - xC);
                        int cxseg = segx - 10;
                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = segy / 6;

                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = segy + y0;
                        int e1x = x0;
                        int e1y = 2 * segy + y0;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = segy + y0;
                        int m2x = xA + x0;
                        int m2y = 2 * segy + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = segy + y0;
                        int m4x = m2x + xB;
                        int m4y = 2 * segy + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = segy + y0;
                        int m6x = m4x + xC;
                        int m6y = 2 * segy + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + segx;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + segx;
                        int k1y = m6y;

                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + segx, segy);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                        //grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                        //grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);

                        grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, cyseg);
                        //grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, cyseg);
                        grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
                        //grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);
                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);
                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);
                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);
                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);
                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);
                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);
                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                    }

                    if (estilo == "es_fon")
                    {

                        largoC = largoS - 20;
                        segy = (anchoS) / 2;
                        segx = (largoC - xA - xB - xC);
                        int cxseg = segx - 10;

                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = segy / 6;
                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = y0;
                        int e1x = x0;
                        int e1y = segy + y0;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = y0;
                        int m2x = xA + x0;
                        int m2y = segy + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = y0;
                        int m4x = m2x + xB;
                        int m4y = segy + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = y0;
                        int m6x = m4x + xC;
                        int m6y = segy + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + segx;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + segx;
                        int k1y = m6y;

                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + segx, segy);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        //grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                        //grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                        grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);

                        //grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, cyseg);
                        grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, cyseg);
                        //grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
                        grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);

                    }

                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox4.Enabled = true;
                    textBox5.Enabled = true;
                    textBox5.Focus();
                    textBox5.Text = "";
                    //int len = (int)(70 / escalag);
                    //textBox5.Text = (largo - A - B - C - len).ToString();
                    //textBox9.Text = len.ToString();
                    refsh();
                }
                else
                    MessageBox.Show("Error: La medida introducida supera las dimensiones de la hoja.", "Fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && textBox5.Text != "")
            {
                int nAncho = int.Parse(textBox5.Text);
                D = nAncho;
                int nAnchoS = (int)(nAncho / escalag);
                xD = nAnchoS;
                int largo = (int)numericUpDown2.Value;
                int ancho = (int)numericUpDown1.Value;

                borrar();

                int limitex = pictureBox1.Width, limitey = pictureBox1.Height;
                int espaciocotas = 20;
                int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);
                int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
                float[] dashValues = { 5, 2 };
                Pen blackPen = new Pen(Color.Black, 1);
                blackPen.DashPattern = dashValues;
                Pen cafe = new Pen(Color.Chocolate, 3);

                int largoC = largoS;
                int segy = (anchoS) / 3;
                int segx = (largoC - xA - xB - xC);

                if (nAnchoS <= segx)
                {
                    if (estilo == "es")
                    {
                        int cxseg = segx - 10;
                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = segy / 6;

                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = segy + y0;
                        int e1x = x0;
                        int e1y = 2 * segy + y0;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = segy + y0;
                        int m2x = xA + x0;
                        int m2y = 2 * segy + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = segy + y0;
                        int m4x = m2x + xB;
                        int m4y = 2 * segy + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = segy + y0;
                        int m6x = m4x + xC;
                        int m6y = 2 * segy + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + xD;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + xD;
                        int k1y = m6y;


                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, segy);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                        grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);

                        grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, cyseg);
                        grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, cyseg);
                        grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, cyseg);
                        grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, cyseg);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (xD / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);
                        xE = largoS - xA - xB - xC - xD;

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);

                    }

                    if (estilo == "es_tap")
                    {
                        largoC = largoS - 20;
                        segy = (anchoS) / 2;
                        segx = (largoC - xA - xB - xC);
                        int cxseg = segx - 10;
                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = segy / 6;

                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = segy + y0;
                        int e1x = x0;
                        int e1y = 2 * segy + y0;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = segy + y0;
                        int m2x = xA + x0;
                        int m2y = 2 * segy + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = segy + y0;
                        int m4x = m2x + xB;
                        int m4y = 2 * segy + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = segy + y0;
                        int m6x = m4x + xC;
                        int m6y = 2 * segy + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + xD;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + xD;
                        int k1y = m6y;


                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, segy);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                        //grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                        //grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);

                        grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, cyseg);
                        // grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, cyseg);
                        grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, cyseg);
                        // grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, cyseg);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (xD / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                        xE = largoS - xA - xB - xC - xD;

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                    }

                    if (estilo == "es_fon")
                    {

                        largoC = largoS - 20;
                        segy = (anchoS) / 2;
                        segx = (largoC - xA - xB - xC);
                        int cxseg = segx - 10;

                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = segy / 6;
                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = y0;
                        int e1x = x0;
                        int e1y = segy + y0;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = y0;
                        int m2x = xA + x0;
                        int m2y = segy + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = y0;
                        int m4x = m2x + xB;
                        int m4y = segy + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = y0;
                        int m6x = m4x + xC;
                        int m6y = segy + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + xD;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + xD;
                        int k1y = m6y;

                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, segy);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        //grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                        grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                        //grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                        grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);

                        //grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, cyseg);
                        grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, cyseg);
                        //grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, cyseg);
                        grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, cyseg);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (xD / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                        xE = largoS - xA - xB - xC - xD;

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                    }

                    refsh();

                    textBox9.Enabled = true;
                    textBox9.Focus();
                    E = largo - A - B - C - D;
                    textBox9.Text = E.ToString();

                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox4.Enabled = false;
                    textBox5.Enabled = true;


                }
                else
                    MessageBox.Show("Error: La medida introducida supera las dimensiones de la hoja.", "Fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter && textBox9.Text != "")
            {
                int largo = (int)numericUpDown2.Value;
                int ancho = (int)numericUpDown1.Value;
                E = int.Parse(textBox9.Text);


                if (A + B + C + D + E > largo || E <= 0)
                {
                    MessageBox.Show("Las medidas introducidas superan el largo de la hoja, por favor revisalas de nuevo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (A + B + C + D + E < largo)
                    {
                        if (MessageBox.Show("Las medidas introducidas son menores al total de la hoja. \n ¿Desea continuar?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            int nAncho = int.Parse(textBox9.Text);
                            E = nAncho;
                            int nAnchoS = (int)(nAncho / escalag);
                            xE = nAnchoS;
                            borrar();
                            int limitex = pictureBox1.Width, limitey = pictureBox1.Height;
                            int espaciocotas = 20;
                            int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);
                            int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
                            float[] dashValues = { 5, 2 };
                            Pen blackPen = new Pen(Color.Black, 1);
                            blackPen.DashPattern = dashValues;
                            Pen cafe = new Pen(Color.Chocolate, 3);

                            int largoC = largoS - 20;
                            int segy = (anchoS) / 3;
                            int segx = (largoC - xA - xB - xC - xD);

                            if (estilo == "es")
                            {
                                int cxseg = segx - 10;
                                int cxseg1 = segx - 5;
                                int cyseg = segy;
                                int pyseg = segy / 6;

                                int a1x = x0;
                                int a1y = y0;
                                int Ax = x0;
                                int Ay = segy + y0;
                                int e1x = x0;
                                int e1y = 2 * segy + y0;
                                int e3x = x0;
                                int e3y = 3 * segy;

                                int m1x = xA + x0;
                                int m1y = segy + y0;
                                int m2x = xA + x0;
                                int m2y = 2 * segy + y0;

                                int b1x = m1x + 5;
                                int b1y = y0;
                                int f1x = m2x + 5;
                                int f1y = m2y;

                                int m3x = m1x + xB;
                                int m3y = segy + y0;
                                int m4x = m2x + xB;
                                int m4y = 2 * segy + y0;

                                int c1x = m3x + 5;
                                int c1y = y0;
                                int g1x = m4x + 5;
                                int g1y = m4y;

                                int m5x = m3x + xC;
                                int m5y = segy + y0;
                                int m6x = m4x + xC;
                                int m6y = 2 * segy + y0;

                                int d1x = m5x + 5;
                                int d1y = y0;
                                int h1x = m6x + 5;
                                int h1y = m6y;

                                int r1x = m5x + xD;
                                int r1y = m5y;
                                int j1x = r1x + xE;
                                int j1y = m5y + pyseg;

                                int l1x = r1x + xE;
                                int l1y = m6y - pyseg;
                                int k1x = m5x + xD;
                                int k1y = m6y;


                                grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                                grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, segy);
                                grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                                grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                                grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                                grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                                grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                                grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                                grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                                grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                                grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                                grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);

                                grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, cyseg);
                                grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, cyseg);
                                grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, cyseg);
                                grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, cyseg);

                                TextBox tA = new TextBox();
                                Point pb = pictureBox1.Location;
                                int ptw = textBox1.Width;
                                int pth = textBox1.Height;
                                int ptw1 = textBox9.Width;

                                textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox5.Location = new Point(d1x + pb.X + (xD / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                                textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

                                textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                                textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);

                                i = i + 1;
                            }

                            if (estilo == "es_tap")
                            {
                                largoC = largoS - 20;
                                segy = (anchoS) / 2;
                                segx = (largoC - xA - xB - xC - xD);
                                int cxseg = segx - 10;
                                int cxseg1 = segx - 5;
                                int cyseg = segy;
                                int pyseg = segy / 6;

                                int a1x = x0;
                                int a1y = y0;
                                int Ax = x0;
                                int Ay = segy + y0;
                                int e1x = x0;
                                int e1y = 2 * segy + y0;
                                int e3x = x0;
                                int e3y = 3 * segy;

                                int m1x = xA + x0;
                                int m1y = segy + y0;
                                int m2x = xA + x0;
                                int m2y = 2 * segy + y0;

                                int b1x = m1x + 5;
                                int b1y = y0;
                                int f1x = m2x + 5;
                                int f1y = m2y;

                                int m3x = m1x + xB;
                                int m3y = segy + y0;
                                int m4x = m2x + xB;
                                int m4y = 2 * segy + y0;

                                int c1x = m3x + 5;
                                int c1y = y0;
                                int g1x = m4x + 5;
                                int g1y = m4y;

                                int m5x = m3x + xC;
                                int m5y = segy + y0;
                                int m6x = m4x + xC;
                                int m6y = 2 * segy + y0;

                                int d1x = m5x + 5;
                                int d1y = y0;
                                int h1x = m6x + 5;
                                int h1y = m6y;

                                int r1x = m5x + xD;
                                int r1y = m5y;
                                int j1x = r1x + xE;
                                int j1y = m5y + pyseg;

                                int l1x = r1x + xE;
                                int l1y = m6y - pyseg;
                                int k1x = m5x + xD;
                                int k1y = m6y;

                                grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                                grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, segy);
                                grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                                grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                                grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                                grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                                grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                                grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                                grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                                //grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                                grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                                //grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);
                                grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, cyseg);
                                //grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, cyseg);
                                grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, cyseg);
                                //grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, cyseg);

                                TextBox tA = new TextBox();
                                Point pb = pictureBox1.Location;
                                int ptw = textBox1.Width;
                                int pth = textBox1.Height;
                                int ptw1 = textBox9.Width;

                                textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox5.Location = new Point(d1x + pb.X + (xD / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                                textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

                                textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                                textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                                i = i + 1;
                            }

                            if (estilo == "es_fon")
                            {

                                largoC = largoS - 20;
                                segy = (anchoS) / 2;
                                segx = (largoC - xA - xB - xC - xD);
                                int cxseg = segx - 10;

                                int cxseg1 = segx - 5;
                                int cyseg = segy;
                                int pyseg = segy / 6;
                                int a1x = x0;
                                int a1y = y0;
                                int Ax = x0;
                                int Ay = y0;
                                int e1x = x0;
                                int e1y = segy + y0;
                                int e3x = x0;
                                int e3y = 3 * segy;

                                int m1x = xA + x0;
                                int m1y = y0;
                                int m2x = xA + x0;
                                int m2y = segy + y0;

                                int b1x = m1x + 5;
                                int b1y = y0;
                                int f1x = m2x + 5;
                                int f1y = m2y;

                                int m3x = m1x + xB;
                                int m3y = y0;
                                int m4x = m2x + xB;
                                int m4y = segy + y0;

                                int c1x = m3x + 5;
                                int c1y = y0;
                                int g1x = m4x + 5;
                                int g1y = m4y;

                                int m5x = m3x + xC;
                                int m5y = y0;
                                int m6x = m4x + xC;
                                int m6y = segy + y0;

                                int d1x = m5x + 5;
                                int d1y = y0;
                                int h1x = m6x + 5;
                                int h1y = m6y;

                                int r1x = m5x + xD;
                                int r1y = m5y;
                                int j1x = r1x + xE;
                                int j1y = m5y + pyseg;

                                int l1x = r1x + xE;
                                int l1y = m6y - pyseg;
                                int k1x = m5x + xD;
                                int k1y = m6y;

                                grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                                grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, segy);
                                grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                                grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                                grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                                grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                                grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                                grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                                //grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, cyseg);
                                grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, cyseg);
                                //grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, cyseg);
                                grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, cyseg);
                                //grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, cyseg);
                                grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, cyseg);
                                //grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, cyseg);
                                grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, cyseg);

                                TextBox tA = new TextBox();
                                Point pb = pictureBox1.Location;
                                int ptw = textBox1.Width;
                                int pth = textBox1.Height;
                                int ptw1 = textBox9.Width;

                                textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox5.Location = new Point(d1x + pb.X + (xD / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

                                textBox7.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                                textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

                                textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                            }

                            refsh();

                            textBox1.Enabled = false;
                            textBox2.Enabled = false;
                            textBox4.Enabled = false;
                            textBox5.Enabled = true;
                            textBox9.Enabled = true;
                            textBox9.Focus();

                        }

                        textBox6.Text = "";
                        textBox5.Enabled = false;
                        textBox6.Enabled = true;
                        textBox6.Focus();
                    }


                    textBox6.Text = "";
                    textBox5.Enabled = false;
                    textBox9.Enabled = false;
                    textBox6.Enabled = true;
                    textBox6.Focus();
                }

            }

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && textBox6.Text != "")
            {
                int largo = (int)numericUpDown2.Value;
                int ancho = (int)numericUpDown1.Value;
                int nancho = int.Parse(textBox6.Text);

                F = nancho;
                H = nancho;
                G = ancho - F - H;
                int nanchoS = (int)(nancho / escalag);
                yF = nanchoS;
                yH = (int)(H / escalag);
                yG = (int)(G / escalag);

                borrar();

                int limitex = pictureBox1.Width, limitey = pictureBox1.Height;
                int espaciocotas = 22;
                int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);
                int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
                float[] dashValues = { 5, 2 };
                Pen blackPen = new Pen(Color.Black, 1);
                blackPen.DashPattern = dashValues;
                Pen cafe = new Pen(Color.Chocolate, 3);

                int largoC = largoS - 22;
                int segy = (anchoS - yF) / 2;
                int segx = (largoC - xA - xB - xC - xE);
                if ((yF + yG + yH <= anchoS && yG > 0 && estilo == "es") || (F < ancho && (estilo == "es_tap" || estilo == "es_fon")))
                {
                    segy = 0;
                    if (estilo == "es")
                    {
                        int cxseg = segx - 10;
                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = yG / 6;

                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = yF + y0;
                        int e1x = x0;
                        int e1y = yG + y0 + yF;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = yF + y0;
                        int m2x = xA + x0;
                        int m2y = yF + yG + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = yF + y0;
                        int m4x = m2x + xB;
                        int m4y = yF + yG + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = yF + y0;
                        int m6x = m4x + xC;
                        int m6y = yF + yG + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + xD;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + xD;
                        int k1y = m6y;



                        grafico.DrawRectangle(blackPen, x0, y0, largoS, yF + yG + yH);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, yG);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, yF);
                        grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, yH);
                        grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, yF);
                        grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, yH);

                        grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, yF);
                        grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, yH);
                        grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, yF);
                        grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, yH);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (yF / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (yG / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (yH / 2) - (pth / 2) - 3);

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                    }

                    if (estilo == "es_tap")
                    {
                        F = nancho;

                        G = ancho - F;
                        nanchoS = (int)(nancho / escalag);
                        yF = nanchoS;
                        yH = (int)(H / escalag);
                        yG = (int)(G / escalag);

                        int cxseg = segx - 10;
                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = yG / 6;

                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = yF + y0;
                        int e1x = x0;
                        int e1y = yG + y0 + yF;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = yF + y0;
                        int m2x = xA + x0;
                        int m2y = yF + yG + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = yF + y0;
                        int m4x = m2x + xB;
                        int m4y = yF + yG + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = yF + y0;
                        int m6x = m4x + xC;
                        int m6y = yF + yG + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + xD;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + xD;
                        int k1y = m6y;

                        grafico.DrawRectangle(blackPen, x0, y0, largoS, yF + yG);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, yG);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, yF);
                        //grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, yH);
                        grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, yF);
                        //grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, yH);

                        grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, yF);
                        //grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, yH);
                        grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, yF);
                        //grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, yH);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (yF / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (yG / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (yH / 2) - (pth / 2) - 3);

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                    }


                    if (estilo == "es_fon")
                    {

                        F = nancho;

                        G = ancho - F;
                        nanchoS = (int)(nancho / escalag);
                        yF = nanchoS;
                        yH = (int)(H / escalag);
                        yG = (int)(G / escalag);
                        int cxseg = segx - 10;

                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = yF / 6;
                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = y0;
                        int e1x = x0;
                        int e1y = y0 + yF;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = y0;
                        int m2x = xA + x0;
                        int m2y = yF + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = y0;
                        int m4x = m2x + xB;
                        int m4y = yF + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = y0;
                        int m6x = m4x + xC;
                        int m6y = yF + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + xD;
                        int r1y = m5y;
                        int j1x = r1x + xE;
                        int j1y = m5y + pyseg;

                        int l1x = r1x + xE;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + xD;
                        int k1y = m6y;

                        grafico.DrawRectangle(blackPen, x0, y0, largoS, yF + yG);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, yF);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        //grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, yF);
                        grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, yG);
                        //grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, yF);
                        grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, yG);

                        //grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, yF);
                        grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, yG);
                        //grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, yF);
                        grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, yG);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (yF / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (yG / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (yH / 2) - (pth / 2) - 3);

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                    }
                    refsh();


                    textBox9.Enabled = false;
                    textBox6.Enabled = true;
                    textBox7.Enabled = true;
                    textBox7.Focus();
                    textBox7.Text = G.ToString();
                    textBox8.Text = H.ToString();

                }
                else
                    MessageBox.Show("Error: La medida introducida supera las dimensiones de la hoja.", "Fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && textBox7.Text != "")
            {
                int Gp = G;
                int nancho = int.Parse(textBox7.Text);
                G = nancho;
                int nanchoS = (int)(nancho / escalag);
                yG = nanchoS;


                int largo = (int)numericUpDown2.Value;
                int ancho = (int)numericUpDown1.Value;
                int limitex = pictureBox1.Width, limitey = pictureBox1.Height;
                int espaciocotas = 22;
                int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);
                int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
                float[] dashValues = { 5, 2 };
                Pen blackPen = new Pen(Color.Black, 1);
                blackPen.DashPattern = dashValues;
                Pen cafe = new Pen(Color.Chocolate, 3);
                anchoS = (int)(ancho / escalag);
                int largoC = largoS - 22;
                int segy = anchoS - yF - yG;
                int segx = (largoC - xA - xB - xC - xE);
                if (nanchoS <= anchoS - yF)
                {
                    if (estilo == "es")
                    {
                        borrar();
                        int cxseg = segx - 10;
                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = yG / 6;

                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = yF + y0;
                        int e1x = x0;
                        int e1y = yG + y0 + yF;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = yF + y0;
                        int m2x = xA + x0;
                        int m2y = yF + yG + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = yF + y0;
                        int m4x = m2x + xB;
                        int m4y = m2y;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = yF + y0;
                        int m6x = m4x + xC;
                        int m6y = m2y;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + xD;
                        int r1y = m5y;
                        int j1x = largoS + x0;
                        int j1y = m5y + pyseg;

                        int l1x = largoS + x0;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + xD;
                        int k1y = m6y;


                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, yG);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, yF);
                        grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, segy);
                        grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, yF);
                        grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, segy);

                        grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, yF);
                        grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, cyseg);
                        grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, yF);
                        grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, cyseg);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (yF / 2) - (pth / 2));

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (yG / 2) - (pth / 2));

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2));

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                        textBox6.Enabled = false;
                        textBox7.Enabled = true;
                        textBox8.Enabled = true;
                        textBox8.Focus();
                        textBox8.Text = (ancho - F - G).ToString();
                    }
                    if (estilo == "es_tap")
                    {
                        if (G < Gp)
                        {
                            if (MessageBox.Show("Las medidas introducidas son menores al total de la hoja. \n ¿Desea continuar?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                borrar();

                                int cxseg = segx - 10;
                                int cxseg1 = segx - 5;
                                int cyseg = segy;
                                int pyseg = yG / 6;

                                int a1x = x0;
                                int a1y = y0;
                                int Ax = x0;
                                int Ay = yF + y0;
                                int e1x = x0;
                                int e1y = yG + y0 + yF;
                                int e3x = x0;
                                int e3y = 3 * segy;

                                int m1x = xA + x0;
                                int m1y = yF + y0;
                                int m2x = xA + x0;
                                int m2y = yF + yG + y0;

                                int b1x = m1x + 5;
                                int b1y = y0;
                                int f1x = m2x + 5;
                                int f1y = m2y;

                                int m3x = m1x + xB;
                                int m3y = yF + y0;
                                int m4x = m2x + xB;
                                int m4y = yF + yG + y0;

                                int c1x = m3x + 5;
                                int c1y = y0;
                                int g1x = m4x + 5;
                                int g1y = m4y;

                                int m5x = m3x + xC;
                                int m5y = yF + y0;
                                int m6x = m4x + xC;
                                int m6y = yF + yG + y0;

                                int d1x = m5x + 5;
                                int d1y = y0;
                                int h1x = m6x + 5;
                                int h1y = m6y;

                                int r1x = m5x + xD;
                                int r1y = m5y;
                                int j1x = largoS + x0;
                                int j1y = m5y + pyseg;

                                int l1x = largoS + x0;
                                int l1y = m6y - pyseg;
                                int k1x = m5x + xD;
                                int k1y = m6y;

                                grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                                grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, yG);
                                grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                                grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                                grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                                grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                                grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                                grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                                grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, yF);
                                //grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, yH);
                                grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, yF);
                                //grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, yH);

                                grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, yF);
                                //grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, yH);
                                grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, yF);
                                //grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, yH);

                                TextBox tA = new TextBox();
                                Point pb = pictureBox1.Location;
                                int ptw = textBox1.Width;
                                int pth = textBox1.Height;

                                textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                                textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                                textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                                textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                                textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (yF / 2) - (pth / 2) - 3);

                                textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (yG / 2) - (pth / 2) - 3);

                                textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (yH / 2) - (pth / 2) - 3);

                                textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);
                            }
                        }
                    }

                    if (estilo == "es_fon")
                    {
                        borrar();

                        int cxseg = segx - 10;

                        int cxseg1 = segx - 5;
                        int cyseg = segy;
                        int pyseg = yF / 6;
                        int a1x = x0;
                        int a1y = y0;
                        int Ax = x0;
                        int Ay = y0;
                        int e1x = x0;
                        int e1y = y0 + yF;
                        int e3x = x0;
                        int e3y = 3 * segy;

                        int m1x = xA + x0;
                        int m1y = y0;
                        int m2x = xA + x0;
                        int m2y = yF + y0;

                        int b1x = m1x + 5;
                        int b1y = y0;
                        int f1x = m2x + 5;
                        int f1y = m2y;

                        int m3x = m1x + xB;
                        int m3y = y0;
                        int m4x = m2x + xB;
                        int m4y = yF + y0;

                        int c1x = m3x + 5;
                        int c1y = y0;
                        int g1x = m4x + 5;
                        int g1y = m4y;

                        int m5x = m3x + xC;
                        int m5y = y0;
                        int m6x = m4x + xC;
                        int m6y = yF + y0;

                        int d1x = m5x + 5;
                        int d1y = y0;
                        int h1x = m6x + 5;
                        int h1y = m6y;

                        int r1x = m5x + xD;
                        int r1y = m5y;
                        int j1x = r1x + xE;
                        int j1y = m5y + pyseg;

                        int l1x = r1x + xE;
                        int l1y = m6y - pyseg;
                        int k1x = m5x + xD;
                        int k1y = m6y;


                        grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                        grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, yF);
                        grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                        grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                        grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                        grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                        grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                        grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                        //grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, yF);
                        grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, yG);
                        //grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, yF);
                        grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, yG);

                        //grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, yF);
                        grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, yG);
                        //grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, yF);
                        grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, yG);

                        TextBox tA = new TextBox();
                        Point pb = pictureBox1.Location;
                        int ptw = textBox1.Width;
                        int pth = textBox1.Height;

                        textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                        textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                        textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (yF / 2) - (pth / 2) - 3);

                        textBox7.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (yG / 2) - (pth / 2) - 3);

                        textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (yH / 2) - (pth / 2) - 3);

                        textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);


                    }
                    textBox6.Enabled = false;
                    textBox7.Enabled = false;
                    button17.Enabled = true;
                    button17.Focus();
                    refsh();
                    comboBox4.Enabled = true;
                    comboBox4.Focus();
                }

                else
                    MessageBox.Show("Error: La medida introducida supera las dimensiones de la hoja.", "Fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            int largo = (int)numericUpDown2.Value;
            int ancho = (int)numericUpDown1.Value;

            if (e.KeyChar == (char)Keys.Enter && textBox8.Text != "")
            {
                int nancho = int.Parse(textBox8.Text);
                H = nancho;
                int nanchoS = (int)(nancho / escalag);
                yH = nanchoS;
                if (F + G + H > ancho || H <= 0)
                {
                    MessageBox.Show("Las medidas introducidas superan el ancho de la hoja, por favor revisalas de nuevo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (F + G + H < ancho)
                    {

                        if (MessageBox.Show("Las medidas introducidas son menores al total de la hoja. \n ¿Desea continuar?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            borrar();
                            int limitex = pictureBox1.Width, limitey = pictureBox1.Height;
                            int espaciocotas = 22;
                            int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);
                            int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
                            float[] dashValues = { 5, 2 };
                            Pen blackPen = new Pen(Color.Black, 1);
                            blackPen.DashPattern = dashValues;
                            Pen cafe = new Pen(Color.Chocolate, 3);

                            int largoC = largoS - 22;
                            int segy = anchoS - yF - yG;
                            int segx = (largoC - xA - xB - xC - xE);

                            int cxseg = segx - 10;
                            int cxseg1 = segx - 5;
                            int cyseg = segy;
                            int pyseg = segy / 6;

                            int a1x = x0;
                            int a1y = y0;
                            int Ax = x0;
                            int Ay = yF + y0;
                            int e1x = x0;
                            int e1y = yG + y0 + yF;
                            int e3x = x0;
                            int e3y = 3 * segy;

                            int m1x = xA + x0;
                            int m1y = yF + y0;
                            int m2x = xA + x0;
                            int m2y = yF + yG + y0;

                            int b1x = m1x + 5;
                            int b1y = y0;
                            int f1x = m2x + 5;
                            int f1y = m2y;

                            int m3x = m1x + xB;
                            int m3y = yF + y0;
                            int m4x = m2x + xB;
                            int m4y = m2y;

                            int c1x = m3x + 5;
                            int c1y = y0;
                            int g1x = m4x + 5;
                            int g1y = m4y;

                            int m5x = m3x + xC;
                            int m5y = yF + y0;
                            int m6x = m4x + xC;
                            int m6y = m2y;

                            int d1x = m5x + 5;
                            int d1y = y0;
                            int h1x = m6x + 5;
                            int h1y = m6y;

                            int r1x = m5x + xD;
                            int r1y = m5y;
                            int j1x = largoS + x0;
                            int j1y = m5y + pyseg;

                            int l1x = largoS + x0;
                            int l1y = m6y - pyseg;
                            int k1x = m5x + xD;
                            int k1y = m6y;

                            grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
                            grafico.DrawRectangle(cafe, Ax, Ay, xA + xB + xC + xD, yG);
                            grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
                            grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

                            grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
                            grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
                            grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
                            grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

                            grafico.DrawRectangle(cafe, a1x, a1y, xA - 5, yF);
                            grafico.DrawRectangle(cafe, e1x, e1y, xA - 5, yH);
                            grafico.DrawRectangle(cafe, b1x, b1y, xB - 10, yF);
                            grafico.DrawRectangle(cafe, f1x, f1y, xB - 10, yH);

                            grafico.DrawRectangle(cafe, c1x, c1y, xC - 10, yF);
                            grafico.DrawRectangle(cafe, g1x, g1y, xC - 10, yH);
                            grafico.DrawRectangle(cafe, d1x, d1y, xD - 5, yF);
                            grafico.DrawRectangle(cafe, h1x, h1y, xD - 5, yH);

                            TextBox tA = new TextBox();
                            Point pb = pictureBox1.Location;
                            int ptw = textBox1.Width;
                            int pth = textBox1.Height;

                            textBox1.Location = new Point(x0 + pb.X + (xA / 2) - (ptw / 2), y0 - 30 + pb.Y);

                            textBox2.Location = new Point(b1x + pb.X + (xB / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                            textBox4.Location = new Point(c1x + pb.X + (xC / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                            textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) + 3, y0 - 30 + pb.Y);

                            textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (yF / 2) - (pth / 2));

                            textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (yG / 2) - (pth / 2));

                            textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (yH / 2) - (pth / 2));

                            textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);

                            refsh();

                            textBox6.Enabled = false;
                            textBox7.Enabled = false;
                            textBox8.Enabled = false;
                            button17.Enabled = true;
                            button17.Focus();
                        }
                    }

                    else
                    {
                        textBox6.Enabled = false;
                        textBox7.Enabled = false;
                        textBox8.Enabled = false;
                        button17.Enabled = true;
                        button17.Focus();
                    }
                }
                comboBox4.Enabled = true;
                comboBox4.Focus();
            }


            if (char.IsDigit(e.KeyChar) == true)

            { }

            else if (e.KeyChar == '\b')

            { }

            else
            {
                ToolTip tp = new ToolTip();
                tp.ToolTipIcon = ToolTipIcon.Info;
                tp.SetToolTip(this.textBox1, " Introduce solo números.");
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox9.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            button17.Enabled = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Impresora.Resources.Images.button3;
            label11.Visible = true;
            label12.Visible = true;
            label11.Text = "A = " + numericUpDown2.Value.ToString();
            label12.Text = "L = " + numericUpDown1.Value.ToString();
            label11.BringToFront();
            label12.BringToFront();
            comboBox4.Enabled = true;
            comboBox4.Focus();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            borrar();
            estilo = "es_tap";
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            bool a = true;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            textBox1.Visible = a;
            textBox2.Visible = a;
            textBox4.Visible = a;
            textBox5.Visible = a;
            textBox9.Visible = a;
            textBox6.Visible = a;
            textBox7.Visible = a;
            textBox8.Visible = a;

            int largo = (int)numericUpDown2.Value;
            int ancho = (int)numericUpDown1.Value;

            int limitex = pictureBox1.Width, limitey = pictureBox1.Height;
            int espaciocotas = 20;
            meds = escalar(largo + espaciocotas, ancho + espaciocotas, limitex - 80, limitey - espaciocotas - 50);
            largoS = meds[0];
            anchoS = meds[1];
            int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);


            int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
            float[] dashValues = { 5, 2 };
            Pen blackPen = new Pen(Color.Black, 1);
            blackPen.DashPattern = dashValues;
            Pen cafe = new Pen(Color.Chocolate, 3);


            int largoC = largoS - 20;
            int segy = anchoS / 2;
            int segx = largoC / 4;

            int cxseg = segx - 10;
            int cxseg1 = segx - 5;
            int cyseg = segy;
            int pyseg = segy / 6;

            int a1x = x0;
            int a1y = y0;
            int Ax = x0;
            int Ay = segy + y0;
            int e1x = x0;
            int e1y = 2 * segy + y0;
            int e3x = x0;
            int e3y = 3 * segy;

            int m1x = segx + x0;
            int m1y = segy + y0;
            int m2x = segx + x0;
            int m2y = 2 * segy + y0;

            int b1x = m1x + 5;
            int b1y = y0;
            int f1x = m2x + 5;
            int f1y = m2y;

            int m3x = 2 * segx + x0;
            int m3y = segy + y0;
            int m4x = 2 * segx + x0;
            int m4y = 2 * segy + y0;

            int c1x = m3x + 5;
            int c1y = y0;
            int g1x = m4x + 5;
            int g1y = m4y;

            int m5x = 3 * segx + x0;
            int m5y = segy + y0;
            int m6x = 3 * segx + x0;
            int m6y = 2 * segy + y0;

            int d1x = m5x + 5;
            int d1y = y0;
            int h1x = m6x + 5;
            int h1y = m6y;

            int r1x = m5x + segx;
            int r1y = m5y;
            int j1x = largoS + x0;
            int j1y = m5y + pyseg;

            int l1x = largoS + x0;
            int l1y = m6y - pyseg;
            int k1x = m5x + segx;
            int k1y = m6y;
            xE = 0;

            grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
            grafico.DrawRectangle(cafe, Ax, Ay, segx * 4, segy);
            grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
            grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

            grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
            grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
            grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
            grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

            grafico.DrawRectangle(cafe, a1x, a1y, cxseg1, cyseg);
            //grafico.DrawRectangle(cafe, e1x, e1y, cxseg1, cyseg);
            grafico.DrawRectangle(cafe, b1x, b1y, cxseg, cyseg);
            //grafico.DrawRectangle(cafe, f1x, f1y, cxseg, cyseg);

            grafico.DrawRectangle(cafe, c1x, c1y, cxseg, cyseg);
            //grafico.DrawRectangle(cafe, g1x, g1y, cxseg, cyseg);
            grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
            //grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

            TextBox tA = new TextBox();
            Point pb = pictureBox1.Location;
            int ptw = textBox1.Width;
            int pth = textBox1.Height;
            ptw1 = textBox9.Width;

            textBox1.Location = new Point(x0 + pb.X + (segx / 2) - (ptw / 2), y0 - 30 + pb.Y);

            textBox2.Location = new Point(b1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

            textBox4.Location = new Point(c1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

            textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

            textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

            textBox7.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

            textBox7.Location = new Point(x0 + pb.X - ptw - 5, Ay + pb.Y + (segy / 2) - (pth / 2) - 3);

            textBox8.Hide();
            //textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

            textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);

            textBox1.Enabled = true;

            textBox1.Text = "   A";
            textBox2.Text = "   B";
            textBox4.Text = "   C";
            textBox5.Text = "   D";
            textBox6.Text = "   F";
            textBox7.Text = "   G";
            textBox8.Text = "   H";
            textBox9.Text = " E";
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            button17.Enabled = false;

            refsh();
            textBox1.Focus();


        }

        private void button5_Click(object sender, EventArgs e)
        {

            borrar();
            estilo = "es_fon";
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            bool a = true;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            textBox1.Visible = a;
            textBox2.Visible = a;
            textBox4.Visible = a;
            textBox5.Visible = a;
            textBox9.Visible = a;
            textBox6.Visible = a;
            textBox7.Visible = a;
            textBox8.Visible = a;

            int largo = (int)numericUpDown2.Value;
            int ancho = (int)numericUpDown1.Value;

            int limitex = pictureBox1.Width, limitey = pictureBox1.Height;
            int espaciocotas = 20;
            meds = escalar(largo + espaciocotas, ancho + espaciocotas, limitex - 80, limitey - espaciocotas - 50);
            largoS = meds[0];
            anchoS = meds[1];
            int x0 = ((limitex / 2) + espaciocotas + 10) - ((int)largoS / 2);
            int y0 = ((limitey / 2) + espaciocotas) - ((int)anchoS / 2);
            float[] dashValues = { 5, 2 };
            Pen blackPen = new Pen(Color.Black, 1);
            blackPen.DashPattern = dashValues;
            Pen cafe = new Pen(Color.Chocolate, 3);


            int largoC = largoS - 20;
            int segy = anchoS / 2;
            int segx = largoC / 4;
            int cxseg = segx - 10;

            int cxseg1 = segx - 5;
            int cyseg = segy;
            int pyseg = segy / 6;

            int a1x = x0;
            int a1y = y0;
            int Ax = x0;
            int Ay = y0;
            int e1x = x0;
            int e1y = segy + y0;
            int e3x = x0;
            int e3y = 3 * segy;

            int m1x = segx + x0;
            int m1y = y0;
            int m2x = segx + x0;
            int m2y = segy + y0;

            int b1x = m1x + 5;
            int b1y = y0;
            int f1x = m2x + 5;
            int f1y = m2y;

            int m3x = 2 * segx + x0;
            int m3y = y0;
            int m4x = 2 * segx + x0;
            int m4y = segy + y0;

            int c1x = m3x + 5;
            int c1y = y0;
            int g1x = m4x + 5;
            int g1y = m4y;

            int m5x = 3 * segx + x0;
            int m5y = y0;
            int m6x = 3 * segx + x0;
            int m6y = segy + y0;

            int d1x = m5x + 5;
            int d1y = y0;
            int h1x = m6x + 5;
            int h1y = m6y;

            int r1x = m5x + segx;
            int r1y = m5y;
            int j1x = largoS + x0;
            int j1y = m5y + pyseg;

            int l1x = largoS + x0;
            int l1y = m6y - pyseg;
            int k1x = m5x + segx;
            int k1y = m6y;
            xE = 0;
            grafico.DrawRectangle(blackPen, x0, y0, largoS, anchoS);
            grafico.DrawRectangle(cafe, Ax, Ay, segx * 4, segy);
            grafico.DrawLine(cafe, m1x, m1y, m2x, m2y);
            grafico.DrawLine(cafe, m3x, m3y, m4x, m4y);

            grafico.DrawLine(cafe, m5x, m5y, m6x, m6y);
            grafico.DrawLine(cafe, r1x, r1y, j1x, j1y);
            grafico.DrawLine(cafe, j1x, j1y, l1x, l1y);
            grafico.DrawLine(cafe, l1x, l1y, k1x, k1y);

            //grafico.DrawRectangle(cafe, a1x, a1y, cxseg1, cyseg);
            grafico.DrawRectangle(cafe, e1x, e1y, cxseg1, cyseg);
            //grafico.DrawRectangle(cafe, b1x, b1y, cxseg, cyseg);
            grafico.DrawRectangle(cafe, f1x, f1y, cxseg, cyseg);

            //grafico.DrawRectangle(cafe, c1x, c1y, cxseg, cyseg);
            grafico.DrawRectangle(cafe, g1x, g1y, cxseg, cyseg);
            //grafico.DrawRectangle(cafe, d1x, d1y, cxseg1, cyseg);
            grafico.DrawRectangle(cafe, h1x, h1y, cxseg1, cyseg);

            TextBox tA = new TextBox();
            Point pb = pictureBox1.Location;
            int ptw = textBox1.Width;
            int pth = textBox1.Height;
            ptw1 = textBox9.Width;

            textBox1.Location = new Point(x0 + pb.X + (segx / 2) - (ptw / 2), y0 - 30 + pb.Y);

            textBox2.Location = new Point(b1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

            textBox4.Location = new Point(c1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

            textBox5.Location = new Point(d1x + pb.X + (segx / 2) - (ptw / 2) - 3, y0 - 30 + pb.Y);

            textBox6.Location = new Point(x0 + pb.X - ptw - 5, y0 + pb.Y + (segy / 2) - (pth / 2) - 3);

            textBox7.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

            textBox8.Hide();
            //textBox8.Location = new Point(x0 + pb.X - ptw - 5, e1y + pb.Y + (segy / 2) - (pth / 2) - 3);

            textBox9.Location = new Point(pb.X + r1x + (xE / 2) - (ptw1 / 2) + 5, y0 - 30 + pb.Y);

            textBox1.Enabled = true;

            textBox1.Text = "   A";
            textBox2.Text = "   B";
            textBox4.Text = "   C";
            textBox5.Text = "   D";
            textBox6.Text = "   F";
            textBox7.Text = "   G";
            textBox8.Text = "   H";
            textBox9.Text = " E";
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            button17.Enabled = false;

            refsh();
            textBox1.Focus();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && textBox3.Text != "" && textBox3.Text != "0")
            {
                string res = "0";
                conexionBD cnn = new conexionBD();
                res = cnn.busca("clave", "caja", textBox3.Text);
                if (res != "0")
                {
                    if (res == "")
                    {
                        numericUpDown2.Enabled = true;
                        numericUpDown2.Focus();
                    }
                    else
                        MessageBox.Show("La clave ya esta dada de alta, utiliza otra.", "Intenta de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (comboBox1.SelectedItem.ToString() != "")
                {


                    if (comboBox1.Text == "Estandar")
                    {
                        button1.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button6.Enabled = false;
                        button7.Enabled = false;


                        numericUpDown3.Maximum = 1;
                    }
                    else
                    {
                        button1.Enabled = false;
                        button4.Enabled = false;
                        button5.Enabled = false;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button6.Enabled = true;
                        button7.Enabled = true;
                        numericUpDown3.Maximum = 10;
                    }

                }
            }
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && textBox3.Text != "" && numericUpDown2.Value != 0)
            {
                comboBox1.Enabled = true;
                comboBox1.Focus();
            }
        }

        private void numericUpDown2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && textBox3.Text != "" && numericUpDown2.Value != 0)
            {
                numericUpDown1.Enabled = true;
                numericUpDown1.Focus();
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && comboBox1.Text != "")
            {
                //comboBox4.Enabled = true;
                //comboBox4.Focus();
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && comboBox4.Text != "")
            {
                comboBox2.Enabled = true;
                comboBox2.Focus();
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && comboBox2.Text != "")
            {
                comboBox3.Enabled = true;
                comboBox3.Focus();
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && comboBox3.Text != "")
            {
                button1.Enabled = true;
                button1.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox6.Enabled = true;
            }
            if (checkBox1.Checked == false)
            {
                comboBox6.Enabled = false;
                comboBox6.SelectedItem = null;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked == true)
            {
                comboBox7.Enabled = true;
            }
            if (checkBox2.Checked == false)
            {
                comboBox7.Enabled = false;
                comboBox7.SelectedItem = null;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                comboBox8.Enabled = true;
            }
            if (checkBox3.Checked == false)
            {
                comboBox8.Enabled = false;
                comboBox8.SelectedItem = null;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                comboBox9.Enabled = true;
            }
            if (checkBox4.Checked == false)
            {
                comboBox9.Enabled = false;
                comboBox9.SelectedItem = null;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                comboBox2.Enabled = true;
                comboBox2.Focus();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                comboBox3.Enabled = true;
                comboBox3.Focus();
                if (comboBox1.Text == "Troquelado")
                {
                    label13.Visible = true;
                    label13.Text = "F = " + comboBox2.Text;
                    label13.BringToFront();
                }
            }
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                numericUpDown3.Enabled = true;
                numericUpDown3.Focus();
                if (comboBox1.Text == "Troquelado")
                {
                    //button17.Enabled = true;
                    istroq = true;
                }
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_Enter(object sender, EventArgs e)
        {



        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void pinta(ComboBox sender, Button cuadro)
        {
            conexionBD cnn = new conexionBD();
            if (sender.Text == "")
            {
                cuadro.BackColor = Button.DefaultBackColor;
            }
            else
            {
                int idesp = sender.Text.IndexOf(" ", 0);
                string tx = sender.Text.Substring(0, idesp);
                string argb = cnn.busca1("argb", "color", "Tinta ='" + tx + "'");
                cuadro.BackColor = Color.FromArgb(int.Parse(argb));
            }
        }


        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pinta(comboBox6, button9);
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            pinta(comboBox7, button10);
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            pinta(comboBox8, button11);
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            pinta(comboBox9, button12);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            numericUpDown3.UpButton();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            numericUpDown3.DownButton();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox9.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Impresora.Resources.Images.button7;
            button17.Enabled = true;
            label11.Visible = true;
            label12.Visible = true;
            label11.Text = "A = " + numericUpDown2.Value.ToString();
            label12.Text = "L = " + numericUpDown1.Value.ToString();
            label11.BringToFront();
            label12.BringToFront();
            comboBox4.Enabled = true;
            comboBox4.Focus();
            //Rectangle region = new Rectangle(0,0,largoS+textBox1.Width + 23,anchoS+ textBox1.Height+35);
            //Bitmap bitmap = new Bitmap(region.Width, region.Height, PixelFormat.Format32bppPArgb);
            //Graphics graphic = Graphics.FromImage(bitmap);
            //graphic.CopyFromScreen(textBox6.Location.X+this.Location.X, textBox1.Location.Y+this.Location.Y, 0, 0, region.Size);
            //bitmap.Save(path + textBox3.Text + ".bmp");             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Impresora.Resources.Images.button2;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox9.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            button17.Enabled = true;
            label11.Visible = true;
            label12.Visible = true;
            label11.Text = "A = " + numericUpDown2.Value.ToString();
            label12.Text = "L = " + numericUpDown1.Value.ToString();
            label11.BringToFront();
            label12.BringToFront();
            comboBox4.Enabled = true;
            comboBox4.Focus();


        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Impresora.Resources.Images.button6;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox9.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            button17.Enabled = true;
            label11.Visible = true;
            label12.Visible = true;
            label11.Text = "A = " + numericUpDown2.Value.ToString();
            label12.Text = "L = " + numericUpDown1.Value.ToString();
            label11.BringToFront();
            label12.BringToFront();
            comboBox4.Enabled = true;
            comboBox4.Focus();

        }

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;
            nu.Text = "";
        }

        private void numericUpDown2_Enter(object sender, EventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;
            nu.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
