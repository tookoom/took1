using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Data.Converter
{
	/// <summary>
	/// Conversor de strings para outros tipos de valores
	/// </summary>
    public class StringConverter
    {
        /// <summary>
        /// Converte string para valor booleano, retornando "false" caso conversão não seja possível.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        public static bool ToBool(string value)
        {
            return ToBool(value, false);
        }
        /// <summary>
        /// Converte string para valor booleano, retornando valor default passado por
        /// parâmetro caso conversão não seja possível.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <param name="defaultValue">Valor a ser retornado caso conversão não seja possível</param>
        public static bool ToBool(string value, bool defaultValue)
        {
            bool result = defaultValue;
            if (value != null)
            {
                if (!bool.TryParse(value, out result))
                    result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Converte string para valor inteiro, retornando 0 (zero) caso conversão não seja possível.
        /// </summary>
        /// <param name="value"></param>
        public static int ToInt(string value)
        {
            return ToInt(value, 0);
        }
        /// <summary>
        /// Converte string para valor inteiro, retornando valor default passado por
        /// parâmetro caso conversão não seja possível.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <param name="defaultValue">Valor a ser retornado caso conversão não seja possível</param>
        public static int ToInt(string value, int defaultValue)
        {
            int result = defaultValue;
            if (value != null)
            {
                if (!int.TryParse(value, out result))
                    result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Converte string para DateTime, retornando DateTime minimo representável caso conversão não seja possível.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        public static DateTime ToDateTime(string value)
        {
            return ToDateTime(value, DateTime.MinValue); ;
        }
        /// <summary>
        /// Converte string para valor DateTime, retornando valor default passado por
        /// parâmetro caso conversão não seja possível.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <param name="defaultValue">Valor a ser retornado caso conversão não seja possível</param>
        public static DateTime ToDateTime(string value, DateTime defaultValue)
        {
            DateTime result = defaultValue;
            if (value != null)
            {
                if (!DateTime.TryParse(value, out result))
                    result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Converte string para valor double, retornando 0(zero) caso conversão não seja possível.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        public static double ToDouble(string value)
        {
            return ToDouble(value, 0);
        }
        /// <summary>
        /// Converte string para valor double, retornando valor default passado por
        /// parâmetro caso conversão não seja possível.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <param name="defaultValue">Valor a ser retornado caso conversão não seja possível</param>
        public static double ToDouble(string value, double defaultValue)
        {
            double result = defaultValue;
            if (value != null)
            {
                if (!double.TryParse(value, out result))
                    result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Converte string para valor float, retornando 0(zero) caso conversão não seja possível.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        public static float ToFloat(string value)
        {
            return ToFloat(value, 0);
        }
        /// <summary>
        /// Converte string para valor float, retornando valor default passado por
        /// parâmetro caso conversão não seja possível.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <param name="defaultValue">Valor a ser retornado caso conversão não seja possível</param>
        public static float ToFloat(string value, float defaultValue)
        {
            float result = defaultValue;
            if (value != null)
            {
                if (!float.TryParse(value, out result))
                    result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Converte string para valor string, retornando valor default passado por
        /// parâmetro valor a ser convertido seja nulo.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <param name="defaultValue">Valor a ser retornado caso conversão não seja possível</param>
        public static string ToString(string value, string defaultValue)
        {
            string result = defaultValue;
            if (value != null)
            {
                result = value;
            }
            return result;
        }
    }
}
