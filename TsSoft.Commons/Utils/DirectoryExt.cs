using System.IO;

namespace TsSoft.Commons.Utils
{
    public class DirectoryExt
    {
        /// <summary>
        /// http://stackoverflow.com/questions/627504/what-is-the-best-way-to-recursively-copy-contents-in-c
        /// </summary>
        public static void Copy(string source, string destination)
        {
            int pathLen = source.Length + 1;

            foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
            {
                string subPath = dirPath.Substring(pathLen);
                string newpath = Path.Combine(destination, subPath);
                Directory.CreateDirectory(newpath);
            }

            foreach (string filePath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
            {
                string subPath = filePath.Substring(pathLen);
                string newpath = Path.Combine(destination, subPath);
                File.Copy(filePath, newpath);
            }
        }

        public static void Clear(string source)
        {
            var directoryInfo = new DirectoryInfo(source);
            foreach (var directory in directoryInfo.GetDirectories("*"))
            {
                Directory.Delete(directory.FullName, true);
            }
            foreach (var file in directoryInfo.GetFiles("*"))
            {
                File.Delete(file.FullName);
            }
        }
    }
}