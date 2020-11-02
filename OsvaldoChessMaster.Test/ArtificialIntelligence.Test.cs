using System;
using System.Collections.Generic;
using System.Text;

namespace OsvaldoChessMaster.Test
{
    using System.Collections;
    using NUnit.Framework;
    using OsvaldoChessMaster.Piece;

    class ArtificialIntelligenceTests
    {
        private const int Size = 9;

        [TestCase(0, 0, 1, 0, false)]
        [TestCase(0, 3, 4, 5, false)]
        [TestCase(2, 5, 6, 8, true)]
        public void IsInRange(int x1, int y1, int x2, int y2, bool expected)
        {
            // Arrange
            var board = new Board(true);

            // Act
            var result = board.IsInRange(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(expected, result);
        }
        
    }
}
