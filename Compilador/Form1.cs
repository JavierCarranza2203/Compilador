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
                int contadorIdentificadores = 1;
                List<Token> tokens = new List<Token>();
                List<string> errores = new List<string> { "PRE1", "IDE2", "CNE3", "COE4", "CAE5", "OAE6", "OLE7", "ORE8", "CEE9", "0A10", "FDCE" };

                Matriz matrizTransicion = new Matriz();

                BuscadorColumnas bc = new BuscadorColumnas();


                for (int i = 0; i < arrayCadena.Length; i++)
                {
                    cadenaActual += arrayCadena[i];

                    char[] arrayActual = cadenaActual.ToCharArray();
                    bool palabraExiste = false;
                    if (i == arrayCadena.Length - 1 || arrayCadena[i].ToString() == " " || matrizTransicion.ValidarEstadoSiguiente(estadoSiguiente))
                    {
                        Token token = matrizTransicion.ObtenerToken(estadoAnterior, estadoSiguiente, arrayCadena[i].ToString() != " ", cadenaActual);

                        if (token.Id == "IDVAL")
                        {
                            foreach (Token token2 in tokens)
                            {
                                if (token2.Palabra == token.Palabra) { 
                                    palabraExiste = true; 

                                    token.Id = token2.Id;
                                }
                            }

                            if (palabraExiste == false)
                            {
                                token.Id += contadorIdentificadores;
                                tokens.Add(token);
                                contadorIdentificadores++;
                                dgvTablaSimbolos.Rows.Add(contadorIdentificadores, token.Palabra);
                            }
                        }
                        else if(token.Id == "IDE2")
                        {
                            foreach (Token token2 in tokens)
                            {
                                if (token2.Palabra == token.Palabra) return;
                            }

                            token.Id += contadorIdentificadores;
                            tokens.Add(token);
                            errores.Add(token.Id);
                            contadorIdentificadores++;

                            dgvTablaSimbolos.Rows.Add(contadorIdentificadores, token.Palabra);
                            dgvTablaErrores.Rows.Add(EncontrarNumeroLinea(token.Palabra), token.Descripcion);
                        }
                        else if (token.EsError)
                            dgvTablaErrores.Rows.Add(EncontrarNumeroLinea(token.Palabra), token.Descripcion);

                        copiaCadena = this.MostrarArchivoDeTokens(copiaCadena, cadenaActual, token.Id);

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
                ResaltarPalabrasClave(errores, Color.Red);
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


        private void ResaltarPalabrasClave(List<string> palabrasClaves, Color color)
        {
            // Limpiar cualquier selección previa
            txtTokens.SelectAll();
            txtTokens.SelectionColor = Color.Black;

            foreach (string palabraClave in palabrasClaves)
            {
                int startIndex = 0;
                while ((startIndex = txtTokens.Text.IndexOf(palabraClave, startIndex)) != -1)
                {
                    // Selecciona la palabra encontrada
                    txtTokens.Select(startIndex, palabraClave.Length);
                    // Aplica el color a la selección
                    txtTokens.SelectionColor = color;
                    // Avanza el índice para continuar buscando la siguiente ocurrencia
                    startIndex += palabraClave.Length;
                }
            }
        }


        private string EncontrarNumeroLinea(string cadena)
        {
            // Supongamos que "richTextBox1" es tu RichTextBox y buscas la palabra "palabraClave".
            int index = txtCodigo.Find(cadena); // Encuentra el índice de la palabra

            if (index != -1) // Si se encuentra la palabra
            {
                int numeroLinea = txtCodigo.GetLineFromCharIndex(index); // Obtiene el número de línea
                return (numeroLinea + 1).ToString();
            }
            else {
                int index2 = txtCodigo.Find(cadena + " ");
                int numeroLinea = txtCodigo.GetLineFromCharIndex(index2); // Obtiene el número de línea
                return (numeroLinea + 1).ToString();
            }
        }
    }
}