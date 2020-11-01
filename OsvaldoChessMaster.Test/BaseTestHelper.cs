namespace OsvaldoChessMaster.Test
{
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    public abstract class BaseTestHelper
    {
        protected void Equals<T>(bool canCastling1, bool lCastling1, bool sCastling1, bool color1,
            bool canCastling2, bool lCastling2, bool sCastling2, bool color2,
            bool expected) where T : PieceBase, new()
        {
            // Arrange
            var piece1 = new T
            {
                CanCastling = canCastling1,
                LCastling = lCastling1,
                SCastling = sCastling1,
                Color = color1
            };

            var piece2 = new T
            {
                CanCastling = canCastling2,
                LCastling = lCastling2,
                SCastling = sCastling2,
                Color = color2
            };

            // Act
            var result = piece1.Equals(piece2);

            // Assert
            Assert.AreEqual(expected, result, $"expected: {expected} | result: {result}");
        }
    }
}