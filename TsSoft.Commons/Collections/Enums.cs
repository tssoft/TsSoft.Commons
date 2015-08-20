namespace TsSoft.Commons.Collections
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public static class Enums
    {
        /// <summary>
        /// Получает текстовое описание элемента перечисления
        /// <example> 
        /// <code>
        /// using TSSoftCommons.Text;
        /// 
        /// public enum Choice
        /// {
        ///     [Description("Да")]
        ///     Yes = 1,
        ///     [Description("Нет")]
        ///     No = -1,
        ///     [Description("Возможно")]
        ///     Maybe = 0
        /// }
        /// </code>
        /// </example>    
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes2 =
                (System.ComponentModel.DescriptionAttribute[])field.GetCustomAttributes(
                typeof(System.ComponentModel.DescriptionAttribute), false);
            return (attributes2 != null && attributes2.Length > 0)
                ? attributes2[0].Description : value.ToString();
        }

        public static E StringToEnum<E>(string value)
        {
            return (E)Enum.Parse(typeof(E), value, true);
        }

        /// <summary>
        /// Converts the string of the name or numeric value to an equivalent enumerated object. 
        /// If string cannot be converted, default value is returned
        /// </summary>
        public static E StringToEnum<E>(string value, E defaultValue, bool ignoreCase = true) where E : struct
        {
            E result;
            bool validValue = Enum.TryParse<E>(value, ignoreCase, out result);
            return validValue ? result : defaultValue;
        }

        public static E IntToEnum<E>(int value)
        {
            return (E)Enum.ToObject(typeof(E), value);
        }

        public static EnumValue[] GetValues<E>()
        {
            return Enum.GetValues(typeof(E)).Cast<Enum>().Select(x => new EnumValue(x)).ToArray();
        }
    }
}
