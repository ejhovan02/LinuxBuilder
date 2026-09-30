public class UserSession
{
    public string userId { get; set; }
    public string Token { get; set; } = "";
    public string Email { get; set; } = "";
}

public class UserSessionService
{
    public UserSession Session { get; set; }

    public void StartSession(string userId, string token, string Email)
    {
        Session = new UserSession { userId = userId, Token = token, Email = Email};
    }
}