using DBEntity.DBContext;
using DBEntity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SignalRAPI.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _HubContext;

        private readonly PostgreSqlDB _postgreSqlDB;
        public TestController(IHubContext<ChatHub> HubContext, PostgreSqlDB postgreSqlDB)
        {
            _HubContext = HubContext;
            _postgreSqlDB = postgreSqlDB;
        }

        [HttpGet]
        public async void test([FromQuery] string user,string message)
        {
            await Index(user, message);
        }

        public  Task Index(string user, string message)
        {
           return  _HubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        [HttpPost]
        public async Task<Student> InsertStudent([FromBody] Student student)
        {
            await _postgreSqlDB.Students.AddAsync(student);
            await _postgreSqlDB.SaveChangesAsync();
            sendtoAllClient(student);
            return student;
        }

        public void sendtoAllClient(Student student)
        {
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            string value = JsonConvert.SerializeObject(student, serializerSettings);
            //_HubContext.Clients.All.SendAsync("addStudent", value);
            _HubContext.Clients.Groups("test").SendAsync("groupmethod", "1");
        }
    }
}
