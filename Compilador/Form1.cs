using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public partial class frmIDE : Form
    {
        public frmIDE()
        {
            InitializeComponent();
        }

        private void btnComprobarErrores_Click(object sender, EventArgs e)
        {
            try
            {
                string cadena = txtCodigo.Text;
                char[] arrayCadena = cadena.ToCharArray();
                int estado = 0;
                string cadenaActual = "";

                Matriz matrizTransicion = new Matriz();
                for (int i = 0; i < arrayCadena.Length; i++)
                {
                    cadenaActual += arrayCadena[i];
                    if (i < arrayCadena.Length - 1 && arrayCadena[i].ToString() != " ")
                    {
                        Estado miEstado = matrizTransicion.EvaluarCaracter(arrayCadena[i].ToString(), estado);

                        estado = miEstado.SiguienteEstado;
                    }
                    else
                    {
                        string token = matrizTransicion.ObtenerToken(estado, arrayCadena[i].ToString());

                        MessageBox.Show(cadenaActual + "------>" + token);

                        estado = 0;
                        cadenaActual = "";
                    }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }
    }
}