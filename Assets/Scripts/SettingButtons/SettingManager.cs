using System;
using System.Collections.Generic;
using MovingUI.Buttons.Datas;
using MovingUI.Save;
using UnityEngine;
using UnityEngine.UI;

namespace MovingUI.Settings
{
    public class SettingManager : MonoBehaviour
    {
        [Header("Scale")]
        public SettingView scaleSetting;

        [Header("Opacity")]
        public SettingView opacitySetting;

        [Header("Buttons")]
        [SerializeField]
        private List<ButtonInfo> buttonInfo;

        [Header("Setting Button")]
        [SerializeField]
        private Button defaultButton;
        
        [Header("Setting")]
        [SerializeField]
        private GameObject setting;
        
        [Header("Save")]
        [SerializeField]
        private SaveManager saveManager;
        
        private ButtonInfo _currentInfo;

        public Action SetDefaultSetting;
        
        private void Awake()
        {
            buttonInfo.ForEach(info =>
            {
                info.SetDataSlider += SetDataSlider;
            });

            scaleSetting.Slider.onValueChanged.AddListener(SetScaleButton);
            opacitySetting.Slider.onValueChanged.AddListener(SetOpacityButton);
            defaultButton.onClick.AddListener(SetDefaultDatas);
           
            saveManager.OnSaveSuccessful += () =>
            {
                setting.SetActive(false);
            };
        }

        private void OnDestroy()
        {
            buttonInfo.ForEach(info =>
            {
                info.SetDataSlider -= SetDataSlider;
            });

            scaleSetting.Slider.onValueChanged.RemoveListener(SetScaleButton);
            opacitySetting.Slider.onValueChanged.RemoveListener(SetOpacityButton);
            
            saveManager.OnSaveSuccessful -= () =>
            {
                setting.SetActive(false);
            };
        }
        
        #region Set Data
        
        private void SetDataSlider(ButtonData buttonData, ButtonInfo info)
        {
            if (!setting.activeSelf)
            {
                setting.SetActive(true);
            }

            _currentInfo = info;

            scaleSetting.Slider.value = buttonData.Scale;
            SetScaleText(buttonData.Scale);
            SetScaleButton(buttonData.Scale);

            opacitySetting.Slider.value = buttonData.Opacity;
            SetOpacityText(buttonData.Opacity);
            SetOpacityButton(buttonData.Opacity);
        }

        private void SetDefaultDatas()
        {
            setting.SetActive(false);
            
            buttonInfo.ForEach(info =>
            {
                info.SetDefaultDataButton();
            });
            
            SetDefaultSetting?.Invoke();
        }
        
        private void SetScaleButton(float value)
        {
            if (_currentInfo != null)
            {
                _currentInfo.SetScale(value);
            }

            SetScaleText(value);
        }

        private void SetOpacityButton(float value)
        {
            if (_currentInfo != null)
            {
                _currentInfo.SetOpacity(value);
            }

            SetOpacityText(value);
        }
        
        #endregion
        
        #region Set Text

        private void SetScaleText(float value)
        {
            scaleSetting.Text.text = (float)Math.Round(value, 2) * 100 + "%";
        }

        private void SetOpacityText(float value)
        {
            opacitySetting.Text.text = (float)Math.Round(value, 2) * 100 + "%";
        }

        #endregion

    }
}
