using System;
using System.Collections;
using System.Collections.Generic;
using MovingUI.DragAndDrops;
using UnityEngine;

namespace MovingUI.Buttons.Datas
{
    public class ButtonInfo : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private MovingUIElement _movingUIElement;
        private Vector2 _defaultTransform;
       
        private ButtonData _buttonData;
        public ButtonData ButtonData => _buttonData;
        
        public Action<ButtonData, ButtonInfo> SetDataSlider;
       
        private const float DEFAULT_SCALE = 1;
        private const float DEFAULT_OPACITY = 1;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _movingUIElement = GetComponent<MovingUIElement>();
            
            _defaultTransform = _rectTransform.anchoredPosition;
            
            _movingUIElement.OnDragStart += GetDataButton;
        }

        private void OnDestroy()
        {
            _movingUIElement.OnDragStart -= GetDataButton;
        }


        #region Get Data

        private void GetDataButton()
        {
            _buttonData = new ButtonData(_rectTransform.localScale.x, _canvasGroup.alpha, _rectTransform.anchoredPosition);
            SetDataSlider?.Invoke(_buttonData, this);
        }

        #endregion
        
        #region Set Data

        public void SetDataButton(ButtonData buttonData)
        {
            if (buttonData != null)
            {
                _buttonData = buttonData;
        
                _rectTransform.localScale = new Vector3(_buttonData.Scale, _buttonData.Scale, _buttonData.Scale);
                _canvasGroup.alpha = _buttonData.Opacity;
                _rectTransform.anchoredPosition = _buttonData.Transform;
            }
        }

        public void SetDefaultDataButton()
        {
            _rectTransform.localScale = new Vector3(DEFAULT_SCALE, DEFAULT_SCALE, DEFAULT_SCALE);
            _canvasGroup.alpha = DEFAULT_OPACITY;
            _rectTransform.anchoredPosition = _defaultTransform;
            
            _buttonData = new ButtonData(_rectTransform.localScale.x, _canvasGroup.alpha, _rectTransform.anchoredPosition);
        }
        
        public void SetScale(float value)
        {
            _rectTransform.localScale = new Vector3(value, value, value);

            _buttonData = new ButtonData(_rectTransform.localScale.x, _canvasGroup.alpha, _rectTransform.anchoredPosition);
        }
        
        public void SetOpacity(float value)
        {
            _canvasGroup.alpha = value;
            
            _buttonData = new ButtonData(_rectTransform.localScale.x, _canvasGroup.alpha, _rectTransform.anchoredPosition);
        }

        #endregion
       
    }
}

