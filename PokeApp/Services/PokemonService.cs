using PokeApp.Models;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace PokemonApp.Services
{
    public class PokemonService
    {
        public readonly HttpClient httpClient;
        List<Pokemon> pokemons = new List<Pokemon>();
        public PokemonService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<Pokemon> GetDetails(string name)
        {
            var response = await httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}");
            dynamic data = JsonConvert.DeserializeObject(response);

            var abilities = new List<Ability>();
            foreach (var ability in data.abilities)
            {
                abilities.Add(new Ability { Name = ability.ability.name });
            }

            return new Pokemon
            {
                Id = data.id,
                Name = data.name,
                Abilities = abilities,
                SpriteUrl = data.sprites.front_default
            };

        }

        public async Task<List<Pokemon>> GetFirst100()
        {
            var tasks = new List<Task<Pokemon>>();

            for (int i = 1; i <= 100; i++)
            {
                int id = i; // Capture the loop variable
                tasks.Add(Task.Run(async () =>
                {
                    var response = await httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
                    dynamic data = JsonConvert.DeserializeObject(response);

                    var abilities = new List<Ability>();
                    foreach (var ability in data.abilities)
                    {
                        abilities.Add(new Ability { Name = ability.ability.name });
                    }

                    var types = new List<PokeApp.Models.Type>();
                    foreach (var type1 in data.types)
                    {
                        types.Add(new PokeApp.Models.Type { TypeName = type1.type.name });
                    }

                    return new Pokemon
                    {
                        Id = data.id,
                        Name = data.name,
                        Abilities = abilities,
                        SpriteUrl = data.sprites.front_default,
                        Types = types
                    };
                }));
            }

            // Wait for all tasks to complete
            var pokemons = await Task.WhenAll(tasks);
            return pokemons.ToList();
        }
    }
}