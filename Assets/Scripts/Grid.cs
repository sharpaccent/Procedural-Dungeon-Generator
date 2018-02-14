using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class Grid : MonoBehaviour
    {
        public LevelGenerator levelGenerator;

        public GameObject nodePrefab;

        public Sprite ground;
        public Sprite wall;
        public Sprite isoTile;

        Node[,] grid;
        int maxX;
        int maxY;
        public float scale = 0.32f;
        public float isoScale = 1.3f;

        public GridType gridType;
        public enum GridType
        {
            ortho, iso
        }

        void Start()
        {
            levelGenerator.OnLevelGenerationComplete = LevelGenerated;
            levelGenerator.GenerateLevel();
        }

        void LevelGenerated()
        {
            InitGrid();
        }

        void InitGrid()
        {
            Vector2 maxXY = levelGenerator.GetMaxXY();
            maxX = Mathf.CeilToInt(maxXY.x);
            maxY = Mathf.CeilToInt(maxXY.y);

            Vector2 minXY = levelGenerator.GetMinXY();
            int minX = Mathf.CeilToInt(Mathf.Abs(minXY.x));
            int minY = Mathf.CeilToInt(Mathf.Abs(minXY.y));

            maxX += minX;
            maxY += minY;

            grid = new Node[maxX + 1, maxY + 1];

            #region Render Cells
            foreach (GeneratorCell c in levelGenerator.cells)
            {
                for (int x = c.x; x <= c.x + c.width; x++)
                {
                    for (int y = c.y; y <= c.y + c.height; y++)
                    {
                        Node n = grid[x, y];
                        if(n == null)
                        {
                            n = CreateAt(x, y, false);
                            grid[x, y] = n;
                        }

                        bool isWall = false;
    
                        if (x == c.x || x == c.x + c.width || y == c.y || y == c.y + c.height)
                        {
                            isWall = true;
                        }

                        n.isWall = isWall;

                        if (gridType == GridType.ortho)
                        {
                            if (isWall)
                            {
                                n.nodeReferences.render.sprite = wall;
                            }
                            else
                            {
                                n.nodeReferences.render.sprite = ground;
                            }
                        }
                        else
                        {
                            n.nodeReferences.render.sprite = isoTile;
                        }

                    }
                }
            }
            #endregion

            #region Render Paths
            foreach (Path p in levelGenerator.paths)
            {
                foreach (BlockPath bp in p.path)
                {
                    int startX = Mathf.FloorToInt(bp.start.x);
                    int startY = Mathf.FloorToInt(bp.start.y);
                    int endX = Mathf.CeilToInt(bp.end.x);
                    int endY = Mathf.CeilToInt(bp.end.y);

                    int tmp = startX;
                    startX = Mathf.Min(startX, endX);
                    endX = Mathf.Max(endX, tmp);

                    tmp = startY;
                    startY = Mathf.Min(startY, endY);
                    endY = Mathf.Max(endY, tmp);

                    for (int x = startX; x <= endX; x++)
                    {
                        for (int y = startY; y <= endY; y++)
                        {
                            Node n = grid[x, y];
                            if(n == null)
                            {
                                CreateAt(x, y, false);
                            }
                            else
                            {
                                if (gridType == GridType.ortho)
                                {
                                    if (n.isWall)
                                    {
                                        n.isWall = false;
                                        n.nodeReferences.render.sprite = ground;
                                    }
                                }
                                else
                                {
                                    n.nodeReferences.render.sprite = isoTile;
                                }
                            }

                            AddPathWalls(x, y);

                            if(startY == endY)
                            {
                                int targetY = y + 1;
                                if(y == maxY)
                                {
                                    targetY = y - 1;
                                }

                                Node nn = grid[x, targetY];
                                if(nn == null)
                                {
                                    CreateAt(x, targetY, false);
                                }
                                else
                                {
                                    if (gridType == GridType.ortho)
                                    {
                                        if (nn.isWall)
                                        {
                                            nn.isWall = false;
                                            nn.nodeReferences.render.sprite = ground;
                                        }
                                    }
                                    else
                                    {
                                        nn.nodeReferences.render.sprite = isoTile;
                                    }
                                }

                                AddPathWalls(x, targetY);
                            }
                        }
                    }
                }
            }
            #endregion
        }

        Node CreateAt(int x, int y, bool isWall)
        {
            Node n = grid[x, y];
            if(n == null)
            {
                n = new Node();
                n.x = x;
                n.y = y;

                GameObject go = Instantiate(nodePrefab);
                Vector3 tp = Vector3.zero;
                NodeReferences nr = go.GetComponent<NodeReferences>();
                n.nodeReferences = nr;

                if (gridType == GridType.ortho)
                {
                    tp.x = x * scale;
                    tp.y = y * scale;
                    tp.z = y;
                }
                else
                {
                    tp.x = (x * isoScale);
                    tp.x += (y * isoScale);
                    tp.y = y * isoScale / 2;
                    tp.y += -x * isoScale / 2;
                    tp.z = 500 - x + y;
                }

                go.transform.position = tp;
                go.transform.parent = this.transform;
                grid[x, y] = n;
     
            }

            n.isWall = isWall;

            if (gridType == GridType.ortho)
            {
                if (isWall)
                {
                    n.nodeReferences.render.sprite = wall;
                }
                else
                {
                    n.nodeReferences.render.sprite = ground;
                }
            }
            else
            {
                n.nodeReferences.render.sprite = isoTile;
            }

            return n;
        }

        void AddPathWalls(int x, int y)
        {
            //top node
            if(y < maxY)
            {
                Node n = grid[x, y + 1];
                if(n == null)
                {
                    CreateAt(x, y + 1, true);
                }
            }

            //right node
            if(x < maxX)
            {
                Node n = grid[x + 1, y];
                if(n == null)
                {
                    CreateAt(x + 1, y, true);
                }
            }

            //bottom
            if(y > 0)
            {
                Node n = grid[x, y - 1];
                if(n == null)
                {
                    CreateAt(x, y - 1, true);
                }
            }

            //left node
            if(x > 0)
            {
                Node n = grid[x - 1, y];
                if(n == null)
                {
                    CreateAt(x - 1, y, true);
                }
            }
        }
    }

    public class Node
    {
        public int x;
        public int y;
        public bool isWall;
        public NodeReferences nodeReferences;
    }
}
