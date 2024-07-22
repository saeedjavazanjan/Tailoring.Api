using System.Runtime.InteropServices.JavaScript;

namespace Tailoring.kavehneghar;

public static class SendSMS
{

    public static async Task <String>SendSMSToUser(String token, String receptor )
    { 
        
        try {

     String apiKey = "";
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);
            var result = await api.VerifyLookup(receptor, token, "verify");
            foreach(var r in result.Message) {
                Console.Write(r+"r.Messageid.ToString()");
            }
            return "ارسال موفق";
        } catch (Exception ex) {
            Console.Write("Message : " + ex.Message);
            return ex.Message;
        }
    }
    
    
}
