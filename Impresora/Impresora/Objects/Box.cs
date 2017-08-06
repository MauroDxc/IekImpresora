using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Impresora.Objects
{
    class Box
    {
        private int id;
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                conexionBD cx = new conexionBD();
                string q = "valoresp where idValoresP=(select valoresactuales_idvaloresActuales from caja where idcaja={0})";
                DataTable dt = cx.selectFrom("*", string.Format(q, value));
                DataTable dto = cx.selectFrom("idoffset,offsetcol", "offset where idoffset<>0");
                foreach (DataRow r in dt.Rows)
                {
                    foreach (var p in this.GetType().GetProperties())
                    {
                        if (p.Name == "ID")
                        {
                            p.SetValue(this, int.Parse(r[0].ToString()), null);
                        }
                        else
                        {
                            p.SetValue(this, (double.Parse(r[p.Name].ToString()) * 1000) - double.Parse(dto.Select("idoffset ="+p.Name.Replace("P",""))[0][1].ToString()), null);

                        }
                    }
                }
            }
        }
        public double P10 { get; set; }
        public double P11 { get; set; }
        public double P12 { get; set; }
        public double P13 { get; set; }
        public double P14 { get; set; }
        public double P15 { get; set; }
        public double P16 { get; set; }
        public double P20 { get; set; }
        public double P21 { get; set; }
        public double P22 { get; set; }
        public double P23 { get; set; }
        public double P24 { get; set; }
        public double P25 { get; set; }
        public double P30 { get; set; }
        public double P31 { get; set; }
        public double P32 { get; set; }
        public double P33 { get; set; }
        public double P34 { get; set; }
        public double P35 { get; set; }
        public double P40 { get; set; }
        public double P41 { get; set; }
        public double P42 { get; set; }
        public double P50 { get; set; }
        public double P51 { get; set; }
        public double P52 { get; set; }
        public double P53 { get; set; }
        public double P54 { get; set; }
        public double P120 { get; set; }
        public double P121 { get; set; }
        public double P122 { get; set; }
        public double P123 { get; set; }
        public double P124 { get; set; }
        public double P125 { get; set; }
        public double P130 { get; set; }
        public double P131 { get; set; }
        public double P132 { get; set; }
        public double P133 { get; set; }
        public double P134 { get; set; }
        public double P135 { get; set; }
        public double P150 { get; set; }
        public double P151 { get; set; }
        public double P152 { get; set; }
        public double P153 { get; set; }
    }
}
