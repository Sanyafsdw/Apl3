﻿namespace WebApplication3.Data.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ShortDesc { get; set; }
        public string? LongDesc { get; set; }
        public string? Img { get; set; }
        public int Price { get; set; }
        public bool IsFavourite { get; set; }
        public bool Available { get; set; }
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
