using System.Collections.Generic;

namespace Models
{
	public class BoardModel
	{
		public List<List<SquareModel>> Squares => _squares;

		private readonly List<List<SquareModel>> _squares;

		public BoardModel(int rowsCount, int cellsCount)
		{
			_squares = new List<List<SquareModel>>();
			PlaceSquaresOnBoard(_squares, rowsCount, cellsCount);
			LinkSquaresOnBoard(_squares);
		}

		private void PlaceSquaresOnBoard(List<List<SquareModel>> squares, int rowsCount, int cellsCount)
		{
			for (var i = 0; i < rowsCount; i++)
			{
				var squaresRow = new List<SquareModel>();
				squares.Add(squaresRow);
				for (var j = 0; j < cellsCount; j++)
				{
					squares[i].Add(new SquareModel());
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
