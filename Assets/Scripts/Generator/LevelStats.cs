using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu]
    public class LevelStats : ScriptableObject
    {
        public float mainRoomCutoff = 2;
        public float percFromGraphToPaths = 0.1f;
        public int numberOfCells = 40;
        public float cellMinWidth = 3;
        public float cellMaxWidth = 6;
        public float cellMinHeight = 3;
        public float cellMaxHeight = 6;
        public float roomCircleRadius = 10;
    }
}
