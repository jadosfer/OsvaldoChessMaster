

namespace OsvaldoChessMaster.Test
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    [TestFixture]
    class PieceBaseTest
    {
        [Test]
        public void ClonePieceTest()
        {
            // Arrange
            PieceBase piece1 = new Pawn(true, 0,0);


            // Act
            var pieceCloned = piece1.Clone();
            pieceCloned = new King(false, 7, 7);
            var resultPiece = pieceCloned;

            // Assert

            Assert.AreNotEqual(piece1, resultPiece);
            //Assert.IsTrue(ComparePiece(piece1, resultPiece));
        }

        public void PiecePositionCloneTest()
        {
            // Arrange
            var piece1 = new Pawn(true, 0, 0);


            // Act
            var pieceCloned = (PieceBase)piece1.Clone();
            var resultPiece = pieceCloned;

            // Assert
            
            //Assert.AreEqual(piece1.Position.PositionX, resultPiece.Position.PositionX);
            Assert.IsTrue(ComparePiece(piece1, resultPiece));
        }

        private bool ComparePiece(PieceBase source, PieceBase target)
        {

            var areBothNull = source == null && target == null;
            var areBothEqual = source?.Equals(target) ?? false;

            return (!areBothNull && !areBothEqual);





        }
    }
}
