using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Perfomance;

namespace AxPlugin
{
    /// <summary>
    /// Класс MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        
        /// <summary>
        /// Поле хранящее в себе объект класса Builder.
        /// </summary>
        private Builder _builder = new Builder();

        /// <summary>
        /// Поле хранящее в себе объект класса Parameters.
        /// </summary>
        private AxParameters _parameters = new AxParameters();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            //StressTester stress = new StressTester();
            //stress.StressTesting();
        }

        private static bool _isCrossValidating = false;
        private bool _isValidatingDependencies = false;

        /// <summary>
        /// Инициализация ряда параметров при загрузке формы.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this._parameters.AllParameters = new Dictionary<ParamType, Parameter>();

            this.toolTip1.SetToolTip(
                this.LenghtBladeTextBox,
                "Длина лезвия должна находиться в диапазоне от 100 до 300 мм");
            this.toolTip1.SetToolTip(
                this.textBoxWidthButt,
                "Длинна топорища должена находиться в диапазоне от 80 до 150 мм");
            this.toolTip1.SetToolTip(
                this.TextBoxLengthHandle,
                "Длина ручки топора должна быть в 3 раза больше длинны Лезвия");
            this.toolTip1.SetToolTip(
                this.textBoxLenghtButt,
                "Длинна обуха топора должна состовлять 80% от длинны лезвия топора ");
            this.toolTip1.SetToolTip(
                this.textBoxWidthHandle,
                "Ширина рукояти топора должна состовлять 20% от длинны лезвия топора ");
            this.toolTip1.SetToolTip(
                this.textBoxThicknessButt,
                "Ширина топорища топора должна состовлять 30% от длинны лезвия топора ");
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Создать".
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            if (this.LenghtBladeTextBox.BackColor == Color.Red ||
                this.textBoxWidthButt.BackColor == Color.Red ||
                this.TextBoxLengthHandle.BackColor == Color.Red ||
                this.textBoxLenghtButt.BackColor == Color.Red ||
                this.textBoxWidthHandle.BackColor == Color.Red ||
                this.textBoxThicknessButt.BackColor == Color.Red ||
                this.LenghtBladeTextBox.BackColor == SystemColors.Window ||
                this.textBoxWidthButt.BackColor == SystemColors.Window ||
                this.TextBoxLengthHandle.BackColor == SystemColors.Window ||
                this.textBoxLenghtButt.BackColor == SystemColors.Window ||
                this.textBoxThicknessButt.BackColor == SystemColors.Window ||
                this.textBoxWidthHandle.BackColor == SystemColors.Window)
            {
            }
            else
            {
                this._builder.BuildAx(this._parameters);
            }
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Длина лезвия".
        /// </summary>
   
        private void LenghtBladeTextBox_Leave(object sender, EventArgs e) //DBM
        {
            ValidateAndSetColors(this.LenghtBladeTextBox, ParamType.LengthBlade);
            ValidateDependencies();
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Длинна топорища".
        /// </summary>
     
        private void textBoxWidthButt_Leave(object sender, EventArgs e)
        {
            ValidateAndSetColors(this.textBoxWidthButt, ParamType.WidthButt);
            ValidateDependencies();
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Длинна ручки топора".
        /// </summary>
        private void TextBoxLengthHandle_Leave(object sender, EventArgs e)
        {
            ValidateAndSetColors(this.TextBoxLengthHandle, ParamType.LengthHandle);
            ValidateDependencies();
        }
        /// <summary>
        /// Обработчик выхода из текстбокса "Длинна ручки топора".
        /// </summary>
        private void TextBoxLenghtButt_Leave(object sender, EventArgs e)
        {
            ValidateAndSetColors(this.textBoxLenghtButt, ParamType.LengthButt);
            ValidateDependencies();
        }

        private void textBoxWidthHandle_Leave(object sender, EventArgs e)
        {
            ValidateAndSetColors(this.textBoxWidthHandle, ParamType.WidthHandle);
            
        }

        private void textBoxThicknessButt_Leave(object sender, EventArgs e)
        {
            ValidateAndSetColors(this.textBoxThicknessButt, ParamType.ThicknessButt);
            ValidateDependencies();
        }
        /// <summary>
        /// Метод для установки цвета и подсказки для текстбокса в зависимости от типа параметра.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="color">Цвет подсветки.</param>
        /// <param name="tooltip">Текст подсказки.</param>
        private void SetColors(ParamType parameterType, Color color, string tooltip)
        {
            System.Windows.Forms.TextBox targetTextBox = null;

            // Стандартный switch-case для определения текстбокса
            switch (parameterType)
            {
                case ParamType.LengthBlade:
                    targetTextBox = this.LenghtBladeTextBox;
                    break;
                case ParamType.WidthButt:
                    targetTextBox = this.textBoxWidthButt;
                    break;
                case ParamType.LengthHandle:
                    targetTextBox = this.TextBoxLengthHandle;
                    break;
                case ParamType.LengthButt:
                    targetTextBox = this.textBoxLenghtButt;
                    break;
                case ParamType.WidthHandle:
                    targetTextBox = this.textBoxWidthHandle;
                    break;
                case ParamType.ThicknessButt:
                    targetTextBox = this.textBoxThicknessButt;
                    break;
                default:
                    throw new ArgumentException("Неизвестный тип параметра", nameof(parameterType));
            }

            if (targetTextBox != null)
            {
                targetTextBox.BackColor = color;
                this.toolTip1.SetToolTip(targetTextBox, tooltip);
            }
        }

        /// <summary>
        /// Универсальный метод валидации, объединяет первичную и вторичную проверки.
        /// </summary>
        /// <param name="textBox">Используемый текстбокс.</param>
        /// <param name="parameterType">Тип параметра.</param>
        private void ValidateAndSetColors(
        System.Windows.Forms.TextBox textBox,
        ParamType parameterType)
        {
            try
            {
                double value = double.Parse(textBox.Text); // Преобразуем значение
                Parameter parameter = new Parameter();

                // Устанавливаем границы параметра
                switch (parameterType)
                {
                    case ParamType.LengthBlade:
                        parameter.MaxValue = 300;
                        parameter.MinValue = 100;
                        break;
                    case ParamType.WidthButt:
                        parameter.MaxValue = 150;
                        parameter.MinValue = 80;
                        break;
                    case ParamType.LengthHandle:
                        parameter.MaxValue = 900;
                        parameter.MinValue = 300;
                        break;
                    case ParamType.LengthButt:
                        parameter.MaxValue = 270;
                        parameter.MinValue = 80;
                        break;
                    case ParamType.WidthHandle:
                        parameter.MaxValue = 60;
                        parameter.MinValue = 20;
                        break;
                    case ParamType.ThicknessButt:
                        parameter.MaxValue = 72;
                        parameter.MinValue = 24;
                        break;
                }

                // Устанавливаем значение параметра
                parameter.Value = value;

                // Сохраняем параметр в коллекцию
                this._parameters.SetParameter(parameterType, parameter);

                // Успешная валидация — зеленый цвет
                SetColors(parameterType, Color.Green, null);

                // Перекрестная проверка зависимых параметров
                if (!_isCrossValidating)
                {
                    _isCrossValidating = true;
                    CrossValidate(parameterType);
                    _isCrossValidating = false;
                }
            }
            catch (Exception ex)
            {
                // Ошибка валидации — красный цвет
                SetColors(parameterType, Color.Red, ex.Message);
            }
        }

        private void ValidateDependencies()
        {
            if (_isValidatingDependencies) return; // Если уже идет валидация, выходим
           
            try
            {
                _isValidatingDependencies = true;

                // Проверяем зависимые параметры от длины лезвия
                LenghtBladeTextBox_Leave(null, null);

                // Проверяем зависимые параметры от длины обуха
                TextBoxLenghtButt_Leave(null, null);

                // Обновляем валидацию всех остальных параметров
                TextBoxLengthHandle_Leave(null, null);
                textBoxWidthHandle_Leave(null, null);
                textBoxThicknessButt_Leave(null, null);
            }
            finally
            {
                _isValidatingDependencies = false; // Обязательно сбрасываем флаг
            }
        }


        private void CrossValidate(ParamType parameterType)
        {
            // Проверяем зависимости
            switch (parameterType)
            {
                case ParamType.LengthBlade:
                    // Длина лезвия влияет на длину рукояти, длину обуха и ширину рукояти
                    ValidateAndSetColors(this.TextBoxLengthHandle, ParamType.LengthHandle);
                    ValidateAndSetColors(this.textBoxLenghtButt, ParamType.LengthButt);
                    ValidateAndSetColors(this.textBoxWidthHandle, ParamType.WidthHandle);
                    break;
                case ParamType.LengthButt:
                    // Длина обуха влияет на ширину топорища
                    ValidateAndSetColors(this.textBoxThicknessButt, ParamType.ThicknessButt);
                    break;
                case ParamType.LengthHandle:
                case ParamType.WidthButt:
                case ParamType.ThicknessButt:
                case ParamType.WidthHandle:
                    // Нет перекрестных зависимостей для этих параметров
                    break;
            }
        }


        /// <summary>
        /// Обработчик изменения состояния чекбокса "Пожарный топор".
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Данные события.</param>
        private void checkBoxFireAx_CheckedChanged(object sender, EventArgs e)
        {
            // Устанавливаем состояние CheckBoxFireAx в зависимости от текущего состояния CheckBox
            _builder.CheckBoxFireAx = checkBoxFireAx.Checked;
        }

        /// <summary>
        /// Обработчик изменения состояния чекбокса "Отверстие для подвеса".
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Данные события.</param>
        private void checkBoxMountingHole_CheckedChanged(object sender, EventArgs e)
        {
            // Устанавливаем состояние CheckBoxMountingHole в зависимости от текущего состояния CheckBox
            _builder.CheckBoxMountingHole = checkBoxMountingHole.Checked;
        }
    }
}