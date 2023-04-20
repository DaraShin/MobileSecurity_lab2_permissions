using System.IO;
using System;
using System.IO.Compression;
using System.Threading.Tasks;

class ZipProcess
{

    async public static Task Main(String[] args)
    {
        if (args.Length < 1)
        {
            Environment.Exit(-1);
        }

        string sourceFile = args[0];
        string compressedFile = sourceFile + ".zip";

        try
        {
            await CompressAsync(sourceFile, compressedFile);
        }
        catch (UnauthorizedAccessException exc)
        {
            Environment.Exit(-2);
        }
        catch (Exception exc)
        {
            Environment.Exit(-1);
        }
    }

    async private static Task CompressAsync(string sourceFile, string compressedFile)
    {
        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open))
        {
            using (FileStream targetStream = File.Create(compressedFile))
            {
                using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                {
                    await sourceStream.CopyToAsync(compressionStream);
                }
            }

        }
    }
}





