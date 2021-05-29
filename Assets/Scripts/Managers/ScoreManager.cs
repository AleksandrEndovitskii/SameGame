using System.Collections.Generic;
using Helpers;
using Models;
using UniRx;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ScoreManager : MonoBehaviour, IInitilizable
    {
        public ReactiveProperty<int> Score = new ReactiveProperty<int>();

        public void Initialize()
        {
            GameManager.Instance.PiecesManager.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
        }

        private void PiecesManagerOnPieceModelsRemoved(List<PieceModel> pieceModels)
        {
            Score.Value += pieceModels.Count;

            Score.Subscribe(ScoreChanged);
        }
        private void ScoreChanged(int score)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                      $"\n{score}");
        }
    }
}
