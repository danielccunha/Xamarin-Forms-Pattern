using System;
using System.Linq;

namespace MyProject.UnitTests.Extensions
{
    public static class RandomExtensions
    {
        public static string RandomString(this Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
