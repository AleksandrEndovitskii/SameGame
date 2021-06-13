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
        public override void Initialize()
        {
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
            _pieceModelPieceModelOnChangedSubscription = PieceModel.Subscribe(PieceModelOnChanged);
        }
        public override void UnSubscribe()
        {
            _pieceModelPieceModelOnChangedSubscription?.Dispose();
        }

        private void PieceModelOnChanged(PieceModel pieceModel)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}");
        }
    }
}
