using System;
using KompasAPI7;
using Kompas;

namespace AxPlugin
{
    
    public class Builder
    {
        /// <summary>
        /// Объект обертки для взаимодействия с API Компас.
        /// </summary>
        private readonly Wrapper _wrapper;

        //
        /// <summary>
        /// Флаг для активации функциональности "Пожарный топор".
        /// </summary>
        public bool IsFireAx { get; set; } = false;

        //
        /// <summary>
        /// Флаг для активации функциональности "Отверстие для подвеса".
        /// </summary>
        public bool IsMountingHole { get; set; } = false;

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
            if (IsFireAx)
            {
                BuildFireAxTip(part, parameters);
            }

            // Добавляем отверстие для подвеса, если активирован соответствующий флажок.
            if (IsMountingHole)
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
        /// Выбрасывается, если параметры <see cref="ParamType.WidthHandle"/> и
        /// <see cref="ParamType.LengthHandle"/> не заданы.
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
                throw new ArgumentException("Параметры WidthHandle и LengthHandle " +
                    "обязательны для построения рукояти.");
            }
        }

        /// <summary>
        /// Метод для построения обуха топора.
        /// </summary>
        /// <param name="part">Часть топора, в которой строится обух.</param>
        /// <param name="parameters">Параметры топора, содержащие размеры обуха.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если параметры <see cref="ParamType.LengthBlade"/>, 
        /// <see cref="ParamType.WidthButt"/>,
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
                throw new ArgumentException("Параметры LengthBlade, WidthButt, " +
                    "LengthButt и ThicknessButt обязательны для построения обуха.");
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
        private void BuildBlade(IPart7 part, double width, double lengthButt, 
            double lengthBlade, double thickness)
        {
            // Параметры трапеции (равнобедренная)
            double topBase = lengthButt;    // Верхняя основа (меньшая сторона)
            double bottomBase = lengthBlade; // Нижняя основа (большая сторона)
            double height = width;     // Высота трапеции

            //Задаем плоскость
            object sidePlane = _wrapper.GetSidePlane(part, 
                Kompas6Constants3D.ksObj3dTypeEnum.o3d_planeXOZ);

            // Создаем эскиз на плоскости ХОZ
            ISketch trapezoidSketch = _wrapper.CreateSketchOnPlane(part, sidePlane, 
                "Эскиз: равнобедренная трапеция");

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
            _wrapper.ExtrudeSketch(trapezoidSketch, thickness / 2, 
                "Равнобедренная трапеция на боковой плоскости", false);
            _wrapper.ExtrudeSketch(trapezoidSketch, -thickness / 2, 
                "Равнобедренная трапеция на боковой плоскости", false);

            // Задаем плоскость для треугольников
            object sidePlane2 = _wrapper.GetSidePlane(part,
                Kompas6Constants3D.ksObj3dTypeEnum.o3d_planeXOY);

            // Создаем эскиз треугольника на плоскости XOY
            ISketch triangleSketch = _wrapper.CreateSketchOnPlane(part, sidePlane2, 
                "Эскиз: Треугольник для выреза");

            // Координаты первой точки
            double xFirstPointTriangle = -width / 2;
            double yFirstPointTriangle = thickness / 2;

            // Координаты Второй точки
            double xSecondPointTriangle = -lengthButt * 1.5;
            double ySecondPointTriangle = thickness / 2;

            // Координаты третьей точки
            double xThirdPointTriangle = -width * 1.5;
            double yThirdPointTriangle = 0;

            // Соединаем точки
            _wrapper.CreateLine(triangleSketch, xFirstPointTriangle, yFirstPointTriangle, 
                xSecondPointTriangle, ySecondPointTriangle);

            _wrapper.CreateLine(triangleSketch, xSecondPointTriangle, ySecondPointTriangle, 
                xThirdPointTriangle, yThirdPointTriangle);

            _wrapper.CreateLine(triangleSketch, xThirdPointTriangle, yThirdPointTriangle, 
                xFirstPointTriangle, yFirstPointTriangle);
            //Симетрично вырезаем треугольник
            _wrapper.CutExtrudeSymmetric(triangleSketch, 700, "Симметричное вырезание");


            // Создаем второй эскиз треугольника на плоскости XOY
            ISketch triangleSketch2 = _wrapper.CreateSketchOnPlane(part, sidePlane2, 
                "Эскиз: Треугольник для выреза");

            // Координаты первой точки
            xFirstPointTriangle = -width / 2;
            yFirstPointTriangle = -thickness / 2;

            // Координаты Второй точки
            xSecondPointTriangle = -lengthButt * 1.5;
            ySecondPointTriangle = -thickness / 2;

            // Координаты третьей точки
            xThirdPointTriangle = -width * 1.5;
            yThirdPointTriangle = 0;

            // Соединаем точки
            _wrapper.CreateLine(triangleSketch2, xFirstPointTriangle, yFirstPointTriangle, 
                xSecondPointTriangle, ySecondPointTriangle);
            _wrapper.CreateLine(triangleSketch2, xSecondPointTriangle, ySecondPointTriangle, 
                xThirdPointTriangle, yThirdPointTriangle);
            _wrapper.CreateLine(triangleSketch2, xThirdPointTriangle, yThirdPointTriangle, 
                xFirstPointTriangle, yFirstPointTriangle);

            //Симетрично вырезаем треугольник
            _wrapper.CutExtrudeSymmetric(triangleSketch2, 700, "Симметричное вырезание");

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

                object sidePlane = _wrapper.GetSidePlane(part, 
                    Kompas6Constants3D.ksObj3dTypeEnum.o3d_planeXOY);

                // Создаем эскиз для наконечника на плоскости XOY
                ISketch fireTip = _wrapper.CreateSketchOnPlane(part, sidePlane, "Эскиз: наконечник");

                // Длина прямоугольника
                double tipLenght = widthButt.Value;

                // Ширина прямоугольника
                double tipWidth = thicknessButt.Value;  

                // Центрируем прямоугольник 
                double xStart = widthButt.Value / 2;
                double yStart = -thicknessButt.Value / 2;

                // Рисуем прямоугольник
                _wrapper.CreateRectangle(fireTip, xStart, yStart, tipLenght, tipWidth);
                // Выдавливаем
                _wrapper.ExtrudeSketch(fireTip, lengthButt.Value / 5, 
                    "Прямоугольник для наконечника", false);

                // Задаем плоскость XOZ
                object sidePlane2 = _wrapper.GetSidePlane(part, 
                    Kompas6Constants3D.ksObj3dTypeEnum.o3d_planeXOZ);

                // Создаем эскиз треугольника на плоскости XOZ
                ISketch triangleSketch = _wrapper.CreateSketchOnPlane(part, sidePlane2, 
                    "Эскиз: Треугольник для выреза наконечника");

                // Координаты первой точки
                double x1 = widthButt.Value / 2;
                double y1 = 0;

                // Координаты Второй точки
                double x2 = tipLenght * 1.5;
                double y2 = 0;

                // Координаты Третьей точки
                double x3 = tipLenght * 1.5;
                double y3 = -lengthButt.Value / 5;

                // Соединяем точки
                _wrapper.CreateLine(triangleSketch, x1, y1, x2, y2);
                _wrapper.CreateLine(triangleSketch, x2, y2, x3, y3);
                _wrapper.CreateLine(triangleSketch, x3, y3, x1, y1);

                // Симетрично выдавливаем
                _wrapper.CutExtrudeSymmetric(triangleSketch, 400, "Симметричное вырезание");
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

                //Смешение относительно ширины обуха (делим на 8 частей)
                double xHoleOffset = ((-widthButt.Value / 2) + -widthButt.Value) 
                    - (((-widthButt.Value / 2) + -widthButt.Value) / 8);

                //Смешение относительно длинны лезвие (делим на 12 частей)
                double yHoleOffset = ((-lengthButt.Value / 5) + (lengthBlade.Value / 5))
                    - (lengthBlade.Value / 12);

                //Задаем координаты в смещение
                double x = xHoleOffset;
                double y = yHoleOffset;

                ISketch sketch = _wrapper.CreateCircleAndReturnSketch(part, 
                    _wrapper.GetSidePlane(part, Kompas6Constants3D.ksObj3dTypeEnum.o3d_planeXOZ),
                    x, y, diameter, "Эскиз: отверстие для подвеса");

                _wrapper.CutExtrudeSymmetric(sketch, 400, "Симметричное вырезание отверстия для подвеса");
            }
        }
    }
}
