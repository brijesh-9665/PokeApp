namespace PokeApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Ability>? Abilities { get; set; }
        public string? SpriteUrl { get; set; }

        public List<Type>? Types { get; set; }
    }
    public class Ability
    {
        public string? Name { get; set; }
    }

    public class Type
    {
        public string? TypeName { get; set;}
    }
}
