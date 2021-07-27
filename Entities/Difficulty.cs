namespace cookingBook.Entities
{
    public class Difficulty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}