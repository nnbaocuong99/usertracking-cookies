using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserActionTrackingApp.Controllers
{
    public class AbstractBaseController : Controller
    {
        private static string totalVisitsCount = "total_visits_count";
        private static string sessionVisitsCount = "session_visits_count";

        private int SessionCount(string pageName)
        {

            string sessionKey = sessionVisitsCount + pageName;
            int count = HttpContext.Session.GetInt32(sessionKey) ?? 0;
            count++;
            HttpContext.Session.SetInt32(sessionKey, count);
            return count;


        }
        



        private int TotalVisitsCount(string pageName)
        {
            string visitscountJson;
            if (Request.Cookies.ContainsKey(totalVisitsCount))
            {
                visitscountJson = Request.Cookies[totalVisitsCount];
            }
            else
            {
                visitscountJson = "{}";

            }

            Dictionary<string, int> visitcounts = JsonConvert.DeserializeObject<Dictionary<string, int>>(visitscountJson);


            visitcounts.TryGetValue(pageName, out var currentcount);
            currentcount++;
            visitcounts[pageName] = currentcount;   
            visitscountJson = JsonConvert.SerializeObject(visitcounts);
            CookieOptions cookieOptions = new CookieOptions() { Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append(totalVisitsCount, visitscountJson, cookieOptions);
            return currentcount;

        }



        public string TrackingMessage(string pageName)
        {
            int totalVisitsCount = TotalVisitsCount(pageName);

            int sessionVisitsCount = SessionCount(pageName);
            return $"total visits: {totalVisitsCount} . session visits: {sessionVisitsCount}";
        }
    }
}
