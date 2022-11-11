namespace ShopManagmentAPI.domain.service.email;

public interface IEmailSender
{
    public void sendEmail(string email, string title, string message);
}
