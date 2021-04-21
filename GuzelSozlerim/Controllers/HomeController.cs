using GuzelSozlerim.Data;
using GuzelSozlerim.Extensions;
using GuzelSozlerim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GuzelSozlerim.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.GuzelSozler.Include(x => x.Begenenler).ToList());
        }

        [Authorize] //sadece üyeler kullanabilir
        [HttpPost]
        public IActionResult begenidurumuguncelle(int id, bool begenildiMi)
        {
            try
            {
                string userId = User.GetUserId();
                var begeni = new KullaniciSoz() { GuzelSozId = id, KullaniciId = userId };
                if (begenildiMi)
                {
                    if (!_dbContext.KullaniciSozler.Contains(begeni))
                    {
                        _dbContext.KullaniciSozler.Add(begeni);
                    }
                }
                else
                {
                    if (_dbContext.KullaniciSozler.Contains(begeni))
                    {
                        _dbContext.KullaniciSozler.Remove(begeni);

                    }
                }
                //_dbContext.Entry(begeni).State = begenildiMi ? EntityState.Added : EntityState.Deleted;
                _dbContext.SaveChanges();
                return new EmptyResult();

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
