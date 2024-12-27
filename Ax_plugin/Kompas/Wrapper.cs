using System;
using KompasAPI7;
using Kompas6API5;
using Kompas6Constants3D;
using Kompas6Constants;

namespace Kompas
{
    /// <summary>
    /// Класс для работы с API Компас.
    /// </summary>
    public class Wrapper //Обертка тут происходит магия 
    {
        /// <summary>
        /// Объект API для работы с Kompas
        /// </summary>
        private IKompasAPIObject _kompas;

        /// <summary>
        /// Метод для открытия CAD-приложения
        /// </summary>
        public void OpenCad()
        {
            Type t = Type.GetTypeFromProgID("KOMPAS.Application.7");
            _kompas = (IKompasAPIObject)Activator.CreateInstance(t);
            _kompas.Application.Visible = true;
        }

        /// <summary>
        /// Метод для создания части в 3D документе
        /// </summary>
        /// <returns>Возвращает созданную часть</returns>
        public IPart7 CreatePart()
        {
            _kompas.Application.Documents.Add(DocumentTypeEnum.ksDocumentPart);
            IKompasDocument3D document3d = (IKompasDocument3D)_kompas.Application.ActiveDocument;
            return document3d.TopPart;
        }

        /// <summary>
        /// Метод для создания эскиза на заданной части
        /// </summary>
        /// <param name="part">Часть, к которой добавляется эскиз</param>
        /// <param name="name">Имя создаваемого эскиза</param>
        /// <returns>Созданный эскиз</returns>
        public ISketch CreateSketch(IPart7 part, string name)
        {
            IModelContainer modelContainer = (IModelContainer)part;
            ISketch sketch = modelContainer.Sketchs.Add();
            sketch.Plane = part.DefaultObject[ksObj3dTypeEnum.o3d_planeXOY];
            sketch.Name = name;
            sketch.Hidden = false;
            sketch.Update();

            return sketch;
        }

        /// <summary>
        /// Метод для создания прямоугольника в эскизе
        /// </summary>
        /// <param name="sketch">Эскиз, в который добавляется прямоугольник</param>
        /// <param name="x">Координата X начальной точки</param>
        /// <param name="y">Координата Y начальной точки</param>
        /// <param name="width">Ширина прямоугольника</param>
        /// <param name="height">Высота прямоугольника</param>
        public void CreateRectangle(ISketch sketch, double x, double y, double width, double height)
        {
            IKompasDocument documentSketch = sketch.BeginEdit();
            IKompasDocument2D document2D = (IKompasDocument2D)documentSketch;
            IViewsAndLayersManager viewsAndLayersManager = document2D.ViewsAndLayersManager;
            IView view = viewsAndLayersManager.Views.ActiveView;
            IDrawingContainer drawingContainer = (IDrawingContainer)view;

            IRectangle rectangle = drawingContainer.Rectangles.Add();
            rectangle.Style = (int)Kompas6Constants.ksCurveStyleEnum.ksCSNormal;
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
            rectangle.Update();
            sketch.EndEdit();
        }

        /// <summary>
        /// Метод для создания круга в эскизе
        /// </summary>
        /// <param name="sketch">Эскиз, в который добавляется круг</param>
        /// <param name="x">Координата X центра круга</param>
        /// <param name="y">Координата Y центра круга</param>
        /// <param name="diameter">Диаметр круга</param>
        public void CreateCircle(ISketch sketch, double x, double y, double diameter)
        {
            IKompasDocument documentSketch = sketch.BeginEdit();
            IKompasDocument2D document2D = (IKompasDocument2D)documentSketch;
            IViewsAndLayersManager viewsAndLayersManager = document2D.ViewsAndLayersManager;
            IView view = viewsAndLayersManager.Views.ActiveView;
            IDrawingContainer drawingContainer = (IDrawingContainer)view;

            ICircle circle = drawingContainer.Circles.Add();
            circle.Style = (int)Kompas6Constants.ksCurveStyleEnum.ksCSNormal;
            circle.Xc = x;
            circle.Yc = y;
            circle.Radius = diameter / 2;
            circle.Update();
            sketch.EndEdit();
        }

        //TODO:XML
        public ISketch CreateCircleAndReturnSketch(IPart7 part, object plane, double x, double y, double diameter, string sketchName)
        {
            // Создаем эскиз на заданной плоскости с именем
            ISketch sketch = CreateSketchOnPlane(part, plane, sketchName);

            // Начинаем редактирование эскиза
            IKompasDocument documentSketch = sketch.BeginEdit();
            IKompasDocument2D document2D = (IKompasDocument2D)documentSketch;
            IViewsAndLayersManager viewsAndLayersManager = document2D.ViewsAndLayersManager;
            IView view = viewsAndLayersManager.Views.ActiveView;
            IDrawingContainer drawingContainer = (IDrawingContainer)view;

            // Создаем окружность
            ICircle circle = drawingContainer.Circles.Add();
            circle.Style = (int)Kompas6Constants.ksCurveStyleEnum.ksCSNormal;
            circle.Xc = x;
            circle.Yc = y;
            circle.Radius = diameter / 2;
            circle.Update();

            // Завершаем редактирование эскиза
            sketch.EndEdit();

            // Возвращаем созданный эскиз
            return sketch;
        }

        public void CutCircleOnSketch(ISketch sketch, double depth, string cutName)
        {
            // Выполняем симметричное вырезание по переданному эскизу
            CutExtrudeSymmetric(sketch, depth, cutName);
        }

        /// <summary>
        /// Метод для экструзии эскиза
        /// </summary>
        /// <param name="sketch">Эскиз, который будет экструзирован</param>
        /// <param name="depth">Глубина экструзии</param>
        /// <param name="name">Имя экструзии</param>
        /// <param name="draftOutward">Флаг, указывающий направление экструзии</param>
        public void ExtrudeSketch(ISketch sketch, double depth, string name, bool draftOutward)
        {
            var part = sketch.Part;
            var modelContainer = (IModelContainer)part;
            var extrusions = modelContainer.Extrusions;

            IExtrusion extrusion = extrusions.Add(Kompas6Constants3D.ksObj3dTypeEnum.o3d_bossExtrusion);
            extrusion.Direction = Kompas6Constants3D.ksDirectionTypeEnum.dtNormal;
            extrusion.Name = name;
            extrusion.Hidden = false;
            extrusion.ExtrusionType[true] = Kompas6Constants3D.ksEndTypeEnum.etBlind;
            extrusion.DraftOutward[true] = draftOutward;
            extrusion.DraftValue[true] = 0.0;
            extrusion.Depth[true] = depth;

            IExtrusion1 extrusion1 = (IExtrusion1)extrusion;
            extrusion1.Profile = sketch;
            extrusion1.DirectionObject = sketch;
            extrusion.Update();
        }

        /// <summary>
        /// Метод для создания линии в заданном эскизе.
        /// </summary>
        /// <param name="sketch">Эскиз, в котором создается линия.</param>
        /// <param name="x1">Координата X начальной точки линии.</param>
        /// <param name="y1">Координата Y начальной точки линии.</param>
        /// <param name="x2">Координата X конечной точки линии.</param>
        /// <param name="y2">Координата Y конечной точки линии.</param>
        public void CreateLine(ISketch sketch, double x1, double y1, double x2, double y2)
        {
            // Начинаем редактирование эскиза.
            IKompasDocument documentSketch = sketch.BeginEdit();
            IKompasDocument2D document2D = (IKompasDocument2D)documentSketch;

            // Получаем менеджер видов и активный вид.
            IViewsAndLayersManager viewsAndLayersManager = document2D.ViewsAndLayersManager;
            IView view = viewsAndLayersManager.Views.ActiveView;

            // Получаем контейнер для рисования.
            IDrawingContainer drawingContainer = (IDrawingContainer)view;

            // Создаем линию с указанными координатами.
            ILineSegment line = drawingContainer.LineSegments.Add();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.Update();

            // Завершаем редактирование эскиза.
            sketch.EndEdit();
        }

        /// <summary>
        /// Получение стандартной плоскости на основе типа
        /// </summary>
        /// <param name="part">Часть, из которой нужно получить стандартную плоскость</param>
        /// <param name="planeType">Тип стандартной плоскости (например, ksObj3dTypeEnum.o3d_planeXOZ)</param>
        /// <returns>Объект стандартной плоскости</returns>
        public object GetSidePlane(IPart7 part, ksObj3dTypeEnum planeType)
        {
            // Получаем стандартный объект плоскости
            var plane = part.DefaultObject[planeType];

            if (plane == null)
            {
                throw new InvalidOperationException("Не удалось получить стандартную плоскость.");
            }

            return plane; // Возвращаем объект
        }

        //TODO:XML
        public ISketch CreateSketchOnPlane(IPart7 part, object plane, string name)
        {
            IModelContainer modelContainer = part as IModelContainer;
            if (modelContainer == null)
            {
                throw new InvalidOperationException("Не удалось преобразовать part к IModelContainer.");
            }

            ISketch sketch = modelContainer.Sketchs.Add(); // Добавляем эскиз через контейнер

            // Преобразуем объект plane к IModelObject
            sketch.Plane = plane as IModelObject;
            if (sketch.Plane == null)
            {
                throw new InvalidOperationException("Плоскость не может быть преобразована в IModelObject.");
            }

            sketch.Name = name;   // Устанавливаем имя
            sketch.Hidden = false; // Делаем эскиз видимым
            sketch.Update();

            return sketch;
        }

        /// <summary>
        /// Вырезать выдавливанием симметрично из указанного эскиза
        /// </summary>
        /// <param name="sketch">Эскиз для вырезания</param>
        /// <param name="depth">Глубина вырезания (в обе стороны от эскиза)</param>
        /// <param name="name">Имя операции вырезания</param>
        public void CutExtrudeSymmetric(ISketch sketch, double depth, string name)
        {
            // Получаем часть, к которой привязан эскиз
            var part = sketch.Part;
            if (part == null)
            {
                throw new InvalidOperationException("Эскиз не привязан к части.");
            }

            // Получаем контейнер операций для части
            var modelContainer = part as IModelContainer;
            if (modelContainer == null)
            {
                throw new InvalidOperationException("Не удалось преобразовать part к IModelContainer.");
            }

            // Добавляем операцию вырезания
            var extrusions = modelContainer.Extrusions;
            var cutExtrusion = extrusions.Add(Kompas6Constants3D.ksObj3dTypeEnum.o3d_cutExtrusion);

            cutExtrusion.Direction = Kompas6Constants3D.ksDirectionTypeEnum.dtBoth; // Симметричное направление
            cutExtrusion.Name = name; // Имя операции
            cutExtrusion.Hidden = false; // Сделать операцию видимой
            cutExtrusion.ExtrusionType[true] = Kompas6Constants3D.ksEndTypeEnum.etBlind; // Глубина вырезания
            cutExtrusion.Depth[true] = depth; // Устанавливаем глубину вырезания
            cutExtrusion.Depth[false] = depth; // Глубина в противоположную сторону
            cutExtrusion.DraftOutward[true] = false; // Без уклона
            cutExtrusion.DraftOutward[false] = false; // Без уклона в обе стороны

            // Привязываем профиль (эскиз) к операции вырезания
            var cutExtrusion1 = cutExtrusion as IExtrusion1;
            if (cutExtrusion1 == null)
            {
                throw new InvalidOperationException("Не удалось преобразовать выдавливание к IExtrusion1.");
            }

            cutExtrusion1.Profile = sketch; // Эскиз для вырезания
            cutExtrusion1.DirectionObject = sketch; // Направление вырезания от эскиза

            // Обновляем операцию вырезания
            cutExtrusion.Update();
        }
 
    }
}
