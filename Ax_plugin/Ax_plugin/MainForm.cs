using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AxPlugin
{
    /// <summary>
    /// Класс MainForm.
    /// </summary>
    public partial class AxPlugin : Form
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
        /// Initializes a new instance of the <see cref="AxPlugin"/> class.
        /// </summary>
        public AxPlugin()
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

        //RSDN +
        /// <summary>
        /// Инициализация ряда параметров при загрузке формы.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void AxPlugin_Load(object sender, EventArgs e)
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
        /// Универсальный обработчик события выхода из текстового поля.
        /// Определяет параметр, связанный с текстовым полем, и вызывает метод для обработки валидации и обновления.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие (обычно текстовое поле).</param>
        /// <param name="e">Данные события <see cref="EventArgs"/>.</param>
        /// <remarks>
        /// Этот метод связывает текстовые поля с их соответствующими параметрами, используя имя текстового поля.
        /// Если имя текстового поля не соответствует известным параметрам, выводится сообщение об ошибке.
        /// </remarks>
        private void UniversalTextBoxLeaveHandler(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                ParamType parameterType;
                switch (textBox.Name)
                {
                    case nameof(LenghtBladeTextBox):
                        parameterType = ParamType.LengthBlade;
                        break;
                    case nameof(textBoxWidthButt):
                        parameterType = ParamType.WidthButt;
                        break;
                    case nameof(TextBoxLengthHandle):
                        parameterType = ParamType.LengthHandle;
                        break;
                    case nameof(textBoxLenghtButt):
                        parameterType = ParamType.LengthButt;
                        break;
                    case nameof(textBoxWidthHandle):
                        parameterType = ParamType.WidthHandle;
                        break;
                    case nameof(textBoxThicknessButt):
                        parameterType = ParamType.ThicknessButt;
                        break;
                    default:
                        // Если текстбокс не соответствует известным параметрам
                        MessageBox.Show($"Не удалось определить параметр для {textBox.Name}");
                        return;
                }
                // Выполняем обработку текстового поля и валидацию
                HandleTextBoxLeave(textBox, parameterType);
            }
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
        /// Метод для установки цвета и подсказки для текстбокса в зависимости от типа параметра.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="color">Цвет подсветки.</param>
        /// <param name="tooltip">Текст подсказки.</param>
        private void SetColors(ParamType parameterType, 
            Color color, string tooltip)
        {
            System.Windows.Forms.TextBox targetTextBox = null;

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
                double value = double.Parse(textBox.Text); 

                Parameter parameter = new Parameter(parameterType, value);

                _parameters.SetParameter(parameterType, parameter);

                SetColors(parameterType, Color.Green, null);

                if (!isCrossValidating)
                {
                    isCrossValidating = true;
                    CrossValidate(parameterType);
                    isCrossValidating = false;
                }
            }
            catch (Exception ex)
            {
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
            if (isValidatingDependencies) return; 

            try
            {
                isValidatingDependencies = true;

                // Проверяем зависимые параметры от длины лезвия
                UniversalTextBoxLeaveHandler(null, null);
 
            }
            finally
            {
                // Обязательно сбрасываем флаг
                isValidatingDependencies = false; 
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
            _builder.IsFireAx = checkBoxFireAx.Checked;
        }
       
        /// <summary>
        /// Обработчик изменения состояния чекбокса "Отверстие для подвеса".
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Данные события.</param>
        private void checkBoxMountingHole_CheckedChanged(object sender, EventArgs e)
        {
            _builder.IsMountingHole = checkBoxMountingHole.Checked;
        }

        
    }
}