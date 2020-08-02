using System;
using System.Collections.Generic;
using System.Linq;

namespace NavMenuApi
{
    public class NavMenuBuilder
    {
        private Dictionary<Guid, NavMenuTreeItem> _navMenuItems;

        private readonly List<NavMenuTreeItem> _navMenuTreeItems;

        public NavMenuBuilder(IEnumerable<NavMenuTreeItem> navMenuTreeItems)
        {
            _navMenuTreeItems = navMenuTreeItems.ToList();
            _navMenuItems = FlattenTree(_navMenuTreeItems);
        }


        public void Merge(IEnumerable<NavMenuTreeItem> navMenuTreeItems)
        {
            var flatted = FlattenTree(navMenuTreeItems);
            var remove = _navMenuItems.Keys.Where(key => !flatted.Keys.Contains(key));
            foreach (var guid in remove) RemoveIfExists(guid);

            foreach (var navMenuTreeItem in navMenuTreeItems) AddIfMissing(navMenuTreeItem);
        }

        public List<NavMenuTreeItem> GetNavMenu()
        {
            return _navMenuTreeItems;
        }

        private Dictionary<Guid, NavMenuTreeItem> FlattenTree(IEnumerable<NavMenuTreeItem> navMenuTreeItems)
        {
            var dict = new Dictionary<Guid, NavMenuTreeItem>();

            foreach (var navMenuTreeItem in navMenuTreeItems)
            {
                dict.Add(navMenuTreeItem.Id, navMenuTreeItem);
                if (navMenuTreeItem.Children != null)
                    foreach (var (key, value) in FlattenTree(navMenuTreeItem.Children))
                        dict.Add(key, value);
            }

            return dict;
        }

        private void AddIfMissing(NavMenuTreeItem navMenuTreeItem)
        {
            if (!navMenuTreeItem.Parent.HasValue) // If no Parent it must be a Module
            {
                if (!_navMenuItems.ContainsKey(navMenuTreeItem.Id))
                {
                    Add(null, navMenuTreeItem);
                }
                else
                {
                    if (navMenuTreeItem.Children != null)
                        foreach (var menuTreeItem in navMenuTreeItem.Children)
                            AddIfMissing(menuTreeItem);
                }
            }
            else
            {
                if (!_navMenuItems.ContainsKey(navMenuTreeItem.Id)) Add(navMenuTreeItem.Parent, navMenuTreeItem);
            }
        }

        private void RemoveIfExists(Guid id)
        {
            if (_navMenuItems.ContainsKey(id))
            {
                _navMenuTreeItems.RemoveAll(module => module.Id == id);
                foreach (var navMenuTreeItem in _navMenuTreeItems)
                    navMenuTreeItem.Children?.RemoveAll(child => child.Id == id);
                _navMenuItems = FlattenTree(_navMenuTreeItems);
            }
        }

        private void Add(Guid? parent, NavMenuTreeItem navMenuTreeItem)
        {
            if (parent.HasValue)
            {
                var module = _navMenuTreeItems.FirstOrDefault(item => item.Id == parent.Value);
                module.Children.Add(navMenuTreeItem);
            }
            else
            {
                _navMenuTreeItems.Add(navMenuTreeItem);
            }

            _navMenuItems = FlattenTree(_navMenuTreeItems);
        }
    }
}