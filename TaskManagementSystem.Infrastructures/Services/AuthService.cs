using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Interface.Dtos;
using TaskManagementSystem.Interface.Repositories;

namespace TaskManagementSystem.Infrastructures.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        #region Add Role
        public async Task<string> AddRoleAsync(AddRoleModel roleModel)
        {
            var user = await _userManager.FindByIdAsync(roleModel.UserId);
            if (user is null || !await _roleManager.RoleExistsAsync(roleModel.Role))
                return "Invalid user ID or Role";

            bool roleExisted = await _userManager.IsInRoleAsync(user,roleModel.Role);
            if (roleExisted)
                return "User already assigned to this role";
            
            var result =  await _userManager.AddToRoleAsync(user,roleModel.Role);
            if (result.Succeeded)
                return string.Empty;

            var errors = string.Join(", ",result.Errors.Select(idx=>idx.Description));
            return $"Failed to assign role: {errors}";


        }
        #endregion


        #region  Login
        public async Task<AuthModel> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return new AuthModel { IsAuthenticated = false, Message = "Username or Password is incorrect" };

            // التأكد من تأكيد البريد الإلكتروني قبل السماح بتسجيل الدخول
            if (!user.EmailConfirmed)
                return new AuthModel { IsAuthenticated = false, Message = "Please confirm your email before logging in." };

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthModel { IsAuthenticated = false, Message = "Username or Password is incorrect" };

            string jwtToken = await CreateJwtToken(user);

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenExpiryTime = refreshToken.ExpireDate;

            await _userManager.UpdateAsync(user);

            return new AuthModel
            {
                IsAuthenticated = true,
                Message = "Login successful",
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserId = user.Id, 
                UserName = user.UserName
            };

        }

        #endregion

        #region Create JWT 

        private async Task<string> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            List<Claim> roles = new();
            foreach (var role in userRoles)
                roles.Add(new Claim("role", role));

            IEnumerable<Claim> claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub,user.UserName??""),
                 new Claim(JwtRegisteredClaimNames.Email,user.Email ?? ""),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Name,user.FirstName+user.LastName),
                 new Claim("UID",user.Id),

             }
            .Union(userClaims)
            .Union(roles);



            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwt.Key));
            SigningCredentials signingCredentials = new(key: key, algorithm: SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurity = new(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                expires: DateTime.UtcNow.AddHours(2),
                claims: claims,
                signingCredentials: signingCredentials


            );


            string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurity);
            return jwtToken;
           
        }
        #endregion

        #region Registeration
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel
                {
                    IsAuthenticated = false,
                    Message = "Email is already registered!"

                };
            }

            if(await _userManager.FindByNameAsync(model.UserName) is not null)
            {
                return new AuthModel
                {
                    IsAuthenticated = false,
                    Message = "Email is already registered!"

                };
            }
            User user = new User
            {
                FullName = model.FirstName + model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            IdentityResult result =  await _userManager.CreateAsync(user,model.Password);
            if (!result.Succeeded)
            {
                StringBuilder errors = new();

                foreach (var error in result.Errors)
                    errors.Append($"{error.Description},");

                return new AuthModel { IsAuthenticated = false, Message = errors.ToString() };
            }
            if(!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(user,"User");

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthModel
            {
                IsAuthenticated = true,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles.ToList(),

            };

        }
        #endregion
     

        #region Refresh Token
        private RefreshTokenRequestModel GenerateRefreshToken()
        {
            return new RefreshTokenRequestModel
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpireDate = DateTime.UtcNow.AddDays(7),
            }; 
        }
        #endregion
    }
}
