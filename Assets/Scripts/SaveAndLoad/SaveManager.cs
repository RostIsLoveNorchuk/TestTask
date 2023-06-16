using System;
using System.Collections.Generic;
using System.IO;
using MovingUI.Buttons.Datas;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

namespace MovingUI.Save
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField]
        private List<ButtonInfo> buttonInfo;

        [Header("Button Save")]
        public Button buttonSave;

        [Header("Setting JSON")]
        public string filePath;

        public Action OnSaveSuccessful;
        public Action OnLoadSuccessful;
        public Action OnLoadUnsuccessful;

        private List<ButtonData> _buttonData;

        private void Awake()
        {
            _buttonData = new List<ButtonData>();

            buttonSave.onClick.AddListener(SaveData);
        }

        private void Start()
        {
            LoadData();
        }

        private void OnDestroy()
        {
            buttonSave.onClick.RemoveListener(SaveData);
        }

        #region Save and Load

        private void SaveData()
        {
            for (int i = 0; i < buttonInfo.Count; i++)
            {
                if (_buttonData.Count <= i)
                {
                    _buttonData.Add(buttonInfo[i].ButtonData);
                }
                else
                {
                    _buttonData[i] = buttonInfo[i].ButtonData;
                }
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(_buttonData, Formatting.Indented, settings);
            File.WriteAllText(filePath, json);
           
            OnSaveSuccessful?.Invoke();
        }

        private void LoadData()
        {
            _buttonData.Clear();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                _buttonData = JsonConvert.DeserializeObject<List<ButtonData>>(json, settings);

                for (int i = 0; i < buttonInfo.Count; i++)
                {
                    if (_buttonData != null && i < _buttonData.Count)
                    {
                        buttonInfo[i].SetDataButton(_buttonData[i]);
                    }
                    else
                    {
                        buttonInfo[i].SetDefaultDataButton();
                    }
                }

                OnLoadSuccessful?.Invoke();
            }
            else
            {
                OnLoadUnsuccessful?.Invoke();
            }
        }

        #endregion
        
    }
}
