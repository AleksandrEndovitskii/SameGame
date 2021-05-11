using System;
using Components;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(LayoutElement))]
    public class SquareView : BaseComponent
    {
        public Action<SquareModel> SquareModelChanged = delegate {  };

        public SquareModel SquareModel
        {
            get
            {
                return _squareModel;
            }
            set
            {
                if (_squareModel == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{nameof(SquareModelChanged)}");

                if (_squareModel != null)
                {
                    _squareModel.PieceModelChanged -= PieceModelChanged;
                }
                _squareModel = value;
                if (_squareModel != null)
                {
                    _squareModel.PieceModelChanged += PieceModelChanged;
                }

                SquareModelChanged.Invoke(_squareModel);
            }
        }

        public float Width => _layoutElement.preferredWidth;
        public float Height => _layoutElement.preferredHeight;

        public Vector2 Index => SquareModel.Index;

        private LayoutElement _layoutElement;

        private SquareModel _squareModel;

        private PieceView _pieceViewInstance;

        public void Initialize(SquareModel squareModel)
        {
            SquareModel = squareModel;
        }

        protected override void Initialize()
        {
            _layoutElement = this.gameObject.GetComponent<LayoutElement>();
        }
        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
        }
        protected override void UnSubscribe()
        {
        }

        private void PieceModelChanged(PieceModel pieceModel)
        {
            if (_pieceViewInstance != null)
            {
                Destroy(_pieceViewInstance.gameObject);
                _pieceViewInstance = null;
            }

            if (pieceModel == null)
            {
                return;
            }

            _pieceViewInstance = GameManager.Instance.GameObjectsManager.CreatePiece(pieceModel);
            _pieceViewInstance.gameObject.transform.SetParent(this.gameObject.transform);
            _pieceViewInstance.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
