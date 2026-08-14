using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinylStore;
using VinylStore.Models;

namespace VinylStore.Controllers;

public class GenresController : Controller
{
    private readonly AppDbContext _context;

    public GenresController(AppDbContext context)
    {
        _context = context;
    }

    // Просмотр списка жанров
    public async Task<IActionResult> Index()
    {
        return View(await _context.Genres.ToListAsync());
    }

    // GET: Форма создания
    public IActionResult Create()
    {
        return View();
    }

    // POST: Сохранение жанра
    [HttpPost]
    public async Task<IActionResult> Create(Genre genre)
    {
        if (ModelState.IsValid)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }
}