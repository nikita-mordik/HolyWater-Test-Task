using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HolyWater.MykytaTask.UI
{
    public class HorizontalScrollPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform viewport;
        [SerializeField] private RectTransform content;
        [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
        [SerializeField] private RectTransform[] items;

        private Vector2 oldVelocity = Vector2.zero;
        private bool isUpdateVelocity;
        private RectTransform[] allItems;
        private int centeredPanel;
        private bool isSelected;

        public bool IsScrolling { get; set; }

        private void Start()
        {
            var itemsToAdd = CalculateItemsToAdd();
            CreateLastSiblingItems(itemsToAdd);
            CreateFirstSiblingItems(itemsToAdd);
            content.localPosition = CentralContentPanel(itemsToAdd);

            allItems = new RectTransform[content.childCount];
            for (int i = 0; i < content.childCount; i++)
            {
                allItems[i] = (RectTransform) content.GetChild(i);
            }
        }
        
        private void Update()
        {
            UpdateScrollVelocity();
            CheckToRightInfiniteScrolling();
            CheckToLeftInfiniteScrolling();

            if (scrollRect.velocity.magnitude <= 0 && !IsScrolling)
            {
                int nearestPanel = GetNearestPanel();
                GoToPanel(nearestPanel);
            }
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            IsScrolling = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsScrolling = false;
        }

        private void UpdateScrollVelocity()
        {
            if (isUpdateVelocity)
            {
                isUpdateVelocity = false;
                scrollRect.velocity = oldVelocity;
            }
        }

        private void CheckToRightInfiniteScrolling()
        {
            if (content.localPosition.x > 0)
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scrollRect.velocity;
                content.localPosition -=
                    new Vector3(items.Length * (items[0].rect.width + horizontalLayoutGroup.spacing), 0f, 0f);
                isUpdateVelocity = true;
            }
        }

        private void CheckToLeftInfiniteScrolling()
        {
            if (content.localPosition.x < (0 - items.Length * (items[0].rect.width + horizontalLayoutGroup.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scrollRect.velocity;
                content.localPosition +=
                    new Vector3(items.Length * (items[0].rect.width + horizontalLayoutGroup.spacing), 0f, 0f);
                isUpdateVelocity = true;
            }
        }

        private int CalculateItemsToAdd() => 
            Mathf.CeilToInt(viewport.rect.width / (items[0].rect.width + horizontalLayoutGroup.spacing));

        private void CreateLastSiblingItems(int itemsToAdd)
        {
            for (int i = 0; i < itemsToAdd; i++)
            {
                var rect = Instantiate(items[i % items.Length], content);
                rect.SetAsLastSibling();
            }
        }

        private void CreateFirstSiblingItems(int itemsToAdd)
        {
            for (int i = 0; i < itemsToAdd; i++)
            {
                var index = items.Length - i - 1;
                while (index < 0)
                {
                    index += items.Length;
                }
                var rect = Instantiate(items[index], content);
                rect.SetAsFirstSibling();
            }
        }

        private Vector3 CentralContentPanel(int itemsToAdd) =>
            new((0 - items[0].rect.width + horizontalLayoutGroup.spacing) * itemsToAdd,
                content.localPosition.y,
                content.localPosition.z);
        
        private int GetNearestPanel()
        {
            float[] distances = new float[allItems.Length];
            for (int i = 0; i < allItems.Length; i++)
            {
                distances[i] = GetDisplacementFromCenter(i).magnitude;
            }

            int nearestPanel = 0;
            float minDistance = Mathf.Min(distances);
            for (int i = 0; i < allItems.Length; i++)
            {
                if (minDistance == distances[i])
                {
                    nearestPanel = i;
                    break;
                }
            }
            
            return nearestPanel;
        }
        
        private Vector2 GetDisplacementFromCenter(int index)
        {
            return allItems[index].anchoredPosition + content.anchoredPosition - new Vector2(viewport.rect.width * (0.5f - content.anchorMin.x), 
                viewport.rect.height * (0.5f - content.anchorMin.y));
        }

        private void GoToPanel(int panelNumber)
        {
            centeredPanel = panelNumber;
            isSelected = true;
            scrollRect.inertia = false;
            
            float xOffset = viewport.rect.width / 2f;
            float yOffset = -viewport.rect.height / 2f;
            Vector2 offset = new Vector2(xOffset, yOffset);
            Vector2 targetPosition = -allItems[centeredPanel].anchoredPosition + offset;
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition,  t: Time.deltaTime * 10f);
        }
    }
}