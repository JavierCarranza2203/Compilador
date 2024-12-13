using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class TopDownParser
{
    // Gramática representada como un diccionario
    private static readonly Dictionary<string, List<string>> Gramaticas = new Dictionary<string, List<string>>
    {
        { "S", new List<string> { "COND" } },
        { "COND", new List<string> { "ARG OPRE ARG" } },
        { "ARG", new List<string> { "IDVAL", "CAD", "CONS" } },
        { "CONS", new List<string> { "CONSR", "CONSE", "CNEXP" } },
        { "OPRE", new List<string> { "OPRI", "ORMEN", "ORMAY", "ORMEI", "ORMAI", "ORDIF" } }
    };

    public static string errorMessage = string.Empty;

    // Método para evaluar la sintaxis
    public static bool EvaluarSintaxis(List<string> tokens)
    {
        errorMessage = string.Empty;
        return EvaluarRegla("S", tokens, out _);
    }

    // Evaluación recursiva de reglas según la gramática
    private static bool EvaluarRegla(string regla, List<string> tokens, out List<string> TokensRestantes)
    {
        TokensRestantes = new List<string>(tokens);

        if (!Gramaticas.ContainsKey(regla))
        {
            if (TokensRestantes.Count > 0 && (regla == "IDVAL" && Regex.IsMatch(TokensRestantes[0], "^IDVAL\\d+$") || TokensRestantes[0] == regla))
            {
                TokensRestantes.RemoveAt(0);
                return true;
            }

            if (regla == "IDVAL" || regla == "CAD" || regla == "CONSR" || regla == "CONSE" || regla == "CNEXP")
            {
                errorMessage = "Se esperaba un operando.";
            }
            else if(regla == "OPRI" || regla == "ORMEN"|| regla == "ORMAY"|| regla == "ORMEI"|| regla == "ORMAI"|| regla == "ORDIF")
            {
                errorMessage = "Se esperaba un operador.";
            }

            return false;
        }

        // Evaluar cada posible producción de la regla
        foreach (string produccion in Gramaticas[regla])
        {
            List<string> tokensTemp = new List<string>(TokensRestantes);
            string[] producciones = produccion.Split(' ');
            bool esValido = true;

            foreach (string simbolo in producciones)
            {
                if (!EvaluarRegla(simbolo, tokensTemp, out tokensTemp))
                {
                    esValido = false;
                    break;
                }
            }

            if (esValido)
            {
                TokensRestantes = tokensTemp;
                return true;
            }
        }

        if (regla == "IDVAL" || regla == "CAD" || regla == "CONSR" || regla == "CONSE" || regla == "CNEXP")
        {
            errorMessage = "Se esperaba un operando.";
        }
        else if (regla == "OPRI" || regla == "ORMEN" || regla == "ORMAY" || regla == "ORMEI" || regla == "ORMAI" || regla == "ORDIF")
        {
            errorMessage = "Se esperaba un operador.";
        }

        return false;
    }
}
