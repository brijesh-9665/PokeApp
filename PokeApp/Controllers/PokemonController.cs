using Microsoft.AspNetCore.Mvc;
using PokeApp.Models; // Make sure this matches your actual namespace
using PokemonApp.Services;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class PokemonController : Controller
{
    private readonly PokemonService _pokemonService;
    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Search(string pokemonName)
    {
        var pokemon = await _pokemonService.GetDetails(pokemonName);
        return View("Index", pokemon);
    }

    [HttpGet]
    public async Task<IActionResult> Get100()
    {
        return View(await _pokemonService.GetFirst100());
    }
}
