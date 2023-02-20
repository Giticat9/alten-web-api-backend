namespace WebApi.Base
{
    public abstract class BaseApiModel
    {
        /// <summary>
        /// Сообщение об ошибки. В случае успешного запроса - пустая строка
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// Флаг корректности обработки запроса
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}