using Tailoring.Entities;
using Tailoring.kavehneghar;
using Tailoring.Repository;

namespace Tailoring.Endpoints;

public static class UsersEndPoints
{

    public static RouteGroupBuilder MapUsersEndPoints(this IEndpointRouteBuilder routes){
        var group=routes.MapGroup("/users").WithParameterValidation();
        string generatedPassword = GenerateRandomNo();
        group.MapPost("/register",async (IRepository iRepository , RegisterUserDto registerUserDto)=>
        {

            User existedUser = iRepository.GetRegesteredPhoneNumberAsync(registerUserDto.PhoneNumber);
            
        String result= await SendSMS.SendSMSToUser("رمز شما در خیاط باشی"+generatedPassword+"می باشد",registerUserDto.PhoneNumber);
            return Results.Ok(result);
        });
        
        group.MapPost("/registerPassword",async (IRepository iRepository ,AddUserDto addUserDto)=>{

            if (addUserDto.Passworsd == generatedPassword)
            {
                
                User user = new()
                {
                    UserName = addUserDto.UserName,
                    PassWord = addUserDto.Passworsd,
                    PhoneNumber = addUserDto.PhoneNumber,
                    Avatar = "",
                    Bio = "",
                    Bookmarks = [],
                    Followers = 0,
                    Followings = 0,
                    Likes = []
                };
                iRepository.AddUser(user);
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