using Microsoft.AspNetCore.Mvc;

namespace JewelleryStoreManagementSystem.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
