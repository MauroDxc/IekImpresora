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
    public partial class consultas : Form
    {
        public consultas()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conexionBD cnn = new conexionBD();
            string busqueda = textBox1.Text;

            DataTable datos = cnn.selectFrom("o.idorden as Orden,o.fecha as Fecha,o.numeroPiezas as Piezas,c.cla" +
            "ve as Caja,cl.nombre as Cliente,op.nombre as Operador",
            "orden as o, caja as c, cliente as cl, operador as op where o.caja_idcaja = c.idcaja and o.cliente_idc" +
            "liente = cl.idcliente and o.operador_idoperardor = op.idoperardor and idorden like '%" + busqueda + "'");
            dataGridView2.DataSource = datos;
        }

        private void consultas_Load(object sender, EventArgs e)
        {
            dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            conexionBD cnn = new conexionBD();
            DataTable datos = cnn.selectFrom("o.idorden as Orden,o.fecha as Fecha,o.numeroPiezas as Piezas,c.cla" +
            "ve as Caja,cl.nombre as Cliente,op.nombre as Operador,o.caja_idcaja as IdCaja, cl.idCliente as IdCliente, op.idoperardor as IdOperador ",
            "orden as o, caja as c, cliente as cl, operador as op where o.caja_idcaja = c.idcaja and o.cliente_idc" +
            "liente = cl.idcliente and o.operador_idoperardor = op.idoperardor");
            dataGridView2.DataSource = datos;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[8].Visible = false;

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idCa = (int)dataGridView2.Rows[e.RowIndex].Cells["IdCaja"].Value;
            int idCl = (int)dataGridView2.Rows[e.RowIndex].Cells["IdCliente"].Value;
            int idOp = (int)dataGridView2.Rows[e.RowIndex].Cells["IdOperador"].Value;
            int p = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["Piezas"].Value.ToString());
            Objects.Order o = new Objects.Order();
            o.IdCaja = idCa;
            o.IdCliente = idCl;
            o.IdOperador = idOp;
            o.Volumen = p;
            AppModule.Instance.Object = o;

            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

