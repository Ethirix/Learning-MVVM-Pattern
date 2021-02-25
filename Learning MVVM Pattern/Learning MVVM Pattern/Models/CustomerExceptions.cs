namespace Learning_MVVM_Pattern.Models
{
    using System;
    internal static class CustomerExceptions
    {
        public class InputLengthIsZero : Exception
        {
            public override string ToString()
            {
                return "Exception: InputLengthIsZero";
            }

            public override string Message => "Input Length is zero, which is invalid";
        }
    }
}