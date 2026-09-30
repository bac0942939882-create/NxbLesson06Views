using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenXuanBac2410900011_exam.Data;
using NguyenXuanBac2410900011_exam.Models;

namespace NguyenXuanBac2410900011_exam.Controllers
{
    public class NxbStudentsController : Controller
    {
        private readonly NxbDbContext _context;

        public NxbStudentsController(NxbDbContext context)
        {
            _context = context;
        }

        // GET: NxbStudents
        public async Task<IActionResult> Index(string? search, bool? active, int page = 1)
        {
            const int pageSize = 10;
            search = search?.Trim();
            var query = _context.NxbStudents.AsNoTracking();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(s => s.NxbName.Contains(search) || s.NxbEmail.Contains(search) || s.NxbPhone.Contains(search));
            if (active.HasValue)
                query = query.Where(s => s.NxbActive == active.Value);
            var totalMatches = await query.CountAsync();
            var pageCount = Math.Max(1, (int)Math.Ceiling(totalMatches / (double)pageSize));
            page = Math.Clamp(page, 1, pageCount);
            return View(new NxbStudentListViewModel
            {
                Students = await query.OrderBy(s => s.NxbName).ThenBy(s => s.Id)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                Search = search, Active = active, Page = page, PageCount = pageCount,
                TotalMatches = totalMatches,
                TotalStudents = await _context.NxbStudents.CountAsync(),
                ActiveStudents = await _context.NxbStudents.CountAsync(s => s.NxbActive)
            });
        }

        // GET: NxbStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nxbStudent = await _context.NxbStudents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nxbStudent == null)
            {
                return NotFound();
            }

            return View(nxbStudent);
        }

        // GET: NxbStudents/Create
        public IActionResult Create()
        {
            return View(new NxbStudent());
        }

        // POST: NxbStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NxbName,NxbGender,NxbBirthDay,NxbEmail,NxbPhone,NxbActive")] NxbStudent nxbStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nxbStudent);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã thêm sinh viên thành công.";
                return RedirectToAction(nameof(Index));
            }
            return View(nxbStudent);
        }

        // GET: NxbStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nxbStudent = await _context.NxbStudents.FindAsync(id);
            if (nxbStudent == null)
            {
                return NotFound();
            }
            return View(nxbStudent);
        }

        // POST: NxbStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NxbName,NxbGender,NxbBirthDay,NxbEmail,NxbPhone,NxbActive")] NxbStudent nxbStudent)
        {
            if (id != nxbStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nxbStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NxbStudentExists(nxbStudent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Success"] = "Đã cập nhật thông tin sinh viên.";
                return RedirectToAction(nameof(Index));
            }
            return View(nxbStudent);
        }

        // GET: NxbStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nxbStudent = await _context.NxbStudents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nxbStudent == null)
            {
                return NotFound();
            }

            return View(nxbStudent);
        }

        // POST: NxbStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nxbStudent = await _context.NxbStudents.FindAsync(id);
            if (nxbStudent == null) return NotFound();
            _context.NxbStudents.Remove(nxbStudent);

            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã xóa sinh viên.";
            return RedirectToAction(nameof(Index));
        }

        private bool NxbStudentExists(int id)
        {
            return _context.NxbStudents.Any(e => e.Id == id);
        }
    }
}
