
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace AxPlugin
{
    //TODO:XML
    public class Parameter
    {
        private double _maxValue;
        private double _minValue;
        private double _value;

        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                Validator();
            }
        }

        //TODO: refactor
        public double MinValue
        {
            get => _minValue;
            set => _minValue = value;
        }

        //TODO: refactor
        public double MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }


        /// <summary>
        /// Конструктор для инициализации параметра.
        /// </summary>
        /// <param name="minValue">Минимальное значение.</param>
        /// <param name="maxValue">Максимальное значение.</param>
        /// <param name="value">Начальное значение.</param>
        public Parameter(double minValue, double maxValue, double value)
        {
            _minValue = minValue;
            _maxValue = maxValue;

            // Устанавливаем значение через свойство, чтобы применялась валидация
            Value = value;
        }

        public Parameter()
        {
        }

        // Валидация для класса Parameter
        public virtual void Validator()
        {
            if (Value < _minValue)
            {
                throw new ArgumentException($"Значение меньше минимального допустимого значения ({_minValue} мм)");
            }
            else if (Value > _maxValue)
            {
                throw new ArgumentException($"Значение больше максимального допустимого значения ({_maxValue} мм)");
            }
        }
    }
    
}