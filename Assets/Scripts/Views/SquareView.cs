using Components;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(LayoutElement))]
    public class SquareView : BaseComponent
    {
        public float Width => _layoutElement.preferredWidth;
        public float Height => _layoutElement.preferredHeight;

        private LayoutElement _layoutElement;

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
