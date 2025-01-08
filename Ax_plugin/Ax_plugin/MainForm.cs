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

        /// <summary>
        /// Указывает, выполняется ли в данный момент перекрестная валидация.
        /// </summary>
        private static bool isCrossValidating = false;

        /// <summary>
        /// Указывает, выполняется ли в данный момент проверка зависимостей.
        /// </summary>
        private bool isValidatingDependencies = false;


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
        /// Универсальный обработчик выхода из текстбокса. Выполняет валидацию и обновляет зависимости.
        /// </summary>
        /// <param name="textBox">Текстбокс, который вызвал обработчик.</param>
        /// <param name="parameterType">Тип параметра, связанный с текстбоксом.</param>
        private void HandleTextBoxLeave(TextBox textBox, ParamType parameterType)
        {
            ValidateAndSetColors(textBox, parameterType);
            ValidateDependencies();
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Длина лезвия".
        /// </summary>
        private void LenghtBladeTextBox_Leave(object sender, EventArgs e)
        {
            HandleTextBoxLeave(this.LenghtBladeTextBox, ParamType.LengthBlade);
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Длинна топорища".
        /// </summary>
        private void textBoxWidthButt_Leave(object sender, EventArgs e)
        {
            HandleTextBoxLeave(this.textBoxWidthButt, ParamType.WidthButt);
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Длинна ручки топора".
        /// </summary>
        private void TextBoxLengthHandle_Leave(object sender, EventArgs e)
        {
            HandleTextBoxLeave(this.TextBoxLengthHandle, ParamType.LengthHandle);
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Длинна обуха".
        /// </summary>
        private void TextBoxLenghtButt_Leave(object sender, EventArgs e)
        {
            HandleTextBoxLeave(this.textBoxLenghtButt, ParamType.LengthButt);
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Ширина рукояти".
        /// </summary>
        private void textBoxWidthHandle_Leave(object sender, EventArgs e)
        {
            HandleTextBoxLeave(this.textBoxWidthHandle, ParamType.WidthHandle);
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Толщина обуха".
        /// </summary>
        private void textBoxThicknessButt_Leave(object sender, EventArgs e)
        {
            HandleTextBoxLeave(this.textBoxThicknessButt, ParamType.ThicknessButt);
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
        private void ValidateAndSetColors(TextBox textBox, ParamType parameterType)
        {
            try
            {
                double value = double.Parse(textBox.Text); // Преобразуем значение.

                // Создаем параметр, передавая тип параметра и значение.
                Parameter parameter = new Parameter(parameterType, value);

                // Сохраняем параметр в коллекцию.
                _parameters.SetParameter(parameterType, parameter);

                // Успешная валидация — зеленый цвет.
                SetColors(parameterType, Color.Green, null);

                // Перекрестная проверка зависимых параметров.
                if (!isCrossValidating)
                {
                    isCrossValidating = true;
                    CrossValidate(parameterType);
                    isCrossValidating = false;
                }
            }
            catch (Exception ex)
            {
                // Ошибка валидации — красный цвет.
                SetColors(parameterType, Color.Red, ex.Message);
            }
        }


        /// <summary>
        /// Выполняет валидацию зависимых параметров, связанных с длиной лезвия, длиной обуха, 
        /// а также других параметров ножа. Метод предотвращает повторный запуск валидации, 
        /// если она уже выполняется.
        /// </summary>
        private void ValidateDependencies()
        {
            if (isValidatingDependencies) return; // Если уже идет валидация, выходим

            try
            {
                isValidatingDependencies = true;

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
                isValidatingDependencies = false; // Обязательно сбрасываем флаг
            }
        }

        /// <summary>
        /// Выполняет перекрестную валидацию параметров в зависимости от их типа. 
        /// Метод обновляет зависимые параметры и их состояние (например, цвет) 
        /// на основе заданного параметра.
        /// </summary>
        /// <param name="parameterType">Тип параметра <see cref="ParamType"/>, 
        /// для которого выполняется перекрестная валидация.</param>
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
                    // Длина обуха влияет на толщину топорища
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
            // Устанавливаем состояние IsFireAx в зависимости от текущего состояния CheckBox
            _builder.IsFireAx = checkBoxFireAx.Checked;
        }

       
        /// <summary>
        /// Обработчик изменения состояния чекбокса "Отверстие для подвеса".
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Данные события.</param>
        private void checkBoxMountingHole_CheckedChanged(object sender, EventArgs e)
        {
            // Устанавливаем состояние IsMountingHole в зависимости от текущего состояния CheckBox
            _builder.IsMountingHole = checkBoxMountingHole.Checked;
        }
    }
}