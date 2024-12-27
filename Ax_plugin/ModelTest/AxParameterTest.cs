using NUnit.Framework;
using System;
using System.Collections.Generic;
using AxPlugin;

namespace ModelTest.AxParameterTest
{
    /// <summary>
    /// Класс для тестирования функционала класса AxParameters.
    /// </summary>
    [TestFixture]
    public class AxParametersTests
    {
        /// <summary>
        /// Тест проверяет, что после инициализации коллекция параметров не равна null.
        /// </summary>
        [Test]
        public void AllParameters_Get_ReturnsNotNullDictionary()
        {
            var axParameters = new AxParameters();
            Assert.IsNotNull(axParameters.AllParameters);
        }

        /// <summary>
        /// Тест проверяет, что свойство AllParameters корректно устанавливает словарь параметров.
        /// </summary>
        [Test]
        public void AllParameters_Set_ValidDictionary_SetsCorrectly()
        {
            var axParameters = new AxParameters();
            var expected = new Dictionary<ParamType, Parameter>
            {
                { ParamType.LengthBlade, new Parameter() },
                { ParamType.LengthHandle, new Parameter() }
            };
            axParameters.AllParameters = expected;
            Assert.AreSame(expected, axParameters.AllParameters);
        }

        /// <summary>
        /// Тест проверяет, что при попытке присвоить null в AllParameters выбрасывается исключение ArgumentNullException.
        /// </summary>
        [Test]
        public void AllParameters_Set_NullValue_ThrowsArgumentNullException()
        {
            var axParameters = new AxParameters();
            Assert.Throws<ArgumentNullException>(() => axParameters.AllParameters = null);
        }

        /// <summary>
        /// Тест проверяет, что метод SetParameter корректно устанавливает значение параметра.
        /// </summary>
        [Test]
        public void SetParameter_ValidParameter_SetsCorrectly()
        {
            var axParameters = new AxParameters();
            var paramType = ParamType.LengthBlade;
            var parameter = new Parameter { MinValue = 100, MaxValue = 300, Value = 150 };

            axParameters.SetParameter(paramType, parameter);
            Assert.IsTrue(axParameters.AllParameters.ContainsKey(paramType));
            Assert.That(axParameters.AllParameters[paramType].Value, Is.EqualTo(150));
        }

        /// <summary>
        /// Тест проверяет, что метод SetParameter выбрасывает исключение ArgumentNullException, если передан null.
        /// </summary>
        [Test]
        public void SetParameter_NullParameter_ThrowsArgumentNullException()
        {
            var axParameters = new AxParameters();
            var paramType = ParamType.LengthBlade;
            Assert.Throws<ArgumentNullException>(() => axParameters.SetParameter(paramType, null));
        }

        /// <summary>
        /// Тест проверяет, что зависимый параметр с корректным значением устанавливается без ошибок.
        /// </summary>
        [Test]
        public void SetParameter_ValidDependentValue_DoesNotThrowException()
        {
            var axParameters = new AxParameters();

            // Устанавливаем базовый параметр
            var lengthBlade = new Parameter { MinValue = 100, MaxValue = 300, Value = 200 };
            axParameters.SetParameter(ParamType.LengthBlade, lengthBlade);

            // Устанавливаем корректный зависимый параметр
            var validWidthHandle = new Parameter { MinValue = 20, MaxValue = 60, Value = 40 };

            Assert.DoesNotThrow(() => axParameters.SetParameter(ParamType.WidthHandle, validWidthHandle));
        }
    }
}
