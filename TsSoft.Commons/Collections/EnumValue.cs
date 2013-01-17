namespace TsSoft.Commons.Collections
{
    using System;

    /// <summary>
    /// <example> 
    /// <code>
    /// using System.ComponentModel;
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
    /// 
    /// public void Demo()
    /// {
    ///     EnumValue enumValue = new EnumValue(Choice.Maybe);
    ///     var value = enumValue.Value;            // => Choice.Maybe
    ///     string Name = enumValue.Name;           // => "Maybe"
    ///     int intValue = enumValue.Ordinal;       // => 0
    ///     string text = enumValue.Description;    // => "Возможно"
    /// }
    /// </code>
    /// </example>    
    /// </summary>
    public class EnumValue
    {
        public Enum Value { get; private set; }
        public int Ordinal { get { return Convert.ToInt32(Value); } }
        public string Name { get { return ToString(); } }
        public string Description { get { return Enums.GetEnumDescription(Value); } }

        public EnumValue(Enum value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
