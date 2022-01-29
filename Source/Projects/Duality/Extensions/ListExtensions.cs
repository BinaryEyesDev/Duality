using System.Collections.Generic;
using Duality.Utilities;

namespace Duality.Extensions
{
    public static class ListExtensions
    {
        public static T GetRandomItem<T>(this List<T> list)
        {
            var index = GetRandom.Int32(0, list.Count);
            return list[index];
        }

        public static T PopRandom<T>(this List<T> list)
        {
            var index = GetRandom.Int32(0, list.Count);
            var item = list[index];
            list.RemoveAt(index);

            return item;
        }
    }
}
