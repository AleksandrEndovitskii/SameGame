using System;
using Models;
using UnityEngine;

namespace Views
{
    public class PieceView : MonoBehaviour
    {
        public Action<PieceModel> PieceModelChanged = delegate {  };

        public PieceModel PieceModel
        {
            get
            {
                return _pieceModel;
            }
            set
            {
                if (_pieceModel == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{nameof(PieceModelChanged)}");

                _pieceModel = value;

                PieceModelChanged.Invoke(_pieceModel);
            }
        }

        private PieceModel _pieceModel;

        public void Initialize(PieceModel pieceModel)
        {
            PieceModel = pieceModel;
        }
    }
}
