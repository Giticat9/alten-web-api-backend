using System.Net;

namespace WebApi.Common
{
    /// <summary>
    /// Описывает исключение обработки запроса
    /// </summary>
    /// <remarks>
    /// Используется для назначения статуса ошибки, перехватываемой через промежуточное ПО
    /// </remarks>
    public class HttpException : Exception
    {
        /// <summary>
        /// Код статуса HTTP
        /// </summary>
        public int StatusCode { get; protected set; } = StatusCodes.Status500InternalServerError;

        /// <summary>
        /// Создает новые экземпляр исключения обработки запроса
        /// </summary>
        /// <param name="statusCode">Код статуса HTTP</param>
        public HttpException(int statusCode) 
        { 
            StatusCode = statusCode;
        }

        /// <summary>
        /// Создает новые экземпляр исключения обработки запроса
        /// </summary>
        /// <param name="statusCode"></param>
        public HttpException(HttpStatusCode statusCode)
        {
            StatusCode = (int)statusCode;
        }

        /// <summary>
        /// Создает ошибку с сообщением и статусом 500 (Internal Server Error)
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public HttpException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        /// <summary>
        /// Создает новые экземпляр исключения обработки запроса
        /// </summary>
        /// <param name="statusCode">Код статуса HTTP</param>
        /// <param name="message">Сообщение об ошибке</param>
        public HttpException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Создает новые экземпляр исключения обработки запроса
        /// </summary>
        /// <param name="statusCode">Код статуса HTTP</param>
        /// <param name="message">Сообщение об ошибке</param>
        public HttpException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = (int)statusCode;
        }

        /// <summary>
        /// Создает новые экземпляр исключения обработки запроса
        /// </summary>
        /// <param name="statusCode">Код статуса HTTP</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Внутренняя ошибка</param>
        public HttpException(int statusCode, string message, Exception innerException) : base(message, innerException) 
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Создает новые экземпляр исключения обработки запроса
        /// </summary>
        /// <param name="statusCode">Код статуса HTTP</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Внутренняя ошибка</param>
        public HttpException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = (int)statusCode;
        }

        public override string ToString() => new HttpErrorDescription(this).ToString();
    }
}
