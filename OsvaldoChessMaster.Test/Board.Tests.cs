namespace OsvaldoChessMaster.Test
{
    using System.Collections;
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    public class BoardTests
    {
        private const int Size = 9;

        [Test]
        public void CloneChessBoard()
        {
            // Arrange
            var board = new Board(true);
            board.ChessBoard = GetExpectedChessBoard();

            // Act
            var resultBoard = board.CloneChessBoard();
            board.ChessBoard[0, 0] = null;

            // Assert
            Assert.IsTrue(CompareChessBoards(GetExpectedChessBoard(), resultBoard));
            Assert.IsFalse(CompareChessBoards(resultBoard, board.ChessBoard));
            Assert.AreEqual(null, board.ChessBoard[0, 0]);
        }

        private PieceBase[,] GetExpectedChessBoard()
        {
            var chessBoard = new PieceBase[Size, Size];
            chessBoard[0, 0] = new Pawn(true);
            chessBoard[Size - 1, Size - 1] = new Rook(false);

            return chessBoard;
        }

        private bool CompareChessBoards(PieceBase[,] source, PieceBase[,] target)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var areBothNull = source[i, j] == null && target[i, j] == null;
                    var areBothEqual = source[i, j]?.Equals(target[i, j]) ?? false;

                    if (!areBothNull && !areBothEqual)
                        return false;
                }
            }

            return true;
        }
    }
}