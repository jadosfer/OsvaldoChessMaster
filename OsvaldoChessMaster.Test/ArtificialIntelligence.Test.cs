﻿using System;
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

        [Test]
        //public void AllPosiblePlaysTest()
        //{
        //    // Arrange
        //    var board = new Board(true);
        //    var AI = new ArtificialIntelligence(board);

        //    // Act
        //    List<Move> AllMovesExpected = new List<Move>();
        //    var result = AI.AllPosiblePlays(board);

        //    // Assert
        //    Assert.AreEqual(AllMovesExpected, result);
        //}
        [TestCase(0, 9, 9, 10, 10)]
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(0, 4, 2, 4, 4)]
        [TestCase(0, 4, 2, 4, 8)]
        [TestCase(0, 4, 4, 4, 3)]
        [TestCase(0, 5, 2, 5, 4)]
        [TestCase(0, 2, 1, 3, 3)]
        [TestCase(-5, 2, 8, 3, 6)]
        [TestCase(0, 2, 8, 2, 4)]
        [TestCase(0, 4, 8, 1, 8)]

        public void EvaluateBoardTest(int EvaluateExpected, int x1, int y1, int x2, int y2) 
        { 
            // Arrange
            var board = new Board(true);
            var AI = new ArtificialIntelligence(board);

            // Act        
            board.TurnChange();
            board.FinallyMove(x1, y1, x2, y2);
            var result = AI.EvaluateBoard(board);
         

        // Assert
        Assert.AreEqual(EvaluateExpected, result);
        }
}
}
