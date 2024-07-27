using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserStudy
{
    public class Sphere : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _hoverColor;
        [SerializeField] private Color _normalColor;

        private bool _hovered = false;
        private bool _selected = false;
        private float _hoverDuration = 2f;
        private float _hoverCountDown = 2f;

        private void Start()
        {
            OnNormal();
            _hoverCountDown = _hoverDuration;
        }

        public void OnHovered()
        {
            _renderer.material.color = _hoverColor;
            _hovered = true;
        }

        public void OnSelected()
        {
            _renderer.material.color = _selectedColor;
            _selected = true;
        }

        public void OnNormal()
        {
            _renderer.material.color = _normalColor;
            _hovered = false;
            _selected = false;
        }

        private void Update()
        {
            if (_hovered)
            {
                _hoverCountDown -= Time.deltaTime;
                if(_hoverCountDown < 0)
                {
                    _hoverCountDown = -1;
                    if (!_selected)
                    {
                        OnSelected();
                    }
                }
            }
            else
            {
                _hoverCountDown = _hoverDuration;
                OnNormal();
            }
        }
    }
}
