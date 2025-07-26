using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Utilities;

public static class StringSimilarity<TEntity>
    where TEntity : class, IEntity
{
    public static int LevenshteinDistance(string a, string b)
    {
        var costs = new int[b.Length + 1];

        for (var j = 0; j < costs.Length; j++)
            costs[j] = j;

        for (var i = 1; i <= a.Length; i++)
        {
            costs[0] = i;
            var nw = i - 1;
            for (var j = 1; j <= b.Length; j++)
            {
                var cj = Math.Min(1 + Math.Min(costs[j], costs[j - 1]),
                    a[i - 1] == b[j - 1] ? nw : nw + 1);
                nw = costs[j];
                costs[j] = cj;
            }
        }

        return costs[b.Length];
    }

}