using System;
using System.Collections;
using System.Collections.Generic;
using MovingUI.Activate.Controllers;
using MovingUI.Save;
using MovingUI.Settings;
using UnityEngine;
using UnityEngine.Serialization;

namespace MovingUI.Activate.Managers
{
    public class ActivateManager : MonoBehaviour
    {
        [SerializeField]
        private List<ActivateController> activateControllers;
        [SerializeField]
        private SettingManager settingManager;
        [SerializeField]
        private SaveManager saveManager;
        
        private ActivateController _activeController;

        private void Awake()
        {
            activateControllers.ForEach(controller =>
            {
                controller.OnActivated += SetActivated;
            });

            settingManager.SetDefaultSetting += SetDisable;
            saveManager.OnSaveSuccessful += SetDisable;
        }

        private void OnDestroy()
        {
            activateControllers.ForEach(controller =>
            {
                controller.OnActivated -= SetActivated;
            });
            
            settingManager.SetDefaultSetting -= SetDisable;
            saveManager.OnSaveSuccessful -= SetDisable;
        }

        #region Set Status

        private void SetActivated(ActivateController activateController)
        {
            if (_activeController != null && _activeController != activateController)
            { 
                _activeController.SetStatusImage(false);
            }
            
            _activeController = activateController;
            activateController.SetStatusImage(true);
        }

        private void SetDisable()
        {
            if(_activeController != null)
            {
                _activeController.SetStatusImage(false);
                _activeController = null;
            }
        }

        #endregion
        
        
    }
}
