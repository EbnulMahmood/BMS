namespace BMS.BlazorWebApp.Helpers
{
    public static class WebHelper
    {
        public static string SetExceptionMessage(Exception ex, string moreInfo = "")
        {
            Type exceptionType = ex.GetType();
            string errorCode;
            if (exceptionType == typeof(Exception))
                errorCode = "1000";
            else if (exceptionType == typeof(SystemException))
                errorCode = "1001";
            else if (exceptionType == typeof(AccessViolationException))
                errorCode = "1002";
            else if (exceptionType == typeof(ArgumentException))
                errorCode = "1003";
            else if (exceptionType == typeof(ArgumentNullException))
                errorCode = "1004";
            else if (exceptionType == typeof(ArgumentOutOfRangeException))
                errorCode = "1005";
            else if (exceptionType == typeof(ArithmeticException))
                errorCode = "1006";
            else if (exceptionType == typeof(ArrayTypeMismatchException))
                errorCode = "1007";
            else if (exceptionType == typeof(DivideByZeroException))
                errorCode = "1008";
            else if (exceptionType == typeof(IndexOutOfRangeException))
                errorCode = "1009";
            else if (exceptionType == typeof(InvalidCastException))
                errorCode = "1010";
            else if (exceptionType == typeof(InvalidOperationException))
                errorCode = "1011";
            else if (exceptionType == typeof(MissingMemberException))
                errorCode = "1012";
            else if (exceptionType == typeof(NotFiniteNumberException))
                errorCode = "1013";
            else if (exceptionType == typeof(NotSupportedException))
                errorCode = "1014";
            else if (exceptionType == typeof(NullReferenceException))
                errorCode = "1015";
            else if (exceptionType == typeof(OutOfMemoryException))
                errorCode = "1016";
            else if (exceptionType == typeof(StackOverflowException))
                errorCode = "1017";
            else
                errorCode = "9999";

            if (!string.IsNullOrWhiteSpace(errorCode))
            {
                errorCode = $"Error code: {errorCode}.";
            }

            string exceptionMessage = errorCode + moreInfo;
            if (errorCode == "9999")
            {
                exceptionMessage = $"Something went wrong. {exceptionMessage}";
            }
            return exceptionMessage;
        }
    }
}
