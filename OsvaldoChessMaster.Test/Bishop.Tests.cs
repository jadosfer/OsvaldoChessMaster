namespace OsvaldoChessMaster.Test
{
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    public class BishopTests
    {
        [Test]
        public void CanJump()
        {
            // Arrange
            var bishop = new Bishop(true);

            // Act

            // Assert
            Assert.IsFalse(bishop.CanJump);
        }

        [TestCase(0, 0, 0, 0, false)]
        [TestCase(0, 0, 1, 0, false)]
        [TestCase(0, 0, 1, 1, true)]
        public void IsValidMove(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var bishop = new Bishop(true);

            // Act
            var result = bishop.IsValidMove(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}