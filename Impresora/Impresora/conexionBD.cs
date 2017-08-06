using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Impresora

{
    class conexionBD
    {

        public static String path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Kinex\\Resources\\";
        String sconnection = "server=localhost;User Id=root;password=iek5530;Persist Security Info=True;database=impiekv5_3s";//System.Configuration.ConfigurationManager.ConnectionStrings["Impresora.Properties.Settings.impiekv5_1v4_1ConnectionString"].ConnectionString;
        static public string usuario = "";
        static public string idusuario = "";
        static public string tipoUsuario = "";

        public DataTable selectFrom(String atributos, String restricciones)
        {
            MySqlConnection myCnn = new MySqlConnection();
            myCnn.ConnectionString = sconnection;
            myCnn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT " + atributos + " FROM " + restricciones, myCnn);
            DataTable tabla = new DataTable();
            try
            {
                MySqlDataAdapter datos = new MySqlDataAdapter(cmd);
                datos.Fill(tabla);
                return tabla;
            }
            catch (System.Exception e)
            {
                MessageBox.Show("Error: El servidor de datos respondio: " + e.Message + e.Source);
            }
            myCnn.Close();
            return tabla;
        }

        public String selectPass(String id)
        {
            try
            {
                MySqlConnection myCnn = new MySqlConnection();
                myCnn.ConnectionString = sconnection;
                MySqlDataReader Reader;
                myCnn.Open();
                MySqlCommand command = myCnn.CreateCommand();
                string consulta = "";
                consulta = "SELECT hex(AES_DECRYPT(VALUE,'iek-datos-')) FROM Licence where idLicence='" + id + "'";
                command.CommandText = consulta;
                Reader = command.ExecuteReader();
                string sResultado = "";

                while (Reader.Read())
                {
                    for (int i = 0; i < Reader.FieldCount; i++)
                    {

                        sResultado = Reader.GetValue(i).ToString();
                    }
                }
                Reader.Close();
                myCnn.Close();
                sResultado = HexString2Ascii(sResultado);

                return sResultado;
            }
            catch (System.Exception)
            {
                MessageBox.Show("Usuario: '" + id + "' no valido", "Error: Imposible recuperar Contraseña",
         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "0";
            }
        }

        private string HexString2Ascii(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= hexString.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }


        public String selectTipo(String nombre)
        {
            try
            {
                MySqlConnection myCnn = new MySqlConnection();
                myCnn.ConnectionString = sconnection;
                MySqlDataReader Reader;
                myCnn.Open();
                MySqlCommand command = myCnn.CreateCommand();
                string consulta = "";
                consulta = "SELECT tipo FROM usuario where alias='" + nombre + "'";
                command.CommandText = consulta;
                Reader = command.ExecuteReader();
                string sResultado = "";
                while (Reader.Read())
                {
                    for (int i = 0; i < Reader.FieldCount; i++)
                        sResultado = Reader.GetValue(i).ToString();
                }
                Reader.Close();
                myCnn.Close();
                return sResultado;
            }
            catch (System.Exception)
            {
                MessageBox.Show("Tipo de usuario no definido, por favor contacte al administrador de sistema", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return "0";
            }

        }

        public String selectVal(String field, string condition)
        {
            try
            {
                MySqlConnection myCnn = new MySqlConnection();
                myCnn.ConnectionString = sconnection;
                MySqlDataReader Reader;
                myCnn.Open();
                MySqlCommand command = myCnn.CreateCommand();
                string consulta = "";
                consulta = "SELECT {0} FROM {1}";
                command.CommandText = string.Format(consulta, field, condition);
                Reader = command.ExecuteReader();
                string sResultado = "";
                while (Reader.Read())
                {
                    for (int i = 0; i < Reader.FieldCount; i++)
                        sResultado = Reader.GetValue(i).ToString();
                }
                Reader.Close();
                myCnn.Close();
                return sResultado;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Tipo de usuario no definido, por favor contacte al administrador de sistema " + e, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return "0";
            }

        }

        public bool delete(string tabla)
        {
            try
            {
                string myConnectionString = sconnection;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                string myInsertQuery = "delete from " + tabla;
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
                //    MessageBox.Show("Valor Eliminado ", "Información",
                //MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }
            catch (System.Exception e)
            {
                MessageBox.Show("No se pudo borrar el servidor contesto: " + e.Message, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public bool insert(String tabla, String datos)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                string myInsertQuery = "INSERT INTO " + tabla + " Values(" + datos + ")";
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
                MessageBox.Show("Valor agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("El servidor contestó: " + e.Message, " Error Sevidor de datos",
             MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

        }

        public bool insertC(String tabla, String datos, string extra="")
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                string myInsertQuery = "INSERT INTO " + tabla + " Values(" + datos + ")";
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }

        }
        public string getNextId(String tabla)
        {

            try
            {
                MySqlConnection myCnn = new MySqlConnection();
                myCnn.ConnectionString = sconnection;
                MySqlDataReader Reader;
                myCnn.Open();
                MySqlCommand command = myCnn.CreateCommand();
                string consulta = "";
                consulta = "SELECT auto_increment FROM `information_schema`.tables where TABLE_SCHEMA= 'impiekv5_3s' and TABLE_NAME = '" + tabla + "'";
                command.CommandText = consulta;
                Reader = command.ExecuteReader();
                string sResultado = "";
                while (Reader.Read())
                {
                    for (int i = 0; i < Reader.FieldCount; i++)
                        sResultado = Reader.GetValue(i).ToString();
                }
                Reader.Close();
                myCnn.Close();
                return sResultado;
            }
            catch (System.Exception)
            {
                MessageBox.Show("Imposible recuperar siguiente ID", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return "-10";
            }
        }

        public string getUsuarioId(string nombre)
        {
            try
            {
                MySqlConnection myCnn = new MySqlConnection();
                myCnn.ConnectionString = sconnection;
                MySqlDataReader Reader;
                myCnn.Open();
                MySqlCommand command = myCnn.CreateCommand();
                string consulta = "";
                consulta = "SELECT idempleado FROM empleado where correo='" + nombre + "'";
                command.CommandText = consulta;
                Reader = command.ExecuteReader();
                string sResultado = "";
                while (Reader.Read())
                {
                    for (int i = 0; i < Reader.FieldCount; i++)
                        sResultado = Reader.GetValue(i).ToString();
                }
                Reader.Close();
                myCnn.Close();
                return sResultado;
            }
            catch (System.Exception)
            {
                MessageBox.Show("Privilegios no especificados, por favor contacte al administrador de sistema", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return "0";
            }

        }

        public string getId(string tabla, string atributo, string batributo, string clave)
        {
            try
            {
                MySqlConnection myCnn = new MySqlConnection();
                myCnn.ConnectionString = sconnection;
                MySqlDataReader Reader;
                myCnn.Open();
                MySqlCommand command = myCnn.CreateCommand();
                string consulta = "";
                consulta = "SELECT " + atributo + " FROM " + tabla + " where " + batributo + "= '" + clave + "'";
                command.CommandText = consulta;
                Reader = command.ExecuteReader();
                string sResultado = "";
                while (Reader.Read())
                {
                    for (int i = 0; i < Reader.FieldCount; i++)
                        sResultado = Reader.GetValue(i).ToString();
                }
                Reader.Close();
                myCnn.Close();
                return sResultado;
            }
            catch (System.Exception)
            {
                MessageBox.Show("Privilegios no especificados, por favor contacte al administrador de sistema", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return "0";
            }

        }

        public string getId(string tabla, string atributo, string restrics)
        {
            try
            {
                MySqlConnection myCnn = new MySqlConnection();
                myCnn.ConnectionString = sconnection;
                MySqlDataReader Reader;
                myCnn.Open();
                MySqlCommand command = myCnn.CreateCommand();
                string consulta = "";
                consulta = "SELECT " + atributo + " FROM " + tabla + " where " + restrics;
                command.CommandText = consulta;
                Reader = command.ExecuteReader();
                string sResultado = "";
                while (Reader.Read())
                {
                    for (int i = 0; i < Reader.FieldCount; i++)
                        sResultado = Reader.GetValue(i).ToString();
                }
                Reader.Close();
                myCnn.Close();
                return sResultado;
            }
            catch (System.Exception)
            {
                MessageBox.Show("Privilegios no especificados, por favor contacte al administrador de sistema", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return "0";
            }

        }

        public bool update(String tabla, String restricciones)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                string myInsertQuery = "UPDATE " + tabla + " SET " + restricciones;
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();

                return true;

            }
            catch (Exception e)
            {
                MessageBox.Show("Actualizacion no completada el servidor respondio:" + e.Message, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public bool updateEmpleado(string id, string nombre, string apellido, string edad, string puesto, string correo, string direccion, string seguro, string usuarioReg)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                string myInsertQuery = "UPDATE empleado SET nombre=?nombre,apellido=?apellido,edad=?edad,puesto=?puesto,correo=?correo,direccion=?direccion, seguro=?seguro,usuarioReg=?usuarioReg " +
                "Where idempleado=" + id;
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Parameters.Add("?nombre", MySqlDbType.VarChar, 100).Value = nombre;
                myCommand.Parameters.Add("?apellido", MySqlDbType.VarChar, 45).Value = apellido;
                myCommand.Parameters.Add("?edad", MySqlDbType.VarChar, 45).Value = edad;
                myCommand.Parameters.Add("?puesto", MySqlDbType.VarChar, 45).Value = puesto;
                myCommand.Parameters.Add("?correo", MySqlDbType.VarChar, 45).Value = correo;
                myCommand.Parameters.Add("?direccion", MySqlDbType.VarChar, 400).Value = direccion;
                myCommand.Parameters.Add("?seguro", MySqlDbType.VarChar, 45).Value = seguro;
                myCommand.Parameters.Add("?usuarioReg", MySqlDbType.VarChar, 4).Value = usuarioReg;
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();

                return true;

            }
            catch (Exception e)
            {
                MessageBox.Show("Actualizacion empleado no completada el servidor respondio:" + e.Message, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public bool updateTurno(string id, string nombre, string resp, string horaini, string horafin)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                string myInsertQuery = "UPDATE turno SET nombre=?nombre,responsable=?resp,horainicio=?horaini,horafin=?horafin " +
                " Where idempleado=" + id;
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Parameters.Add("?nombre", MySqlDbType.VarChar, 45).Value = nombre;
                myCommand.Parameters.Add("?resp", MySqlDbType.VarChar, 45).Value = resp;
                myCommand.Parameters.Add("?horaini", MySqlDbType.Time).Value = DateTime.Parse(horaini);
                myCommand.Parameters.Add("?horafin", MySqlDbType.Time).Value = DateTime.Parse(horafin);
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Actualizacion no completada, el servidor respondio: " + e.Message, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

        }

        public bool updateUsuario(string id, string tipo, string alias, string password)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                string myInsertQuery = "UPDATE usuario SET tipo=?tipo,alias=?alias,password=?password " +
                " Where idempleado=" + id;
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Parameters.Add("?tipo", MySqlDbType.VarChar, 45).Value = tipo;
                myCommand.Parameters.Add("?alias", MySqlDbType.VarChar, 45).Value = alias;
                myCommand.Parameters.Add("?password", MySqlDbType.VarChar, 45).Value = password;
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Actualizacion no completada, el servidor respondio: " + e.Message, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

        }

        public bool update_emp_has_turno(string idempleado, string idturno)
        {

            try
            {
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                string myInsertQuery = "UPDATE empleado_has_turno SET turno_idturno=?idturno " +
                "Where empleado_idempleado='" + idempleado + "'";
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Parameters.Add("?idturno", MySqlDbType.Int16).Value = idturno;
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Actualizacion no completada, el servidor resopondio: " + e.Message, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public bool deleteFrom(string tabla, string restricciones)
        {
            try
            {
                string myConnectionString = sconnection;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                string myInsertQuery = "delete from " + tabla + " where " + restricciones;
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
                MessageBox.Show("Valor Eliminado ", "Información",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }
            catch (System.Exception e)
            {
                MessageBox.Show("No se pudo borrar el servidor contesto: " + e.Message, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public void inBitacora(string accion, string drive, int handle)
        {
                       
            string sResultado = "";
            try
            {
                MySqlConnection myCnn = new MySqlConnection();
                myCnn.ConnectionString = sconnection;
                MySqlDataReader Reader;
                myCnn.Open();
                MySqlCommand command = myCnn.CreateCommand();
                string consulta = "";
                consulta = "SELECT auto_increment FROM `information_schema`.tables where TABLE_SCHEMA= 'impiekv5_3s' and TABLE_NAME = 'bitacora'";
                command.CommandText = consulta;
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    for (int i = 0; i < Reader.FieldCount; i++)
                        sResultado = Reader.GetValue(i).ToString();
                }
                Reader.Close();
                myCnn.Close();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Imposible recuperar siguiente ID para bítacora", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            try
            {

                string id = sResultado;
                DateTime fecha = DateTime.Now;
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                string myInsertQuery = "INSERT INTO bitacora Values(?id,?drive,?fecha,?accion,?usuario,?handle)";
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Parameters.Add("?id", MySqlDbType.Int16, 11).Value = id;
                myCommand.Parameters.Add("?drive", MySqlDbType.Int16, 11).Value = drive;
                myCommand.Parameters.Add("?handle", MySqlDbType.Int16, 11).Value = handle;
                myCommand.Parameters.Add("?usuario", MySqlDbType.VarChar, 45).Value = "system";
                myCommand.Parameters.Add("?accion", MySqlDbType.VarChar, 200).Value = accion;
                myCommand.Parameters.Add("?fecha", MySqlDbType.DateTime).Value = fecha;
               
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
                //   MessageBox.Show("Registro Exitoso bitacora", "Exito",
                //MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar en bitacora el servidor respondio: " + ex.Message, "Error inesperado",
             MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void inparametro(String idmaq, String uprod, String vel, String tmuerto)
        {

        }

        public string busca(String Atributo, String tabla, String clave)
        {
            try
            {
                MySqlConnection myCnn = new MySqlConnection();
                myCnn.ConnectionString = sconnection;
                MySqlDataReader Reader;
                myCnn.Open();
                MySqlCommand command = myCnn.CreateCommand();
                string consulta = "";
                consulta = "SELECT " + Atributo + " FROM " + tabla + " where " + Atributo + " = '" + clave + "'";
                command.CommandText = consulta;
                Reader = command.ExecuteReader();
                string sResultado = "";
                while (Reader.Read())
                {
                    for (int i = 0; i < Reader.FieldCount; i++)
                        sResultado = Reader.GetValue(i).ToString();
                }
                Reader.Close();
                myCnn.Close();
                return sResultado;
            }
            catch (System.Exception e)
            {
                MessageBox.Show("No se pudo buscar el servidor contesto: " + e.Message, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return "0";
            }
        }

        public string busca1(String Atributo, String tabla, String clave)
        {
            try
            {
                MySqlConnection myCnn = new MySqlConnection();
                myCnn.ConnectionString = sconnection;
                MySqlDataReader Reader;
                myCnn.Open();
                MySqlCommand command = myCnn.CreateCommand();
                string consulta = "";
                consulta = "SELECT " + Atributo + " FROM " + tabla + " where " + clave;
                command.CommandText = consulta;
                Reader = command.ExecuteReader();
                string sResultado = "";
                while (Reader.Read())
                {
                    for (int i = 0; i < Reader.FieldCount; i++)
                        sResultado = Reader.GetValue(i).ToString();
                }
                Reader.Close();
                myCnn.Close();
                return sResultado;
            }
            catch (System.Exception e)
            {
                MessageBox.Show("No se pudo buscar el servidor contesto: " + e.Message, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return "0";
            }
        }
    }
}
