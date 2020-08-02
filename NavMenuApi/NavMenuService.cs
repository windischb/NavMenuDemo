using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavMenuApi
{
    public class NavMenuService
    {
        public NavMenuService(IModulesDataStore modulesDataStore, IUserDataStore userDataStore)
        {
            ModulesDataStore = modulesDataStore;
            UserDataStore = userDataStore;
        }

        private IModulesDataStore ModulesDataStore { get; }
        private IUserDataStore UserDataStore { get; }

        public async Task<IEnumerable<NavMenuTreeItem>> GetNavMenu(IEnumerable<string> roles)
        {
            var userNavMenu = await UserDataStore.GetNavMenuAsync();
            var navBuilder = new NavMenuBuilder(userNavMenu);

            var generatedNavMenu = await GenerateNavMenuFromModules(roles);

            navBuilder.Merge(generatedNavMenu);
            userNavMenu = navBuilder.GetNavMenu();
            await UserDataStore.StoreNavMenuAsync(userNavMenu);

            return userNavMenu;
        }

        public async Task StoreNavMenu(IEnumerable<NavMenuTreeItem> navMenuTreeItems)
        {
            await UserDataStore.StoreNavMenuAsync(navMenuTreeItems);
        }

        private async Task<IEnumerable<NavMenuTreeItem>> GenerateNavMenuFromModules(IEnumerable<string> roles)
        {
            var modules = await ModulesDataStore.GetModulesForRolesAsync(roles);
            return modules.Select(BuildModule);
        }

        private NavMenuTreeItem BuildModule(ModuleEntity moduleEntity)
        {
            return new NavMenuTreeItem
            {
                Id = moduleEntity.Id,
                Image = moduleEntity.Image,
                Href = moduleEntity.Href,
                Label = moduleEntity.Label,
                Children = moduleEntity.SubNavItems.Select(s => BuildSubItem(moduleEntity.Id, s)).ToList()
            };
        }

        private NavMenuTreeItem BuildSubItem(Guid parent, NavItemEntity navItemEntity)
        {
            return new NavMenuTreeItem
            {
                Id = navItemEntity.Id,
                Image = navItemEntity.Image,
                Href = navItemEntity.Href,
                Label = navItemEntity.Label,
                Parent = parent
            };
        }


        // private async Task<IEnumerable<NavMenuTreeItem>> MergeNavMenuTree(List<NavMenuTreeItem> userNavMenu, IEnumerable<NavMenuTreeItem> generatedNavMenu)
        // {
        //
        //     var flattedGeneratedNavMenu = FlattenTree(generatedNavMenu);
        //     
        //     
        //     var navItemsDict = new Dictionary<Guid, NavMenuTreeItem>();
        //     
        //     List<NavMenuTreeItem> newNavMenu = new List<NavMenuTreeItem>(userNavMenu);
        //     
        //     foreach (var filteredModule in filteredModules)
        //     {
        //         var foundModule = FindByIdInTree(filteredModule.Id, userNavMenu);
        //         if (foundModule == null)
        //         {
        //             var newNavMenuModule = BuildModule(filteredModule);
        //             newNavMenu.Add(newNavMenuModule);
        //         }
        //         else
        //         {
        //             
        //         }
        //     }
        // }

        // private Dictionary<Guid, NavMenuTreeItem> FlattenTree(IEnumerable<NavMenuTreeItem> navMenuTreeItems)
        // {
        //     var dict = new Dictionary<Guid, NavMenuTreeItem>();
        //
        //     foreach (var navMenuTreeItem in navMenuTreeItems)
        //     {
        //         dict.Add(navMenuTreeItem.Id, navMenuTreeItem);
        //         if (navMenuTreeItem.Children != null)
        //         {
        //             foreach (var (key, value) in FlattenTree(navMenuTreeItem.Children))
        //             {
        //                 dict.Add(key, value);
        //             }
        //         }
        //     }
        //
        //     return dict;
        // }
        //
        // private NavMenuTreeItem FindByIdInTree(Guid id, IEnumerable<NavMenuTreeItem> userNavMenu)
        // {
        //     foreach (var navMenuTreeItem in userNavMenu)
        //     {
        //         if (navMenuTreeItem.Id == id)
        //         {
        //             return navMenuTreeItem;
        //         }
        //
        //         if (navMenuTreeItem.Children != null)
        //         {
        //             var found = FindByIdInTree(id, navMenuTreeItem.Children);
        //             if (found != null)
        //             {
        //                 return found;
        //             }
        //         }
        //     }
        //
        //     return null;
        // }
        //
    }
}