using ControleRemessaModelo.API.Enums;
using ControleRemessaModelo.API.Utils;

namespace ControleRemessaModelo.API.Excecoes
{
    public class CustomException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public CustomException(ErrorCode errorCode) : base(ErrorMessages.GetMessage(errorCode))
        {
            ErrorCode = errorCode;
        }
    }
}
