using Twilio.Rest.Api.V2010.Account;

namespace BloodBank_EELU.Services
{
    public interface ISMSServices
    {
        MessageResource Send(string mobileNumber, string body);
    }
}
