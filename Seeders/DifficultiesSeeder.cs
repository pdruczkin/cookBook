using System.Collections.Generic;
using cookBook.Entities.Api;

namespace cookBook.Seeders
{
    public static class DifficultiesSeeder
    {
        public static List<Difficulty> GetDifficulties()
        {
            var list = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Name = "medium"
                },
                new Difficulty()
                {
                    Name = "hard"
                }
            };
            return list;
        }
    }
}