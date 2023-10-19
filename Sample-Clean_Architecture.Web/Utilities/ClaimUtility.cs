using System.Security.Claims;

namespace Sample_Clean_Architecture.Web.Utilities
{
    public static class ClaimUtility
    {
        public static int? GetUserId(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                int userId = int.Parse(claimsIdentity!.
                    FindFirst(ClaimTypes.NameIdentifier)!.Value);
                return userId;
            }
            catch (Exception)
            {

                return null;
            }

        }


        public static string GetUserEmail(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                return claimsIdentity.FindFirst(ClaimTypes.Email).Value;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static string GetUserName(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                return claimsIdentity.FindFirst(ClaimTypes.Name).Value;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public static List<string> GetRoles(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                List<string> roles = new List<string>();
                foreach (var item in claimsIdentity.Claims.Where(p => p.Type.EndsWith("role")))
                {
                    roles.Add(item.Value);
                }
                return roles;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
