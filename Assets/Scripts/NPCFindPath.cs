using AxeMan.GameSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCFindPath
    {
        int[][] GetPath(int[] source, int distance);
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

        public int[][] GetPath(int[] source, int distance)
        {
            DrawDistanceMap();
            int[][] path = TryGetPath(source, distance);

            return path;
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
            return GetDistanceMapValue(position) == distanceInitial;
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
            SetDistanceMapValue(e.PC, zeroPoint);

            SetDistance(startPoint);
        }

        private int GetDistanceMapValue(int[] position)
        {
            return distanceMap[position[0], position[1]];
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
                minDistance = Math.Min(minDistance, GetDistanceMapValue(pos));
            }
            return minDistance;
        }

        private bool GetNextStep(int[] source, out int[] next)
        {
            // Step 1: Search all available destinations.
            int[][] neighbors = GameCore.AxeManCore.GetComponent<Distance>()
                .GetNeighbor(source);
            int minDistance = GetDistanceMapValue(source);
            int currentDistance;
            Stack<int[]> steps = new Stack<int[]>();

            foreach (int[] neighbor in neighbors)
            {
                currentDistance = GetDistanceMapValue(neighbor);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    steps.Clear();
                    steps.Push(neighbor);
                }
                else if (currentDistance == minDistance)
                {
                    steps.Push(neighbor);
                }
            }

            // Step 2: Pick one destination.
            next = null;
            int count = steps.Count;
            int pick;

            if (count > 1)
            {
                // TODO: Get a random int from 0 to count.
                pick = 0;
                for (int i = 0; i < count; i++)
                {
                    next = steps.Pop();
                    if (i == pick)
                    {
                        break;
                    }
                }
            }
            else if (count == 1)
            {
                next = steps.Pop();
            }
            return count > 0;
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
            int distance;

            foreach (int[] neighbor in neighbors)
            {
                if (DistanceNotSet(neighbor))
                {
                    distance
                        = move
                        + GetMinDistance(neighbor)
                        + trapMap[neighbor[0], neighbor[1]];

                    SetDistanceMapValue(neighbor, distance);
                    startPoint.Enqueue(neighbor);
                }
            }
            SetDistance(startPoint);
        }

        private void SetDistanceMapValue(int[] position, int distance)
        {
            distanceMap[position[0], position[1]] = distance;
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

        private int[][] TryGetPath(int[] source, int distance)
        {
            Queue<int[]> path = new Queue<int[]>();

            for (int i = 0; i < distance; i++)
            {
                if (GetNextStep(source, out int[] next))
                {
                    path.Enqueue(next);
                    source = next;
                }
                else
                {
                    break;
                }
            }
            return path.ToArray();
        }
    }
}
