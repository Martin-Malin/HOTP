using System.Text;
using HOTP;

internal class Program
{
    private static void Main(string[] args)
    {
        byte[] key = Encoding.UTF8.GetBytes("8EjsRNNE5f4Kd2sELR3G1eLq9up5Kj2VofTnUfiyCUDUXaeWwvyc7WP9dlul");
        ulong counter = 144L;

        string hotpCode = HOTPComputation.ComputeHOTP(key, counter);

        Console.WriteLine("DIY :");
        Console.WriteLine(hotpCode);
        Console.ReadLine();
    }
}