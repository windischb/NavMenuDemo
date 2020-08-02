using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavMenuApi
{
    public interface IModulesDataStore
    {
        Task<IEnumerable<ModuleEntity>> GetModulesForRolesAsync(IEnumerable<string> roles);
    }
}