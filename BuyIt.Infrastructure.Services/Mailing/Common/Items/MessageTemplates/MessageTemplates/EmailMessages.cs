namespace BuyIt.Infrastructure.Services.Mailing.Common.Items.MessageTemplates.MessageTemplates;

public abstract class EmailMessages
{
    public const string VerificationRequest =
        "Congratulations and welcome to BuyIt! We're excited to have you on board and thank you" +
        " for choosing us.<br><br>Your registration process is almost complete, but before we get started," +
        " we need to confirm your email address. Please click the button below to verify your account." +
        "<br><br>If you are unable to click the link, you can copy and paste it into your browser's address" +
        " bar.<br><br>Once your email address is verified, you'll gain full access to your account.<br><br>Thank" +
        " you for joining BuyIt!";

    public const string SuccessfulVerification =
        "Congratulations! We're excited to inform you that you have successfully verified your email " +
        "address.<br><br>Your registration process is complete and you are ready to go shopping at BuyIt!" +
        "<br><br>We hope that our marketplace will be able to offer you everything what you need!<br><br>" +
        "Happy purchases and enjoy your time at BuyIt!";
}