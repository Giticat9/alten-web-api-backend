namespace WebApi.Common
{
    /// <summary>
    /// Содержит константы для поддерживаемых Media Types
    /// </summary>
    public static class MediaTypes
    {
        /// <summary>
        /// Картинка в формате JPG/JPEG
        /// </summary>
        public const string ImageJpeg = "image/jpeg";

        /// <summary>
        /// Картинка в формате PNG
        /// </summary>
        public const string ImagePng = "image/png";

        /// <summary>
        /// Объект в формате JSON
        /// </summary>
        public const string ApplicationJson = "application/json";

        /// <summary>
        /// Контент multipart/form-data
        /// </summary>
        public const string MultipartFormData = "multipart/form-data";

        /// <summary>
        /// Проверяет что контент относится к поддерживаемому типу картинок
        /// </summary>
        /// <param name="contentType">Текущий тип контента</param>
        public static bool IsImage(string contentType) =>
            new string[] { ImageJpeg, ImagePng }.Any(x => string.Equals(x, contentType));
    }
}
