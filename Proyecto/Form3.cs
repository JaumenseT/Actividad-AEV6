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
            if (!ValidarDatos())
            {
                return;
            }

            int resultado = 0;
            try
            {
                if (conexionBD.AbrirConexion())
                {
                    Empleado emp = new Empleado();
                    emp.Id = txtId.Text;
                    emp.Nombre = txtNombre.Text;
                    emp.Apellidos = txtApellidos.Text;
                    emp.Admin = ckbAdmin.Checked;
                    emp.Contrasenya = txtContrasenya.Text;
                    resultado = emp.AgregarEmpleado(conexionBD.Conexion, emp);

                    if (resultado > 0) // Si se ha agregado o modificado limpiamos las cajas de texto
                    {
                        txtId.Clear();
                        txtNombre.Clear();
                        if (ckbAdmin.Checked)
                        {
                            ckbAdmin.Checked = false;
                        }
                        txtApellidos.Clear();
                        txtContrasenya.Clear();
                    }

                    // Cierro la conexión
                    conexionBD.CerrarConexion();
                    // volvemos a cargar toda la lista de usuarios;
                    CargaListaUsuarios();

                }
                else
                {
                    MessageBox.Show("No se ha podido abrir la conexión con la Base de Datos");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally  // en cualquier caso cierro la conexión (haya error o no)
            {
                conexionBD.CerrarConexion();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            // Para eliminar, el usuario selecciona un registro del datagrid. 
            // Posteriormente haremos clic en eliminar (nos pedirá confirmación)
            try {
                int resultado;

                if (dgvEmpleados.SelectedRows.Count == 1) // Si hay una fila seleccionada en el datagridview
                {
                    string idUsuario = (string)dgvEmpleados.CurrentRow.Cells[0].Value; // Obtenemos el id de la fila seleccionada

                    DialogResult confirmacion = MessageBox.Show("Borrado de registro seleccionado. ¿Continuar?",
                                                "Eliminación", MessageBoxButtons.YesNo);

                    if (confirmacion == DialogResult.Yes) {
                        if (conexionBD.AbrirConexion()) {
                            resultado = Empleado.EliminaUsuario(conexionBD.Conexion, idUsuario);
                        } else {
                            MessageBox.Show("No se ha podido abrir la conexión con la Base de Datos");
                        }
                        // Cierro la conexión
                        conexionBD.CerrarConexion();
                        // volvemos a cargar toda la lista de usuarios;
                        CargaListaUsuarios();
                    }
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

