using System.Collections.Generic;

namespace Models
{
	public class BoardModel
	{
		public List<List<SquareModel>> SquareModels => _squareModels;

		private readonly List<List<SquareModel>> _squareModels;

		public BoardModel(int rowsCount, int cellsCount)
		{
			_squareModels = new List<List<SquareModel>>();
			PlaceSquaresOnBoard(_squareModels, rowsCount, cellsCount);
			LinkSquaresOnBoard(_squareModels);
		}

		private void PlaceSquaresOnBoard(List<List<SquareModel>> squareModels, int rowsCount, int cellsCount)
		{
			for (var i = 0; i < rowsCount; i++)
			{
				var squaresRow = new List<SquareModel>();
				squareModels.Add(squaresRow);
				for (var j = 0; j < cellsCount; j++)
				{
					squareModels[i].Add(new SquareModel());
				}
			}
		}
		private void LinkSquaresOnBoard(List<List<SquareModel>> squareModels)
		{
			for (var i = 0; i < squareModels.Count; i++)
			{
				for (var j = 0; j < squareModels[i].Count; j++)
				{
					if (i != 0)
					{
						squareModels[i][j].Top = squareModels[i - 1][j];
					}
					if (i != squareModels[i].Count - 1)
					{
						squareModels[i][j].Bot = squareModels[i + 1][j];
					}

					if (j != 0)
					{
						squareModels[i][j].Left = squareModels[i][j - 1];
					}
					if (j != squareModels[i].Count - 1)
					{
						squareModels[i][j].Right = squareModels[i][j + 1];
					}
				}
			}
		}
	}
}
