using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class BaseController : Controller
    {
        public bool CheckIsRendred(HttpRequest request)
        {
            try
            {
                StringValues queryVal;

                if (request.Query.TryGetValue("IsRendred", out queryVal))
                {
                    return true;
                }
            }
            catch
            { }

            return false;
        }
    }
}
