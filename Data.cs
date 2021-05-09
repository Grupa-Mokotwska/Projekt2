using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MeowUtils.Database;

namespace Project2 {
    public class Data {
        public static Database Db = new Database("data.db");

        public static void OnStart() {
            Db.CreateIfNotExists();
            Db.NewQuery(Queries.ActivateWAL, Database.ResponseType.Null);
            Db.NewQuery(Queries.CreateDataTableQuery, Database.ResponseType.Null);
        }

        public static User GetUser(string login, string password) {
            var user = new User();
            var result = (SQLiteDataReader) Db.NewQuery($"select * from users where login='{login}' and password='{password}'", Database.ResponseType.SqliteDataReader);
            while (result.Read()) {
                user.Login = result.GetString(result.GetOrdinal("login"));
                user.Password = result.GetString(result.GetOrdinal("password"));
                user.Message = result.GetString(result.GetOrdinal("message"));
            }
            return user;
        }
    }

    public class Queries {
        public static readonly string ActivateWAL = "PRAGMA journal_mode=WAL;";
        public static readonly string CreateDataTableQuery = @"create table if not exists users(
  id integer primary key,
  login text unique not null,
  password text not null,
  message text not null
);";
    }
}
