namespace OsvaldoChessMaster.Test
{
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    public class PawnTests
    {
        [Test]
        public void CanJump()
        {
            // Arrange
            var pawn = new Pawn(true);

            // Act

            // Assert
            Assert.IsFalse(pawn.CanJump);
        }

        [TestCase(0, 0, 0, 0, false)]
        [TestCase(0, 0, 1, 0, false)]
        [TestCase(0, 0, 1, 1, true)]
        public void IsValidMove(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var pawn = new Pawn(true);

            // Act
            var result = pawn.IsValidMove(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
