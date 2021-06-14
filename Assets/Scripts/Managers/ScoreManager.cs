using System;
using System.Collections.Generic;
using Components;
using Helpers;
using Models;
using UniRx;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : BaseMonoBehaviour
    {
        public ReactiveProperty<int> Score;

        private IDisposable _scoreOnChangedSubscription;

        public override void Initialize()
        {
            Score = new ReactiveProperty<int>();

            Subscribe();
        }
        public override void UnInitialize()
        {
            Score = null;

            UnSubscribe();
        }

        public override void Subscribe()
        {
            _scoreOnChangedSubscription = Score.Pairwise().Subscribe(ScoreOnChanged);

            GameManager.Instance.PiecesManager.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
        }
        public override void UnSubscribe()
        {
            _scoreOnChangedSubscription?.Dispose();

            if (GameManager.Instance.PiecesManager != null)
            {
                GameManager.Instance.PiecesManager.PieceModelsRemoved -= PiecesManagerOnPieceModelsRemoved;
            }
        }

        private void PiecesManagerOnPieceModelsRemoved(List<PieceModel> pieceModels)
        {
            Score.Value += pieceModels.Count;
        }
        private void ScoreOnChanged(Pair<int> pair)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                      $"\n{pair.Previous}->{pair.Current}");
        }
    }
}
