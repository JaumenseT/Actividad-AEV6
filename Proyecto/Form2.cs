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
using MySql.Data.MySqlClient;

namespace Proyecto {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }
        ConexionBD conexionBD = new ConexionBD();
        private void callonload() {
            timer2.Start();
            lblTime2.Text = DateTime.Now.ToLongTimeString();
            txtId.Focus();
        }

        

        private void Form2_Load(object sender, EventArgs e) {
            callonload();
        }

        private void timer2_Tick(object sender, EventArgs e) {
            lblTime2.Text = DateTime.Now.ToLongTimeString();
            timer2.Start();
        }

        private void btnVolver_Click(object sender, EventArgs e) {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void btnAcceder_Click(object sender, EventArgs e) {
            //Comprobamos si el usuario que intenta aceder está dado de alta como admin
            conexionBD.AbrirConexion();
            if (Empleado.ComprobarUsuarioAdmin(conexionBD.Conexion, txtId.Text, txtContrasenya.Text)) { // Crear nuevo objeto empleado
                this.Hide();
                Form3 form3 = new Form3();
                form3.Show();
            }
            else
            {
                MessageBox.Show("Datos incorrectos");
            }
            conexionBD.CerrarConexion();
        }
    }
}
