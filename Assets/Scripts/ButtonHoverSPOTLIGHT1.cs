using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverImage; // Assign your image in the inspector

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverImage != null)
        {
            hoverImage.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverImage != null)
        {
            hoverImage.SetActive(false);
        }
    }
}
