using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            
            //return new SearchService().Search();
            //return new[] {new TaskDocument() {action = "test"}};
            HttpClient c = new HttpClient();
            var resp = c.GetStringAsync("http://localhost:9200").Result;
            return resp;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
