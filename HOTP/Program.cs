using System.Text;
using HOTP;

internal class Program
{
    private static void Main(string[] args)
    {
        string key = "8EjsRNNE5f4Kd2sELR3G1eLq9up5Kj2VofTnUfiyCUDUXaeWwvyc7WP9dlul";
        long counter = Random.Shared.NextInt64();

        string hotpCode = HOTPComputation.ComputeHOTP(Encoding.UTF8.GetBytes(key), counter);
        Console.WriteLine($"Key : {key}");
        Console.WriteLine($"Counter : {counter}");
        Console.WriteLine($"OTP : {hotpCode}");

        Console.ReadLine();
    }
}