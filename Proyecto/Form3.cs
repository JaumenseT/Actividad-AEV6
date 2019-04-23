using System;
using Proyecto.Clases;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto {
    public partial class Form3 : Form {
        
        public Form3() {
            InitializeComponent();
        }
        
        ConexionBD conexionBD = new ConexionBD();
        private void CargaListaUsuarios()
        {
            string seleccion = "Select * from empleados";
            if (conexionBD.AbrirConexion())
            {
                dgvEmpleados.AutoGenerateColumns = false;
                dgvEmpleados.DataSource = Empleado.BuscarEmpleado(conexionBD.Conexion, seleccion);
                conexionBD.CerrarConexion();
            }
            else
            {
                MessageBox.Show("No se ha podido abrir la conexión con la Base de Datos");
            }
        }

        private void Form3_Load(object sender, EventArgs e) {
            CargaListaUsuarios();
        }

        private char generarLetra(int DNI) {
            string tabla = "TRWAGMYFPDXBNJZSQVHLCKE";
            int posisicion = (DNI) % 23;
            return tabla[posisicion];
        }

        private bool ValidarDatos() {
            bool ok = true;

            if (txtId.Text.Length - 1 != generarLetra(txtId.Text.Length - 1)) {

            }
            return true;
        }

        private void btnAgregar_Click(object sender, EventArgs e) {
            if (!ValidarDatos()) {
                return;
            }

            int resultado;
            try {
                if (conexionBD.AbrirConexion()) {
                    Empleado usu = new Empleado(txtId.Text, txtNombre.Text, txtApellidos.Text, ckbAdmin.Checked, txtContrasenya.Text);

                    if (String.IsNullOrEmpty(txtIdentidad.Text))  // Estoy agregando un usuario nuevo
                    {
                        resultado = usu.AgregarUsuario(bdatos.Conexion, usu);
                    } else // Estoy modificando un usuario editado
                    {
                        usu.IdUsuario = Convert.ToInt16(txtIdentidad.Text);
                        resultado = usu.ActualizaUsuario(bdatos.Conexion, usu);
                    }

                    if (resultado > 0) // Si se ha agregado o modificado limpiamos las cajas de texto
                    {
                        txtIdentidad.Clear();
                        txtNombre.Clear();
                        txtApellidos.Clear();
                        txtEmail.Clear();
                        txtEdad.Clear();
                        dtpFecha.ResetText();
                        txtCuota.Clear();
                    }

                    // Cierro la conexión
                    bdatos.CerrarConexion();
                    // volvemos a cargar toda la lista de usuarios;
                    CargaListaUsuarios();

                } else {
                    MessageBox.Show("No se ha podido abrir la conexión con la Base de Datos");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally  // en cualquier caso cierro la conexión (haya error o no)
            {
                bdatos.CerrarConexion();
            }
        }

    }
    }
}
