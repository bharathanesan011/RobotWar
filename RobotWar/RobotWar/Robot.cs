using System;
using System.Collections.Generic;
using System.Text;

namespace RobotWar
{
    class Robot
    {
        public int x { get; set; }
        public int y { get; set; }
        public char direction { get; set; }
        public string moves { get; set; }

        public Robot(int _x, int _y, char _c, string _moves)
        {
            this.x = _x;
            this.y = _y;
            this.direction = _c;
            this.moves = _moves;
        }
    }
}
