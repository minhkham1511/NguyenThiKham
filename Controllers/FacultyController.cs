using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenThiKhambth2.Models;
using NguyenThiKhambth2.Models.Process;


namespace NguyenThiKhambth2.Controllers
{
namespace VuThiHuyenBTH2.Controllers
{
    public class FacultyController : Controller
    {
        private readonly ApplicationDbContext _context;
      
        public FacultyController (ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Employee
        public async Task<IActionResult> Index()
        {
           var model = await _context.Faculty.ToListAsync();
           return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
          public async Task<IActionResult> Create (Faculty fac)
        {
            if(ModelState.IsValid)
            {
                _context.Add(fac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fac);
        }
         public async Task<IActionResult>Edit(string id)
         {
            if (id ==null)
            {
                return NotFound();
            }
            var Faculty = await _context.Faculty.FindAsync(id);
            if (Faculty == null)
            {
                return NotFound();
            }
            return View(Faculty);
         }
          // POST: Faculty/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]

        // public async Task<IActionResult>Edit(string id, [Bind("FacultyID,FacultyName")] Faculty fac)
        // {
        //     if (id != fac.FacultyID)
        //     {
        //        // return NotFound();
        //        return View("NotFound");
        //     }
        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(fac);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if( !FacultyExists(fac.FacultyID))
        //             {
        //                // return NotFound();
        //                return View("NotFound");
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
            
        //     return View(fac);
        //     }
        //  }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
               // return NotFound();
               return View("NotFound");
            }
            var std = await _context.Faculty
                .FirstOrDefaultAsync(m =>m.FacultyID ==id);
            if (std == null)
            {
               // return NotFound();
               return View("NotFound");
            }
            return View(std);
        }
        // POST: Product/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
      
     private bool FacultyExists(string id)
        {
            return _context.Faculty.Any(e => e.FacultyID == id);
        }
    
    }
}