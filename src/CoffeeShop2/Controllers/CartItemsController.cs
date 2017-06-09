using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeeShop2.Data;
using CoffeeShop2.Models;

namespace CoffeeShop2.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartItemsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CartItem.Include(c => c.Cart).Include(c => c.Product).Where(u => u.UserId == User.Identity.Name);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem.Include(c => c.Cart).Include(c => c.Product).SingleOrDefaultAsync(m => m.CartItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public async Task<IActionResult> Create(int productid, int quantity)
        {
            var userCart = await _context.Cart.SingleOrDefaultAsync(m => m.UserId == User.Identity.Name);
            var product = await _context.Product.SingleOrDefaultAsync(p => p.ProductId == productid);
            ViewData["CartId"] = userCart.CartId;
            //ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId");
            ViewData["ProductId"] = productid;
            ViewData["Quantity"] = quantity;
            ViewData["PName"] = product.Name;
            ViewData["PPrice"] = product.Price;
            return View();
        }
        
        // POST: CartItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartItemId,ProductId,Quantity,UserId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                var userCart = await _context.Cart.SingleOrDefaultAsync(m => m.UserId == User.Identity.Name);
                cartItem.CartId = userCart.CartId;
                cartItem.UserId = User.Identity.Name;
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartItem.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem.SingleOrDefaultAsync(m => m.CartItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartItem.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", cartItem.ProductId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartItemId,CartId,ProductId,Quantity,UserId")] CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.CartItemId))
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
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartItem.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem.SingleOrDefaultAsync(m => m.CartItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _context.CartItem.SingleOrDefaultAsync(m => m.CartItemId == id);
            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItem.Any(e => e.CartItemId == id);
        }
    }
}
