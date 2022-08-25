﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class GameBoard
    {
        //colors - Orange, Red, Violet, Yellow, Green, Darkblue, Lightblue
        static int numOfPieces = 0;
        static int[] piecesDistribution = new int[7];
        public char[,] Board;
        public int lines;
        public int level;
        public int score;
        private int[] points;
        public GameBoard()
        {
            Board = new char[20, 10];
            lines = 0;
            level = 1;
            score = 0;
            points = new int[5] { 0, 40, 100, 300, 1200 };
        }
        public void AddToBoard(Shape shp)
        {
            dynamic tvar = shp;
            for (int i = 0; i < 4; i++)
            {
                Board[tvar.Pozice[i, 0], tvar.Pozice[i, 1]] = tvar.Color;
            }
        }
        static public Shape GeneratePiece()
        {
            Random r = new Random(Environment.TickCount);
            int cis;
            ++numOfPieces;
            cis = r.Next(0, 7);
            /*
             * in case of a bad luck
             * fair distribution and making 'rare' pieces fall more
             */
            if (numOfPieces % 5 == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (piecesDistribution[i]<piecesDistribution[cis])
                    {
                        cis = i;
                    }
                }
            }
            
            switch (cis)
            {
                case 0:
                    ++ piecesDistribution[cis];
                    return new Ctverec();
                case 1:
                    ++piecesDistribution[cis];
                    return new Elko();
                case 2:
                    ++piecesDistribution[cis];
                    return new Esko();
                case 3:
                    ++piecesDistribution[cis];
                    return new Jecko();
                case 4:
                    ++piecesDistribution[cis];
                    return new Tecko();
                case 5:
                    ++piecesDistribution[cis];
                    return new Tyc();
                case 6:
                    ++piecesDistribution[cis];
                    return new Zetko();
                default:
                    ++piecesDistribution[cis];
                    return new Tyc();
            }
        }
        public int[] FindFullLines(Shape shp)
        {
            dynamic tvar = shp;
            int[] konec = new int[5];
            int j;
            for (int i = 0; i < 4; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    if (this.Board[tvar.Pozice[i,0],j] == '\0')
                    {
                        break;
                    }
                }
                if (j==10 && !contains(konec,tvar.Pozice[i,0]))
                {
                    konec[konec[4]] = tvar.Pozice[i, 0];
                    ++konec[4];                   
                }
            }
            updateInfo(konec[4]);
            return konec;
        }
        private bool contains(int[] kde, int co)
        {
            for (int i = 0; i < 4; i++)
            {
                if (kde[i] == co)
                {
                    return true;
                }
            }
            return false;
        }
        private void updateInfo(int numLines)
        {
            score += points[numLines] * level;
            lines += numLines;
            level = (lines / 10) + 1;           
        }
        static public void MoveMap(ref GameBoard gb, int[] lines)
        {
            for (int i = 0; i < lines[4]; i++)
            {
                for (int j = lines[i]; j > 0; j--)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        gb.Board[j, k] = gb.Board[j - 1, k];
                    }
                }
            }
        }
    }
}
