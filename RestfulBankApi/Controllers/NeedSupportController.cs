using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulBankApi.Data;
using RestfulBankApi.Models;

namespace RestfulBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NeedSupportController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AccountTransaction AccountTransaction = new AccountTransaction();
        private readonly RandomIBANSeeder _randomIBANSeeder;
        public NeedSupportController(AppDbContext context, IMapper mapper, IConfiguration configuration, RandomIBANSeeder randomIBANSeeder)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _randomIBANSeeder = randomIBANSeeder;
        }
        [HttpPost("createSupport")]
        public IActionResult CreateSupport([FromBody] SupportMessagesDto model)
        {

            var newMessage = _mapper.Map<SupportMessages>(model);
            newMessage.Active = true;
        
            _context.SupportMessages.Add(newMessage);
            _context.SaveChanges();

            return Ok("Talebiniz alınmıştır. En kısa sürede tarafınıza dönüş yapılacaktır");
        }
    }
}
