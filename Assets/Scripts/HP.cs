using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
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
            Min = 0;
            Max = GameCore.AxeManCore.GetComponent<ActorData>().GetIntData(
                GetComponent<MetaInfo>().MainTag,
                GetComponent<MetaInfo>().SubTag,
                ActorDataTag.HP);
            Current = Max;
        }

        private void PublishHP()
        {
            GameCore.AxeManCore.GetComponent<PublishHP>().PublishChangedHP(
               new ChangedHPEventArgs(GetComponent<MetaInfo>().SubTag, Current));
        }
    }
}
