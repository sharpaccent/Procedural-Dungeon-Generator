using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class GeneratorCell 
    {
        public int index;
        public int x;
        public int y;

        public int width;
        public int height;

        public bool isMainRoom = true;
        public bool isPathRoom = false;

        public bool CollidesWith(GeneratorCell cell)
        {
            bool retVal = true;

            if(cell.x >= this.x + this.width ||
                cell.y >= this.y + this.height ||
                cell.x + cell.width <= this.x ||
                cell.y + cell.height <= this.y)
            {
                retVal = false;
            }

            return retVal;
        }

        public void Shift(int shiftX, int shiftY)
        {
            x += shiftX;
            y += shiftY;
        }

    }
}
