using Tailoring.Entities;
using Tailoring.kavehneghar;
using Tailoring.Repository;

namespace Tailoring.Endpoints;

public static class UsersEndPoints
{
    private const  string CreatedUser="User";
    private const string GetUser = "getUser";
    public static RouteGroupBuilder MapUsersEndPoints(this IEndpointRouteBuilder routes){
        var group=routes.MapGroup("/users").WithParameterValidation();
        string generatedPassword  = null;
        
        group.MapGet("/oneUser",async (IRepository repository,int id)=> 
            {
                User? user = await repository.GetUserAsync(id);
                return user is not null ? Results.Ok(user.AsDto()):Results.NotFound();
            }
        ).WithName(GetUser);
        
        
        group.MapPost("/register",async (IRepository iRepository , RegisterUserDto registerUserDto)=>
        {
            generatedPassword = "1234";  //GenerateRandomNo();
            
            User? existedUser = await iRepository.GetRegesteredPhoneNumberAsync(registerUserDto.PhoneNumber);
            if (existedUser is not null )
            {
                return Results.Json("با این شماره قبلا ثبت نام صورت گرفته است.");
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
        group.MapPost("/loginPasswordRequest", async (IRepository iRepository, RegisterUserDto registerUserDto) =>
              {
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
              }) .RequireRateLimiting("fixed");
      
        group.MapPost("/loginPasswordCheck", async (IRepository iRepository, AddUserDto addUserDto) =>
        {
            if (addUserDto.Password == generatedPassword && generatedPassword != null)
            {
                
                User? regesterdeUser = await iRepository.GetRegesteredPhoneNumberAsync(addUserDto.PhoneNumber);

                if (regesterdeUser is not null)
                {
                   
                    return Results.CreatedAtRoute(GetUser,new {regesterdeUser.UserId},regesterdeUser);
                }

                return Results.NoContent();
            }
            else
            {
                return Results.Conflict("رمز اشتباه است");

            }
        });
        
        
        group.MapPost("/registerPassword",async (IRepository iRepository ,AddUserDto addUserDto)=>{

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
                   
                    return Results.Conflict("با این شماره قبلا ثبت نام صورت گرفته است.");

                }
                else
                {
                    await iRepository.AddUser(user);
                    return Results.CreatedAtRoute(GetUser,new {user.UserId},user);
                }
            }
            else
            {
                return Results.Conflict("رمز اشتباه است");

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