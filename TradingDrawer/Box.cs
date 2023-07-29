// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceTradingDrawer
{
    public class Box
    {
        public int Id { get { return _id; } }
        private int _id;
        public Box(int x, int y, int width, int length)
        {
            UpperLeftCorner = new Point(x, y);
            Width = width;
            Length = length;
            _id = new Random().Next(0, 1000);
        }

        public Point UpperLeftCorner { get; set; }
        private Line _input { get; set; }
        private Line _exit { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public bool Cuptured = false;
        public int delx = 0;
        public int dely = 0;
        private int attachx = -1;
        private int attachy = -1;

        public void Move(int x, int y)
        {
            if (attachx != -1 && attachy != -1 && x != -1)
            {
                UpperLeftCorner.X = UpperLeftCorner.X - attachx + x;
                UpperLeftCorner.Y = UpperLeftCorner.Y - attachy + y;
            }
            attachx = x;
            attachy = y;
        }

        public void Rebuid(int x, int y)
        {
            Length = x - UpperLeftCorner.X;
            Width = y - UpperLeftCorner.Y;
        }

        public void Draw(Graphics g)
        {
            Pen blackPen = new Pen(Color.Black, 2);
            Rectangle rect = new Rectangle(UpperLeftCorner.X, UpperLeftCorner.Y, Length, Width);
            g.DrawRectangle(blackPen, rect);
            _input = new Line(UpperLeftCorner.X + Length, UpperLeftCorner.Y + Width / 2, UpperLeftCorner.X + Length + 20, UpperLeftCorner.Y + Width / 2);
            _exit = new Line(UpperLeftCorner.X, UpperLeftCorner.Y + Width / 2, UpperLeftCorner.X - 20, UpperLeftCorner.Y + Width / 2);
            g.DrawLine(blackPen, _input.P1.X, _input.P1.Y, _input.P2.X, _input.P2.Y);
            g.DrawLine(blackPen, _exit.P1.X, _exit.P1.Y, _exit.P2.X, _exit.P2.Y);
        }




        public void ZoomIn(double num, int delx, int dely)
        {
            UpperLeftCorner = new Point((int)(UpperLeftCorner.X * num) - delx, (int)(UpperLeftCorner.Y * num) - dely);
            Width = (int)(Width * num);
            Length = (int)(Length * num);

        }

        public void ZoomOut(double num, int delx, int dely)
        {
            UpperLeftCorner = new Point((int)((UpperLeftCorner.X + delx) / num), (int)((UpperLeftCorner.Y + dely) / num));
            Width = (int)(Width / num);
            Length = (int)(Length / num);
        }

    }
}
