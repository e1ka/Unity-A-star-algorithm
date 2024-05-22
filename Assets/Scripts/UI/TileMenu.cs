using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMenu : MonoBehaviour
{
    UIManager uiManager;
    // Start is called before the first frame update
    void Awake()
    {
        uiManager = UIManager.Instance;
    }

    public void SetObstacle()
    {
        uiManager.SetClickedObstacle();
        this.gameObject.SetActive(false);
    }
    public void SetTraversable()
    {
        uiManager.SetClickedTraversable();
        this.gameObject.SetActive(false);
    }
    public void SetFinish()
    {
        uiManager.SetClickedFinish();
        this.gameObject.SetActive(false);
    }
    public void SetStart()
    {
        uiManager.SetClickedStart();
        this.gameObject.SetActive(false);
    }

}
