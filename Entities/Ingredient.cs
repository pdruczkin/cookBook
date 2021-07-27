namespace cookingBook.Entities
{
    public class Ingredient
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }

        public virtual int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}