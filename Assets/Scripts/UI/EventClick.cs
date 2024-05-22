using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private  UIManager uIManager;
    private void Awake() {
        uIManager = UIManager.Instance;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        uIManager.SetClickedTile(gameObject);
    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {

    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {

    }
}
