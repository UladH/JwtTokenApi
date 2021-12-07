using Microsoft.AspNetCore.Identity;

namespace AppExceptions.Identity
{
    public class IdentityException : Exception
    {
        #region constructor

        public IdentityException(string message = "Identity runtime error")
        : base(message)
        {
        }

        public IdentityException(IdentityResult result) 
            :this(GetErrorsFromResult(result))
        {
        }

        #endregion

        #region private

        private static string GetErrorsFromResult(IdentityResult result) { 
            if(result.Errors == null || result.Errors.Count() == 0)
            {
                return null;
            }

            var error = result.Errors.Aggregate("", (current, next) => string.Format("{0} {1}", current, next.Description));

            return error;
        }

        #endregion
    }
}
