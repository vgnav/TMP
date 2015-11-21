namespace TMP.Attributes
{
    using System;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateAJAXRequest : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                ValidateRequestHeader(filterContext.HttpContext.Request);
            }
            catch (HttpAntiForgeryException)
            {
                throw new HttpAntiForgeryException("Anti forgery token not found");
            }
        }

        private void ValidateRequestHeader(HttpRequestBase request)
        {
            string cookieToken = string.Empty;
            string formToken = string.Empty;
            string tokenValue = request.Headers["RequestVerificationToken"]; // read the header key and validate the tokens.
            if (!string.IsNullOrEmpty(tokenValue))
            {
                string[] tokens = tokenValue.Split(':');
                if (tokens.Length == 2)
                {
                    cookieToken = tokens[0].Trim();
                    formToken = tokens[1].Trim();
                }
            }
            AntiForgery.Validate(cookieToken, formToken); // this validates the request token.
        }
    }
}