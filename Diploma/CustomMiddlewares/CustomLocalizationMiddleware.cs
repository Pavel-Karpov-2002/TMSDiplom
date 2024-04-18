using Diploma.Services;
using System.Globalization;

namespace Diploma.CustomMiddlewares
{
    public class CustomLocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomLocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            CultureInfo culture;

            var authService = context.RequestServices.GetService<AuthService>();

            if (authService.IsAuthenticated())
            {
                culture = new CultureInfo(authService.GetCurrentUserProfile().PreferLocale ?? GetLocale(context));
            }
            else if (context.Request.Cookies["langues"] != null)
            {
                var localFromCookie = context.Request.Cookies["langues"];
                culture = new CultureInfo(localFromCookie);
            }
            else
            {
                culture = new CultureInfo(GetLocale(context));
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            await _next.Invoke(context);
        }

        private string GetLocale(HttpContext context)
        {
            string acceptLanguage = context.Request.Headers.AcceptLanguage;
            var locale = acceptLanguage.Substring(0, 2);
            return locale;
        }
    }
}
