namespace WebApi.Common.Helpers
{
    /// <summary>
    /// Класс, предоставляющий методы для работы с флаговыми значениями
    /// </summary>
    public static class FlagsHelper
    {
        /// <summary>
        /// Проверяет вхождение проверяемого флага во флаговые значения
        /// </summary>
        /// <typeparam name="T">Инстанс флаговых знчений</typeparam>
        /// <param name="flags">Флаговое значение, в котором необходимо проверить флаг</param>
        /// <param name="flag">Флаговое значение, которые необходимо проверить</param>
        /// <returns>Истина, если флаг входит в флаговые значения</returns>
        public static bool IsSet<T>(T flags, T flag) where T : struct
        {
            int values = (int)(object)flags;
            int value = (int)(object)flag;

            return (values & value) != 0;
        }
    }
}
