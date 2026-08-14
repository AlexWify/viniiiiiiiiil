using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VinylStore;

// --- МОДЕЛИ ПРЯМО ТУТ ---

public class Genre
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название жанра")]
    [Display(Name = "Название жанра")]
    public string Name { get; set; } = string.Empty;

    public List<Record> Records { get; set; } = new();
}

public class Record
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название обязательно")]
    [Display(Name = "Название")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Исполнитель")]
    public string Artist { get; set; } = string.Empty;

    [Range(1900, 2026, ErrorMessage = "Год должен быть в диапазоне от 1900 до 2026")]
    [Display(Name = "Год выпуска")]
    public int Year { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть положительной")]
    [Display(Name = "Цена")]
    public decimal Price { get; set; }

    [Display(Name = "Жанр")]
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
}

// --- КОНТЕКСТ БАЗЫ ДАННЫХ ---

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Record> Records { get; set; }
    public DbSet<Genre> Genres { get; set; }
}