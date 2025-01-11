﻿using System;
using AxPlugin;
using NUnit.Framework;

namespace UnitTest.ParameterTest
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void Parameter_Constructor_InitializesCorrectly()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.AreEqual(100, parameter.MinValue);
            Assert.AreEqual(300, parameter.MaxValue);
            Assert.AreEqual(200, parameter.Value);
        }

        [Test]
        public void MinValue_Get_ReturnsCorrectValue()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.AreEqual(100, parameter.MinValue);
        }

        [Test]
        public void MaxValue_Get_ReturnsCorrectValue()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.AreEqual(300, parameter.MaxValue);
        }

        [Test]
        public void Value_Get_ReturnsCorrectValue()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.AreEqual(200, parameter.Value);
        }

        [Test]
        public void Value_Set_ValidValue_SetsCorrectly()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            parameter.Value = 250;
            Assert.AreEqual(250, parameter.Value);
        }

        [Test]
        public void Value_Set_LessThanMinValue_ThrowsException()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.Throws<ArgumentException>(() => parameter.Value = 50);
        }

        [Test]
        public void Value_Set_GreaterThanMaxValue_ThrowsException()
        {
            var parameter = new Parameter(ParamType.LengthBlade, 200);
            Assert.Throws<ArgumentException>(() => parameter.Value = 350);
        }
    }
}