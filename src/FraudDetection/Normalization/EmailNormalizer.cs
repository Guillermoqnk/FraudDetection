namespace FraudDetection.Normalization;

/// <summary>
/// "B.u.g.s+10@BuNNy.COM" → "bugs@bunny.com".
/// In the username, dots are ignored and everything from the first '+' is dropped.
/// </summary>
public sealed class EmailNormalizer : IEmailNormalizer
{
    public string Normalize(string value)
    {
        string email = TextHelper.Clean(value);

        int at = email.LastIndexOf('@');
        if (at < 0)
        {
            return email;
        }

        string user = email.Substring(0, at);
        string domain = email.Substring(at + 1);

        int plus = user.IndexOf('+');
        if (plus >= 0)
        {
            user = user.Substring(0, plus);
        }

        return user.Replace(".", "") + "@" + domain;
    }
}
