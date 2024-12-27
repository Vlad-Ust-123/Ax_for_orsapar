namespace AxPlugin
{
    /// <summary>
    /// Перечисление типов параметров.
    /// </summary>
    public enum ParamType
    {
        /// <summary>
        /// Длина ручки топора.
        /// </summary>
        LengthHandle, // D(A)+

        /// <summary>
        /// Ширина рукояти.
        /// </summary>
        WidthHandle, // M(A)+

        /// <summary>
        /// Длина лезвия.
        /// </summary>
        LengthBlade, // A -

        /// <summary>
        /// Длина топорища.
        /// </summary>
        WidthButt, // H -

        /// <summary>
        /// Длина обуха.
        /// </summary>
        LengthButt, // B(A) -

        /// <summary>
        /// Ширина топорища.
        /// </summary>
        ThicknessButt // E(B) -
    }
}
