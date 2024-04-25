using UnityEngine;
using UnityEngine.EventSystems;

namespace HolyWater.MykytaTask.UI
{
    public class InteractionElement : MonoBehaviour, IPointerMoveHandler
    {
        private HorizontalScrollPanel horizontalScroll;

        private void Start()
        {
            horizontalScroll = GetComponentInParent<HorizontalScrollPanel>();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            horizontalScroll.IsScrolling = eventData.pointerDrag != null;
        }
    }
}