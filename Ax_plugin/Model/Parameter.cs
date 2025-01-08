
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace AxPlugin
{
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

        public double MinValue => _minValue;
        public double MaxValue => _maxValue;

        /// <summary>
        /// Конструктор, который принимает тип параметра и автоматически задает границы.
        /// </summary>
        /// <param name="type">Тип параметра.</param>
        /// <param name="value">Начальное значение.</param>
        public Parameter(ParamType type, double value)
        {
            SetBounds(type);
            Value = value;
        }

        /// <summary>
        /// Устанавливает границы параметра на основе типа.
        /// </summary>
        /// <param name="type">Тип параметра.</param>
        private void SetBounds(ParamType type)
        {
            switch (type)
            {
                case ParamType.LengthBlade:
                    _minValue = 100;
                    _maxValue = 300;
                    break;
                case ParamType.WidthButt:
                    _minValue = 80;
                    _maxValue = 150;
                    break;
                case ParamType.LengthHandle:
                    _minValue = 300;
                    _maxValue = 900;
                    break;
                case ParamType.LengthButt:
                    _minValue = 80;
                    _maxValue = 270;
                    break;
                case ParamType.WidthHandle:
                    _minValue = 20;
                    _maxValue = 60;
                    break;
                case ParamType.ThicknessButt:
                    _minValue = 24;
                    _maxValue = 72;
                    break;
                default:
                    throw new ArgumentException("Неизвестный тип параметра");
            }
        }

        /// <summary>
        /// Валидация значения.
        /// </summary>
        public void Validator()
        {
            if (Value < _minValue)
            {
                throw new ArgumentException($"Значение меньше минимального " +
                    $"допустимого значения ({_minValue} мм)");
            }
            else if (Value > _maxValue)
            {
                throw new ArgumentException($"Значение больше максимального " +
                    $"допустимого значения ({_maxValue} мм)");
            }
        }
    }

}
