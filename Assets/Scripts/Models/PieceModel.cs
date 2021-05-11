using System;
using UnityEngine;

namespace Models
{
    public class PieceModel
    {
        public Action<SquareModel> SquareModelChanged = delegate {  };

        public Color Color => _color;
        public SquareModel SquareModel
        {
            get
            {
                return _squareModel;
            }
            set
            {
                if (_squareModel == value)
                {
                    return;
                }

                if (_squareModel != null)
                {
                    _squareModel.PieceModel = null;
                }
                _squareModel = value;
                if (_squareModel != null)
                {
                    _squareModel.PieceModel = this;
                }

                SquareModelChanged.Invoke(_squareModel);
            }
        }

        private Color _color;
        private SquareModel _squareModel;

        public PieceModel(Color color)
        {
            _color = color;
        }
    }
}