using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get 
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is null");
            }
            return _instance;
        }
    }
    public Tile ClickedTile { get; private set; }
    [SerializeField] Text widthText;
    [SerializeField] Text heightText;
    [SerializeField] GameObject tileMenu;
    private BoardManager boardManager;
    void Awake() 
    {
        _instance = this;
    }
    void Start() {
        boardManager = BoardManager.Instance;
    }
    public void WidthChanged()
    {
        int newValue;
        bool isInt = int.TryParse(widthText.text, out newValue);
        if (!isInt)
        {
            newValue = 1;
        }
        boardManager.ChangeBoardDimensions(newWidth: newValue);
    }
    public void HeightChanged()
    {
        bool isInt = int.TryParse(heightText.text, out int newValue);
        if (!isInt)
        {
            newValue = 1;
        }
        boardManager.ChangeBoardDimensions(newHeight: newValue);
    }

    public void SetClickedTile(GameObject gameObject)
    {
        ClickedTile = gameObject.GetComponent<Tile>();
        ShowTileMenu();
    }
    private void ShowTileMenu()
    {
        tileMenu.SetActive(true);
    }

    public void SetClickedObstacle()
    {
        NullBoardInfo();
        ClickedTile.SetObstacle();
        BoardManager.Instance.AddObstacle(ClickedTile.ArrayX, ClickedTile.ArrayY);
    }
    public void SetClickedTraversable()
    {
        NullBoardInfo();
        ClickedTile.SetTraversable();
        BoardManager.Instance.RemoveObstacle(ClickedTile.ArrayX, ClickedTile.ArrayY);
    }
    public void SetClickedStart()
    {
        NullBoardInfo();
        ClickedTile.SetStart();
        BoardManager.Instance.SetStart(ClickedTile);
    }
    public void SetClickedFinish()
    {

        NullBoardInfo();
        ClickedTile.SetFinish();
        BoardManager.Instance.SetFinish(ClickedTile);
    }

    private void NullBoardInfo()
    {
        if(ClickedTile.Start1 || ClickedTile.Finish || !ClickedTile.Traversable)
        {
            boardManager.ClearClickedTile(ClickedTile);
        }
    }
}
