﻿/*
Tetris
David Kroupa, I. ročník, 31. st. skupina
letní semestr 2021/22
Programování 2 NPRG031
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class QueuePozic
    {
        //standardni fronta s pretizenou funkci Insert() a funkce Pop() navraci objekt InfoBlock
        //fronta pouzita pri prohledavani vsech moznych dosazitelnych pozic
        VagonPozic head;
        VagonPozic tail;
        int count;
        public QueuePozic()
        {
            this.head = null;
            this.tail = null;
            count = 0;
        }
        public bool Count()
        {
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int NumCount()
        {
            return this.count;
        }
        public void Insert(int[,] val, string navigace)
        {
            if (this.head == null)
            {
                this.head = new VagonPozic(navigace, val, null);
                this.tail = this.head;
            }
            else
            {
                VagonPozic pom = new VagonPozic(navigace, val, null);
                this.tail.next = pom;
                this.tail = pom;
            }
            ++this.count;
        }
        public void Insert(InfoBlock ib)
        {
            if (this.head == null)
            {
                this.head = new VagonPozic(ib.StringValue, ib.ArrayValue, null);
                this.tail = this.head;
            }
            else
            {
                VagonPozic pom = new VagonPozic(ib.StringValue, ib.ArrayValue, null);
                this.tail.next = pom;
                this.tail = pom;
            }
            ++this.count;
        }
        public InfoBlock Pop()
        {
            int[,] pozice = this.head.Pozic;
            string nav = this.head.navigace;
            if (this.tail == this.head)
            {
                this.tail = this.head = null;
            }
            else
            {
                this.head = this.head.next;
            }
            --this.count;
            return new InfoBlock(nav, pozice);
        }
    }
}
