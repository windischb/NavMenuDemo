using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NavMenuApi
{
    public class UserDataStore : IUserDataStore
    {
        public UserDataStore(string filePath)
        {
            FilePath = PathHelper.GetAbsoluteFileInfo(filePath);
            EnsureExists();
        }

        private FileInfo FilePath { get; }

        public async Task<List<NavMenuTreeItem>> GetNavMenuAsync()
        {
            var fileContent = await File.ReadAllTextAsync(FilePath.FullName);
            var navMenuTree = JsonConvert.DeserializeObject<List<NavMenuTreeItem>>(fileContent);
            return navMenuTree;
        }

        public async Task StoreNavMenuAsync(IEnumerable<NavMenuTreeItem> navMenu)
        {
            var json = JsonConvert.SerializeObject(navMenu, Formatting.Indented);
            await File.WriteAllTextAsync(FilePath.FullName, json);
        }

        private void EnsureExists()
        {
            if (!FilePath.Exists) File.WriteAllText(FilePath.FullName, "[]");
        }
    }
}