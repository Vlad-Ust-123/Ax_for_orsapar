using NUnit.Framework;
using System;
using System.Collections.Generic;
using AxPlugin;

namespace ModelTest.AxParameterTest
{
    [TestFixture]
    public class AxParametersTests
    {
        [Test]
        public void AllParameters_Get_ReturnsNotNullDictionary()
        {
            var axParameters = new AxParameters();
            Assert.IsNotNull(axParameters.AllParameters);
        }

        [Test]
        public void AllParameters_Set_ValidDictionary_SetsCorrectly()
        {
            var axParameters = new AxParameters();
            var expected = new Dictionary<ParamType, Parameter>
            {
                { ParamType.LengthBlade, new Parameter(ParamType.LengthBlade, 200) },
                { ParamType.LengthHandle, new Parameter(ParamType.LengthHandle, 500) }
            };
            axParameters.AllParameters = expected;
            Assert.AreSame(expected, axParameters.AllParameters);
        }

        [Test]
        public void AllParameters_Set_NullValue_ThrowsArgumentNullException()
        {
            var axParameters = new AxParameters();
            Assert.Throws<ArgumentNullException>(() => axParameters.AllParameters = null);
        }

        [Test]
        public void SetParameter_ValidParameter_SetsCorrectly()
        {
            var axParameters = new AxParameters();
            var paramType = ParamType.LengthBlade;
            var parameter = new Parameter(ParamType.LengthBlade, 150);

            axParameters.SetParameter(paramType, parameter);
            Assert.IsTrue(axParameters.AllParameters.ContainsKey(paramType));
            Assert.That(axParameters.AllParameters[paramType].Value, Is.EqualTo(150));
        }

        [Test]
        public void SetParameter_NullParameter_ThrowsArgumentNullException()
        {
            var axParameters = new AxParameters();
            var paramType = ParamType.LengthBlade;
            Assert.Throws<ArgumentNullException>(() => axParameters.SetParameter(paramType, null));
        }

        [Test]
        public void SetParameter_ValidDependentValue_DoesNotThrowException()
        {
            var axParameters = new AxParameters();

            // Устанавливаем базовый параметр
            var lengthBlade = new Parameter(ParamType.LengthBlade, 200);
            axParameters.SetParameter(ParamType.LengthBlade, lengthBlade);

            // Устанавливаем корректный зависимый параметр
            var validWidthHandle = new Parameter(ParamType.WidthHandle, 40);

            Assert.DoesNotThrow(() => axParameters.SetParameter(ParamType.WidthHandle,
                validWidthHandle));
        }
    }
}
