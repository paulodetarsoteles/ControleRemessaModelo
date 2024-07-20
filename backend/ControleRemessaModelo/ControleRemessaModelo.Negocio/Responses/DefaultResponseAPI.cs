namespace ControleRemessaModelo.Negocio.Responses
{
    public class DefaultResponseAPI
    {
        public bool Success { get; set; } = false;
        public int StatusCode { get; set; } = 500;
        public List<ErrorMessageResponseAPI> Errors { get; set; } = [];
        public List<object> BodyResponse { get; set; } = [];

        public DefaultResponseAPI() { }

        public DefaultResponseAPI(List<ErrorMessageResponseAPI>? errors, List<object> bodyResponse, bool success = false, int statusCode = 500)
        {
            Errors = errors ?? [];
            BodyResponse = bodyResponse ?? [];
            Success = success;
            StatusCode = statusCode;
        }
    }
}
