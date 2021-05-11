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
	}
}
