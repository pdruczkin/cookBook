using System;
using System.Collections.Generic;

namespace cookBook.Models
{
    public class PageResult<T>
    {
        public PageResult(List<T> items, int itemsAmount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsAmount = itemsAmount;
            ItemFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemFrom + pageSize - 1;
            TotalPages = (int) Math.Ceiling(itemsAmount / (double)pageSize);
        }

        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemsAmount { get; set; }

    }
}