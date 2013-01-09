namespace TsSoft.Commons.Text
{
    /// <summary>
    /// Выбирает правильную форму множественного числа для существительного
    /// </summary>
    public class Pluralizer
    {
        /// <summary>
        /// Выбирает правильную форму множественного числа для существительного
        /// </summary>
        /// <param name="amount">Количество</param>
        /// <param name="nounForms">3 формы существительного для количества: 1, 2, 5</param>
        /// <returns></returns>
        public static string Pluralize(int amount, string[] nounForms)
        {
            int lastDigit = amount % 10;
            int last2Digits = amount % 100;
            if (lastDigit == 1)
            {
                if (last2Digits != 11)
                {
                    return nounForms[0];
                }
            }
            else if (2 <= lastDigit && lastDigit <= 4)
            {
                if (last2Digits < 10 || last2Digits > 20)
                {
                    return nounForms[1];
                }
            }
            return nounForms[2];
        }
    }
}