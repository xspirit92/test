using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.Models;

namespace Test
{
    public class TaskController : ApiController
    {
        TaskContext taskContext = new TaskContext();

        [HttpGet]
        public List<Tasks> Get()
        {
            return taskContext.Tasks.ToList();
        }

        [HttpPost]
        public void Post([FromBody]Tasks task)
        {
            task = TestUtils.ParseDate(task);
            taskContext.Tasks.Add(task);
            taskContext.SaveChanges();
        }
    }
}