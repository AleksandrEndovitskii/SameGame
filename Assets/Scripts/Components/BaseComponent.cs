﻿namespace Components
{
    public abstract class BaseComponent : BaseMonoBehaviour
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
    }
}
