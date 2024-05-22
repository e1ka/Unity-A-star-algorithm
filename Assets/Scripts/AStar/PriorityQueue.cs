using System.Collections.Generic;
using System.Linq;

public class PriorityQueue<T>
{
    private Dictionary<Node, int> elements = new Dictionary<Node, int>();

    public int Count => elements.Count;

    public void Clear() => elements.Clear();

    public void Enqueue(Node item, int priority)
    {
        elements.Add(key: item, value: priority);
    }

    public Node Dequeue()
    {
        var bestItem = elements.FirstOrDefault();
        int bestPriority = bestItem.Value;
        foreach(KeyValuePair<Node, int> item in elements)
        {
            if(item.Value < bestPriority)
            {
                bestItem = item;            
                bestPriority = item.Value;
            }
        }
        elements.Remove(bestItem.Key);
        return bestItem.Key;
    }

    public bool Contains(Node item)
    {
        if(elements.ContainsKey(item)) return true;
        else return false;
    }
}