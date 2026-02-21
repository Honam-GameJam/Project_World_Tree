using System.Collections.Generic;
using System.Linq;

class AliasTable
{
    private Item[] items;
    private float[] prob;
    private int[] alias;
    private int n;

    public AliasTable(List<Item> source)
    {
        n = source.Count;
        items = source.ToArray();
        prob = new float[n];
        alias = new int[n];

        Build(source);
    }

    private void Build(List<Item> source)
    {
        float totalWeight = source.Sum(i => i.Weight);
        float scale = n / totalWeight;

        Queue<int> small = new();
        Queue<int> large = new();

        float[] scaled = new float[n];

        for (int i = 0; i < n; i++)
        {
            scaled[i] = source[i].Weight * scale;

            if (scaled[i] < 1f)
                small.Enqueue(i);
            else
                large.Enqueue(i);
        }

        while (small.Count > 0 && large.Count > 0)
        {
            int s = small.Dequeue();
            int l = large.Dequeue();

            prob[s] = scaled[s];
            alias[s] = l;

            scaled[l] = (scaled[l] + scaled[s]) - 1f;

            if (scaled[l] < 1f)
                small.Enqueue(l);
            else
                large.Enqueue(l);
        }

        while (large.Count > 0)
            prob[large.Dequeue()] = 1f;

        while (small.Count > 0)
            prob[small.Dequeue()] = 1f;
    }

    public Item Sample()
    {
        int column = UnityEngine.Random.Range(0, n);
        float coin = UnityEngine.Random.value;

        return coin < prob[column]
            ? items[column]
            : items[alias[column]];
    }
}