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
            var board = new BoardLogic(true);

            // Act
            var result = board.IsInRange(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(expected, result);
        }


        [TestCase(4, 6, 4, 4, true, false)]
        [TestCase(4, 1, 4, 3, true, true)]
        //[TestCase(1, 7, 2, 5, false, true)]
        public void CanMovePieceTest(int x1, int y1, int x2, int y2, bool testing, bool expected)
        {
            // Arrange
            var board = new Board(true);
            var boardLogic = new BoardLogic(true);

            // Act
            //board.TurnChange();
            var result = boardLogic.CanMovePiece(x1, y1, x2, y2, testing, board);

            // Assert
            Assert.AreEqual(expected, result);
        }

        //[TestCase(4, 6, 4, 4, true, false)]
        //[TestCase(2, 6, 1, 5, true, false)]
        //[TestCase(1, 7, 2, 5, true, false)]
        //[TestCase(1, 1, 1, 3, true, true)]
        //[TestCase(1, 1, 1, 2, true, true)]
        //[TestCase(4, 1, 4, 3, true, true)]
        //public void CanMovePawnTest(int x1, int y1, int x2, int y2, bool testing, bool expected)
        //{
        //    // Arrange
        //    var board = new Board(true);
        //    //var pawn = new Pawn(true, x1, y1);

        //    // Act
        //    //board.TurnChange();
        //    var result = board.CanMovePawn(x1, y1, x2, y2, testing);

        //    // Assert
        //    Assert.AreEqual(expected, result);
        //}


        [TestCase(0, 4, 0, 6, false)]
        [TestCase(-1, 0, 1, -1, false)]        
        [TestCase(0, 0, 1, 8, false)]
        [TestCase(2, 1, 3, 2, false)]
        [TestCase(2, 6, 2, 5, true)]
        [TestCase(1, 7, 2, 5, true)]
        [TestCase(2, 8, 8, 8, false)]
        [TestCase(2, 1, 2, 4, false)]
        [TestCase(2, 1, 2, 3, false)]
        [TestCase(2, 8, 1, 1, false)]
        [TestCase(0, 6, 0, 5, true)]
        public void FinallyMoveTest(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var board = new Board(true);
            var boardLogic = new BoardLogic(true);
            boardLogic.FinallyMove(4, 1, 4, 3, board);


            // Act
            //board.TurnChange();
            var result = boardLogic.FinallyMove(x1, y1, x2, y2, board);

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
            var boardLogic = new BoardLogic(true);
            var expected = board.ChessBoard;
            PieceBase[,] BackupBoard = board.CloneChessBoard();
            // Act
            boardLogic.TurnChange();
            if (boardLogic.FinallyMove(x1, y1, x2, y2, board))
            {
                boardLogic.TurnChange();
                board.ChessBoard = BackupBoard; //vuelve al board clonado
            }
            var result = board.ChessBoard;
            // Assert
            Assert.AreEqual(expected, result);
        }


        [TestCase(1, 1, 1, 2)]
        [TestCase(1, 1, 1, 3)]
        [TestCase(1, 0, 0, 2)]
        [TestCase(1, 0, 2, 2)]
        [TestCase(3, 0, 5, 2)]
        [TestCase(3, 0, 3, 5)]
        [TestCase(3, 0, 3, 6)]
        [TestCase(3, 0, 3, 7)]

        public void UndoMoveTest(int x1, int y1, int x2, int y2)
        {

            // Arrange
            var board = new Board(true);
            var boardLogic = new BoardLogic(true);


            // Act
            boardLogic.FinallyMove(4, 1, 4, 3, board);
            boardLogic.FinallyMove(4, 6, 4, 4, board);
            var expected = board.CloneChessBoard();
            var expected2 = board.CloneWhitePieces();
            var expected3 = board.CloneBlackPieces();
            PieceBase auxPiece = board.ChessBoard[x2, y2];
            if (boardLogic.FinallyMove(x1, y1, x2, y2, board))
            {
                boardLogic.UndoMove(x1, y1, x2, y2, auxPiece, board);
            }            
            

            var result = board.ChessBoard;
            var result2 = board.WhitePieces;
            var result3 = board.BlackPieces;

            // Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected2, result2);
            Assert.AreEqual(expected3, result3);
        }



        [TestCase(0, 6, 0, 1, true)]
        [TestCase(0, 1, 0, 6, true)]
        [TestCase(4, 7, 1, 7, false)]
        public void IsLineEmptyTest(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var board = new Board(true);
            var boardLogic = new BoardLogic(true);

            // Act
            boardLogic.TurnChange();
            var result = boardLogic.IsLineEmpty(x1, y1, x2, y2, board);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(4, 7, false, false)]
        [TestCase(4, 0, true, false)]
        public void IsDrawnTest(int Xking, int YKing, bool KingColor, bool expected)
        {
            // Arrange
            var board = new Board(true);
            var boardLogic = new BoardLogic(true);
            //board.FinallyMove(4, 1, 4, 3);

            // Act            
            var result = boardLogic.IsDrawn(Xking, YKing, KingColor, board);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(4, 7, false, true)]
        [TestCase(4, 0, true, true)]
        public void IsAllSquaresAroundCheckorBlockedTest(int Xking, int YKing, bool KingColor, bool expected)
        {
            // Arrange
            var board = new Board(true);
            var boardLogic = new BoardLogic(true);

            // Act            
            var result = boardLogic.IsAllSquaresAroundCheckorBlocked(Xking, YKing, KingColor, board);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(4, 7, false, false)]
        [TestCase(4, 0, true, false)]
        public void IsSquareCheckTest(int x1, int y1, bool targetColor, bool expected) //Color es el color del rey (el opuesto del atacante)
        {
            // Arrange
            var board = new Board(true);
            var boardLogic = new BoardLogic(true);


            // Act            
            var result = boardLogic.IsSquareCheck(x1, y1, targetColor, board);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(false, false)]
        [TestCase(true, true)]        
        public void CanOtherPieceMoveTest(bool KingColor, bool expected)
        {
            // Arrange
            var board = new Board(true);
            var boardLogic = new BoardLogic(true);


            // Act            
            var result = boardLogic.CanOtherPieceMove(KingColor, board);

            // Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void CloneChessBoardTest()
        {
            // Arrange
            var board = new Board(true);
            var boardLogic = new BoardLogic(true);
            board.ChessBoard = GetExpectedChessBoard();

            // Act
            var resultBoard = board.CloneChessBoard();
            board.ChessBoard[0, 0] = null;
            board.ChessBoard[0, 0] = new Pawn(true, 0, 0);

            // Assert
            Assert.IsTrue(CompareChessBoards(GetExpectedChessBoard(), board.ChessBoard));
            //Assert.IsFalse(CompareChessBoards(resultBoard, GetExpectedChessBoard()));
            //Assert.AreNotEqual(null, GetExpectedChessBoard());
        }
        [Test]
        public void CloneChessBoardTest3()
        {
            // Arrange
            var board1 = GetExpectedChessBoard();
            var board2 = GetExpectedChessBoard();

            // Act
            board2[0, 0] = null;
            board2[0, 0] = new Pawn(true, 0, 0);

            // Assert
            Assert.IsTrue(CompareChessBoards(board1, board2));
            
        }

        //[TestCase(4, 8, 1, 8, false)]
        //[TestCase(4, 8, 2, 6, false)]
        //public void MovePieceAndPutEmptyTest(int x1, int y1, int x2, int y2)
        //{
        //    // Arrange
        //    var board1 = new Board(true);

        //    var board2 = GetExpectedChessBoard();

        //    // Act
        //    board2[0, 0] = null;
        //    board2[0, 0] = new Pawn(true, 0, 0);

        //    // Assert
        //    Assert.IsTrue(CompareChessBoards(board1, board2));

        //}
       





        private PieceBase[,] GetExpectedChessBoard()
        {
            var chessBoard = new PieceBase[Size, Size];
            chessBoard[0, 0] = new Pawn(true, 0, 0);
            chessBoard[Size - 1, Size - 1] = new Rook(false, Size - 1, Size - 1);

            return chessBoard;
        }

        private PieceBase[,] GetExpectedChessBoard2()
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