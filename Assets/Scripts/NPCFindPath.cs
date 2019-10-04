using AxeMan.GameSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCFindPath
    {
        int[][] GetNextStep(int[] source, int distance);
    }

    public class NPCFindPath : MonoBehaviour, INPCFindPath
    {
        private int distanceInitial;
        private int[,] distanceMap;
        private int height;
        private int impassable;
        private int move;
        private int trap;
        private int trapInitial;
        private int[,] trapMap;
        private int width;
        private int zeroPoint;

        public int[][] GetNextStep(int[] source, int distance)
        {
            DrawDistanceMap();

            // TODO: Choose steps.

            return null;
        }

        private void Awake()
        {
            width = GameCore.AxeManCore.GetComponent<DungeonBoard>().Width;
            height = GameCore.AxeManCore.GetComponent<DungeonBoard>().Height;
            distanceMap = new int[width, height];
            trapMap = new int[width, height];

            distanceInitial = -99999;
            trapInitial = 0;
            zeroPoint = 0;

            impassable = 99999;
            move = 10;
            trap = 20;
        }

        private bool DistanceNotSet(int[] position)
        {
            return distanceMap[position[0], position[1]] == distanceInitial;
        }

        private void DrawDistanceMap()
        {
            ResetMap(distanceMap, distanceInitial);
            ResetMap(trapMap, trapInitial);

            SearchObstacleEventArgs e = SearchObstacle();
            Queue<int[]> startPoint = new Queue<int[]>();
            startPoint.Enqueue(e.PC);

            SetObstacle(distanceMap, e.Impassable, impassable);
            SetObstacle(trapMap, e.Trap, trap);
            SetZeroPoint(e.PC);

            SetDistance(startPoint);
        }

        private int GetMinDistance(int[] position)
        {
            int[][] neighbors = GameCore.AxeManCore.GetComponent<Distance>()
                .GetNeighbor(position);
            int minDistance = impassable;

            foreach (int[] pos in neighbors)
            {
                if (DistanceNotSet(pos))
                {
                    continue;
                }
                minDistance = Math.Min(minDistance, distanceMap[pos[0], pos[1]]);
            }
            return minDistance;
        }

        private void ResetMap(int[,] map, int initial)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j] = initial;
                }
            }
        }

        private SearchObstacleEventArgs SearchObstacle()
        {
            Stack<int[]> impassable = new Stack<int[]>();
            Stack<int[]> trap = new Stack<int[]>();
            int[] pc = new int[] { };
            SearchObstacleEventArgs e = new SearchObstacleEventArgs(pc,
                impassable, trap);

            GameCore.AxeManCore.GetComponent<PathFinding>()
                .PublishSearchObstacle(e);
            return e;
        }

        private void SetDistance(Queue<int[]> startPoint)
        {
            if (startPoint.Count < 1)
            {
                return;
            }

            int[] check = startPoint.Dequeue();
            int[][] neighbors = GameCore.AxeManCore.GetComponent<Distance>()
                .GetNeighbor(check);

            foreach (int[] neighbor in neighbors)
            {
                if (DistanceNotSet(neighbor))
                {
                    distanceMap[neighbor[0], neighbor[1]]
                        = move
                        + GetMinDistance(neighbor)
                        + trapMap[neighbor[0], neighbor[1]];

                    startPoint.Enqueue(neighbor);
                }
            }
            SetDistance(startPoint);
        }

        private void SetObstacle(int[,] map, Stack<int[]> obstacle, int distance)
        {
            int[] position;

            while (obstacle.Count > 0)
            {
                position = obstacle.Pop();

                if (!GameCore.AxeManCore.GetComponent<DungeonBoard>()
                    .IndexOutOfRange(position[0], position[1]))
                {
                    map[position[0], position[1]] = distance;
                }
            }
        }

        private void SetZeroPoint(int[] position)
        {
            distanceMap[position[0], position[1]] = zeroPoint;
        }
    }
}
