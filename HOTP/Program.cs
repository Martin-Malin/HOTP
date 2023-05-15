using System.Text;
using HOTP;

internal class Program
{
    private static void Main(string[] args)
    {
        //Test values
        string key = RandomString.GetRandomString(60);
        long counter = Random.Shared.NextInt64();

        string hotpCode = HOTPComputation.ComputeHOTP(Encoding.UTF8.GetBytes(key), counter);
        Console.WriteLine($"Key : {key}");
        Console.WriteLine($"Counter : {counter}");
        Console.WriteLine($"OTP : {hotpCode}");

        Console.ReadLine();
    }
}