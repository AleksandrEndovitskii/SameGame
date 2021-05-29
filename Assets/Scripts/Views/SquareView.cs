using System;
using Components;
using Helpers;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Utils;

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

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_squareModel}->{value}");

                if (_squareModel != null)
                {
                    _squareModel.PieceModelChanged -= SquareModelOnPieceModelChanged;
                }
                _squareModel = value;
                if (_squareModel != null)
                {
                    _squareModel.PieceModelChanged += SquareModelOnPieceModelChanged;
                }

                SquareModelChanged.Invoke(_squareModel);
            }
        }
        public float Width => _layoutElement.preferredWidth;
        public float Height => _layoutElement.preferredHeight;
        public Position Position => SquareModel.Position;

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

        private void TryDestroyPieceView(PieceView pieceViewInstance)
        {
            if (pieceViewInstance == null)
            {
                return;
            }

            Destroy(pieceViewInstance.gameObject);
        }
        private PieceView TryCreatePieceView(PieceModel pieceModel)
        {
            if (pieceModel == null)
            {
                return null;
            }

            var pieceViewInstance = GameManager.Instance.GameObjectsManager.CreatePiece(pieceModel);
            pieceViewInstance.gameObject.transform.SetParent(this.gameObject.transform);
            pieceViewInstance.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            return pieceViewInstance;
        }

        private void SquareModelOnPieceModelChanged(PieceModel pieceModel)
        {
            TryDestroyPieceView(_pieceViewInstance);

            _pieceViewInstance = TryCreatePieceView(pieceModel);
        }
    }
}
