using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    internal class Estado
    {
        private int _intNumero;
        private int _intSiguienteEstado;
        private string _strFDC;
        private string _strCategoria;

        public Estado(int se, string fdc = "", string c = "")
        {
            _intNumero = 0;
            _intSiguienteEstado = se;
            _strFDC = fdc;
            _strCategoria = c;
        }

        public int Numero { get { return _intNumero; } }
        public int SiguienteEstado { get { return _intSiguienteEstado; } }
        public string FDC { get { return _strFDC; } }
        public string Categoria { get { return _strCategoria; } }
    }
}
