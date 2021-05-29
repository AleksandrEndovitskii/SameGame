using System;
using Components;
using Helpers;
using Models;
using UniRx;
using UnityEngine;

namespace Views
{
    public class PieceView : BaseComponent
    {
        public ReactiveProperty<PieceModel> PieceModel = new ReactiveProperty<PieceModel>();

        private IDisposable _pieceModelPieceModelOnChangedSubscription;

        public void Initialize(PieceModel pieceModel)
        {
            PieceModel.Value = pieceModel;
        }
        protected override void Initialize()
        {
        }
        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
            _pieceModelPieceModelOnChangedSubscription = PieceModel.Subscribe(PieceModelOnChanged);
        }
        protected override void UnSubscribe()
        {
            _pieceModelPieceModelOnChangedSubscription?.Dispose();
        }

        private void PieceModelOnChanged(PieceModel pieceModel)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}");
        }
    }
}
