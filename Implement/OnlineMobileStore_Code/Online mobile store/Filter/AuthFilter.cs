using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Online_mobile_store.Filter
{
    public class AuthFilter : ActionFilterAttribute
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {

                context.Result = new RedirectToActionResult("AccessError", "Account", null);


            }
        }
    }
}
