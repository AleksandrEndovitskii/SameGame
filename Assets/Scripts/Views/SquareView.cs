using Components;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(LayoutElement))]
    public class SquareView : BaseComponent
    {
        public float Width => _layoutElement.preferredWidth;
        public float Height => _layoutElement.preferredHeight;

        public Vector2 Index => _squareModel.Index;

        private LayoutElement _layoutElement;

        private SquareModel _squareModel;

        public void Initialize(SquareModel squareModel)
        {
            _squareModel = squareModel;
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
    }
}
