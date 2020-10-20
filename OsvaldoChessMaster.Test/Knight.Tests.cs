namespace OsvaldoChessMaster.Test
{
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    public class KnightTests
    {
        [Test]
        public void CanJump()
        {
            // Arrange
            var knight = new Knight(true);

            // Act

            // Assert
            Assert.IsTrue(knight.CanJump);
        }

        [TestCase(0, 0, 0, 0, false)]
        [TestCase(0, 0, 1, 0, false)]
        [TestCase(0, 0, 1, 1, false)]
        public void IsValidMove(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var knight = new Knight(true);

            // Act
            var result = knight.IsValidMove(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
