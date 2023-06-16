using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MovingUI.Save.Notification
{
    public class NotificationSave : MonoBehaviour
    {
        [SerializeField]
        private SaveManager saveManager;

        [Header("Notification Game Object")]
        [SerializeField]
        private RectTransform notification;
        [SerializeField]
        private Text textNotification;

        private const string LOAD_TEXT = "Successful Load!";
        private const string SAVE_TEXT = "Successful Save!";

        private float _startLocalYPosition;
        
        private void Awake()
        {
            _startLocalYPosition = notification.localPosition.y;
            saveManager.OnSaveSuccessful += ShowNotificationSave;
            saveManager.OnLoadSuccessful += ShowNotificationLoad;
        }

        private void OnDestroy()
        {
            saveManager.OnSaveSuccessful -= ShowNotificationSave;
            saveManager.OnLoadSuccessful -= ShowNotificationLoad;
        }

        #region Notification

        private void ShowNotificationSave()
        {
            notification.DOLocalMoveY(_startLocalYPosition - 200, 1f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                notification.DOLocalMoveY(_startLocalYPosition, 1f).SetEase(Ease.InBack);
            });
            
            textNotification.text = SAVE_TEXT;
        }
        
        private void ShowNotificationLoad()
        {
            notification.DOLocalMoveY(_startLocalYPosition - 200, 1f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                notification.DOLocalMoveY(_startLocalYPosition, 1f).SetEase(Ease.InBack);
            });
            
            textNotification.text = LOAD_TEXT;
        }

        #endregion
     
    }
}
