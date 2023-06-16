using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MovingUI.DragAndDrops
{
    public class MovingUIElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private Canvas canvas;
        
        private CanvasGroup _canvasGroup;
        private RectTransform  _gameObjectTransform;
        
        public Action OnDragStart;
        public Action OnDragEnd;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _gameObjectTransform = GetComponent<RectTransform>();
        }

        #region Drag and Drop
        
        public void OnBeginDrag(PointerEventData eventData)
        {
      
            _canvasGroup.blocksRaycasts = false;
            _gameObjectTransform.SetAsLastSibling();
      
            OnDragStart?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _gameObjectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
      
            OnDragEnd?.Invoke();
        }

        #endregion
        
    }

}