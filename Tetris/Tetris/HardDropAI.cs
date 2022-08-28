﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class HardDropAI
    {
        static public int[,,] FindAllHardDrops(ref GameBoard gb, Shape shp)
        {
            int[,,] konec;
            dynamic tvar = shp;
            switch (tvar.Color)
            {
                case 'O':
                    konec = new int[17, 4, 2];
                    break;
                case 'R':
                    konec = new int[9, 4, 2];
                    break;
                case 'D':
                    konec = new int[34, 4, 2];
                    break;
                case 'V':
                    konec = new int[34, 4, 2];
                    break;
                case 'Y':
                    konec = new int[34, 4, 2];
                    break;
                case 'L':
                    konec = new int[17, 4, 2];
                    break;
                case 'G':
                    konec = new int[17, 4, 2];
                    break;
                default:
                    konec = new int[1, 1, 1];
                    break;
            }
            tvar.MoveUp();
            for (int i = 0; i < 4; i++)
            {
                tvar.MoveLeft(ref gb);
            }
            switch (konec.GetLength(0))
            {
                case 9:
                    leftToRightHardDrop(ref gb, tvar, 0, 9, ref konec);
                    break;
                case 17:
                    if (tvar.Color == 'O')
                    {
                        leftToRightHardDrop(ref gb, tvar, 0, 7, ref konec);
                        tvar.RotRight(ref gb);
                        tvar.MoveLeft(ref gb);
                        leftToRightHardDrop(ref gb, tvar, 7, 17, ref konec);
                        tvar.MoveRight(ref gb);
                        tvar.RotRight(ref gb);
                    }
                    else
                    {
                        leftToRightHardDrop(ref gb, tvar, 0, 8, ref konec);
                        tvar.RotRight(ref gb);
                        tvar.MoveLeft(ref gb);
                        leftToRightHardDrop(ref gb, tvar, 8, 17, ref konec);
                        tvar.MoveRight(ref gb);
                        tvar.RotRight(ref gb);
                    }
                    break;
                case 34:
                    leftToRightHardDrop(ref gb, tvar, 0, 8, ref konec);
                    tvar.RotRight(ref gb);
                    tvar.MoveLeft(ref gb);
                    leftToRightHardDrop(ref gb, tvar, 8, 17, ref konec);
                    tvar.RotRight(ref gb);
                    leftToRightHardDrop(ref gb, tvar, 17, 25, ref konec);
                    tvar.RotRight(ref gb);
                    tvar.MoveLeft(ref gb);
                    leftToRightHardDrop(ref gb, tvar, 25, 34, ref konec);
                    tvar.MoveRight(ref gb);
                    tvar.RotRight(ref gb);
                    break;
                default:
                    break;
            }
            return konec;
        }
        static private void leftToRightHardDrop(ref GameBoard gb, Shape tvar, int od, int kam, ref int[,,] konec)
        {
            dynamic tvr = tvar;
            for (int i = od; i < kam; i++)
            {
                int[,] hardDrop = tvar.FakeHardDrop(ref gb, tvr.Pozice);
                for (int j = 0; j < 4; j++)
                {
                    konec[i, j, 0] = hardDrop[j, 0];
                    konec[i, j, 1] = hardDrop[j, 1];
                }
                tvar.MoveRight(ref gb);
            }
            for (int i = od; i < kam; i++)
            {
                tvar.MoveLeft(ref gb);
            }
        }
        static public int checkBlockedHoles(ref GameBoard gb, int[,] Pozice)
        {
            Queue q;
            int numOfHoles = 0;
            char[,] deska = (char[,])gb.Board.Clone();
            for (int i = 0; i < 4; i++)
            {
                deska[Pozice[i, 0], Pozice[i, 1]] = 'P';//Pozice
            }
            for (int i = 0; i < 4; i++)
            {
                q = new Queue();
                int tempHoles = 0;
                if (Pozice[i, 0] != 19 && deska[Pozice[i, 0] + 1, Pozice[i, 1]] == '\0')
                {
                    deska[Pozice[i, 0] + 1, Pozice[i, 1]] = 'C'; //oCCupied
                    q.Insert(new int[2] { Pozice[i, 0] + 1, Pozice[i, 1] });
                    ++tempHoles;
                    while (q.Count())
                    {
                        int[] coord = q.Pop();
                        if (coord[0] == 2)
                        {
                            tempHoles = 0;
                            break;
                        }
                        else
                        {
                            if (coord[0] - 1 >= 2 && deska[coord[0] - 1, coord[1]] == '\0')
                            {
                                ++tempHoles;
                                deska[coord[0] - 1, coord[1]] = 'C';
                                q.Insert(new int[2] { coord[0] - 1, coord[1] });
                            }
                            if (coord[0] + 1 <= 19 && deska[coord[0] + 1, coord[1]] == '\0')
                            {
                                ++tempHoles;
                                deska[coord[0] + 1, coord[1]] = 'C';
                                q.Insert(new int[2] { coord[0] + 1, coord[1] });
                            }
                            if (coord[1] - 1 >= 0 && deska[coord[0], coord[1] - 1] == '\0')
                            {
                                ++tempHoles;
                                deska[coord[0], coord[1] - 1] = 'C';
                                q.Insert(new int[2] { coord[0], coord[1] - 1 });
                            }
                            if (coord[1] + 1 <= 9 && deska[coord[0], coord[1] + 1] == '\0')
                            {
                                ++tempHoles;
                                deska[coord[0], coord[1] + 1] = 'C';
                                q.Insert(new int[2] { coord[0], coord[1] + 1 });
                            }
                        }
                    }
                    if (tempHoles == 0)
                    {
                        clearAfterBFS(deska, new int[2] { Pozice[i, 0] + 1, Pozice[i, 1] });
                    }
                    else
                    {
                        numOfHoles += tempHoles;
                    }
                }
            }
            return numOfHoles;
        }
        static private void clearAfterBFS(char[,] deska, int[] coord)
        {
            Queue q = new Queue();
            deska[coord[0], coord[1]] = '\0';
            q.Insert(coord);
            while (q.Count())
            {
                int[] souradnice = q.Pop();
                if (souradnice[0] - 1 >= 2 && deska[souradnice[0] - 1, souradnice[1]] == 'C')
                {
                    deska[souradnice[0] - 1, souradnice[1]] = '\0';
                    q.Insert(new int[2] { souradnice[0] - 1, souradnice[1] });
                }
                if (souradnice[0] + 1 <= 19 && deska[souradnice[0] + 1, souradnice[1]] == 'C')
                {
                    deska[souradnice[0] + 1, souradnice[1]] = '\0';
                    q.Insert(new int[2] { souradnice[0] + 1, souradnice[1] });
                }
                if (souradnice[1] - 1 >= 0 && deska[souradnice[0], souradnice[1] - 1] == 'C')
                {
                    deska[souradnice[0], souradnice[1] - 1] = '\0';
                    q.Insert(new int[2] { souradnice[0], souradnice[1] - 1 });
                }
                if (souradnice[1] + 1 <= 9 && deska[souradnice[0], souradnice[1] + 1] == 'C')
                {
                    deska[souradnice[0], souradnice[1] + 1] = '\0';
                    q.Insert(new int[2] { souradnice[0], souradnice[1] + 1 });
                }
            }
        }
    }
}
