using System.Collections.Generic;

public class BoardModel
{
	public List<List<Square>> Squares => _squares;

	private readonly List<List<Square>> _squares;

	public BoardModel(int rowsCount, int cellsCount)
	{
		_squares = new List<List<Square>>();
		PlaceSquaresOnBoard(_squares, rowsCount, cellsCount);
	}

	private void PlaceSquaresOnBoard(List<List<Square>> squares, int rowsCount, int cellsCount)
	{
		for (var i = 0; i < rowsCount; i++)
		{
			var squaresRow = new List<Square>();
			squares.Add(squaresRow);
			for (var j = 0; j < cellsCount; j++)
			{
				squares[i].Add(new Square());
			}
		}
	}
}
