using System.Security.Cryptography;

namespace NetCore.Utils.Hash
{
    public interface IHashUtil
    {
        /// <summary>
        /// MD5 Hash
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <returns></returns>
        byte[] Md5(byte[] plainBytes);

        /// <summary>
        /// SHA256 Hash
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <returns></returns>
        byte[] Sha256(byte[] plainBytes);

        /// <summary>
        /// SHA512 Hash
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <returns></returns>
        byte[] Sha512(byte[] plainBytes);
    }

    public class HashUtil : IHashUtil
    {
        public byte[] Md5(byte[] plainBytes)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(plainBytes);
            }
        }

        public byte[] Sha256(byte[] plainBytes)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(plainBytes);
            }
        }

        public byte[] Sha512(byte[] plainBytes)
        {
            using (var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(plainBytes);
            }
        }
    }
}