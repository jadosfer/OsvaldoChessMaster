namespace OsvaldoChessMaster.Test
{
    using System.Collections;
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    public class BoardTests
    {
        private const int Size = 8;

        [TestCase(0, 0, 1, 0, true)]
        [TestCase(0, 3, 4, 5, true)]
        [TestCase(2, 5, 6, 8, false)]
        public void IsInRange(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var board = new Board(true);

            // Act
            var result = board.IsInRange(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(expected, result);
        }


        [TestCase(4, 8, 1, 8, true, false)]
        [TestCase(2, 8, 2, 4, true, false)]
        public void CanMovePieceTest(int x1, int y1, int x2, int y2, bool player1, bool expected)
        {
            // Arrange
            var board = new Board(true);

            // Act
            var result = board.CanMovePiece(x1, y1, x2, y2, true);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(0, 0, 9, 9, false)]
        [TestCase(-1, 0, 1, -1, false)]
        [TestCase(0, 0, 1, 8, false)]
        [TestCase(0, 0, 1, 8, false)]
        [TestCase(0, 0, 1, 8, false)]
        [TestCase(2, 6, 2, 4, true)]
        [TestCase(2, 6, 2, 5, true)]
        [TestCase(1, 7, 2, 5, true)]
        [TestCase(2, 8, 8, 8, false)]
        [TestCase(2, 1, 2, 4, false)]
        [TestCase(2, 1, 2, 3, false)]
        [TestCase(2, 8, 1, 1, false)]
        public void FinallyMoveTest(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var board = new Board(true);

            // Act
            board.TurnChange();
            var result = board.FinallyMove(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(0, 0, 9, 9)]
        [TestCase(-1, 0, 1, -1)]
        [TestCase(0, 0, 1, 7)]
        [TestCase(0, 0, 1, 0)]
        [TestCase(0, 4, 1, 4)]
        [TestCase(0, 3, 1, 3)]
        [TestCase(4, 7, 0, 7)]
        [TestCase(1, 7, 0, 6)]
        [TestCase(2, 7, 7, 7)]
        [TestCase(2, 7, 7, 1)]
        [TestCase(1, 7, 1, 7)]
        [TestCase(2, 7, 1, 1)]
        public void FinallyMoveReverterTest(int x1, int y1, int x2, int y2)
        {
            // Arrange
            var board = new Board(true);
            var expected = board.ChessBoard;
            PieceBase[,] BackupBoard = board.CloneChessBoard();
            // Act
            board.TurnChange();
            if (board.FinallyMove(x1, y1, x2, y2))
            {
                board.TurnChange();
                board.ChessBoard = BackupBoard; //vuelve al board clonado
            }
            var result = board.ChessBoard;
            // Assert
            Assert.AreEqual(expected, result);
        }


        [TestCase(4, 8, 1, 8, false)]
        [TestCase(4, 8, 2, 6, false)]
        public void IsLineEmptyTest(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var board = new Board(true);

            // Act
            board.TurnChange();
            var result = board.IsLineEmpty(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(expected, result);
        }


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

        public void CloneChessBoard2()
        {
            // Arrange
            var board = new Board(true);
            board.ChessBoard = GetExpectedChessBoard();            
            var backupBoard = board.ChessBoard;
            var expectedBoard = backupBoard;

            // Act
            var ClonedBoard = board.CloneChessBoard();
            //agrego probar cambiar y volver
            PieceBase auxPiece = board.ChessBoard[1, 3];
            board.ChessBoard[1, 3] = board.ChessBoard[1, 7];
            //revierto:
            board.ChessBoard[1, 7] = board.ChessBoard[1, 3];
            board.ChessBoard[1, 3] = auxPiece;

            board.ChessBoard = backupBoard;
            var resultBoard = board.ChessBoard;
            

            // Assert
            Assert.IsTrue(CompareChessBoards(GetExpectedChessBoard(), resultBoard));
            Assert.IsTrue(CompareChessBoards(expectedBoard, resultBoard));
            Assert.IsTrue(CompareChessBoards(backupBoard, resultBoard));

            Assert.AreNotEqual(null, resultBoard[0, 0]);
            Assert.AreNotEqual(null, GetExpectedChessBoard()[0, 0]);
        }



        private PieceBase[,] GetExpectedChessBoard()
        {
            var chessBoard = new PieceBase[Size, Size];
            chessBoard[0, 0] = new Pawn(true, 0, 0);
            chessBoard[Size - 1, Size - 1] = new Rook(false, Size - 1, Size - 1);



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

        private bool ComparePiece(PieceBase source, PieceBase target)
        {

            var areBothNull = source == null && target == null;
            var areBothEqual = source?.Equals(target) ?? false;

            return (!areBothNull && !areBothEqual);


            


        }
    }
}