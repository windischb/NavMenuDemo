using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavMenuApi
{
    public interface IUserDataStore
    {
        public Task<List<NavMenuTreeItem>> GetNavMenuAsync();
        Task StoreNavMenuAsync(IEnumerable<NavMenuTreeItem> navMenu);
    }
}