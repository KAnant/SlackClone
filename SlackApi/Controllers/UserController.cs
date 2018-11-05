using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlackApi.Data;
using SlackApi.Data.Entities;
using SlackApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SlackApi.Controllers
{
    [EnableCors("SlackCorsPolicy")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly SlackApiDbContext _context;
        private readonly MailService _emailService;
        public UserController(SlackApiDbContext context, MailService mailService)
        {
            _context = context;
            _emailService = mailService;
        }

        [HttpGet]
        [Route("getUsers")]
        public ActionResult<List<User>> GetUsers()
        {
            return _context.Users.ToList();
        }

        [HttpGet]
        [Route("getOwner")]
        public List<User> GetOwner()
        {
            List< User> owner = _context.Users.Where(x => Convert.ToInt32(x.Account_Type) == 0).ToList();
            return owner;
        }

        [HttpGet]
        [Route("getAdmin")]
        public List<User> GetAdmin()
        {
            List<User> admin = _context.Users.Where(x => Convert.ToInt32(x.Account_Type) == 1).ToList();
            return admin;
        }

        [HttpGet]
        [Route("getMem")]
        public List<User> GetMem()
        {
            List<User> mem = _context.Users.Where(x => Convert.ToInt32(x.Account_Type) == 2).ToList();
            return mem;
        }

        [HttpGet]
        [Route("getGuest")]
        public List<User> GetGuest()
        {
            List<User> guest = _context.Users.Where(x => Convert.ToInt32(x.Account_Type) == 3).ToList();
            return guest;
        }


        [HttpGet]
        [Route("getActive")]
        public List<User> GetActive()
        {
            List<User> active = _context.Users.Where(x => Convert.ToInt32(x.Billing_Status) == 0).ToList();
            return active;
        }

        [HttpGet]
        [Route("getInactive")]
        public List<User> GetInactive()
        {
            List<User> inactive = _context.Users.Where(x => Convert.ToInt32(x.Billing_Status) == 1).ToList();
            return inactive;
        }

        //[HttpGet]
        //[Route("ChangeToAdmin/{id}")]
        //public User ChangeToAdmin([FromRoute] int id, [FromBody] User newUser)
        //{
        //    User dbUser = _context.Users.Where(e => e.Id == newUser.Id).SingleOrDefault();
        //    dbUser.Name = newUser.Name;           
        //    dbUser.Email = newUser.Email;
        //    dbUser.Account_Type = (Account_Type)1;
        //    dbUser.Billing_Status = newUser.Billing_Status;
        //    dbUser.Authentication = newUser.Authentication;
        //    _context.SaveChanges();
        //    return newUser;
        //}

        [HttpGet]
        [Route("ChangeToAdmin/{id}")]
        public ActionResult ChangeToAdmin([FromRoute] int id)
        {
            var result = GetUsersById(id);
            var userData = ((ObjectResult)result.Result).Value as User;
            userData.Account_Type = (Account_Type)1;
            var updatedUser = UpdateUsers(userData);
            return Ok(updatedUser);
        }

        [HttpGet]
        [Route("ChangeToGuest/{id}")]
        public ActionResult ChangeToGuest([FromRoute] int id)
        {
            var result = GetUsersById(id);
            var userData = ((ObjectResult)result.Result).Value as User;
            userData.Account_Type = (Account_Type)3;
            var updatedUser = UpdateUsers(userData);
            return Ok(updatedUser);
        }

        [HttpGet]
        [Route("DeactivateAccount/{id}")]
        public ActionResult DeactivateAccount([FromRoute] int id)
        {
            var result = GetUsersById(id);
            var userData = ((ObjectResult)result.Result).Value as User;
            userData.Billing_Status = (Billing_Status)1;
            var updatedUser = UpdateUsers(userData);
            return Ok(updatedUser);
        }

        [HttpPut]
        [Route("updateUsers")]
        public IActionResult UpdateUsers([FromBody] User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return Ok(user);
        }
        [HttpGet]
        [Route("getUsersById/{id}")]
        public async Task<IActionResult> GetUsersById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPost]
        [Route("sendInvitation")]
        public ActionResult SendInvitation([FromBody] string[] emailList)
        {
            _emailService.SendInvitation(emailList);
            return Ok();
        }

    }
}
