using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Net_AhmedRaafat.BL;
using Net_AhmedRaafat_Entities;
using Net_AhmedRaafat_Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Net_AhmedRaafat.Manager
{
    public class ProfileManager
    {
        private IBaseRepository<ApplicationUserDtocs> _profileRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public ProfileManager(IBaseRepository<ApplicationUserDtocs> profileRepository, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;

        }


        public List<ApplicationUserDtocs> GetAllUser()
        {
            try
            {
                var AppUsers = _userManager.Users.Where(u => !u.isDeleted).ToList();
                var usersList = _mapper.Map<List<ApplicationUserDtocs>>(AppUsers);

                return usersList;
            }
            catch
            {
                return null;
            }

        }

        public ApplicationUserDtocs GetUserByID(Guid id)
        {
            try
            {
                var user = _userManager.Users.Where(i => i.Id == id && !i.isDeleted).FirstOrDefault();
                var userModel = _mapper.Map<ApplicationUserDtocs>(user);

                return userModel;
            }
            catch
            {
                return null;
            }


        }


        public ApplicationUserDtocs Login(LoginModel loginModel)
        {
            try
            {

                var user = _userManager.Users.Where(i => i.Email == loginModel.emailLogin && !i.isDeleted).FirstOrDefault();

                if (user != null)
                {
                    var result = _signInManager.PasswordSignInAsync(user, loginModel.passwordLogin, false, false).Result;


                    if (result.Succeeded)
                    {
                        var userModel = _mapper.Map<ApplicationUserDtocs>(user);
                        userModel.Token = GetAuthToken(userModel, loginModel.passwordLogin);

                        return userModel;

                    }

                    return null;
                }

                return null;
            }
            catch
            {
                return null;
            }


        }

        public async Task<bool> InsertUser(ApplicationUserDtocs userModel)
        {
            userModel.Id = Guid.NewGuid();
            userModel.CreatedDate = DateTime.Now.Date;

            try
            {
                var user = _mapper.Map<ApplicationUser>(userModel);

                var result = await _userManager.CreateAsync(user, userModel.PasswordHash);

                if (result.Succeeded)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }

        }



        public async Task<bool> UpdateProfile(Guid id, ApplicationUserDtocs input)
        {
            ApplicationUser user = _userManager.Users.Where(i => i.Id == id && !i.isDeleted).FirstOrDefault();
            if (user != null)
            {
                user.firstName = input.firstName;
                user.lastName = input.lastName;
                user.PhoneNumber = input.PhoneNumber;
                user.PasswordHash = input.PasswordHash;
                user.UserName = input.UserName;
                user.Email = input.Email;
                //user.EmailConfirmed = input.EmailConfirmed;
                user.isDeleted = input.isDeleted;


                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return true;
                else
                    return false; ;

            }

            return false;
        }



        public async Task<bool> confirmMail(Guid userId)
        {
            ApplicationUser user = _userManager.Users.Where(i => i.Id == userId && !i.isDeleted).FirstOrDefault();
            if (user != null)
            {
                user.EmailConfirmed = true;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return true;
                else
                    return false; ;

            }

            return false;

        }


        public string GetAuthToken(ApplicationUserDtocs profile, string password)
        {
            var confgManager = new ConfigurationsManager();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, profile.firstName+profile.lastName),
                    new Claim(ClaimTypes.PrimarySid,profile.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(confgManager.Token)), SecurityAlgorithms.HmacSha256Signature)
            };

            //foreach (var role in roles)
            //    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }


        private string GetHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder result = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    result.Append(data[i].ToString("X2"));
                }
                return result.ToString();
            }

        }

        private bool VerifyHash(string input, string hash)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Hash the input. 
                string hashOfInput = GetHash(input);

                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64Encode(string password)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

}
