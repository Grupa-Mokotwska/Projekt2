using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project2 {
    public class User {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }

        public string ToJson() {
            return JsonConvert.SerializeObject(this);
        }

        public User FromJson(string json) {
            var user = new User();

            try { 
                user = JsonConvert.DeserializeObject<User>(json);
                this.Login = user.Login;
                this.Password = user.Password;
                this.Message = user.Message;
            } catch (Exception) { }

            return user;
        }
    }
}