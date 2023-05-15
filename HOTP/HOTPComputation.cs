using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace HOTP
{
    internal class HOTPComputation
    {
        public static int OtpLength = 6;

        public static string ComputeHOTP(byte[] key, long counter)
        {
            byte[] byteCounter = GetBytesAsBigEndian(counter);

            byte[] hashedData = HMACSHA1.HashData(key, byteCounter);

            //Truncate
            byte[] truncatedData = DynamicTruncation(hashedData);
            int value = BitConverter.ToInt32(truncatedData, 0);

            //Return mod 10^Length
            return (value % Math.Pow(10, OtpLength)).ToString();
        }

        private static byte[] GetBytesAsBigEndian(long counter)
        {
            byte[] byteCounter = BitConverter.GetBytes(counter);
            Array.Reverse(byteCounter);

            return byteCounter;
        }

        private static byte[] DynamicTruncation(byte[] hashedData)
        {
            Contract.Requires(hashedData.Length >= 20);
            Contract.Ensures(Contract.Result<byte[]>().Length == 4);

            //Get 4 least significant bits of hashedData, as an int
            int offset = hashedData[19] & 0x0F;

            byte[] truncatedData = new byte[4];
            truncatedData[0] = hashedData[offset + 3]; //FF000000
            truncatedData[1] = hashedData[offset + 2]; //00FF0000
            truncatedData[2] = hashedData[offset + 1]; //0000FF00
            truncatedData[3] = (byte)(hashedData[offset] & 0x7f); //000000FF - 0x7f Make the number positive

            return truncatedData;
        }
    }
}
