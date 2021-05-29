using System;
using Helpers;
using UnityEngine;
using Utils;

namespace Models
{
    public class PieceModel: ISelectable
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

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_squareModel}->{value}");

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
