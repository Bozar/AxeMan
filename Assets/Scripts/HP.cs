using AxeMan.GameSystem;
using AxeMan.GameSystem.GameEvent;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface IHP
    {
        int Current { get; }

        int Max { get; }

        int Min { get; }

        int Add(int hp);

        int Subtract(int hp);
    }

    public class HP : MonoBehaviour, IHP
    {
        public int Current { get; private set; }

        public int Max { get; private set; }

        public int Min { get; private set; }

        public int Add(int hp)
        {
            Current += hp;
            Current = Math.Min(Current, Max);

            PublishHP();
            return Current;
        }

        public int Subtract(int hp)
        {
            Current -= hp;
            Current = Math.Max(Current, Min);

            PublishHP();
            return Current;
        }

        private void Awake()
        {
            // TODO: Get HP from an external XML file.
            Max = 20;
            Min = 0;

            Current = Max;
        }

        private void PublishHP()
        {
            GameCore.AxeManCore.GetComponent<PublishHP>().PublishChangedHP(
               new ChangedHPEventArgs(GetComponent<MetaInfo>().SubTag, Current));
        }
    }
}
