using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestDAL.Model;
using TestDAL.Repository;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        IRepository<Manager> managerRepository;
        public ManagerController(IRepository<Manager> managerRepository)
        {
            this.managerRepository = managerRepository;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Manager> Get(int id)
        {
            return managerRepository.Get(mgr => mgr.UserId == id).Single();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Manager manager)
        {
            managerRepository.Add(manager);
            managerRepository.Save();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Manager manager)
        {
            var dbManagerObject = managerRepository.Get(mgr => mgr.UserId == id).Single();
            dbManagerObject = manager;
            managerRepository.Save();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var dbManagerObject = managerRepository.Get(mgr => mgr.UserId == id).Single();
            managerRepository.Delete(dbManagerObject);
        }
    }
}
