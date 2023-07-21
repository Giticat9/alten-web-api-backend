using System.Text.Json;

namespace WebApi.Common
{
    /// <summary>
    /// Класс, описывающий тело ответа от сервера при возникновении ошибки
    /// </summary>
    /// <remarks>
    /// Описывает объект ошибки, получаемый на клиенте
    /// </remarks>
    public class HttpErrorDescription
    {
        /// <summary>
        /// Создаёт экземпляр описания HTTP ошибки
        /// </summary>
        public HttpErrorDescription()
        {
            
        }

        /// <summary>
        /// Создаёт экземпляр описания HTTP ошибки на основе полученной ошибки
        /// </summary>
        /// <param name="exception"></param>
        public HttpErrorDescription(HttpException exception)
        {
            StatusCode = exception.StatusCode;
            Message = exception.Message;
            StackTrace = exception.StackTrace ?? string.Empty;
        }

        /// <summary>
        /// Код состояния HTTP
        /// </summary>
        public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public object? Message { get; set; }

        /// <summary>
        /// Детальная информация об ошибке
        /// </summary>
        public string? StackTrace { get; set; } = string.Empty;

        public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}
