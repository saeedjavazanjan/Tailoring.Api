using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Tailoring.Authentication;
using Tailoring.Entities;
using Tailoring.kavehneghar;
using Tailoring.Repository;
using Tailoring.Response;

namespace Tailoring.Endpoints;

public static class UsersEndPoints
{
    private const  string CreatedUser="User";
    private const string GetUser = "getUser";

    
    public static RouteGroupBuilder MapUsersEndPoints(this IEndpointRouteBuilder routes){
        var group=routes.MapGroup("/users").WithParameterValidation();
        string generatedPassword  = null;
        
        group.MapGet("/currentUser",async (
                IRepository repository,
                ClaimsPrincipal user
                )=> 
            {
                var userId= user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    User? currentUser = await repository.GetUserAsync(Int32.Parse(userId));
                    return currentUser is not null ? 
                        Results.Ok(currentUser.AsDto()) : Results.NotFound(new{error="کاربر یافت نشد."});
                }
                return Results.Conflict(new{error="کاربر یافت نشد."});
 
            }
        ).WithName(GetUser).RequireAuthorization();
        
        
        group.MapPost("/register",async (
            IRepository iRepository , 
            RegisterUserDto registerUserDto)=>
        {
            generatedPassword = "1234";  //GenerateRandomNo();
            
            User? existedUser = await iRepository.GetRegesteredPhoneNumberAsync(registerUserDto.PhoneNumber);
            if (existedUser is not null )
            {
                return Results.Conflict(new{error="با این شماره قبلا ثبت نام صورت گرفته است."});
            }
            else
            {

                String result= await SendSMS.SendSMSToUser(generatedPassword,registerUserDto.PhoneNumber);

              //  if (result.Equals("ارسال موفق"))
            //    {
                    return Results.Ok(result);
           //    }
               // else
            //    {
               //    return Results.Json(result);
           //    }
                
            }
        }) .RequireRateLimiting("fixed");
        
        group.MapPost("/loginPasswordRequest", async (
            IRepository iRepository,
            RegisterUserDto registerUserDto) =>
              {
                  User? regesterdeUser = await iRepository.GetRegesteredPhoneNumberAsync(registerUserDto.PhoneNumber);

                  if (regesterdeUser is not null)
                  {
                      generatedPassword = "1234";
                      // generatedPassword =  GenerateRandomNo();
                      String result= await SendSMS.SendSMSToUser(generatedPassword,registerUserDto.PhoneNumber);
                      //  if (result.Equals("ارسال موفق"))
                      // {
                      return Results.Ok(result);
                      //  }
                  
                      //   else
                      //   {
                      return Results.Json(result);
                      //   }
                  }
                  return Results.NotFound(new { error="شما ثبت نام نکرده اید."});
                  
              }) .RequireRateLimiting("fixed");
      
        group.MapPost("/loginPasswordCheck", async (
            IJwtProvider iJwtProvider,
            IRepository iRepository,
            AddUserDto addUserDto
            ) =>
        {
            if (addUserDto.Password == generatedPassword && generatedPassword != null)
            {
                
                User? regesterdeUser = await iRepository.GetRegesteredPhoneNumberAsync(addUserDto.PhoneNumber);

                if (regesterdeUser is not null)
                {
                    var token=  await iJwtProvider.Generate(regesterdeUser);
                    // var userData=Results.CreatedAtRoute(GetUser,new {user.UserId},user);
                    return Results.Ok(new{token=token,userData=regesterdeUser});
                    
                }

                return Results.NotFound(new { error="شما ثبت نام نکرده اید."});

            }
            else
            {
                return Results.Conflict(new { error="رمز اشتباه است"});
            }
        });
        
        
        group.MapPost("/registerPasswordCheck",async (
            IJwtProvider iJwtProvider,
            IRepository iRepository,
            AddUserDto addUserDto
            )=>{

            if (addUserDto.Password == generatedPassword && generatedPassword != null)
            {

                User user = new()
                {
                    UserName = addUserDto.UserName,
                    PssWord = addUserDto.Password,
                    PhoneNumber = addUserDto.PhoneNumber,
                    Avatar = "",
                    Bio = "",
                    Bookmarks = [],
                    Followers = 0,
                    Followings = 0,
                    Likes = []
                };

                User? regesterdeUser = await iRepository.GetRegesteredPhoneNumberAsync(addUserDto.PhoneNumber);

                if (regesterdeUser is not null)
                {
                   
                    return Results.Conflict(new{error="با این شماره قبلا ثبت نام صورت گرفته است."});

                }
                else
                {
                    await iRepository.AddUser(user);
                 var token=  await iJwtProvider.Generate(user);
                    // var userData=Results.CreatedAtRoute(GetUser,new {user.UserId},user);
                  return Results.Ok(new{token=token,userData=user});
                }
            }
            else
            {
                return Results.Conflict(new { error="رمز اشتباه است"});

            }
        });
        
        group.MapDelete("/{id}",async (IRepository repository,int id)=>
        {
            User? user =await repository.GetUserAsync(id);

            if(user is not null){
                await repository.DeleteUser(id); 
            }
            return Results.NoContent();   
        });

        group.MapPut("/updateUser", async (
            IRepository iRepository,
            IFileService iFileService,
            ClaimsPrincipal? user,
          [FromForm] UserUpdateDto userUpdateDto
            ) => {
            var userId= user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId!= null)
            {
                User? currentUser = await iRepository.GetUserAsync(Int32.Parse(userId));
                if(currentUser==null){
                    return Results.NotFound(new{error="کاربر یافت نشد."}); 
                }

                if (userUpdateDto.AvatarFile != null)
                {
                    iFileService.DeleteAvatar(currentUser.Avatar);
                    var fileResult = iFileService.SaveAvatar(userUpdateDto.AvatarFile);
                    if (fileResult.Item1 == 1)
                    {
                        currentUser.Avatar = "http://10.0.2.2:5198/Avatars/"+fileResult.Item2; 
                    }
                   
                }

                currentUser.UserName = userUpdateDto.UserName;
                currentUser.Bio = userUpdateDto.Bio;
                await iRepository.UpdateUserAsync(currentUser);
                return Results.Ok();


            }

            return Results.Conflict(new{error="کاربر یافت نشد."});


        }).RequireAuthorization().DisableAntiforgery();
        
        /*group.MapGet("antiforgery/token", (IAntiforgery forgeryService, HttpContext context) =>
        {
            var tokens = forgeryService.GetAndStoreTokens(context);
            var xsrfToken = tokens.RequestToken!;
            return TypedResults.Content(xsrfToken, "text/plain");
        });*/
        return group;
    }
    
   
    
    private static string GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max).ToString();
    }
}