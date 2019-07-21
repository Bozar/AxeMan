using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public interface IBlueprint
    {
        IPrototype[] GetBlueprint(BlueprintTag bTag);
    }

    public interface IPrototype
    {
        MainTag MainTag { get; }

        int[] Position { get; }

        SubTag SubTag { get; }
    }

    public class Blueprint : MonoBehaviour, IBlueprint
    {
        public event EventHandler<DrawingBlueprintEventArgs> DrawingBlueprint;

        public IPrototype[] GetBlueprint(BlueprintTag blueprintTag)
        {
            var ea = new DrawingBlueprintEventArgs(blueprintTag);
            OnDrawingBlueprint(ea);

            return ea.Data;
        }

        protected virtual void OnDrawingBlueprint(DrawingBlueprintEventArgs e)
        {
            DrawingBlueprint?.Invoke(this, e);
        }
    }

    public class DrawingBlueprintEventArgs : EventArgs
    {
        public DrawingBlueprintEventArgs(BlueprintTag blueprintTag)
        {
            BlueprintTag = blueprintTag;
        }

        public BlueprintTag BlueprintTag { get; }

        public IPrototype[] Data { get; set; }
    }

    public class ProtoObject : IPrototype
    {
        public ProtoObject(MainTag mainTag, SubTag subTag, int[] position)
        {
            MainTag = mainTag;
            SubTag = subTag;
            Position = position;
        }

        public MainTag MainTag { get; }

        public int[] Position { get; }

        public SubTag SubTag { get; }
    }
}
