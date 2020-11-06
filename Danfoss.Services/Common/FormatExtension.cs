using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Danfoss.Services.Common
{
    public static class FormatExtension
    {
        /// <summary>
        /// Убирает пробелы в начале и конце строки
        /// и заменяет одним пробелом повторяющиеся пробелы в строке.
        /// </summary>
        public static string FormatWhitespaces(this string str)
        {
            return $"{Regex.Replace(str.Trim(), @"\s+", " ")}";
        }
    }
}
