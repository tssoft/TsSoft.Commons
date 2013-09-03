using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TsSoft.Commons.Text
{
    [TestClass]
    public class FormatterTest
    {
        [TestMethod()]
        public void TestFormatFileSize()
        {
            Assert.AreEqual("1024,00 байт", Formatter.FormatFileSize(1024));
            Assert.AreEqual("9,8 КБ", Formatter.FormatFileSize(10000, 1));
            Assert.AreEqual("1024,0 КБ", Formatter.FormatFileSize(1048576, 1));
            Assert.AreEqual("пустой", Formatter.FormatFileSize(-1048576, 1));
            Assert.AreEqual("пустой", Formatter.FormatFileSize(0));
            Assert.AreEqual("1024 КБ", Formatter.FormatFileSize(1048576, 0));
            Assert.AreEqual("1024,00 КБ", Formatter.FormatFileSize(1048576, -1));
        }

        [TestMethod()]
        public void ShortenByWordTest()
        {
            Assert.AreEqual(null, Formatter.ShortenTextByWord(null, 50));
            Assert.AreEqual(string.Empty, Formatter.ShortenTextByWord(string.Empty, 50));
            var text = "Этикет, как бы это ни казалось парадоксальным";
            Assert.AreEqual(text, Formatter.ShortenTextByWord(text, 50));
            text = @"Этикет, как бы это ни казалось парадоксальным, диссонирует горизонт ожидания,
                    таким образом, все перечисленные признаки архетипа и мифа подтверждают,
                    что действие механизмов мифотворчества сродни механизмам художественно-продуктивного
                    мышления. Художественное восприятие, следовательно, мгновенно. Выявляя устойчивые
                    архетипы на примере художественного творчества, можно сказать, что стиль дает гений,
                    что-то подобное можно встретить в работах Ауэрбаха и Тандлера. Возрождение иллюстрирует
                    фактографический филогенез, таким образом, второй комплекс движущих сил получил
                    разработку в трудах А.Берталанфи и Ш.Бюлера.";
            Assert.AreEqual("Этикет, как бы это ни казалось парадоксальным, диссонирует горизонт…", Formatter.ShortenTextByWord(text, 70));
            Assert.AreEqual("Этике…", Formatter.ShortenTextByWord(text, 5));
        }

        [TestMethod]
        public void TestShortenUrlByLength()
        {
            Assert.AreEqual(null, Formatter.ShortenUrlByLength(null));
            Assert.AreEqual(string.Empty, Formatter.ShortenUrlByLength(string.Empty));
            Assert.AreEqual("ya.ru", Formatter.ShortenUrlByLength("ya.ru"));
            Assert.AreEqual("ya.ru", Formatter.ShortenUrlByLength("ya.ru/"));
            Assert.AreEqual("www.ya.ru", Formatter.ShortenUrlByLength("www.ya.ru"));
            Assert.AreEqual("www.dailyhaha.c…", Formatter.ShortenUrlByLength("http://www.dailyhaha.com/_pics/care_o_meter.jpg"));
            Assert.AreEqual("www.dailyhaha.c…", Formatter.ShortenUrlByLength("www.dailyhaha.com/_pics/care_o_meter.jpg"));
            Assert.AreEqual("www.dailyhaha.co…", Formatter.ShortenUrlByLength("http://www.dailyhaha.com/_pics/care_o_meter.jpg", 16));
            Assert.AreEqual("www.dailyhaha.co…", Formatter.ShortenUrlByLength("www.dailyhaha.com/_pics/care_o_meter.jpg", 16));
        }
    }
}