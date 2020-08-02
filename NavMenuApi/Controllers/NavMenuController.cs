using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NavMenuApi.Controllers
{
    [Route("navmenu")]
    [ApiController]
    public class NavMenuController : Controller
    {
        private NavMenuService NavMenuService { get; }
        
        public NavMenuController(NavMenuService navMenuService)
        {
            NavMenuService = navMenuService;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> GetNavMenu([FromQuery] string[]? roles)
        {
            if (roles == null || roles.Length == 0)
            {
                roles = new[] {"Customer"};
            }
            var menu = await NavMenuService.GetNavMenu(roles);
            return Ok(menu);
        }

        [HttpPost("")]
        public async Task<IActionResult> StoreNavMenu(List<NavMenuTreeItem> navMenuTreeItems)
        {
            await NavMenuService.StoreNavMenu(navMenuTreeItems);
            return Ok();
        }
    }
}