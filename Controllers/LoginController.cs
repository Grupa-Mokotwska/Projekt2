using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Project2.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase {

        [HttpPost("/api/login")]
        public ContentResult OnLogin([FromBody] object body) {
            var output = new Response{Message = "User does not exist or provided password is incorrect", Status = false};

            var json = JObject.Parse(body.ToString() ?? string.Empty);
            //Encoded to prevent SQL Injections
            var login = HttpUtility.UrlEncode(json["nickname"]?.ToString() ?? string.Empty);// ? -> Provide only if not NULL
            var pass = HttpUtility.UrlEncode(json["password"]?.ToString() ?? string.Empty);// ?? -> Provide 2nd only if 1st is NULL

            var user = Data.GetUser(login, pass);

            if (user.Message != null) {
                output.Message = user.Message;
                output.Status = true;
            }

            return base.Content(JsonConvert.SerializeObject(output), "application/json");
        }
    }
}
