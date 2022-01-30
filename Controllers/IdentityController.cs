// using System.Linq;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
// using Microsoft.AspNetCore.Mvc;
//
// namespace SpotOps.Api.Controllers;
//
// [Route("{controller}")]
// public class IdentityController : ControllerBase
// {
//     [HttpGet]
//     public IActionResult Get()
//     {
//         RegisterModel registerModel = RegisterModel();
//         registerModel.OnGetAsync()
//         return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
//     }
// }