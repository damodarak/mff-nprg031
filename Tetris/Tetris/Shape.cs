﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    abstract class Shape
    {
        private int[,] poziceAI;
        public int[,] Pozice;
        protected bool checkDownSide(ref GameBoard gb)
        {
            //'\0' je prazdne misto, 'F' je char oznacujici jiz projdenou pozici pri tetris DFS
            return (
                Pozice[0, 0] != 19 && Pozice[1, 0] != 19 &&
                Pozice[2, 0] != 19 && Pozice[3, 0] != 19 &&
                (gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] != 'F' || gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] != 'F' ||
                gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] != 'F' || gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] != 'F') &&
                (gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] == '\0' || gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] == 'F') &&
                (gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] == '\0' || gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] == 'F') &&
                (gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] == '\0' || gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] == 'F') &&
                (gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] == '\0' || gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] == 'F'));
        }
        protected bool checkLeftSide(ref GameBoard gb)
        {
            //'\0' je prazdne misto, 'F' je char oznacujici jiz projdenou pozici pri tetris DFS
            return (
                Pozice[0, 1] != 0 && Pozice[1, 1] != 0 &&
                Pozice[2, 1] != 0 && Pozice[3, 1] != 0 &&
                (gb.Board[Pozice[0, 0], Pozice[0, 1] - 1] != 'F' || gb.Board[Pozice[1, 0], Pozice[1, 1] - 1] != 'F' ||
                gb.Board[Pozice[2, 0], Pozice[2, 1] - 1] != 'F' || gb.Board[Pozice[3, 0], Pozice[3, 1] - 1] != 'F') &&
                (gb.Board[Pozice[0, 0], Pozice[0, 1] - 1] == '\0' || gb.Board[Pozice[0, 0], Pozice[0, 1] - 1] == 'F') &&
                (gb.Board[Pozice[1, 0], Pozice[1, 1] - 1] == '\0' || gb.Board[Pozice[1, 0], Pozice[1, 1] - 1] == 'F') &&
                (gb.Board[Pozice[2, 0], Pozice[2, 1] - 1] == '\0' || gb.Board[Pozice[2, 0], Pozice[2, 1] - 1] == 'F') &&
                (gb.Board[Pozice[3, 0], Pozice[3, 1] - 1] == '\0' || gb.Board[Pozice[3, 0], Pozice[3, 1] - 1] == 'F'));

        }
        protected bool checkRightSide(ref GameBoard gb)
        {
            //'\0' je prazdne misto, 'F' je char oznacujici jiz projdenou pozici pri tetris DFS
            return (
                Pozice[0, 1] != 9 && Pozice[1, 1] != 9 &&
                Pozice[2, 1] != 9 && Pozice[3, 1] != 9 &&
                (gb.Board[Pozice[0, 0], Pozice[0, 1] + 1] != 'F' || gb.Board[Pozice[1, 0], Pozice[1, 1] + 1] != 'F' ||
                gb.Board[Pozice[2, 0], Pozice[2, 1] + 1] != 'F' || gb.Board[Pozice[3, 0], Pozice[3, 1] + 1] != 'F') &&
                (gb.Board[Pozice[0, 0], Pozice[0, 1] + 1] == '\0' || gb.Board[Pozice[0, 0], Pozice[0, 1] + 1] == 'F') &&
                (gb.Board[Pozice[1, 0], Pozice[1, 1] + 1] == '\0' || gb.Board[Pozice[1, 0], Pozice[1, 1] + 1] == 'F') &&
                (gb.Board[Pozice[2, 0], Pozice[2, 1] + 1] == '\0' || gb.Board[Pozice[2, 0], Pozice[2, 1] + 1] == 'F') &&
                (gb.Board[Pozice[3, 0], Pozice[3, 1] + 1] == '\0' || gb.Board[Pozice[3, 0], Pozice[3, 1] + 1] == 'F'));

        }
        public abstract void MoveUp();
        public abstract bool MoveDown(ref GameBoard gb);
        public abstract bool MoveLeft(ref GameBoard gb);
        public abstract bool MoveRight(ref GameBoard gb);
        public abstract void RotLeft(ref GameBoard gb);
        public abstract bool RotRight(ref GameBoard gb);
        public abstract int HardDrop(ref GameBoard gb);
        public int[,] FakeHardDrop(ref GameBoard gb)
        {

            poziceAI = (int[,])Pozice.Clone();
            while (poziceAI[0, 0] != 19 && poziceAI[1, 0] != 19 &&
                poziceAI[2, 0] != 19 && poziceAI[3, 0] != 19 &&
                gb.Board[poziceAI[0, 0] + 1, poziceAI[0, 1]] == '\0' &&
                gb.Board[poziceAI[1, 0] + 1, poziceAI[1, 1]] == '\0' &&
                gb.Board[poziceAI[2, 0] + 1, poziceAI[2, 1]] == '\0' &&
                gb.Board[poziceAI[3, 0] + 1, poziceAI[3, 1]] == '\0')
            {
                for (int i = 0; i < 4; i++)
                {
                    poziceAI[i, 0] += 1;
                }
            }
            return poziceAI;
        }
        public void markVisited(ref GameBoard gb)
        {
            for (int i = 0; i < 4; i++)
            {
                gb.Board[Pozice[i, 0], Pozice[i, 1]] = 'F';//tetris dFs
            }
        }
        public bool MoveDownNotPossible(ref GameBoard gb)
        {
            return (
               Pozice[0, 0] == 19 || Pozice[1, 0] == 19 ||
               Pozice[2, 0] == 19 || Pozice[3, 0] == 19 ||
               (gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] != '\0' && gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] != 'F') ||
               (gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] != '\0' && gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] != 'F') ||
               (gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] != '\0' && gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] != 'F') ||
               (gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] != '\0' && gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] != 'F'));

        }
    }
}
