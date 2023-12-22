using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM
{
    public static class WalidacjaDanych
    {
        public static bool CzyUzupelnione(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return true;
            }
                return false;
        }
    }
}
