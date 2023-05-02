using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace HOTP
{
    internal class HOTPComputation
    {
        public static int OtpLength = 6;

        public static string ComputeHOTP(byte[] key, ulong counter)
        {
            //Compute HMAC SHA-1
            byte[] byteCounter = BitConverter.GetBytes(counter);

            byte[] hashedData = HMACSHA1.HashData(key, byteCounter);

            //Truncate
            byte[] truncatedData = DynamicTruncation(hashedData);
            uint value = BitConverter.ToUInt32(truncatedData, 0);

            //Return mod 10^Length
            return (value % (Math.Pow(10, OtpLength))).ToString();
        }

        private static byte[] DynamicTruncation(byte[] hashedData)
        {
            Contract.Requires(hashedData.Length >= 20);
            Contract.Ensures(Contract.Result<byte[]>().Length == 4);

            //Get 4 least significant bits of hashedData, as an int
            int offset = hashedData[19] & 0x0F;

            byte[] truncatedData = new byte[4];
            for (int i = 0; i < truncatedData.Length; i++)
            {
                truncatedData[i] = hashedData[offset + i];
            }

            return truncatedData;
        }
    }
}
