namespace OsvaldoChessMaster.Test
{
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    public class KingTests : BaseTestHelper
    {
        [Test]
        public void CanJump()
        {
            // Arrange
            var king = new King(true);

            // Act

            // Assert
            Assert.IsFalse(king.CanJump);
        }

        [TestCase(0, 0, 0, 0, false)]
        [TestCase(0, 0, 1, 0, true)]
        [TestCase(0, 0, 1, 1, true)]
        public void IsValidMove(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var king = new King(true);

            // Act
            var result = king.IsValidMove(x1, y1, x2, y2);

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
            Equals<King>(canCastling1, lCastling1, sCastling1, color1,
                canCastling2, lCastling2, sCastling2, color2,
                expected);
        }
    }
}