using System;
using AxPlugin;
using NUnit.Framework;

namespace ModelTest.UnitTestParameter
{
    /// <summary>
    /// Класс Unit тестов класса <see cref="Parameter"/>.
    /// </summary>
    [TestFixture]
    public class ParameterTest
    {
        /// <summary>
        /// Тестовый параметр.
        /// </summary>
        private Parameter _parameter = new Parameter();

        /// <summary>
        /// Позитивный тест геттера MaxValue.
        /// </summary>
        [Test(Description = "Позитивный тест геттера MaxValue.")]
        public void TestProjectGetMaxValue()
        {
            var expected = 300;
            this._parameter.MaxValue = 300;
            var actual = this._parameter.MaxValue;
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Позитивный тест сеттера MaxValue.
        /// </summary>
        [Test(Description = "Позитивный тест сеттера MaxValue.")]
        public void TestProjectSetMaxValue()
        {
            Parameter expected = new Parameter();
            this._parameter.MaxValue = 300;
            expected.MaxValue = 300;
            var actual = this._parameter;
            Assert.AreEqual(expected.MaxValue, actual.MaxValue);
        }

        /// <summary>
        /// Позитивный тест геттера MinValue.
        /// </summary>
        [Test(Description = "Позитивный тест геттера MinValue.")]
        public void TestProjectGetMinValue()
        {
            var expected = 100;
            this._parameter.MinValue = 100;
            var actual = this._parameter.MinValue;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Позитивный тест сеттера MinValue.
        /// </summary>
        [Test(Description = "Позитивный тест сеттера MinValue.")]
        public void TestProjectSetMinValue()
        {
            Parameter expected = new Parameter();
            this._parameter.MinValue = 100;
            expected.MinValue = 100;
            var actual = this._parameter;
            Assert.AreEqual(expected.MinValue, actual.MinValue);
        }

        /// <summary>
        /// Позитивный тест геттера Value.
        /// </summary>
        [Test(Description = "Позитивный тест геттера Value.")]
        public void TestProjectGetValue()
        {
            var expected = 150;
            this._parameter.MinValue = 100;
            this._parameter.MaxValue = 300;
            this._parameter.Value = 150;
            var actual = this._parameter.Value;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Позитивный тест сеттера Value.
        /// </summary>
        [Test(Description = "Позитивный тест сеттера Value.")]
        public void TestProjectSetValue()
        {
            Parameter expected = new Parameter();
            this._parameter.MinValue = 100;
            this._parameter.MaxValue = 300;
            this._parameter.Value = 150;
            expected.MinValue = 100;
            expected.MaxValue = 300;
            expected.Value = 150;
            var actual = this._parameter.Value;
            Assert.AreEqual(expected.Value, actual);
        }

        /// <summary>
        /// TestCase методов проверки сеттера свойства Value.
        /// </summary>
        /// <param name="wrongValue">Неверное поле текст.</param>
        /// <param name="message">Текст ошибки.</param>
        [TestCase(
            90,
            "Должно возникать исключение, если значение меньше MinValue",
            TestName = "Простая ошибка")]
        [TestCase(
            310,
            "Должно возникать исключение, если значение больше MaxValue",
            TestName = "Простая ошибка")]
        public void TestSetArgumentException(int wrongValue, string message)
        {
            this._parameter.MaxValue = 300;
            this._parameter.MinValue = 100;
            Assert.Throws<ArgumentException>(
            () => { this._parameter.Value = wrongValue; },
            message);
        }

    }
}
