using AutoApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public dynamic Adduser(string User)
        {
            DB.DBStore = User;
            DB.Database.Create(User);
            return "UserCreated";
        }
        [HttpPost("Model")]
        public dynamic CreateModel(string User, string ModelName)
        {
            DB.DBStore = User;

            DB.Model.Create(ModelName);
            return "ModelCreated";
        }
        [HttpPost("CreateController")]
        public dynamic CreateController(string User, string ControllerName)
        {
            DB.DBStore = User;
            DB.Controller.Create(ControllerName);
            return "success";
        }
        [HttpPost("AddProperty")]
        public dynamic AddProperty(string User, string ModelName,string type, string name)
        {
            DB.DBStore = User;
            DB.Model.AddProperty(ModelName,type, name);
            return "hey";
        }
    }
}
