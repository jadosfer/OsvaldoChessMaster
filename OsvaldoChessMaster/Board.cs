using System;
using System.Collections.Generic;
using System.Text;
using OsvaldoChessMaster.Piece;

namespace OsvaldoChessMaster
{
    public class Board
    {        
        public bool Turn { get; set; } //turn=true es el turno del player1        
        public int TurnNumber { get; set; } // incrementa con cada movida      
        public int FullMoveNumber { get; set; } //incrementa cuando ambos movieron
        public bool player1 { get; set; }

        public bool IsCheckmateFlag { get; set; }
        public bool IsCheckFlag { get; set; }
        public bool IsCantMoveCheckFlag { get; set; }

        public static bool P1LRookCanCastling { get; set; }
        public static bool P1RRookCanCastling { get; set; }
        public static bool P2LRookCanCastling { get; set; }
        public static bool P2RRookCanCastling { get; set; }
        public bool PromotionFlag { get; private set; }
        public PieceBase[,] ChessBoard { get; set; }
        public HashSet<PieceBase> WhitePieces { get; set; }
        public HashSet<PieceBase> BlackPieces { get; set; }

        public Board(bool player1)
        {
            Turn = true;
            TurnNumber = 0;
            FullMoveNumber = 0;
            this.player1 = player1;

            ChessBoard = new PieceBase[Constants.Size, Constants.Size];
            WhitePieces = new HashSet<PieceBase>();
            BlackPieces = new HashSet<PieceBase>();

            P1RRookCanCastling = true;
            P1LRookCanCastling = true;
            P2RRookCanCastling = true;
            P2LRookCanCastling = true;

            ChessBoard[0, 1] = new Pawn(player1, 0, 1);
            ChessBoard[1, 1] = new Pawn(player1, 1, 1);
            ChessBoard[2, 1] = new Pawn(player1, 2, 1);
            ChessBoard[3, 1] = new Pawn(player1, 3, 1);
            ChessBoard[4, 1] = new Pawn(player1, 4, 1);
            ChessBoard[5, 1] = new Pawn(player1, 5, 1);
            ChessBoard[6, 1] = new Pawn(player1, 6, 1);
            ChessBoard[7, 1] = new Pawn(player1, 7, 1);

            ChessBoard[0, 6] = new Pawn(!player1, 0, 6);
            ChessBoard[1, 6] = new Pawn(!player1, 1, 6);
            ChessBoard[2, 6] = new Pawn(!player1, 2, 6);
            ChessBoard[3, 6] = new Pawn(!player1, 3, 6);
            ChessBoard[4, 6] = new Pawn(!player1, 4, 6);
            ChessBoard[5, 6] = new Pawn(!player1, 5, 6);
            ChessBoard[6, 6] = new Pawn(!player1, 6, 6);
            ChessBoard[7, 6] = new Pawn(!player1, 7, 6);

            ChessBoard[0, 0] = new Rook(player1, 0, 0);
            ChessBoard[7, 0] = new Rook(player1, 7, 0);
            ChessBoard[0, 7] = new Rook(!player1, 0, 7);
            ChessBoard[7, 7] = new Rook(!player1, 7, 7);

            ChessBoard[1, 0] = new Knight(player1, 1, 0);
            ChessBoard[6, 0] = new Knight(player1, 6, 0);
            ChessBoard[1, 7] = new Knight(!player1, 1, 7);
            ChessBoard[6, 7] = new Knight(!player1, 6, 7);

            ChessBoard[2, 0] = new Bishop(player1, 2, 0);
            ChessBoard[5, 0] = new Bishop(player1, 5, 0);
            ChessBoard[2, 7] = new Bishop(!player1, 2, 7);
            ChessBoard[5, 7] = new Bishop(!player1, 5, 7);

            ChessBoard[4, 0] = new King(player1, 4, 0);
            ChessBoard[4, 7] = new King(!player1, 4, 7);

            ChessBoard[3, 0] = new Queen(player1, 3, 0);
            ChessBoard[3, 7] = new Queen(!player1, 3, 7);

            // creates 2 arrays with same color pieces
            for (int i = Constants.ForStart; i < Constants.ColorPieces / 2; i++)
            {
                WhitePieces.Add(ChessBoard[i, 1]);
                BlackPieces.Add(ChessBoard[i, 6]);
            }
            for (int i = Constants.ForStart; i < Constants.ColorPieces / 2; i++)
            {
                WhitePieces.Add(ChessBoard[i, 0]);
                BlackPieces.Add(ChessBoard[i, 7]);
            }


            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.EmptyPieceStartLine; j < Constants.EmptyPieceEndLine; j++)
                {
                    ChessBoard[i, j] = null;
                }
            }
        }

        /// <summary>
        /// Remueve ficha en x1, y1 tanto del chessboard como del hashSet correspondiente
        /// </summary>   
        public void Remove(int x1, int y1)
        {
            var hashSet = GetPiece(x1, y1).Color ? WhitePieces : BlackPieces;
            hashSet.Remove(GetPiece(x1, y1));
            ChessBoard[x1, y1] = null;
        }

        public void RemoveCapturePassant(int x1, int y1, int x2, int y2)
        {
            if (y2 == 2)
            {
                Remove(x2, 3);
            }
            if (y2 == 6)
            {
                Remove(x2, 4);
            }

        }

        /// <summary>
        /// Solo mueve la pieza y pone un null en donde estaba
        /// </summary>      
        public void Move(int x1, int y1, int x2, int y2)
        {            
                ChessBoard[x2, y2] = GetPiece(x1, y1);
                ChessBoard[x2, y2].Position.PositionX = x2;
                ChessBoard[x2, y2].Position.PositionY = y2;
                ChessBoard[x1, y1] = null;  
        }

        public void PutEmpty(int x1, int y1)
        {
            ChessBoard[x1, y1] = null;
        }

        public void PutPiece(PieceBase removedPiece, int x2, int y2)
        {
            if (removedPiece != null)
            {
                ChessBoard[x2, y2] = removedPiece;
                ChessBoard[x2, y2].Position.PositionX = x2;
                ChessBoard[x2, y2].Position.PositionY = y2;
                var hashSet = removedPiece.Color ? WhitePieces : BlackPieces;
                hashSet.Add(removedPiece);
            }            
        }

        public PieceBase GetPiece(int x, int y)
        {            
           return ChessBoard[x, y] ?? null;           
        }

        public bool IsPawn(int x, int y)
        {
            return ChessBoard[x, y].GetType() == typeof(Pawn);
        }

        public bool IsEmpty(int x2, int y2)
        {
            try
            {
                return ChessBoard[x2, y2] == null;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            catch (NullReferenceException)
            {
                return true;
            }
        }

        public void Promotion(int x, int y, bool color)
        {
            Remove(x, y);
            ChessBoard[x, y] = new Queen(color, x, y);
        }

        /// <summary>
        /// Method to perform deep clone/copy of a Chess Board.
        /// </summary>
        /// <returns></returns>
        public PieceBase[,] CloneChessBoard()
        {
            var clonedChessBoard = new PieceBase[Constants.Size, Constants.Size];

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
                {
                    if (this.ChessBoard[i, j] != null)
                    {
                        clonedChessBoard[i, j] = this.ChessBoard[i, j].Clone() as PieceBase;
                    }
                }
            }


            return clonedChessBoard;
        }

        /// <summary>
        /// clona el chessBoard e instancia nuevas HashSet con los mismos objetos piece del nuevo chessboard clonado
        /// </summary>        
        public PieceBase[,] CloneChessBoardAndPiecesHashs()
        {
            var clonedChessBoard = new PieceBase[Constants.Size, Constants.Size];

            this.WhitePieces = new HashSet<PieceBase>();
            this.BlackPieces = new HashSet<PieceBase>();

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
                {
                    if (this.ChessBoard[i, j] != null)
                    {
                        clonedChessBoard[i, j] = this.ChessBoard[i, j].Clone() as PieceBase;
                        if (clonedChessBoard[i, j].Color)
                        {
                            this.WhitePieces.Add(clonedChessBoard[i, j]);
                        }
                        else
                        {
                            this.BlackPieces.Add(clonedChessBoard[i, j]);
                        }
                    }
                }
            }

            return clonedChessBoard;
        }
        public HashSet<PieceBase> CloneWhitePieces()
        {
            var clonedWhitePieces = new HashSet<PieceBase>();
            foreach (PieceBase piece in this.WhitePieces)
            {
                clonedWhitePieces.Add(piece);
            }
            return clonedWhitePieces;
        }

        public HashSet<PieceBase> CloneBlackPieces()
        {
            var clonedBlackPieces = new HashSet<PieceBase>();
            foreach (PieceBase piece in this.BlackPieces)
            {
                clonedBlackPieces.Add(piece);
            }
            return clonedBlackPieces;
        }
    }
}
