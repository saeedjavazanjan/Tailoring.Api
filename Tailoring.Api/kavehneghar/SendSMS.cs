using System.Runtime.InteropServices.JavaScript;

namespace Tailoring.kavehneghar;

public static class SendSMS
{

    public static async Task <String>SendSMSToUser(String message, String receptor )
    { 
        
        try {

     String sender = "1000689696";
     String apiKey = "31586633704961526C6966716F6365766C3151522F7873466D5054577261724B6A6930716B615876334C493D";
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);
            var result =await api.Send(sender, receptor, message);
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