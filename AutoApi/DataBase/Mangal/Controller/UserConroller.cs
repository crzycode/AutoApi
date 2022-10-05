using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewJsonCrud.DataBase.Mangal.Model;

namespace NewJsonCrud.DataBase.Mangal.Controller
{

[Route("api/Mangal/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
[HttpPost("created")]
public dynamic hey(User u){

return "dfd";
}
}
}
