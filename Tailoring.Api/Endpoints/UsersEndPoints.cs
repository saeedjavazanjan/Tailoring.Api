using Tailoring.Entities;
using Tailoring.kavehneghar;
using Tailoring.Repository;

namespace Tailoring.Endpoints;

public static class UsersEndPoints
{

    public static RouteGroupBuilder MapUsersEndPoints(this IEndpointRouteBuilder routes){
        var group=routes.MapGroup("/users").WithParameterValidation();
        string generatedPassword = null;
        group.MapPost("/register",async (IRepository iRepository , RegisterUserDto registerUserDto)=>
        {

          //  User existedUser = await iRepository.GetRegesteredPhoneNumberAsync(registerUserDto.PhoneNumber);
           generatedPassword = GenerateRandomNo();
        String result= await SendSMS.SendSMSToUser(generatedPassword,registerUserDto.PhoneNumber);
            return Results.Ok(result);
        });
        
        group.MapPost("/registerPassword",async (IRepository iRepository ,AddUserDto addUserDto)=>{

            if (addUserDto.Password == generatedPassword && generatedPassword !=null)
            {
                
                User user = new()
                {
                    UserName = addUserDto.UserName,
                    PassWord = addUserDto.Password,
                    PhoneNumber = addUserDto.PhoneNumber,
                    Avatar = "",
                    Bio = "",
                    Bookmarks = [],
                    Followers = 0,
                    Followings = 0,
                    Likes = []
                };
               await iRepository.AddUser(user);
               return Results.Ok("با موفقیت ثبت شد.");
            }
            else
            {
                return Results.Conflict("رمز اشتباه است");

            }
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