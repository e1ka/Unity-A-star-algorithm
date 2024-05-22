using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Color ObstacleColor;
    [SerializeField] Color StartColor;
    [SerializeField] Color TraversableColor;
    [SerializeField] Color FinishColor;

    public bool Traversable { get => traversable; set => traversable = value; }
    public bool Finish { get => finish; set => finish = value; }
    public bool Start1 { get => start1; set => start1 = value; }
    public Vector3 Pos { get => pos; set => pos = value; }
    public int ArrayY { get => arrayY; set => arrayY = value; }
    public int ArrayX { get => arrayX; set => arrayX = value; }

    private int arrayX;
    private int arrayY;
    private bool traversable = true;
    private bool finish = false;
    private bool start1 = false;
    private Vector3 pos;
    private Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        if (renderer != null)
            renderer.material.color = TraversableColor;
        Pos = this.transform.position;
    }
    
    private void SetDefault()
    {
        Finish = false;
        Start1 = false;
        Traversable = true;
    }
    public void SetObstacle()
    {
        SetDefault();
        Traversable = false;
        ChangeColor(ObstacleColor);
    }

    public void SetTraversable()
    {
        SetDefault();
        ChangeColor(TraversableColor);
    }

    public void SetStart()
    {
        SetDefault();
        Start1 = true;
        ChangeColor(StartColor);
    }
    public void SetFinish()
    {
        SetDefault();
        Finish = true;
        ChangeColor(FinishColor);
    }
    public void ChangeColor(Color newColor)
    {
        if (renderer != null)
        {
            renderer.material.color = newColor;
        }
    }
    public void SetArrayCords(int x, int y)
    {
        ArrayX = x;
        ArrayY = y;
    }
}
