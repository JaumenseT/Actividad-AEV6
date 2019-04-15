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

        private void btnAgregar_Click(object sender, EventArgs e) {

        }
    }
}
