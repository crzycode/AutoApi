using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewJsonCrud.DataBase.Mangal.Model;

namespace NewJsonCrud.DataBase.Mangal.Controller
{

[Route("api/Mangal/[controller]")]
[ApiController]
public class rakeshController : ControllerBase
{
[HttpPost("created")]
public dynamic hey(rakesh u){

return "dfd";
}
}
}
