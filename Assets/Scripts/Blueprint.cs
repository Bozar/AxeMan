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
        MainTag MTag { get; }

        int[] Position { get; }

        SubTag STag { get; }
    }

    public class Blueprint : MonoBehaviour, IBlueprint
    {
        public event EventHandler<DrawingBlueprintEventArgs> DrawingBlueprint;

        public IPrototype[] GetBlueprint(BlueprintTag bTag)
        {
            var ea = new DrawingBlueprintEventArgs(bTag);
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
        public DrawingBlueprintEventArgs(BlueprintTag bTag)
        {
            BTag = bTag;
        }

        public BlueprintTag BTag { get; }

        public IPrototype[] Data { get; set; }
    }

    public class ProtoObject : IPrototype
    {
        public ProtoObject(MainTag mTag, SubTag sTag, int[] position)
        {
            MTag = mTag;
            STag = sTag;
            Position = position;
        }

        public MainTag MTag { get; }

        public int[] Position { get; }

        public SubTag STag { get; }
    }
}
