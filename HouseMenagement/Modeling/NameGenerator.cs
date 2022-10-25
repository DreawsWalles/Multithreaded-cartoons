using System;
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

namespace Modeling
{
    //Генератор имён для сотрудников
    public static class NameGenerator
    {
        private static readonly Random Random = new Random();

        private static readonly string[] Names = {"Евгений", "Михаил", "Ольга", "Светлана", "Дмитрий", "Екатерина"};

        public static string GetName()
        {
            return Names[Random.Next(0, Names.Length)];
        }
    }
}