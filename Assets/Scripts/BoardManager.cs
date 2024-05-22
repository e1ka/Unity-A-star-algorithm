using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BoardManager : MonoBehaviour
{
    private static BoardManager _instance;
    public static BoardManager Instance
    {
        get 
        {
            if (_instance == null)
            {
                Debug.LogError("BoardManager is null");
            }
            return _instance;
        }
    }
    [SerializeField] public bool LimitedRange = true;
    [SerializeField] public int maxRange = 100;
    [SerializeField] int width = 4;
    [SerializeField] int height = 4;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] LineRenderer lineRenderer;

    public Tile [,] Tiles { get; set; }

    private Tile startTile;
    private Tile finishTile;
    private int playerPosX;
    private int playerPosY;
    private GameObject tilesPool;
    private List<(int, int)> obstacles;
    private List<Tile> road;
    private GameObject player;
    
    private bool showPath = true;
    private bool updatedPath = false;
    void Awake() 
    {
        _instance = this;
    }
    void Start()
    {
        ResetTileBoard();
        PlacePlayer();
        ResetPlayer();
    }

    void Update()
    {
        if(showPath && startTile && finishTile && !updatedPath)
        {
            updatedPath = true;
            ShowRoad();
        }
    }

    public void ToggleLimit()
    {
        LimitedRange = !LimitedRange;
    }
    private void ResetTileBoard()
    {
        obstacles = new List<(int, int)>();
        road = new List<Tile>();
        if(tilesPool != null)
        {
            Destroy(tilesPool);
        }
        
        Tiles = new Tile[width, height];
        tilesPool = new GameObject("tilesPool");
        tilesPool.transform.parent = transform;
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var newTile = Instantiate(tilePrefab, tilesPool.transform);
                newTile.transform.position = new Vector3(i * 1.15f, 0, j * 1.15f);
                Tiles[i,j] = newTile.GetComponent<Tile>();
                Tiles[i,j].SetArrayCords(i,j);
            }
        }
    }

    public void AddObstacle(int x, int y)
    {
        if(obstacles == null)
            obstacles = new List<(int, int)>();
        obstacles.Add((x, y));
        ClearRoad();
    }

    public void RemoveObstacle(int x, int y)
    {
        if (obstacles.Contains((x, y)))
        {
            obstacles.Remove((x, y));
        }
        ClearRoad();
    }

    public void SetStart(Tile start)
    {
        if(startTile != null)
        {
            startTile.SetTraversable();
            startTile = null;
        }
        startTile = start;
        ClearRoad();
    }

    public void SetFinish(Tile finish)
    {
        if(finishTile != null)
        {
            finishTile.SetTraversable();
            finishTile = null;
        }
        finishTile = finish;
        ClearRoad();
    }

    public void ClearClickedTile(Tile clickedTile)
    {
        if (clickedTile == startTile)
            startTile = null;
        if (clickedTile == finishTile)
            finishTile = null;

        RemoveObstacle(clickedTile.ArrayX, clickedTile.ArrayY);
    }
    private void PlacePlayer()
    {
        player = Instantiate(playerPrefab, this.transform);
    }

    private void ResetPlayer()
    {
        player.transform.position = Tiles[0,0].transform.position;
        playerPosX = 0;
        playerPosY = 0;
    }
    public Vector3 GetNextTile(MoveDirection direction, Vector3 currentPos)
    {
        if(!tilesPool)
            return currentPos;
        
        int _playerPosY = playerPosY;
        int _playerPosX = playerPosX;
        if(direction == MoveDirection.Up)
        {
            if(playerPosY + 1 < height) _playerPosY++;
        }
        else if(direction == MoveDirection.Down)
        {
            if(playerPosY - 1 >= 0) _playerPosY--;
            
        }
        else if(direction == MoveDirection.Left)
        {
            if(playerPosX - 1 >= 0) _playerPosX--;
        }
        else if(direction == MoveDirection.Right)
        {
            if(playerPosX + 1 < width) _playerPosX++;
        }

        var nextTile = Tiles[_playerPosX, _playerPosY];
        if(nextTile.Traversable)
        {
            playerPosX = _playerPosX;
            playerPosY = _playerPosY;
            return nextTile.Pos;
        }
        else
            return currentPos;
    }
    public void ChangeBoardDimensions(int newWidth = 0, int newHeight = 0)
    {
        ClearRoad();
        if(newWidth > 0) 
            width = newWidth;

        if(newHeight > 0)
            height = newHeight;

        RecalculateBoard();
    }
    private void RecalculateBoard()
    {
        updatedPath = false;
        startTile = null;
        finishTile = null;
        ResetTileBoard();
        ResetPlayer();
    }
    public void ShowRoad()
    {
        PathFinding.CreateGrid(width, height, obstacles, (startTile.ArrayX, startTile.ArrayY), (finishTile.ArrayX, finishTile.ArrayY));
        var p = PathFinding.ASearch();
        if(p == null)
        {
            Debug.LogWarning("PATH NOT FOUND");
            return;
        }
        while (p.Count > 0)
        {
            var con = p.Pop();
            road.Add(Tiles[con.x, con.y]);
        }

        PathRenderer.GenerateRoadLine(road, lineRenderer);
    }
    private void ClearRoad()
    {
        if(road != null)
        {
            road.Clear();
        }
        updatedPath = false;
        PathRenderer.ClearRoadLine(lineRenderer);
    }
}
