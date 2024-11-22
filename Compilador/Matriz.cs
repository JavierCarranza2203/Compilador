using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    internal class Matriz
    {
        private MySqlConnection connection;
        private BuscadorColumnas buscadorColumnas;
        private string nombreDeTabla;

        public Matriz()
        {
            this.connection = new MySqlConnection("Server=localhost;Database=matriz_transicion;Uid=root;Pwd=;");
            this.buscadorColumnas = new BuscadorColumnas();
            this.nombreDeTabla = "estados1";
        }

        public Estado EvaluarCaracter(string caracter, int numeroEstado)
        {
            MySqlDataReader dr = this.ConsultarTabla("SELECT " + buscadorColumnas.ObtenerClave(caracter) + " FROM " + this.nombreDeTabla + " WHERE ESTADO = " + numeroEstado);

            this.ValidarDatosDB(dr.Read(), "Error al evaluar el caracter");

            Estado miEstado = new Estado(int.Parse(dr[buscadorColumnas.ObtenerClave(caracter)].ToString()));
            this.connection.Close();

            return miEstado;
        }

        public string ObtenerToken(int numeroEstado, string caracter)
        {
            MySqlDataReader dr = this.ConsultarTabla("SELECT FDC FROM " + this.nombreDeTabla + " WHERE ESTADO = " + numeroEstado);

            this.ValidarDatosDB(dr.Read(), "Error al leer el FDC, revise que sea un espacio en blanco");

            if (dr["FDC"].ToString() == "ACEPTAR" || dr["FDC"].ToString() == "ERROR")
            {
                this.connection.Close();

                string token = this.ObtenerCategoria(numeroEstado);
                this.connection.Close();

                return token;
            }
            else
            {
                int estadoDeError = int.Parse(dr["FDC"].ToString());
                this.connection.Close();
                string token = this.ObtenerCategoria(estadoDeError);
                this.connection.Close();

                return token;
            }
        }

        private string ObtenerCategoria(int numeroEstado)
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
    }
}
