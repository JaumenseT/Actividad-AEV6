using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        
        private void callonload() {
            timer1.Start();
            lblTime.Text = DateTime.Now.ToLongTimeString();
            txtId.Focus();
        }

        private void Form1_Load_1(object sender, EventArgs e) {
            callonload();
        }

        private char generarLetra(int DNI)
        {
            string tabla = "TRWAGMYFPDXBNJZSQVHLCKE";
            int posisicion = (DNI) % 23;
            return tabla[posisicion];
        }

        

        private bool ValidarDatos()
       {
            bool ok = true;

            if (txtId.Text.Length-1 != generarLetra(txtId.Text.Length-1))
            {

            }

            
            return true;
       }

        private void timer1_Tick(object sender, EventArgs e) {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void btnEntrada_Click(object sender, EventArgs e) {



        }

        private void btnSalida_Click(object sender, EventArgs e) {

        }

        

        private void btnPresencia_Click(object sender, EventArgs e) {

        }

        private void btnPermanencia_Click(object sender, EventArgs e) {

        }

        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
            
        }


    }
}
