using System.IO;
using System.Text.RegularExpressions;

namespace TsSoft.Commons.Utils
{
    public class PathExt
    {
        public static string StripIllegalChars(string path)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }

        public static string GetRelativePath(string destPath, string startPath)
        {
            if (!destPath.StartsWith(startPath))
            {
                return Path.GetFileName(destPath);
            }
            else
            {
                return destPath.Substring(startPath.Length + 1);
            }
        }
    }
}