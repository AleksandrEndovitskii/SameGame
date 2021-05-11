using UnityEngine;

namespace Components
{
    public abstract class BaseComponent : MonoBehaviour
    {
        private void Awake()
        {
            Initialize();

            Subscribe();
        }
        private void OnDestroy()
        {
            UnSubscribe();

            UnInitialize();
        }

        protected abstract void Initialize();
        protected abstract void UnInitialize();
        protected abstract void Subscribe();
        protected abstract void UnSubscribe();
    }
}
