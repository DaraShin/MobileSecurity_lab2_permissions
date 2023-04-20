using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ConvertToPngProcess
{
    class Program
    {
        private static HashSet<string> imageExtensions = new HashSet<string>() { ".jpg", ".jpeg", ".bmp", ".tif", ".tiff", ".gif" };

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Environment.Exit(-1);
            }

            string sourceFile = args[0];
           
            try
            {
                ConvertImageToPng(sourceFile);
            }
            catch (UnauthorizedAccessException exc)
            {
                Environment.Exit(-2);
            }
            catch (ArgumentException  exc)
            {
                Environment.Exit(-2);
            }
            catch (Exception exc)
            {
                Environment.Exit(-1);
            }
        }

        private static void ConvertImageToPng(string imageFile)
        {
            FileInfo file = new FileInfo(imageFile);
            if (!imageExtensions.Contains(file.Extension))
            {
                Environment.Exit(-3);
            }

            Bitmap bitmap = new Bitmap(imageFile);
            string pngImageFile = imageFile.Substring(0, imageFile.LastIndexOf('.')) + ".png";
            bitmap.Save(pngImageFile, ImageFormat.Png);
        }
    }
}
