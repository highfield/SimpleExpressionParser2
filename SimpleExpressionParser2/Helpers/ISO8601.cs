using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Cet.Core
{
    /// <summary>
    /// Funzioni di utilità relative alle specifiche ISO-8601
    /// </summary>
    public static class ISO8601
    {

        /// <summary>
        /// Converte un valore <see cref="TimeSpan"/> in una stringa
        /// secondo il formato standard ISO-8601
        /// </summary>
        /// <param name="ts">Il valore <see cref="TimeSpan"/> da convertire.</param>
        /// <returns>La stringa che rappresenta il valore indicato.</returns>
        public static string TimeSpanToString(TimeSpan ts) => XmlConvert.ToString(ts);


        /// <summary>
        /// Converte una stringa in un valore <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="s">La stringa da convertire.</param>
        /// <returns>Il valore <see cref="TimeSpan"/> convertito.</returns>
        public static TimeSpan StringToTimeSpan(string s) => XmlConvert.ToTimeSpan(s);


        /// <summary>
        /// Tenta la conversione di una stringa in un valore <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="s">La stringa da convertire.</param>
        /// <param name="result">Il valore <see cref="TimeSpan"/> convertito
        /// o il default se la conversione non è stata possibile.</param>
        /// <returns>Indica se la conversione è stata possibile.</returns>
        /// <seealso cref="https://referencesource.microsoft.com/#system.xml/System/Xml/Schema/XsdDuration.cs,448"/>
        public static bool TryStringToTimeSpan(string s, out TimeSpan result)
        {
            result = default(TimeSpan);
            if (s == null) return false;
            int i = -1, len = s.Length;
            while (++i < len && char.IsWhiteSpace(s[i])) ;
            if (i >= len) return false;
            if (s[i] == '-') i++;
            if (i >= len) return false;
            if (s[i] != 'P') return false;
            try
            {
                result = XmlConvert.ToTimeSpan(s);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Converte un valore <see cref="DateTime"/> in una stringa
        /// secondo il formato standard ISO-8601
        /// </summary>
        /// <param name="dt">Il valore <see cref="DateTime"/> da convertire.</param>
        /// <returns>La stringa che rappresenta il valore indicato.</returns>
        public static string DateTimeToString(DateTime dt) => dt.ToString("o", CultureInfo.InvariantCulture);


        /// <summary>
        /// Converte una stringa in un valore <see cref="DateTime"/>
        /// </summary>
        /// <param name="s">La stringa da convertire.</param>
        /// <returns>Il valore <see cref="DateTime"/> convertito.</returns>
        public static DateTime StringToDateTime(string s) => DateTime.Parse(
            s,
            CultureInfo.InvariantCulture,
            DateTimeStyles.RoundtripKind
            );


        /// <summary>
        /// Tenta la conversione di una stringa in un valore <see cref="DateTime"/>
        /// </summary>
        /// <param name="s">La stringa da convertire.</param>
        /// <param name="result">Il valore <see cref="DateTime"/> convertito
        /// o il default se la conversione non è stata possibile.</param>
        /// <returns>Indica se la conversione è stata possibile.</returns>
        public static bool TryStringToDateTime(string s, out DateTime result)
        {
            return DateTime.TryParse(
                s,
                CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind,
                out result
                );
        }


        /// <summary>
        /// Converte un valore <see cref="DateTimeOffset"/> in una stringa
        /// secondo il formato standard ISO-8601
        /// </summary>
        /// <param name="dto">Il valore <see cref="DateTimeOffset"/> da convertire.</param>
        /// <returns>La stringa che rappresenta il valore indicato.</returns>
        /// <remarks>
        /// E' utile ricordare che la struttura <see cref="DateTimeOffset"/> offre
        /// il cast implicito alla struttura <see cref="DateTime"/>
        /// </remarks>
        public static string DateTimeOffsetToString(DateTimeOffset dto)
        {
            return dto.ToString("o", CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// Converte una stringa in un valore <see cref="DateTimeOffset"/>
        /// </summary>
        /// <param name="s">La stringa da convertire.</param>
        /// <returns>Il valore <see cref="DateTimeOffset"/> convertito.</returns>
        /// <remarks>
        /// La funzione, avvalendosi del metodo nativo <see cref="DateTimeOffset.Parse(string, IFormatProvider)"/>,
        /// in realtà accetta anche formati diversi da quello standard ISO-8601
        /// </remarks>
        public static DateTimeOffset StringToDateTimeOffset(string s)
        {
            return DateTimeOffset.Parse(
                s,
                CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind
                );
        }


        /// <summary>
        /// Tenta la conversione di una stringa in un valore <see cref="DateTimeOffset"/>
        /// </summary>
        /// <param name="s">La stringa da convertire.</param>
        /// <param name="result">Il valore <see cref="DateTimeOffset"/> convertito
        /// o il default se la conversione non è stata possibile.</param>
        /// <returns>Indica se la conversione è stata possibile.</returns>
        public static bool TryStringToDateTimeOffset(string s, out DateTimeOffset result)
        {
            return DateTimeOffset.TryParse(
                s,
                CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind,
                out result
                );
        }

    }
}
