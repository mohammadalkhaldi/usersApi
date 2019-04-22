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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using MongoDB.Bson;

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

        public User Get(int id)
        {
            return _users.Find<User>(user => user.id == id).FirstOrDefault();
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(int id, User UpdatedUser)
        {
            _users.ReplaceOne(user => user.id == id, UpdatedUser);
        }

        public void Remove(int id)
        {
            _users.DeleteOne(user => user.id == id);
        }

        public List<User> Search(IFormCollection data)
        {
            StringValues text;
            FilterDefinition<User> filter = FilterDefinition<User>.Empty;
            if (data.TryGetValue("name",out text))
            {
                filter = filter & new FilterDefinitionBuilder<User>().Eq("name", text.First());
            }
            if (data.TryGetValue("username", out text))
            {
    
                filter = filter & new FilterDefinitionBuilder<User>().Eq("username", text.First());
            }
            if (data.TryGetValue("zipcode", out text))
            {       
                filter = filter & Builders<User>.Filter.Eq(user => user.address.zipcode, text.First());
            }
            if (data.TryGetValue("companyName", out text))
            {
                filter = filter & Builders<User>.Filter.Eq(user => user.company.name, text.First());
            }

            return _users.Find(filter).ToList();
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
