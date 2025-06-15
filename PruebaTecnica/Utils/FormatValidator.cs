using System.Text.RegularExpressions;

namespace PruebaTecnica.Utils
{
    public static class FormatValidator
    {
        private static string ID_FORMAT = "^[0-9]{1,10}$";

        public static bool ValidateId(string id)
        {
            if (string.IsNullOrEmpty(id) || id == "0")
            {
                return false;
            }

            var esValido = Regex.IsMatch(id, ID_FORMAT);
            return esValido;
        }
    }
}
