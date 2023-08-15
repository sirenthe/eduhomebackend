using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EduHome.Utils
{
    public static class Extensions
    {
         public static  bool CheckFileType(this IFormFile file , string fileType)
        {
            return file.ContentType.Contains(fileType);
        }
        public static bool CheckFileSize(this IFormFile file, int size)
        {
            return file.Length/(1024*1024)<size;
        }
    }
}
