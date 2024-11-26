using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    internal class Token
    {
        public string Palabra;
        public bool EsError;
        public string Descripcion;
        public string Id;

        public Token(string palabra, bool esError, string descripcion, string id)
        {
            this.Descripcion = descripcion;
            this.Palabra = palabra;
            this.EsError = esError;
            this.Id = id;
        }

    }
}
