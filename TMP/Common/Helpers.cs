namespace TMP.Common
{
    using System;
    using System.Web.Helpers;

    public sealed class Helpers
    {
        #region Singleton constructor
        private static readonly Lazy<Helpers> _instance = new Lazy<Helpers>(() => new Helpers());
        public static Helpers Instance
        {
            get
            {
                return _instance.Value;
            }
        }            
        private Helpers()
        { }
        #endregion

        public string GetAntiForgeryToken()
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }
    }
}