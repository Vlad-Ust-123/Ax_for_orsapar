using System;
using KompasAPI7;
using Kompas6Constants3D;
using Kompas;
using System.Security.Cryptography;

namespace AxPlugin
{
    //TODO:XML
    public class Builder
    {
        /// <summary>
        /// Объект обертки для взаимодействия с API Компас.
        /// </summary>
        private readonly Wrapper _wrapper;

        //TODO: rename
        /// <summary>
        /// Флаг для активации функциональности "Пожарный топор".
        /// </summary>
        public bool CheckBoxFireAx { get; set; } = false;

        //TODO: rename
        /// <summary>
        /// Флаг для активации функциональности "Отверстие для подвеса".
        /// </summary>
        public bool CheckBoxMountingHole { get; set; } = false;

        /// <summary>
        /// Конструктор класса <see cref="Builder"/>.
        /// Инициализирует объект обертки для работы с API Компас.
        /// </summary>
        public Builder()
        {
            _wrapper = new Wrapper();
        }

        /// <summary>
        /// Метод для построения топора с заданными параметрами.
        /// Учитывает флаги для дополнительных элементов: "Пожарный топор" и "Отверстие для подвеса".
        /// </summary>
        /// <param name="parameters">Параметры топора.</param>
        public void BuildAx(AxParameters parameters)
        {
            // Открываем CAD-приложение.
            _wrapper.OpenCad();

            // Создаем новую часть.
            IPart7 part = _wrapper.CreatePart();

            // Строим основные элементы топора.
            BuildHandle(part, parameters);
            BuildButt(part, parameters);

            // Добавляем наконечник для пожарного топора, если активирован соответствующий флажок.
            if (CheckBoxFireAx)
            {
                BuildFireAxTip(part, parameters);
            }

            // Добавляем отверстие для подвеса, если активирован соответствующий флажок.
            if (CheckBoxMountingHole)
            {
                BuildMountingHole(part, parameters);
            }
        }

        /// <summary>
        /// Метод для построения рукояти топора.
        /// </summary>
        /// <param name="part">Часть топора, в которой строится рукоять.</param>
        /// <param name="parameters">Параметры топора, содержащие размеры рукояти.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если параметры <see cref="ParamType.WidthHandle"/> и <see cref="ParamType.LengthHandle"/> не заданы.
        /// </exception>
        private void BuildHandle(IPart7 part, AxParameters parameters)
        {
            // Проверяем наличие необходимых параметров рукояти.
            if (parameters.AllParameters.TryGetValue(ParamType.WidthHandle, out Parameter widthHandle) &&
                parameters.AllParameters.TryGetValue(ParamType.LengthHandle, out Parameter lengthHandle))
            {
                // Создаем эскиз для рукояти.
                ISketch sketch = _wrapper.CreateSketch(part, "Эскиз: рукоять");

                // Строим окружность и выполняем экструзию для создания рукояти.
                _wrapper.CreateCircle(sketch, 0, 0, widthHandle.Value);
                _wrapper.ExtrudeSketch(sketch, lengthHandle.Value, "Рукоять", false);
            }
            else
            {
                // Выбрасываем исключение, если параметры не заданы.
                throw new ArgumentException("Параметры WidthHandle и LengthHandle обязательны для построения рукояти.");
            }
        }

        /// <summary>
        /// Метод для построения обуха топора.
        /// </summary>
        /// <param name="part">Часть топора, в которой строится обух.</param>
        /// <param name="parameters">Параметры топора, содержащие размеры обуха.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если параметры <see cref="ParamType.LengthBlade"/>, <see cref="ParamType.WidthButt"/>,
        /// <see cref="ParamType.LengthButt"/> и <see cref="ParamType.ThicknessButt"/> не заданы.
        /// </exception>
        private void BuildButt(IPart7 part, AxParameters parameters)
        {
            // Проверяем наличие необходимых параметров обуха.
            if (parameters.AllParameters.TryGetValue(ParamType.LengthBlade, out Parameter lengthBlade) &&
                parameters.AllParameters.TryGetValue(ParamType.WidthButt, out Parameter widthButt) &&
                parameters.AllParameters.TryGetValue(ParamType.LengthButt, out Parameter lengthButt) &&
                parameters.AllParameters.TryGetValue(ParamType.ThicknessButt, out Parameter thicknessButt))
            {
                // Строим прямоугольник на обухе.
                BuildTopRectangle(part, widthButt.Value, thicknessButt.Value, lengthButt.Value);

                // Строим лезвие топора.
                BuildBlade(part, widthButt.Value, lengthButt.Value, lengthBlade.Value, thicknessButt.Value);
            }
            else
            {
                // Выбрасываем исключение, если параметры не заданы.
                throw new ArgumentException("Параметры LengthBlade, WidthButt, LengthButt и ThicknessButt обязательны для построения обуха.");
            }
        }

        /// <summary>
        /// Метод для построения прямоугольника сверху обуха.
        /// </summary>
        /// <param name="part">Часть топора, в которой строится прямоугольник.</param>
        /// <param name="width">Ширина прямоугольника.</param>
        /// <param name="height">Высота прямоугольника.</param>
        /// <param name="depth">Глубина экструзии прямоугольника.</param>
        private void BuildTopRectangle(IPart7 part, double width, double height, double depth)
        {
            // Создаем эскиз для прямоугольника.
            ISketch sketch = _wrapper.CreateSketch(part, "Эскиз: прямоугольник сверху обуха");

            // Строим прямоугольник и выполняем экструзию.
            _wrapper.CreateRectangle(sketch, -width / 2, -height / 2, width, height);
            _wrapper.ExtrudeSketch(sketch, depth, "Прямоугольник на обухе", false);
        }

        /// <summary>
        /// Метод для построения рубящей части топора.
        /// </summary>
        /// <param name="part">Часть топора, в которой строится лезвие.</param>
        /// <param name="width">Ширина лезвия.</param>
        /// <param name="lengthButt">Длина обуха.</param>
        /// <param name="lengthBlade">Длина лезвия.</param>
        /// <param name="thickness">Толщина лезвия.</param>
        private void BuildBlade(IPart7 part, double width, double lengthButt, double lengthBlade, double thickness)
        {
            // Создаем рубящую часть топора от сюда

            // Параметры трапеции (равнобедренная)
            double topBase = lengthButt;    // Верхняя основа (меньшая сторона)
            double bottomBase = lengthBlade; // Нижняя основа (большая сторона)
            double height = width;     // Высота трапеции
                                                //Задаем плоскость
            object sidePlane = _wrapper.GetSidePlane(part, Kompas6Constants3D.ksObj3dTypeEnum.o3d_planeXOZ);

            // Создаем эскиз на плоскости ХОZ
            ISketch trapezoidSketch = _wrapper.CreateSketchOnPlane(part, sidePlane, "Эскиз: равнобедренная трапеция");

            // Смещение эскиза трапеции
            double offsetX = -width / 2; // Смещение по оси X (конец прямоугольника)
            double offsetY = -lengthButt / 2; // Смещение по оси Y (центр прямоугольника)

            // Координаты для равнобедренной трапеции (с учётом смещений)
            double x1 = offsetX;                        // Левая вершина верхней основы
            double y1 = offsetY - (topBase / 2);        // Верхняя основа центрирована по Y

            double x2 = offsetX;                        // Правая вершина верхней основы
            double y2 = offsetY + (topBase / 2);

            double x3 = offsetX - height;               // Левая вершина нижней основы
            double y3 = offsetY - (bottomBase / 2);     // Нижняя основа центрирована по Y

            double x4 = offsetX - height;               // Правая вершина нижней основы
            double y4 = offsetY + (bottomBase / 2);

            // Рисуем трапецию
            _wrapper.CreateLine(trapezoidSketch, x1, y1, x2, y2); // Верхняя основа
            _wrapper.CreateLine(trapezoidSketch, x2, y2, x4, y4); // Правая боковая сторона
            _wrapper.CreateLine(trapezoidSketch, x4, y4, x3, y3); // Нижняя основа
            _wrapper.CreateLine(trapezoidSketch, x3, y3, x1, y1); // Левая боковая сторона

            // Выдавливаем трапецию в обе стороны
            _wrapper.ExtrudeSketch(trapezoidSketch, thickness / 2, "Равнобедренная трапеция на боковой плоскости", false);
            _wrapper.ExtrudeSketch(trapezoidSketch, -thickness / 2, "Равнобедренная трапеция на боковой плоскости", false);


            // Задаем плоскость для треугольников
            object sidePlane2 = _wrapper.GetSidePlane(part, Kompas6Constants3D.ksObj3dTypeEnum.o3d_planeXOY);
            // Создаем эскиз треугольника на плоскости XOY
            ISketch triangleSketch = _wrapper.CreateSketchOnPlane(part, sidePlane2, "Эскиз: Треугольник для выреза");

            //TODO: RSDN
            // Координаты первой точки
            double x1_2 = -width / 2;
            double y1_2 = thickness / 2;
            // Координаты Второй точки
            double x2_2 = -lengthButt * 1.5;
            double y2_2 = thickness / 2;
            // Координаты третьей точки
            double x3_2 = -width * 1.5;
            double y3_2 = 0;

            // Соединаем точки
            _wrapper.CreateLine(triangleSketch, x1_2, y1_2, x2_2, y2_2);
            _wrapper.CreateLine(triangleSketch, x2_2, y2_2, x3_2, y3_2);
            _wrapper.CreateLine(triangleSketch, x3_2, y3_2, x1_2, y1_2);
            //Симетрично вырезаем треугольник
            _wrapper.CutExtrudeSymmetric(triangleSketch, 700, "Симметричное вырезание");


            // Создаем второй эскиз треугольника на плоскости XOY
            ISketch triangleSketch_2 = _wrapper.CreateSketchOnPlane(part, sidePlane2, "Эскиз: Треугольник для выреза");

            //TODO: RSDN
            // Координаты первой точки
            double x1_3 = -width / 2;
            double y1_3 = -thickness / 2;
            // Координаты Второй точки
            double x2_3 = -lengthButt * 1.5;
            double y2_3 = -thickness / 2;
            // Координаты третьей точки
            double x3_3 = -width * 1.5;
            double y3_3 = 0;

            // Соединаем точки
            _wrapper.CreateLine(triangleSketch_2, x1_3, y1_3, x2_3, y2_3);
            _wrapper.CreateLine(triangleSketch_2, x2_3, y2_3, x3_3, y3_3);
            _wrapper.CreateLine(triangleSketch_2, x3_3, y3_3, x1_3, y1_3);
            //Симетрично вырезаем треугольник
            _wrapper.CutExtrudeSymmetric(triangleSketch_2, 700, "Симметричное вырезание");
            //До сюда

        }

        /// <summary>
        /// Метод для построения наконечника пожарного топора.
        /// </summary>
        /// <param name="part">Часть топора, в которой строится наконечник.</param>
        /// <param name="parameters">Параметры топора, содержащие размеры наконечника.</param>
        private void BuildFireAxTip(IPart7 part, AxParameters parameters)
        {
            if (parameters.AllParameters.TryGetValue(ParamType.WidthButt, out Parameter widthButt) &&
                parameters.AllParameters.TryGetValue(ParamType.LengthButt, out Parameter lengthButt) &&
                parameters.AllParameters.TryGetValue(ParamType.ThicknessButt, out Parameter thicknessButt))
            {

                object sidePlane = _wrapper.GetSidePlane(part, Kompas6Constants3D.ksObj3dTypeEnum.o3d_planeXOY);
                // Создаем эскиз для наконечника на плоскости XOY
                ISketch fireTip = _wrapper.CreateSketchOnPlane(part, sidePlane, "Эскиз: наконечник");

                //TODO: RSDN
                double TipLenght = widthButt.Value;       // длина прямоугольника
                double TipWidth = thicknessButt.Value;  // ширина прямоугольника

                // Центрируем прямоугольник 
                double xStart = widthButt.Value / 2;
                double yStart = -thicknessButt.Value / 2;

                // Рисуем прямоугольник
                _wrapper.CreateRectangle(fireTip, xStart, yStart, TipLenght, TipWidth);
                // Выдавливаем
                _wrapper.ExtrudeSketch(fireTip, lengthButt.Value / 5, "Прямоугольник для наконечника", false);

                // Задаем плоскость XOZ
                object sidePlane2 = _wrapper.GetSidePlane(part, Kompas6Constants3D.ksObj3dTypeEnum.o3d_planeXOZ);
                // Создаем эскиз треугольника на плоскости XOZ
                ISketch triangleSketch = _wrapper.CreateSketchOnPlane(part, sidePlane2, "Эскиз: Треугольник для выреза наконечника");

                // Координаты первой точки
                double x1 = widthButt.Value / 2;
                double y1 = 0;
                // Координаты Второй точки
                double x2 = TipLenght * 1.5;
                double y2 = 0;
                // Координаты Третьей точки
                double x3 = TipLenght * 1.5;
                double y3 = -lengthButt.Value / 5;

                // Соединяем точки
                _wrapper.CreateLine(triangleSketch, x1, y1, x2, y2);
                _wrapper.CreateLine(triangleSketch, x2, y2, x3, y3);
                _wrapper.CreateLine(triangleSketch, x3, y3, x1, y1);
                // Симетрично выдавливаем
                _wrapper.CutExtrudeSymmetric(triangleSketch, 400, "Симметричное вырезание");
                //До этой строчки
            }
        }

        /// <summary>
        /// Метод для создания отверстия для подвеса топора.
        /// </summary>
        /// <param name="part">Часть топора, в которой создается отверстие.</param>
        /// <param name="parameters">Параметры топора, содержащие размеры для отверстия.</param>
        private void BuildMountingHole(IPart7 part, AxParameters parameters)
        {
            if (parameters.AllParameters.TryGetValue(ParamType.WidthButt, out Parameter widthButt) &&
                parameters.AllParameters.TryGetValue(ParamType.LengthBlade, out Parameter lengthBlade) &&
                parameters.AllParameters.TryGetValue(ParamType.LengthButt, out Parameter lengthButt))
            {
                double diameter = 10;
                //TODO: const
                double x = ((-widthButt.Value / 2) + -widthButt.Value) - (((-widthButt.Value / 2) + -widthButt.Value) / 8);
                double y = ((-lengthButt.Value / 5) + (lengthBlade.Value / 5)) - (lengthBlade.Value / 12);

                ISketch sketch = _wrapper.CreateCircleAndReturnSketch(
                    part,
                    _wrapper.GetSidePlane(part, Kompas6Constants3D.ksObj3dTypeEnum.o3d_planeXOZ),
                    x, y, diameter, "Эскиз: отверстие для подвеса");

                _wrapper.CutCircleOnSketch(sketch, 400, "Симметричное вырезание отверстия для подвеса");
            }
        }
    }
}
