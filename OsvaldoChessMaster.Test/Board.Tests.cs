﻿namespace OsvaldoChessMaster.Test
{
    using System.Collections;
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    public class BoardTests
    {
        private const int Size = 9;

        [TestCase(0, 0, 1, 0, false)]
        [TestCase(0, 3, 4, 5, false)]        
        [TestCase(2, 5, 6, 8, true)]
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
            var result = board.CanMovePiece(x1, y1, x2, y2, true, true);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(4, 8, 1, 8, false)]
        [TestCase(2, 8, 1, 6, true)]
        [TestCase(2, 8, 8, 8, false)]
        [TestCase(2, 8, 8, 1, false)]
        [TestCase(2, 8, 1, 8, false)]
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
            //agrego probar cambiar y volver
            PieceBase auxPiece = resultBoard[2, 4];
            resultBoard[2, 4] = resultBoard[2, 8];
            //revierto:
            resultBoard[2, 8] = resultBoard[2, 4];
            resultBoard[2, 4] = auxPiece;

            board.ChessBoard[0, 0] = null;

            // Assert
            Assert.IsTrue(CompareChessBoards(GetExpectedChessBoard(), resultBoard));
            Assert.IsFalse(CompareChessBoards(resultBoard, board.ChessBoard));
            Assert.AreEqual(null, board.ChessBoard[0, 0]);
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
    }
}