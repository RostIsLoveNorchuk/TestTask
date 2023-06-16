using System;
using MovingUI.DragAndDrops;
using UnityEngine;
using UnityEngine.Serialization;

namespace MovingUI.Activate.Controllers
{
    public class ActivateController : MonoBehaviour
    {
        
        [SerializeField]
        private GameObject imageActivate;

        private MovingUIElement _movingUIElement;
        
        public Action<ActivateController> OnActivated;
        public Action<ActivateController> OnDeactivated;
        
        private void Awake()
        {
            _movingUIElement = GetComponent<MovingUIElement>();
            _movingUIElement.OnDragStart += SetEnable;
        }

        private void OnDestroy()
        {
            _movingUIElement.OnDragStart -= SetEnable;
        }

        #region Set Status

        private void SetEnable()
        {
            SetStatusImage(true);
            OnActivated?.Invoke(this);
        }

        private void SetDisable()
        {
            SetStatusImage(false);
            OnDeactivated?.Invoke(this);
        }

        public void SetStatusImage(bool status)
        {
            imageActivate.SetActive(status);
        }

        #endregion
       

    }
}

