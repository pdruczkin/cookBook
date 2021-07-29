using System.Collections.Generic;
using cookBook.Entities.IngredientProperties;

namespace cookBook.Seeders
{
    public static class MeasurementsSeeder
    {
        public static List<MeasurementUnit> GetUnits()
        {
            var list = new List<MeasurementUnit>()
            {

                new MeasurementUnit()
                {
                    Description = "g"
                },
                new MeasurementUnit()
                {
                    Description = "cups"
                }
            };
            return list;
        }

        public static List<MeasurementQuantity> GetQuantities()
        {
            List<MeasurementQuantity> list = new List<MeasurementQuantity>()
            {
                new MeasurementQuantity()
                {
                    Amount = 1
                },
                new MeasurementQuantity()
                {
                    Amount = 5
                }
            };
            return list;
        }

    }
}