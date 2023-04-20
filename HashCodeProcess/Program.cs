using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HashCodeProcess
{
    class Program
    {
        async public static Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                Environment.Exit(-1);
            }

            string sourceFile = args[0];
            FileInfo fi = new FileInfo(sourceFile);
            string targetFile = fi.DirectoryName + "\\hash_" + fi.Name + ".txt";
            try
            {
                await MD5HashToFile(sourceFile, targetFile);
            }
            catch (UnauthorizedAccessException exc)
            {
                Environment.Exit(-2);
            }
            catch (Exception e)
            {
                Environment.Exit(-1);
            }
        }

        private async static Task MD5HashToFile(string sourceFile, string targetFile)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(sourceFile))
                {
                    using (var outputStream = new FileStream(targetFile, FileMode.Create))
                    {
                        byte[] md5Hash = md5.ComputeHash(stream);
                        await outputStream.WriteAsync(md5Hash, 0, md5Hash.Length);
                    }

                }
            }
        }
    }
}
