using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace FireTracker2.Controllers
{
    public class ImageUploadValidator
    {
        
        public static bool IsWebFriendlyImage(HttpPostedFileBase file)
        {
            if (file == null)
                return false;
            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
                return false;
            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                    ImageFormat.Png.Equals(img.RawFormat) ||
                    ImageFormat.Icon.Equals(img.RawFormat) ||
                    ImageFormat.Bmp.Equals(img.RawFormat) ||
                    ImageFormat.Tiff.Equals(img.RawFormat) ||
                    ImageFormat.Gif.Equals(img.RawFormat);
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool IsAttachmentValid(HttpPostedFileBase file)
        {
         
            try 
            {
                if (file == null)
                    return false;
                if (file.ContentLength > 5 * 1024 * 1024 || file.ContentLength < 1024)
                    return false;
                var isvalid = IsWebFriendlyImage(file);
                var extisValid = false;
                foreach (var ext in WebConfigurationManager.AppSettings["AllowedAttachmentExtensions"].Split(','))
                {
                    if (Path.GetExtension(file.FileName) == ext)
                    {
                        extisValid = true;
                        break;
                    }
                }
                return IsWebFriendlyImage(file) || extisValid;
            }
            catch
            {
                return false;
            }
        }
        public static string GetFileExtIco(string filePath)
        {
            
            switch (Path.GetExtension(filePath))
            {
                case ".png":
                case ".bmp":
                case ".tiff":
                case ".jpeg":
                case ".jpg":
                    return filePath;
                case ".pdf":
                    return "/FileExt/pdf.png";
                case ".odt":
                    return "/FileExt/odt.png";
                case ".doc":
                    return "/FileExt/doc.png";
                case ".docx":
                    return "/FileExt/docx.png";
                case ".txt":
                    return "/FileExt/txt.png";
                default:
                    return "/FileExt/txt.png";
            }
            
        }


    }
}