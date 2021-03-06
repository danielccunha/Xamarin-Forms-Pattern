﻿using MyProject.Contracts.Persistence.Domain;
using MyProject.UnitTests.Extensions;
using System;
using System.Collections.Generic;

namespace MyProject.UnitTests.Persistence
{
    internal static class EntitiesHelper
    {
        private static Random Random { get; } = new Random();

        internal static IEnumerable<Product> FakeProducts { get; } = new List<Product>
        {
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) },
            new Product { Name = Random.RandomString(20), Price = Random.Next(0, 1000) }
        };
    }
}
