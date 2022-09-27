namespace MooseEngine.Extensions.Runtime;

public static class StringExtensions
{
    public static IEnumerable<string> Split(this string str, int length)
    {
        return str.Split(' ')
            .Aggregate(new[] { "" }.ToList(), (a, x) =>
            {
                var last = a[a.Count - 1];
                if ((last + " " + x).Length > length)
                {
                    a.Add(x);
                }
                else
                {
                    a[a.Count - 1] = (last + " " + x).Trim();
                }
                return a;
            });
    }
}
