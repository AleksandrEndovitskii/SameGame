using Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class UserInterfaceManager : BaseMonoBehaviour
    {
        [SerializeField]
        private Canvas _canvasPrefab;
        [SerializeField]
        private EventSystem _eventSystemPrefab;

        private Canvas CanvasInstance;
        private EventSystem EventSystemInstance;

        public override void Initialize()
        {
            CanvasInstance = Instantiate(_canvasPrefab);
            EventSystemInstance = Instantiate(_eventSystemPrefab);
        }
        public override void UnInitialize()
        {
            Destroy(EventSystemInstance);
            Destroy(CanvasInstance);
        }

        public override void Subscribe()
        {
        }
        public override void UnSubscribe()
        {
        }
    }
}
