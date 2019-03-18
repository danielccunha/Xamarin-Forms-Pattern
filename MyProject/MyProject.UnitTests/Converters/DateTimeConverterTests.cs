using MyProject.Converters;
using MyProject.UnitTests.Base;
using NUnit.Framework;
using System;
using System.Globalization;

namespace MyProject.UnitTests.Converters
{
    public class DateTimeConverterTests : BaseTestFixture
    {
        private DateTimeConverter Converter { get; set; }

        public override void BeforeEachTest()
        {
            Converter = new DateTimeConverter();
        }

        [Test]
        public void Convert_ValueIsDateTime_ReturnsConvertedDateTime()
        {
            var date = DateTime.Now;

            var result = Converter.Convert(date, null, null, null);

            Assert.That(result.ToString(), Is.EqualTo(date.ToString("dd/MM/yyyy")));
        }

        [Test]
        public void Convert_ValueIsNull_ReturnsNull()
        {
            var result = Converter.Convert(null, null, null, null);

            Assert.That(result, Is.Null);
        }


        [Test]
        public void ConvertBack_WhenCalled_ThrowNotImplementedException()
        {
            Assert.That(() => Converter.ConvertBack(null, null, null, null), Throws.TypeOf<NotImplementedException>());
        }
    }
}
