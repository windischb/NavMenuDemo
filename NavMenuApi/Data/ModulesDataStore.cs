using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NavMenuApi
{
    public class ModulesDataStore : IModulesDataStore
    {
        public ModulesDataStore(string filePath)
        {
            FilePath = PathHelper.GetAbsoluteFileInfo(filePath);
            EnsureExists();
        }

        private FileInfo FilePath { get; }

        public async Task<IEnumerable<ModuleEntity>> GetModulesForRolesAsync(IEnumerable<string> roles)
        {
            roles = roles.Select(r => r.ToLower());
            var fileContent = await File.ReadAllTextAsync(FilePath.FullName);
            var modules = JsonConvert.DeserializeObject<List<ModuleEntity>>(fileContent);
            return modules
                .Where(m => m.AllowedRoles.Select(r => r.ToLower()).Any(roles.Contains))
                .Select(m =>
                {
                    var filteredSubItems = m.SubNavItems
                        .Where(s => s.AllowedRoles.Select(r => r.ToLower()).Any(roles.Contains)).ToList();
                    m.SubNavItems = new Collection<NavItemEntity>(filteredSubItems);
                    return m;
                });
        }

        private void EnsureExists()
        {
            if (!FilePath.Exists) File.WriteAllText(FilePath.FullName, "[]");
        }
    }
}