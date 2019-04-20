using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using usersApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RestSharp;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;

namespace usersApi.Services
{
    public class UserService
    {

        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("usersDb"));
            var database = client.GetDatabase("usersDb");
            _users = database.GetCollection<User>("Users");
        }

       

        public List<User> Get()
        {
            return _users.Find(user => true).ToList();
        }
        public List<User> AddAllUsers()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/users");

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);
            var Users = JsonConvert.DeserializeObject<List<User>>(response.Content);
            if (_users.Count(user => true) == 0)
            {
                _users.InsertMany(Users);
            }
            //return response.Content;
            return _users.Find(user => true).ToList();
        }

    }
}
