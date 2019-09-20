using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.GameEvent
{
    public class BlockPCMovementEventArgs : EventArgs
    {
        public BlockPCMovementEventArgs(int[] targetPosition)
        {
            TargetPosition = targetPosition;
        }

        public int[] TargetPosition { get; }
    }

    public class CheckingTerrainEventArgs : EventArgs
    {
        public CheckingTerrainEventArgs(int objectID, int[] position,
            Stack<bool> isPassable)
        {
            ObjectID = objectID;
            Position = position;
            IsPassable = isPassable;
        }

        public Stack<bool> IsPassable { get; }

        public int ObjectID { get; }

        public int[] Position { get; }
    }

    public class PublishPosition : MonoBehaviour
    {
        public event EventHandler<BlockPCMovementEventArgs> BlockingPCMovement;

        public event EventHandler<CheckingTerrainEventArgs> CheckingTerrain;

        public void BlockPCMovement(BlockPCMovementEventArgs e)
        {
            OnBlockingPCMovement(e);
        }

        public void CheckTerrain(CheckingTerrainEventArgs e)
        {
            OnCheckingTerrain(e);
        }

        protected virtual void OnBlockingPCMovement(BlockPCMovementEventArgs e)
        {
            BlockingPCMovement?.Invoke(this, e);
        }

        protected virtual void OnCheckingTerrain(CheckingTerrainEventArgs e)
        {
            CheckingTerrain?.Invoke(this, e);
        }
    }
}
