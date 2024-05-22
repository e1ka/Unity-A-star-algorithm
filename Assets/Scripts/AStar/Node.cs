using System.Collections.Generic;

public class Node {
    public bool Walkable { get; set; } = true;
    public Node Parent { get; set; }
    public List<Node> Neighbours { get; set; }
    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H; 
    public (int x, int y) Pos { get; set; }
    
}