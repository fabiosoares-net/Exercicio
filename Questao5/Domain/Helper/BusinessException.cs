using System.Globalization;

namespace Questao5.Domain.Helper
{
    public class BusinessException : Exception
    {
        public ResponseError responseError;
        public BusinessException() : base() { }

        public BusinessException(ResponseError responseError) 
        { 
            this.responseError = responseError;
        }
        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
