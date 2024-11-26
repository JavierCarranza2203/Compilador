using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Compilador
{
    public partial class frmIDE : Form
    {
        public frmIDE()
        {
            InitializeComponent();
        }

        private string MostrarArchivoDeTokens(string copiaCadena, string cadenaActual, string token)
        {
            if (copiaCadena.Contains(cadenaActual.Trim()))
            {
                // Encuentra la primera ocurrencia de "cadenaActual"
                int index = copiaCadena.IndexOf(cadenaActual.Trim());

                // Reemplaza solo la primer ocurrencia encontrada
                if (index >= 0)
                {
                    copiaCadena = copiaCadena.Substring(0, index) + token + copiaCadena.Substring(index + cadenaActual.Trim().Length);
                }
            }

            return copiaCadena;
        }

        private void btnComprobarErrores_Click(object sender, EventArgs e)
        {
            try
            {
                dgvTablaSimbolos.Rows.Clear();
                dgvTablaErrores.Rows.Clear();

                string cadena = txtCodigo.Text;
                string copiaCadena = txtCodigo.Text;
                string codigoLimpio = System.Text.RegularExpressions.Regex.Replace(cadena, @"\s+", " ");
                char[] arrayCadena = codigoLimpio.ToCharArray();
                string estadoAnterior = "", estadoSiguiente = "0";
                string cadenaActual = "";

                Matriz matrizTransicion = new Matriz();

                for (int i = 0; i < arrayCadena.Length; i++)
                {
                    cadenaActual += arrayCadena[i];
                    if (i == arrayCadena.Length - 1 || arrayCadena[i].ToString() == " " || matrizTransicion.ValidarEstadoSiguiente(estadoSiguiente))
                    {
                        Token token = matrizTransicion.ObtenerToken(estadoAnterior, estadoSiguiente, arrayCadena[i].ToString() != " ", cadenaActual);

                        copiaCadena = this.MostrarArchivoDeTokens(copiaCadena, cadenaActual, token.Id);


                        if (token.EsError == true)
                            dgvTablaErrores.Rows.Add(token.Palabra, token.Descripcion);
                        else 
                            dgvTablaSimbolos.Rows.Add(token.Palabra, token.Descripcion);

                        estadoSiguiente = "0";
                        cadenaActual = "";
                    }
                    else
                    {
                        estadoAnterior = estadoSiguiente;
                        estadoSiguiente = matrizTransicion.EvaluarCaracter(arrayCadena[i].ToString(), estadoAnterior);
                    }
                }

                txtTokens.Text = copiaCadena;
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardarArchivo_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos de texto (*.txt)|*.txt",
                Title = "Guardar archivo de texto"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, txtCodigo.Text);
                MessageBox.Show("Archivo guardado con éxito en: " + saveFileDialog.FileName);
            }
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de texto (*.txt)|*.txt",
                Title = "Abrir archivo de texto"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Leer el contenido del archivo y mostrarlo en el RichTextBox
                txtCodigo.Text = File.ReadAllText(openFileDialog.FileName);
                MessageBox.Show("Archivo cargado con éxito desde: " + openFileDialog.FileName);
            }
        }

        private void btnGuardarArchivoTokens_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos de texto (*.txt)|*.txt",
                Title = "Guardar archivo de texto"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, txtTokens.Text);
                MessageBox.Show("Archivo guardado con éxito en: " + saveFileDialog.FileName);
            }
        }
    }
}