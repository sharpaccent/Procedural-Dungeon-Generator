using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class DebugViewer : MonoBehaviour
    {
        public LevelGenerator levelGenerator;

        void Start()
        {
            levelGenerator.GenerateLevel();
        }

        void Update()
        {
            if (levelGenerator == null)
                return;

            foreach (GeneratorCell c in levelGenerator.cells)
            {
                Color col = Color.gray;

                if(c.isMainRoom)
                {
                    col = Color.red;
                }
                else if(c.isPathRoom)
                {
                    col = Color.blue;
                }

                Vector3 bottomLineStart = new Vector3(c.x, c.y, 0);
                Vector3 bottomLineEnd = new Vector3(c.x + c.width, c.y, 0);

                Vector3 topLineStart = new Vector3(c.x, c.y + c.height, 0);
                Vector3 topLineEnd = new Vector3(c.x + c.width, c.y + c.height, 0);

                Vector3 leftLineStart = new Vector3(c.x, c.y, 0);
                Vector3 leftLineEnd = new Vector3(c.x, c.y + c.height, 0);

                Vector3 rightLineStart = new Vector3(c.x + c.width, c.y, 0);
                Vector3 rightLineEnd = new Vector3(c.x + c.width, c.y + c.height, 0);

                Debug.DrawLine(bottomLineStart, bottomLineEnd, col);
                Debug.DrawLine(topLineStart, topLineEnd, col);
                Debug.DrawLine(leftLineStart, leftLineEnd, col);
                Debug.DrawLine(rightLineStart, rightLineEnd, col);

                col = Color.green;

                foreach (Delaunay.Geo.LineSegment line in levelGenerator.delaunayLines)
                {
                    Vector3 lstart = new Vector3(line.p0.Value.x, line.p0.Value.y, 0);
                    Vector3 lend = new Vector3(line.p1.Value.x, line.p1.Value.y, 0);

                    Debug.DrawLine(lstart, lend, col);
                }

                col = Color.yellow;
                foreach (Delaunay.Geo.LineSegment line in levelGenerator.spanningTree)
                {
                    Vector3 lstart = new Vector3(line.p0.Value.x, line.p0.Value.y, 0);
                    Vector3 lend = new Vector3(line.p1.Value.x, line.p1.Value.y, 0);

                    Debug.DrawLine(lstart, lend, col);
                }
            }

            foreach (Path p in levelGenerator.paths)
            {
                foreach (BlockPath bp in p.path)
                {
                    Debug.DrawLine(new Vector3(bp.start.x, bp.start.y, 0), new Vector3(bp.end.x, bp.end.y, 0), Color.cyan);
                }
            }
        }
    }
}
