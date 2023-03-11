using BlockRelated;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CellRelated
{
    public class Border : MonoBehaviour, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            BlockInteraction.Instance.DeSelect();
        }
    }
}
