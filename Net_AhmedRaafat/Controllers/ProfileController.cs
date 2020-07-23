using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net_AhmedRaafat.BL;
using Net_AhmedRaafat.Manager;
using Net_AhmedRaafat_Entities;
using Net_AhmedRaafat_Repository;

namespace Net_AhmedRaafat.Controllers
{

    [Produces("application/json")]
    [Route("api/Profile")]
    public class ProfileController : Controller
    {
        private static HttpClient client;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IEmailSender _emailSender;

        private readonly ProfileManager _profileManager;
        private IBaseRepository<ApplicationUserDtocs> _profileRepository;
        private ConfigurationsManager _configurationsManager;


        public ProfileController(IBaseRepository<ApplicationUserDtocs> profileRepository, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {

            _configurationsManager = new ConfigurationsManager();
            _emailSender = emailSender;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            //_profileManager = profileManager;
            _profileRepository = profileRepository;
            _profileManager = new ProfileManager(_profileRepository, _mapper, _userManager, _signInManager);

        }


        private static void PrepareClient(string Token)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var token = Token;
            //var token = "/4654AHROM;WPdsfjkghJFHFKFKZXVkgkjglk/*fgkl78878mEWJKFfskl$#@l,sdzcx,DKFLJOEI..,cv;l!!!/*-=za23";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var allUsers = _profileManager.GetAllUser();

            if (allUsers != null)
                return Ok(allUsers);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            var user = _profileManager.GetUserByID(id);

            if (user != null)
                return Ok(user);

            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            var user = _profileManager.Login(loginModel);

            if (user != null)
                return Ok(user);

            return NotFound();

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody]ApplicationUserDtocs userModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = await _profileManager.InsertUser(userModel);
            registerResult registerResult = new registerResult();
            registerResult.success = res;

            if (res)
            {
                registerResult.token = _profileManager.GetAuthToken(userModel, userModel.PasswordHash);
                registerResult.id = userModel.Id;    
            }


            return Ok(registerResult);
        }


        [HttpGet]
        [Route("~/confirmMail/{id}")]
        public async Task<IActionResult> ConfirmMail(Guid id)
        {

            var res = await _profileManager.confirmMail(id);

            if (res)
                return Redirect(_configurationsManager.redirectAfterConfirmMail);

            return Redirect(_configurationsManager.SiteUrl);
        }

        [HttpGet("sendConfirm/{id}")]
        public IActionResult SendConfirmAgain(Guid id)
        {
            var userModel = _profileManager.GetUserByID(id);
            if (userModel != null)
            {
                var url = _configurationsManager.ApiUrl;
                string htmlString = $"<html> <body> Please confirm your account by <a href='{HtmlEncoder.Default.Encode(url + "confirmMail/" + userModel.Id)}'>clicking here</a>.</body> </html>";


                var result = _emailSender.SendEmail("confirm your mail", htmlString, "tt436209@gmail.com", "Test Mail", userModel.Email, userModel.firstName + " " + userModel.lastName,
                                        null, null, null, null, null,
                                        null, 0, null
                                        );

                if (result)
                    return Ok(true);
            }


            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(Guid id, [FromBody]ApplicationUserDtocs userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = _profileManager.UpdateProfile(id, userInfo);

            if (res.Result)
                return Ok(true);

            return NotFound();
        }



    }
}
