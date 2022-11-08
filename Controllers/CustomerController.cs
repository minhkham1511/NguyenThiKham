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
    public class CustomerController : Controller
    {
        private readonly ApplicationDbcontext _context;
        private ExcelProcess _ExcelProcess = new ExcelProcess();

        public CustomerController(ApplicationDbcontext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
              return _context.Customer != null ? 
                          View(await _context.Customer.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbcontext.Customer'  is null.");
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return View ("NotFound");
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return View ("NotFound");
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseDate,address,number")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return View ("NotFound");
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseDate,address,number")] Customer customer)
        {
            if (id != customer.Id)
            {
                return View ("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return View ("NotFound");
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customer == null)
            {
                return Problem("Entity set 'ApplicationDbcontext.Customer'  is null.");
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
         public async Task<IActionResult> Upload()
    {
        return View();
    }
    [HttpPost]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> Upload(IFormFile file)
     {
        if (file != null)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension !=".xls"&& fileExtension !=".xlsx")
            {
                ModelState.AddModelError("","Please choose excel file to upload!");
            }
            else 
            {
                //rename file when upload to server
                var FileName = DateTime.Now.ToShortTimeString() + fileExtension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload/Excels", FileName);
                var fileLocation = new FileInfo(filePath).ToString();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    //save file to server
                    await file.CopyToAsync(stream);
                  var dt = _ExcelProcess.ExcelToDataTable(fileLocation);
                        for (int i=0; i< dt.Rows.Count; i++)
                        {
                            var Cus = new Customer();
                            Cus.Id = Convert.ToInt32(dt.Rows[i][0].ToString ());
                            Cus.Name =(dt.Rows[i][0].ToString ());
                            Cus.ReleaseDate = Convert.ToDateTime(dt.Rows[i][0].ToString());
                            Cus.address =(dt.Rows[i][0].ToString ());
                            Cus.number =Convert.ToInt32(dt.Rows[i][0].ToString ());
                            _context.Customer.Add(Cus);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));  
                }
            }
        }
        return View();
     }

        public override bool Equals(object? obj)
        {
            return obj is CustomerController controller &&
                   EqualityComparer<ExcelProcess>.Default.Equals(_ExcelProcess, controller._ExcelProcess);
        }
    }
}
    
