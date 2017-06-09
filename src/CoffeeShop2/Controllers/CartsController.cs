using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoffeeShop2.Data;
using CoffeeShop2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop2.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public CartsController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cart.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //_context.CartItem.Include(p => p.Product);
            //var cart = await _context.Cart.Include(i => i.CartItems).SingleOrDefaultAsync(c => c.CartId == id);
            //var cartItem = _context.CartItem.Include(c => c.Cart).Include(c => c.Product).Where(s => s.CartId == id);


            // We have to send a Cart to the View. Inside a Cart there are CartItmes. Each CartItem has Product. 
            //Cart is being loaded and CartItem is being included. How to include a Product?
            // Cart > CartItem > Product
            //var cart = await _context.Cart.Include(i => i.CartItems).SingleOrDefaultAsync(c => c.CartId == id);

            var currentuser = await _userManager.GetUserAsync(User);
            CartViewModel cart = new CartViewModel();
            //cart.Cart = _context.Cart.SingleAsync(c => c.UserId == currentuser.UserName);
            //foreach (CartItem item in cart.CartItems )
            //{
            //    item.Include
            //}

            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,Status,UserId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                bool oldcart = await _context.Cart.AnyAsync(m => m.UserId == User.Identity.Name);
                if (oldcart==false)
                {
                    cart.UserId = User.Identity.Name;
                    cart.Status = "Open";
                    _context.Add(cart);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
               
            }
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.SingleOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,Status,UserId")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.SingleOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.SingleOrDefaultAsync(m => m.CartId == id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.CartId == id);
        }
    }
}
