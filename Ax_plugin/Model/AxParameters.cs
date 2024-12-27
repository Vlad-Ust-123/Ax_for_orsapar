﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AxPlugin
{
    /// <summary>
    /// Класс для работы с параметрами модели топора.
    /// </summary>
    public class AxParameters
    {
        /// <summary>
        /// Словарь, содержащий параметры модели.
        /// </summary>
        private Dictionary<ParamType, Parameter> _axParameters = new Dictionary<ParamType, Parameter>();

        /// <summary>
        /// Свойство для доступа ко всем параметрам модели.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если значение устанавливается в null.
        /// </exception>
        public Dictionary<ParamType, Parameter> AllParameters
        {
            get => _axParameters;
            set => _axParameters = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Устанавливает новый параметр в словарь.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="parameter">Значение параметра.</param>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если параметр равен null.
        /// </exception>
        public void SetParameter(ParamType parameterType, Parameter parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter), "Параметр не может быть null.");
            }

            _axParameters[parameterType] = parameter;
            ValidateParameters(parameterType, parameter);
        }

        /// <summary>
        /// Валидирует зависимости заданного параметра.
        /// </summary>
        /// <param name="parameterType">Тип базового параметра.</param>
        /// <param name="parameter">Базовый параметр.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если один из зависимых параметров не соответствует ограничениям.
        /// </exception>
        private void ValidateParameters(ParamType parameterType, Parameter parameter)
        {
            var exceptions = new List<string>();

            if (parameterType == ParamType.LengthBlade)
            {
                //TODO: RSDN
                ValidateDependentParameter(ParamType.LengthHandle, parameter, 3, 1, exceptions, "Длина ручки");
                ValidateDependentParameter(ParamType.LengthButt, parameter, 0.8, 2, exceptions, "Длина обуха");
                ValidateDependentParameter(ParamType.WidthHandle, parameter, 0.2, 4, exceptions, "Ширина рукояти");
            }
            else if (parameterType == ParamType.LengthButt)
            {
                //TODO: RSDN
                ValidateDependentParameter(ParamType.ThicknessButt, parameter, 0.3, 4, exceptions, "Ширина топорища");
            }

            if (exceptions.Any())
            {
                throw new ArgumentException(string.Join(Environment.NewLine, exceptions));
            }
        }

        /// <summary>
        /// Валидирует зависимый параметр на основе базового параметра.
        /// </summary>
        /// <param name="dependentType">Тип зависимого параметра.</param>
        /// <param name="baseParameter">Базовый параметр.</param>
        /// <param name="multiplier">Коэффициент для расчета ожидаемого значения.</param>
        /// <param name="tolerance">Допустимое отклонение.</param>
        /// <param name="exceptions">Список исключений для накопления ошибок.</param>
        /// <param name="parameterName">Имя проверяемого параметра.</param>
        private void ValidateDependentParameter(
            ParamType dependentType,
            Parameter baseParameter,
            double multiplier,
            double tolerance,
            List<string> exceptions,
            string parameterName)
        {
            if (_axParameters.TryGetValue(dependentType, out var dependentParameter))
            {
                double expectedMin = (baseParameter.Value - tolerance) * multiplier;
                double expectedMax = (baseParameter.Value + tolerance) * multiplier;

                if (dependentParameter.Value < expectedMin)
                {
                    //TODO: RSDN
                    exceptions.Add($"{parameterName} меньше минимального допустимого значения ({expectedMin} мм).");
                }
                else if (dependentParameter.Value > expectedMax)
                {
                    //TODO: RSDN
                    exceptions.Add($"{parameterName} больше максимального допустимого значения ({expectedMax} мм).");
                }
            }
        }
    }

}
