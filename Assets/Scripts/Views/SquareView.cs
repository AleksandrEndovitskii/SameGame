using System;
using Components;
using Helpers;
using Managers;
using Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Views
{
    [RequireComponent(typeof(LayoutElement))]
    public class SquareView : BaseComponent
    {
        public ReactiveProperty<SquareModel> SquareModel = new ReactiveProperty<SquareModel>();
        public float Width => _layoutElement.preferredWidth;
        public float Height => _layoutElement.preferredHeight;
        public Position Position => SquareModel.Value.Position;

        private LayoutElement _layoutElement;
        private PieceView _pieceViewInstance;
        private IDisposable _squareModelOnPieceModelChangedSubscription;
        private IDisposable _squareModelOnChangedSubscription;

        public void Initialize(SquareModel squareModel)
        {
            SquareModel.Value = squareModel;
        }
        protected override void Initialize()
        {
            _layoutElement = this.gameObject.GetComponent<LayoutElement>();
        }
        protected override void UnInitialize()
        {
            _squareModelOnPieceModelChangedSubscription?.Dispose();
        }

        protected override void Subscribe()
        {
            _squareModelOnChangedSubscription = SquareModel.Pairwise().Subscribe(SquareModelOnChanged);
        }
        protected override void UnSubscribe()
        {
            _squareModelOnChangedSubscription?.Dispose();
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

        private void SquareModelOnChanged(Pair<SquareModel> pair)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                      $"\n{pair.Previous?.Position}->{pair.Current?.Position}");

            if (pair.Previous?.PieceModel != null)
            {
                _squareModelOnPieceModelChangedSubscription?.Dispose();
            }
            if (pair.Current?.PieceModel != null)
            {
                _squareModelOnPieceModelChangedSubscription =
                    pair.Current?.PieceModel.Subscribe(SquareModelOnPieceModelChanged);
            }
        }
        private void SquareModelOnPieceModelChanged(PieceModel pieceModel)
        {
            TryDestroyPieceView(_pieceViewInstance);

            _pieceViewInstance = TryCreatePieceView(pieceModel);
        }
    }
}
