using AlexandriaVer._2.Data;
using AlexandriaVer._2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexandriaVer._2.Controllers
{
    [Authorize]
    public class ReadersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ReadersController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        } 
        
        
        public async Task<IActionResult> ReadingListAsync()
        {

            IdentityUser user = await _userManager.GetUserAsync(User);

            ReadingListViewModel readingListViewModel = new ReadingListViewModel();
            readingListViewModel.Books = _context.UsersBooks.Where(p => p.User == user).Where(b => b.HasRead ==true);
            

            return View(readingListViewModel);
        }

        public async Task<IActionResult> ReadingWishListAsync()
        {

            IdentityUser user = await _userManager.GetUserAsync(User);

            ReadingListViewModel readingListViewModel = new ReadingListViewModel();
            readingListViewModel.Books = _context.UsersBooks.Where(p => p.User == user).Where(b => b.HasRead == false);


            return View("ReadingWishList",readingListViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new UsersBooks());
        }

        [HttpPost]
        public async Task<IActionResult> AddWishAsync(UsersBooks usersBooks)
        {

            IdentityUser user = await _userManager.GetUserAsync(User);

            UsersBooks book = usersBooks;
            book.User = user;

            _context.UsersBooks.Add(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("ReadingWishList", "Readers");
        }
        [HttpGet]
        public IActionResult AddWish()
        {
            return View(new UsersBooks());
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(UsersBooks usersBooks)
        {

            IdentityUser user = await _userManager.GetUserAsync(User);

            UsersBooks book = usersBooks;
            book.User = user;
            book.HasRead = true;

            _context.UsersBooks.Add(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("ReadingList", "Readers");
        }


        public IActionResult Delete(string id)
        {
            UsersBooks book = _context.UsersBooks.Where(b => b.Id == int.Parse(id)).FirstOrDefault();
            _context.UsersBooks.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("ReadingList", "Readers");

        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            UsersBooks book =_context.UsersBooks.Where(b => b.Id == int.Parse(id)).FirstOrDefault();
           

            return View(book);
        }
        [HttpGet]
        public IActionResult EditWish(string id)
        {
            UsersBooks book = _context.UsersBooks.Where(b => b.Id == int.Parse(id)).FirstOrDefault();


            return View(book);
        }
        [HttpPost]
        public async Task<IActionResult> EditWish(UsersBooks usersBooks)
        {
            IdentityUser user = await _userManager.GetUserAsync(User);

            UsersBooks book = usersBooks;
            book.User = user;
            book.HasRead = false;

            _context.UsersBooks.Update(book);
            _context.SaveChanges();
            return RedirectToAction("ReadingList", "Readers");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UsersBooks usersBooks)
        {
            IdentityUser user = await _userManager.GetUserAsync(User);

            UsersBooks book = usersBooks;
            book.User = user;
            book.HasRead = true;

            _context.UsersBooks.Update(book);
            _context.SaveChanges();
            return RedirectToAction("ReadingList", "Readers");
        }
    }
}
