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
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Drawing2D;

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
            // Crear una expresión regular que coincida exactamente con la cadenaActual
            string pattern = $@"(?<!\w){Regex.Escape(cadenaActual.Trim())}(?!\w)";

            // Variable para indicar si ya se hizo un reemplazo
            bool reemplazoHecho = false;

            // Usar MatchEvaluator para reemplazar solo la primera ocurrencia
            copiaCadena = Regex.Replace(copiaCadena, pattern, match =>
            {
                if (!reemplazoHecho)
                {
                    reemplazoHecho = true;  // Marcar que ya se hizo el reemplazo
                    return token;           // Reemplazar por el token
                }
                return match.Value; // Devolver el valor original si ya se hizo el reemplazo
            });

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
                List<string> errores = new List<string> { "PRNV", "IDNV", "CNNV", "CONV", "CANV", "OANV", "OLNV", "OPRNV", "CENV", "OASNV", "FDCE" };

                Matriz matrizTransicion = new Matriz();

                BuscadorColumnas bc = new BuscadorColumnas();
                bool error = false;
                Token token = new Token("", false, "", "");

                for (int i = 0; i < arrayCadena.Length; i++)
                {
                    cadenaActual += arrayCadena[i];

                    bool palabraExiste = false;

                    if (i == arrayCadena.Length - 1 || arrayCadena[i].ToString() == " " || matrizTransicion.ValidarEstadoSiguiente(estadoSiguiente))
                    {
                        token = matrizTransicion.ObtenerToken(estadoAnterior, estadoSiguiente, arrayCadena[i].ToString() != " ", cadenaActual);

                        if (token.Id == "IDVAL")
                        {
                            foreach (Token token2 in tokens)
                            {
                                if (token2.Palabra == token.Palabra)
                                {
                                    palabraExiste = true;

                                    token.Id = token2.Id;
                                }
                            }

                            if (palabraExiste == false)
                            {
                                token.Id += contadorIdentificadores;
                                tokens.Add(token);
                                contadorIdentificadores++;

                                dgvTablaSimbolos.Rows.Add(token.Id, token.Palabra);
                            }
                        }
                        else if (token.Id == "IDNV")
                        {
                            foreach (Token token2 in tokens)
                            {
                                if (token2.Palabra == token.Palabra) return;
                            }

                            token.Id += contadorIdentificadores;
                            tokens.Add(token);
                            contadorIdentificadores++;
                            errores.Add(token.Id);

                            dgvTablaSimbolos.Rows.Add(token.Id, token.Palabra);
                            //dgvTablaErrores.Rows.Add(EncontrarNumeroLinea(token.Palabra, token.Id), token.Descripcion);
                        }
                        else if (token.EsError)
                            continue;
                            //dgvTablaErrores.Rows.Add(EncontrarNumeroLinea(token.Palabra, token.Id), token.Descripcion);

                        copiaCadena = this.MostrarArchivoDeTokens(copiaCadena, cadenaActual, token.Id);
                        txtTokens.Text = copiaCadena;

                        if (token.EsError)
                        {
                            dgvTablaErrores.Rows.Add(EncontrarNumeroLinea(token.Palabra, token.Id), token.Descripcion);
                        }

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

                if(error)
                {
                    dgvTablaErrores.Rows.Add(EncontrarNumeroLinea(token.Palabra, token.Id), token.Descripcion);
                    error = false;
                }

                ResaltarPalabrasClave(errores, Color.Red);
                ActualizarNumerosTokens();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardarArchivo_Click(object sender, EventArgs e)
        {
            GuardarArchivo();
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
            GuardarArchivo();
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

        private string EncontrarNumeroLinea(string cadena, string token)
        {
            int index = txtTokens.Find(token); // Encuentra el índice de la palabra

            if (index != -1) // Si se encuentra la palabra
            {
                int numeroLinea = txtTokens.GetLineFromCharIndex(index); // Obtiene el número de línea
                return (numeroLinea + 1).ToString();
            }
            else {
                int index2 = txtCodigo.Find(cadena);
                int numeroLinea = txtCodigo.GetLineFromCharIndex(index2); // Obtiene el número de línea

                return (numeroLinea + 1).ToString();
            }
        }

        private void ActualizarNumerosYContenido()
        {
            // Obtén todas las líneas del RichTextBox
            string[] lineas = txtCodigo.Lines;

            // Construye el texto para el TextBox con números de línea
            string contenidoConNumeros = "";
            for (int i = 0; i < lineas.Length; i++)
            {
                contenidoConNumeros += $"{i + 1}{Environment.NewLine}";
            }

            // Asigna el texto al TextBox
            txtNumCodigo.Text = contenidoConNumeros;
        }

        private void ActualizarNumerosTokens()
        {
            // Obtén todas las líneas del RichTextBox
            string[] lineas2 = txtTokens.Lines;

            // Construye el texto para el TextBox con números de línea
            string contenidoConNumeros2 = "";
            for (int i = 0; i < lineas2.Length; i++)
            {
                contenidoConNumeros2 += $"{i + 1}{Environment.NewLine}";
            }

            // Asigna el texto al TextBox
            txtNumTokens.Text = contenidoConNumeros2;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            ActualizarNumerosYContenido();
        }

        private void GuardarArchivo()
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

        private void btnComprobarSintaxis_Click(object sender, EventArgs e)
        {
            try
            {
                string archivoTokens = txtTokens.Text;
                string tokens = System.Text.RegularExpressions.Regex.Replace(archivoTokens, @"\s+", " ");
                List<string> lista = GetTokens(tokens);

                if (TopDownParser.EvaluarSintaxis(lista) == true)
                    MessageBox.Show("Cadena aceptada");
                else
                    throw new Exception(TopDownParser.errorMessage);
                
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message,"ERROR", MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }

        private static List<string> GetTokens(string archivoTokens)
        {
            string cleanedTokens = Regex.Replace(archivoTokens, @"\s+", " ");
            return new List<string>(cleanedTokens.Split(' '));
        }
    }
}