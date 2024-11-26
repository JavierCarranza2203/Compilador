using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    internal class BuscadorColumnas
    {
        private Dictionary<string, string> BuscadorCaracteres = new Dictionary<string, string>();

        public BuscadorColumnas()
        {
            BuscadorCaracteres["_"] = "_";
            BuscadorCaracteres[" "] = "FDC";

            for (int i = 0; i < 10; i++)
            {
                BuscadorCaracteres[i.ToString()] = i.ToString();
            }

            // Agregar letras de la A a la Z con el formato "A1", "B1", ...
            for (char letra = 'A'; letra <= 'Z'; letra++)
            {
                BuscadorCaracteres[letra.ToString()] = letra + "1";
            }

            for (char letra = 'a'; letra <= 'z'; letra++)
            {
                BuscadorCaracteres[letra.ToString()] = letra.ToString();
            }

            // Listado de claves especiales
            string[] clavesEspeciales = new string[]
            {
                "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV",
                "BW", "BX", "BY", "BZ", "CA", "CB", "CC", "CD", "CE", "CF",
                "CG", "CH", "CI", "CJ", "CK", "CL", "CM"
            };

            // Valores correspondientes a las claves especiales
            string[] valoresEspeciales = new string[]
            {
                ".", "\"", ",", ";", ":", "&", "|", "!", "¡", "$", "#", "@",
                "%", "^", "¿", "?", "(", ")", "+", "-", "*", "/", "'", "=",
                "<", ">", "~"
            };

            // Agregar las claves y valores especiales al diccionario
            for (int i = 0; i < clavesEspeciales.Length; i++)
            {
                BuscadorCaracteres[valoresEspeciales[i]] = clavesEspeciales[i];
            }
        }

        public string ObtenerClave(string valor)
        {
            return this.BuscadorCaracteres.TryGetValue(valor, out string clave) ? clave : throw new Exception("El símbolo no se encuentra en el alfabeto");
        } 
    }
}
