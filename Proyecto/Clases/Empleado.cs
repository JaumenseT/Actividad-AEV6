using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Proyecto.Clases {
    class Empleado {
        private string id;
        private string nombre;
        private string apellidos;
        private bool admin;
        private string contrasenya;

        public string Id { get { return id; } set { id = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Apellidos { get { return apellidos; } set { apellidos = value; } }
        public bool Admin { get { return admin; } set { admin = value; } }
        public string Contrasenya { get { return contrasenya; } set { contrasenya = value; } }


        public Empleado(string id, string nombre, string apellidos, bool admin, string contrasenya) {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.admin = admin;
            this.contrasenya = contrasenya;
        }

        public Empleado() {

        }

        public int AgregarEmpleado(MySqlConnection connection, Empleado emp) {
            int retorno;
            string consulta = "INSERT INTO empleados (id,nombre,apellidos,admin,contraseña) VALUES " +
                "(@id, @nombre, @apellidos, @admin, @contraseña)";

            // Trim: elimina espacios innecesarios en strings.
            MySqlCommand comando = new MySqlCommand(consulta, connection);
            comando.Parameters.AddWithValue("@id", emp.id.Trim());
            comando.Parameters.AddWithValue("@nombre", emp.nombre.Trim());
            comando.Parameters.AddWithValue("@apellidos", emp.apellidos.Trim());
            comando.Parameters.AddWithValue("@admin", emp.admin);
            comando.Parameters.AddWithValue("@contraseña", emp.contrasenya.Trim());

            retorno = comando.ExecuteNonQuery();

            return retorno;
        }

        public static int EliminaUsuario(MySqlConnection conexion, string id)
        {
            int retorno;
            string consulta = string.Format("DELETE FROM empleados WHERE id={0}", id);
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static List<Empleado> BuscarEmpleado(MySqlConnection conexion, string consulta) {
            List<Empleado> lista = new List<Empleado>();
            // MessageBox.Show(consulta);   -Se puede activar esta línea para testear la sintaxis de la consulta.

            // Creamos el objeto command al cual le pasamos la consulta y la conexión
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            // Ejecutamos el comando y recibimos en un objeto DataReader la lista de registros seleccionados.
            // Recordemos que un objeto DataReader es una especie de tabla de datos virtual.
            MySqlDataReader reader = comando.ExecuteReader();

            if (reader.HasRows)   // En caso que se hayan registros en el objeto reader
            {
                // Recorremos el reader (registro por registro) y cargamos la lista de usuarios.
                while (reader.Read()) {
                    Empleado emp = new Empleado(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                        reader.GetBoolean(3), reader.GetString(4));
                    lista.Add(emp);
                }
            }
            // devolvemos la lista cargada con los usuarios.
            return lista;
        }
    }
}
