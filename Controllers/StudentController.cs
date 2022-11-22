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
    public class StudentController : Controller
    {
        private readonly ApplicationDbcontext _context;
           private ExcelProcess _ExcelProcess = new ExcelProcess();
        public StudentController(ApplicationDbcontext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var id = _context.Student;
              return _context.Student != null ? 
                          View(await _context.Student.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbcontext.Student'  is null.");
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return View ("NotFound");
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return View ("NotFound");
            }

            return View(student);
        }

        // GET: Student/Create
        // public IActionResult Create()
        // {
        //     ViewData["FacultyID"]= new SelectList(_context.Facult, "FacultyID","FacultyID");
        //     return View();
        // }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,FacultyID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["FacultyID"] = new SelectList(_context.Facult,"FacultyID", student.FacultyID);
             return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return View ("NotFound");
            }


            var student = await _context.Student.FindAsync(id);
            if (student == null)

            {
                return View ("NotFound");
            }
            return View(student);
        }
        //

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Student student)
        {
            if (id != student.ID)
            {
                return View ("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
                    {
                        return View ("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return View ("NotFound");
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return View ("NotFound");
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'ApplicationDbcontext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Student?.Any(e => e.ID == id)).GetValueOrDefault();
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
                var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", FileName);
                var fileLocation = new FileInfo(filePath).ToString();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    //save file to server
                    await file.CopyToAsync(stream);
                  var dt = _ExcelProcess.ExcelToDataTable(fileLocation);
                        for (int i=0; i< dt.Rows.Count; i++)
                        {
                            var Std = new Student();
                            Std.ID = Convert.ToInt32(dt.Rows[i][0].ToString ());
                            Std.Name = dt.Rows[i][1].ToString ();

                            _context.Student.Add(Std);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));  
                }
            }
        }
        return View();

     }

    }
}