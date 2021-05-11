using System;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ScoreManager : MonoBehaviour, IInitilizable
    {
        public Action<int> ScoreChanged = delegate {  };

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                if (_score == value)
                {
                    return;
                }

                _score = value;

                Debug.Log($"{this.GetType().Name}.{nameof(ScoreChanged)}" +
                          $"\n {_score}");

                ScoreChanged.Invoke(_score);
            }
        }

        private int _score;

        public void Initialize()
        {
            GameManager.Instance.PiecesManager.PieceModelRemoved += PieceModelRemoved;
        }

        private void PieceModelRemoved(PieceModel pieceModel)
        {
            Score++;
        }
    }
}
