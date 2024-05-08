using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Account;
using Lib_Models.Models_Select.Login;
using Lib_Repository.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_Public_Api
{
    [Route("test-api")]
    [ApiController]
    public class TestSqlApiController : ControllerBase
    {
        IRepository<tbAccount> _repository;
        public TestSqlApiController(IRepository<tbAccount> repository)
        {
            _repository = repository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> TestLogin(Login_Select_v1 login_)
        {
            try
            {

                var account = await _repository.GetAll(a => a.user_Name == login_.user_Name);
                var accountFist = account.First();
                bool veryHassPass = BCrypt.Net.BCrypt.Verify(login_.user_Password, accountFist.user_Password);
                if (!veryHassPass)
                {
                    return BadRequest("Sai pass");
                }
                return Ok(accountFist);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                throw;
            }
        }

        private Account_Login_Select_v1 CheckPasswordAccount(string userPass, string userPassAccount)
        {
            bool veryHassPass = BCrypt.Net.BCrypt.Verify(userPass, userPassAccount);

            if (!veryHassPass)
            {
                return new Account_Login_Select_v1
                {
                    StatusBool = false,
                    StatusType = "Mật khẩu không chính xác"
                };
            }
            return null!;
        }
    }
}
