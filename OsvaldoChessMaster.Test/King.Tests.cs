namespace OsvaldoChessMaster.Test
{
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    public class KingTests
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
    }
}