using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NxbLesson10EFDbFirst.Models;

namespace NxbLesson10EFDbFirst.Controllers;

public class NxbMembersController : Controller
{
    private readonly NxbK24Cnt2Lesson10EfDbContext _context;

    public NxbMembersController(NxbK24Cnt2Lesson10EfDbContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.NxbMembers.AsNoTracking().ToListAsync());

    public async Task<IActionResult> Details(long? id)
    {
        if (id is null) return NotFound();
        var member = await _context.NxbMembers.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        return member is null ? NotFound() : View(member);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("NxbUserName,NxbPassword,NxbFullName,NxbEmail,NxbPhone,NxbStatus")] NxbMember member)
    {
        if (!ModelState.IsValid) return View(member);
        _context.Add(member);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(long? id)
    {
        if (id is null) return NotFound();
        var member = await _context.NxbMembers.FindAsync(id);
        return member is null ? NotFound() : View(member);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, [Bind("Id,NxbUserName,NxbPassword,NxbFullName,NxbEmail,NxbPhone,NxbStatus")] NxbMember member)
    {
        if (id != member.Id) return NotFound();
        if (!ModelState.IsValid) return View(member);

        try
        {
            _context.Update(member);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.NxbMembers.AnyAsync(m => m.Id == member.Id)) return NotFound();
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(long? id)
    {
        if (id is null) return NotFound();
        var member = await _context.NxbMembers.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        return member is null ? NotFound() : View(member);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var member = await _context.NxbMembers.FindAsync(id);
        if (member is not null)
        {
            _context.NxbMembers.Remove(member);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
