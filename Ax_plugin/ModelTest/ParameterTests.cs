using System;
using AxPlugin;
using NUnit.Framework;

namespace UnitTest.ParameterTest
{
    /// <summary>
    /// Набор тестов для класса Parameter.
    /// </summary>
    [TestFixture]
    public class ParameterTests
    {
        /// <summary>
        /// Проверяет, что конструктор Parameter инициализирует объект корректно.
        /// </summary>
        [Test]
        public void Parameter_Constructor_InitializesCorrectly()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.AreEqual(100, parameter.MinValue);
            Assert.AreEqual(300, parameter.MaxValue);
            Assert.AreEqual(200, parameter.Value);
        }

        /// <summary>
        /// Проверяет, что свойство MinValue возвращает корректное значение.
        /// </summary>
        [Test]
        public void MinValue_Get_ReturnsCorrectValue()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.AreEqual(100, parameter.MinValue);
        }

        /// <summary>
        /// Проверяет, что свойство MaxValue возвращает корректное значение.
        /// </summary>
        [Test]
        public void MaxValue_Get_ReturnsCorrectValue()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.AreEqual(300, parameter.MaxValue);
        }

        /// <summary>
        /// Проверяет, что свойство Value возвращает корректное значение.
        /// </summary>
        [Test]
        public void Value_Get_ReturnsCorrectValue()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.AreEqual(200, parameter.Value);
        }

        /// <summary>
        /// Проверяет, что свойство Value можно корректно установить на валидное значение.
        /// </summary>
        [Test]
        public void Value_Set_ValidValue_SetsCorrectly()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            parameter.Value = 250;
            Assert.AreEqual(250, parameter.Value);
        }

        /// <summary>
        /// Проверяет, что установка свойства Value на значение меньше минимального 
        /// допустимого значения вызывает ArgumentException.
        /// </summary>
        [Test]
        public void Value_Set_LessThanMinValue_ThrowsException()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.Throws<ArgumentException>(() => parameter.Value = 50);
        }

        /// <summary>
        /// Проверяет, что установка свойства Value на значение больше максимального 
        /// допустимого значения вызывает ArgumentException.
        /// </summary>
        [Test]
        public void Value_Set_GreaterThanMaxValue_ThrowsException()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.Throws<ArgumentException>(() => parameter.Value = 350);
        }
    }
}
