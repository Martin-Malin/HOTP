namespace HOTP
{
    internal class RandomString
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GetRandomString(int length)
        {
            //https://stackoverflow.com/a/1344242/574059
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
        }
    }
}
