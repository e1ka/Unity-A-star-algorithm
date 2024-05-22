using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private BoardManager boardManager;

    void Start()
    {
        boardManager = BoardManager.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        { 
            transform.position = boardManager.GetNextTile(MoveDirection.Up, transform.position);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        { 
            transform.position = boardManager.GetNextTile(MoveDirection.Down, transform.position);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        { 
            transform.position = boardManager.GetNextTile(MoveDirection.Left, transform.position);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        { 
            transform.position = boardManager.GetNextTile(MoveDirection.Right, transform.position);
        }
    }
}
