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
        private int height;
        private int impassable;
        private int[,] map;
        private int mapInitial;
        private int move;
        private int trap;
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
            map = new int[width, height];

            mapInitial = -99999;
            zeroPoint = 0;

            impassable = 99999;
            move = 10;
            trap = 20;
        }

        private bool DistanceNotSet(int[] position)
        {
            return map[position[0], position[1]] == mapInitial;
        }

        private void DrawDistanceMap()
        {
            ResetMap();

            SearchObstacleEventArgs e = SearchObstacle();
            Stack<int[]> startPoint = new Stack<int[]>();
            startPoint.Push(e.PC);

            SetZeroPoint(e.PC);
            SetDistance(startPoint);
            SetObstacle(e.Impassable, impassable, false);
            SetObstacle(e.Trap, trap, true);
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
                minDistance = Math.Min(minDistance, map[pos[0], pos[1]]);
            }
            return minDistance;
        }

        private void ResetMap()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j] = mapInitial;
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

        private void SetDistance(Stack<int[]> startPoint)
        {
            if (startPoint.Count < 1)
            {
                return;
            }

            int[] check = startPoint.Pop();
            int[][] neighbors = GameCore.AxeManCore.GetComponent<Distance>()
                .GetNeighbor(check);

            foreach (int[] pos in neighbors)
            {
                if (DistanceNotSet(pos))
                {
                    map[pos[0], pos[1]] = move + GetMinDistance(pos);
                    startPoint.Push(pos);
                }
            }

            SetDistance(startPoint);
        }

        private void SetObstacle(Stack<int[]> obstacle, int distance,
            bool addDistance)
        {
            int[] position;

            while (obstacle.Count > 0)
            {
                position = obstacle.Pop();

                if (addDistance)
                {
                    map[position[0], position[1]] += distance;
                }
                else
                {
                    map[position[0], position[1]] = distance;
                }
            }
        }

        private void SetZeroPoint(int[] position)
        {
            map[position[0], position[1]] = zeroPoint;
        }
    }
}
