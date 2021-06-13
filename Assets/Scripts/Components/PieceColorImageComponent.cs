using System;
using Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components
{
    [RequireComponent(typeof(Image))]
    public class PieceColorImageComponent : BaseComponent
    {
        [SerializeField]
        private PieceView _pieceView;

        private Image _image;
        private IDisposable _pieceViewOnPieceModelChangedSubscription;

        public override void Initialize()
        {
            _image = this.gameObject.GetComponent<Image>();
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
            if (_pieceView == null)
            {
                Debug.LogError($"{this.GetType().Name}.{nameof(PieceViewOnPieceModelChanged)} aborted" +
                                 $"\n {nameof(_pieceView)} == null");

                return;
            }

            _pieceViewOnPieceModelChangedSubscription = _pieceView.PieceModel.Subscribe(PieceViewOnPieceModelChanged);
        }
        public override void UnSubscribe()
        {
            _pieceViewOnPieceModelChangedSubscription?.Dispose();
        }

        private void PieceViewOnPieceModelChanged(PieceModel pieceModel)
        {
            if (pieceModel == null)
            {
                return;
            }

            _image.color = pieceModel.Color;
        }
    }
}
