using System;
using UnityEngine;

namespace MovingUI.Buttons.Datas
{
    [Serializable]
    public class ButtonData
    {
        private float _scale;
        private float _opacity;
        private Vector2 _transform;
        
        public float Scale
        {
            get => _scale;
            set => _scale = value; 
        }
        public float Opacity    
        {
            get => _opacity;
            set => _opacity = value; 
        }

        public Vector2 Transform
        {
            get => _transform;
            set => _transform = value;
        }
        
        public ButtonData(float scale, float opacity, Vector2 transform)
        {
            _scale = scale;
            _opacity = opacity;
            _transform = transform;
        }
    }
}