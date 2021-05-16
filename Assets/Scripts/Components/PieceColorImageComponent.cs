using Models;
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

        protected override void Initialize()
        {
            _image = this.gameObject.GetComponent<Image>();
        }
        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
            if (_pieceView == null)
            {
                Debug.LogError($"{this.GetType().Name}.{nameof(PieceModelChanged)} aborted" +
                                 $"\n {nameof(_pieceView)} == null");

                return;
            }

            _pieceView.PieceModelChanged += PieceModelChanged;
        }
        protected override void UnSubscribe()
        {
        }

        private void PieceModelChanged(PieceModel pieceModel)
        {
            _image.color = pieceModel.Color;
        }
    }
}
