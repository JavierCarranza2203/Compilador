using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    internal class Matriz
    {
        private readonly MySqlConnection connection;
        private readonly BuscadorColumnas buscadorColumnas;
        private readonly string nombreDeTabla;

        public Matriz()
        {
            this.connection = new MySqlConnection("Server=localhost;Database=matriz_transicion;Uid=root;Pwd=;");
            this.buscadorColumnas = new BuscadorColumnas();
            this.nombreDeTabla = "estados1";
        }

        public string EvaluarCaracter(string caracter, string numeroEstado)
        {
            MySqlDataReader dr = this.ConsultarTabla("SELECT " + this.nombreDeTabla + "." + buscadorColumnas.ObtenerClave(caracter) + " FROM " + this.nombreDeTabla + " WHERE ESTADO = " + numeroEstado);
            this.ValidarDatosDB(dr.Read(), "Error al evaluar el caracter");

            string siguienteEstado = dr[buscadorColumnas.ObtenerClave(caracter)].ToString();
            this.connection.Close();

            return siguienteEstado;
        }

        public Token ObtenerToken(string estadoAnterior, string estadoSiguiente, bool finDeCadenaIncorrecto, string palabra)
        {
            string estadoObjetivo = this.ValidarEstadoSiguiente(estadoSiguiente)? estadoAnterior : this.EvaluarCaracter(" ", estadoSiguiente);

            if (estadoObjetivo == "ERROR") estadoObjetivo = estadoSiguiente;

            string token = this.ObtenerCategoria(estadoObjetivo);

            if (finDeCadenaIncorrecto) token = "FDCE";
            this.connection.Close();

            MySqlDataReader dr = this.ConsultarTabla("SELECT * FROM tokens WHERE ID = '" + token + "'");

            this.ValidarDatosDB(dr.Read(), "Error al obtener el token");

            Token miToken = new Token(palabra, dr["CATEGORIA"].ToString() == "ERROR", dr["DESCRIPCION"].ToString(), dr["ID"].ToString());

            this.connection.Close();

            return miToken;
        }

        private string ObtenerCategoria(string numeroEstado)
        {
            MySqlDataReader dr = this.ConsultarTabla("SELECT CATEGORIA FROM " + this.nombreDeTabla + " WHERE ESTADO = " + numeroEstado);

            this.ValidarDatosDB(dr.Read(), "Error al obtener la categoría");

            return dr["CATEGORIA"].ToString();
        }

        private void ValidarDatosDB(bool valor, string mensajeDeError)
        {
            if (valor == false)
            {
                this.connection.Close();
                throw new Exception(mensajeDeError);
            }
        }

        private MySqlDataReader ConsultarTabla(string consulta)
        {
            MySqlCommand cmd = new MySqlCommand(consulta, this.connection);

            this.connection.Open();

            return cmd.ExecuteReader();
        }

        public bool ValidarEstadoSiguiente(string estadoSiguiente)
        {
            return (estadoSiguiente == "ACEPTAR" || estadoSiguiente == "ERROR");
        }
    }
}
