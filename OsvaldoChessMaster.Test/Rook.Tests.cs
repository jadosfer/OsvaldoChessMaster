namespace OsvaldoChessMaster.Test
{
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    public class RookTests
    {
        [Test]
        public void CanJump()
        {
            // Arrange
            var rook = new Rook(true);

            // Act

            // Assert
            Assert.IsFalse(rook.CanJump);
        }

        [TestCase(0, 0, 0, 0, false)]
        [TestCase(0, 0, 1, 0, true)]
        [TestCase(0, 0, 1, 1, false)]
        public void IsValidMove(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var rook = new Rook(true);

            // Act
            var result = rook.IsValidMove(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
