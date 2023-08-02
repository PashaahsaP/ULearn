

var graph = Graph.MakeGraph(
    0, 2,
    2, 1,
    2, 3,
    3, 4
     
    );

var path = graph.TarjanAlgorithm( );
Console.WriteLine(
    path.Select(z => z.Number.ToString()).Aggregate((a, b) => a + " " + b));

static IEnumerable<Node> BreathSearch(Node startNode)
{
    var visited = new HashSet<Node>();
    var queue = new Queue<Node>();
    queue.Enqueue(startNode);
    visited.Add(startNode);
    while (queue.Count != 0)
    {
        var node = queue.Dequeue();
        yield return node;
        foreach (var node1 in node.IncedentalNode()
                                  .Where(node => !visited.Contains(node)))
        {
            queue.Enqueue(node1);
            visited.Add(node1);
        }
    }
}
static IEnumerable<Node> DeepthSearch(Node startNode)
{
    var visited = new HashSet<Node>();
    var stack = new Stack<Node>();
    visited.Add(startNode);
    stack.Push(startNode);

    while (stack.Count != 0)
    {
        var node = stack.Pop();
        yield return node;
        foreach (var node1 in node.IncedentalNode()
                                  .Where(node => !visited.Contains(node)))
        {
            stack.Push(node1);
            visited.Add(node1);
        }
    }
}
static IEnumerable<IEnumerable<Node>> FindConnectedNodes(Graph graph)
{
    var marked = new HashSet<Node>();

    while (true)
    {
        var node = graph.Nodes.Where(x => !marked.Contains(x)).FirstOrDefault();
        if (node == null) break;
        var visitedNodes = BreathSearch(node);
        foreach (var innerNode in visitedNodes)
            marked.Add(innerNode);
        yield return visitedNodes;
    }
}




public static class GragpExtension
{
    public static List<Node> TarjanAlgorithm(this Graph graph)
    {
        var topSort = new List<Node>();
        var states = graph.Nodes.ToDictionary(node => node, node => State.White);
        while (true)
        {
            var nodeToSearch = states
                .Where(z => z.Value == State.White)
                .Select(z => z.Key)
                .FirstOrDefault();
            if (nodeToSearch == null) break;

            if (!TarjanDepthSearch(nodeToSearch, states, topSort))
                return null;
        }
        topSort.Reverse();
        return topSort;
    }

    public static bool TarjanDepthSearch(Node node, Dictionary<Node, State> states, List<Node> topSort)
    {
        if (states[node] == State.Gray) return false;
        if (states[node] == State.Black) return true;
        states[node] = State.Gray;

        var outgoingNodes = node.IncendentalEdges()
            .Where(edge => edge.From == node)
            .Select(edge => edge.To);
        foreach (var nextNode in outgoingNodes)
            if (!TarjanDepthSearch(nextNode, states, topSort)) return false;

        states[node] = State.Black;
        topSort.Add(node);
        return true;
    }
    public static IEnumerable<Node> FindShortestPath(this Graph graph, Node from, Node to)
    {
        var track = GetPath(from, to);

        //return path
        var pathItem = to;
        yield return to;
        while (track[pathItem] != null)
        {
            yield return track[pathItem];
            pathItem = track[pathItem];
        }
    }

    private static Dictionary<Node, Node> GetPath(Node from, Node to)
    {
        var track = new Dictionary<Node, Node>();
        track.Add(from, null);
        var queue = new Queue<Node>();
        queue.Enqueue(from);

        while (queue.Count != 0)
        {
            var node = queue.Dequeue();
            foreach (var icedentalNode in node.IncedentalNode().Where(x => !track.ContainsKey(x)))
            {
                queue.Enqueue(icedentalNode);
                track[icedentalNode] = node;
            }

            if (track.ContainsKey(to)) break;
        }

        return track;
    }
}

public class Graph
{
    private readonly Node[] _nodes;
    public IEnumerable<Node> Nodes
    {
        get
        {
            foreach (var node in _nodes)
                yield return node;
        }
    }

    public Graph(int count)
    {
        _nodes = new Node[count];
        for (int i = 0; i < count; i++)
            _nodes[i] = new Node(i);
    }

    public int Length() => _nodes.Length;
    public Node this[int i] => _nodes[i];
    public void Connect(int ind1, int ind2)
    {
        Node.Connect(_nodes[ind1], _nodes[ind2], this);
    }
    public static Graph MakeGraph(params int[] nodesConnections)
    {
        var graph = new Graph(nodesConnections.Max() + 1);

        for (int i = 0; i < nodesConnections.Length; i += 2)
        {
            graph.Connect(nodesConnections[i], nodesConnections[i + 1]);
        }

        return graph;
    }

}

public class Node
{
    private List<Edge> edges = new List<Edge>();
    public int Number { get; set; }

    public Node(int number)
    {
        Number = number;
    }
    public static Edge Connect(Node first, Node second, Graph graph)
    {
        if (!graph.Nodes.Contains(first) || !graph.Nodes.Contains(second)) throw new ArgumentException();
        var edge = new Edge(first, second);
        first.edges.Add(edge);
        second.edges.Add(edge);
        return edge;

    }
    public void Disconnect(Edge edge)
    {
        edge.From.edges.Remove(edge);
        edge.To.edges.Remove(edge);
    }
    public IEnumerable<Edge> IncendentalEdges()
    {
        foreach (var edge in edges)
            yield return edge;
    }
    public IEnumerable<Node> IncedentalNode() => IncendentalEdges().Select(x => x.OtherNode(this));

    public override string ToString()
    {
        return Number.ToString();
    }
}

public class Edge
{
    public readonly Node From;
    public readonly Node To;

    public Edge(Node one, Node two)
    {
        From = one;
        To = two;
    }


    public bool IsIncidental(Node node)
    {
        if (node == From || node == To) return true;
        return false;
    }

    public Node OtherNode(Node node)
    {
        if (!IsIncidental(node)) throw new ArgumentException();
        if (node == From) return To;
        return From;

    }
}
public enum State
{
    White,
    Gray,
    Black
}