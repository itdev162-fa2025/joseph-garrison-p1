using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ColorController : ControllerBase
{
    private readonly ILogger<ColorController> _logger;
    private readonly DataContext _context;
    public ColorController(ILogger<ColorController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    public ActionResult<Color> Create()
    {
        Console.WriteLine($"Db: {_context.DbPath}");

        var color = new Color()
        {
            Name = "Blue",
            HexCode = "#151B54"
        };

        _context.Colors.Add(color);
        var success = _context.SaveChanges() > 0;

        if (success)
        {
            return color;
        }

        throw new Exception("Error creating Color");
    }

    [HttpGet(Name = "GetColor")]
    public ActionResult<List<Color>> Get()
    {
        return this._context.Colors.ToList();
    }

}

