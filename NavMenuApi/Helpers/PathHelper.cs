using System;
using System.IO;

namespace NavMenuApi
{
    public static class PathHelper
    {
        public static string GetAbsolutePath(string path)
        {
            return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, path));
        }


        public static FileInfo GetAbsoluteFileInfo(string path)
        {
            return new FileInfo(GetAbsolutePath(path));
        }
    }
}