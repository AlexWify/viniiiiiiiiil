using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinylStore;
using VinylStore.Models;

namespace VinylStore.Controllers;

public class RecordsController : Controller
{
    private readonly AppDbContext _context;

    public RecordsController(AppDbContext context)
    {
        _context = context;
    }

    // Список с подгрузкой жанров
    public async Task<IActionResult> Index()
    {
        var records = await _context.Records.Include(r => r.Genre).ToListAsync();
        return View(records);
    }

    // GET: Создание
    public IActionResult Create()
    {
        ViewBag.Genres = new SelectList(_context.Genres, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Record record)
    {
        if (ModelState.IsValid)
        {
            _context.Records.Add(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Genres = new SelectList(_context.Genres, "Id", "Name", record.GenreId);
        return View(record);
    }

    // GET: Редактирование
    public async Task<IActionResult> Edit(int id)
    {
        var record = await _context.Records.FindAsync(id);
        if (record == null) return NotFound();

        ViewBag.Genres = new SelectList(_context.Genres, "Id", "Name", record.GenreId);
        return View(record);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Record record)
    {
        if (id != record.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Genres = new SelectList(_context.Genres, "Id", "Name", record.GenreId);
        return View(record);
    }

    // GET: Удаление
    public async Task<IActionResult> Delete(int id)
    {
        var record = await _context.Records.Include(r => r.Genre).FirstOrDefaultAsync(m => m.Id == id);
        if (record == null) return NotFound();
        return View(record);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var record = await _context.Records.FindAsync(id);
        if (record != null)
        {
            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}