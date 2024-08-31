using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingApp.Models;

namespace BankingApp.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase{
        private readonly BankingAppContext _context;

        public UserController(BankingAppContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            Console.WriteLine("user is found: " + user.Username);
            return user;
        }

        [HttpGet("login")]
        public async Task<ActionResult<IEnumerable<UserDto>>> LoginUser([FromQuery]User userLogin)
        {   
            var user = await _context.Users.SingleOrDefaultAsync(user => user.Username == userLogin.Username);

            if (user == null) {
                Console.WriteLine("null user");
                return NotFound();
            }

            bool validPasword = BCrypt.Net.BCrypt.EnhancedVerify(userLogin.Password, user.Password);
           
            if (validPasword) {
                UserDto userDto = new(user.Id, user.Username);
                
                return Ok(userDto);
            } else {
                return NotFound();
            }            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user) {
            if (id != user.Id) {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try{
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException){
                if (!UserExists(id)){
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutFunds(PaymentDto paymentInfo) {
            if (!UserExists(paymentInfo.UserId)) {
                return BadRequest();
            }



            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateException) {
                 if (!UserExists(paymentInfo.UserId)){
                    return NotFound();
                } else {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(LoginDto registerCredentials) {
            User user = new User();
            user.Username = registerCredentials.Username;
            user.Password = registerCredentials.Password;

            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 10);
            user.Password = passwordHash;
            Account checkingAccount = new(user.Id, true);
            Account savingsAccount = new(user.Id, false);
            user.Accounts.Add(checkingAccount);
            user.Accounts.Add(savingsAccount);
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            Console.WriteLine(user.Id);

            CreatedAtActionResult result = CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id) {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id){
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
