namespace OsvaldoChessMaster.Test
{
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    public class PawnTests : BaseTestHelper
    {
        [Test]
        public void CanJump()
        {
            // Arrange
            var pawn = new Pawn(true, 0, 0);

            // Act

            // Assert
            Assert.IsFalse(pawn.CanJump);
        }

        [TestCase(4, 6, 4, 4, true)]
        [TestCase(0, 0, 1, 0, false)]
        [TestCase(0, 0, 1, 1, true)]
        [TestCase(4, 7, 4, 6, true)]
        [TestCase(4, 1, 4, 3, true)]
        [TestCase(1, 1, 1, 1, false)]
        public void IsValidMove(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            Board board = new Board(true);
            var pawn = new Pawn(true, x1, y1);

            // Act
            var result = pawn.IsValidMove(x1, y1, x2, y2, board.Turn, board);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(true, true, true, true, true, true, true, true, true)]
        [TestCase(true, true, true, true, true, true, true, false, false)]
        [TestCase(false, false, false, false, false, false, false, false, true)]
        [TestCase(true, false, false, false, false, false, false, false, false)]
        public void Equals(bool canCastling1, bool lCastling1, bool sCastling1, bool color1,
            bool canCastling2, bool lCastling2, bool sCastling2, bool color2,
            bool expected)
        {
            Equals<Pawn>(canCastling1, lCastling1, sCastling1, color1,
                canCastling2, lCastling2, sCastling2, color2,
                expected);
        }
    }
}