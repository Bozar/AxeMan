using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCMove
    {
        int Distance { get; }
    }

    public class NPCMove : MonoBehaviour, INPCMove
    {
        private int baseDistance;

        public int Distance
        {
            get
            {
                // TODO: Change distance based on actor status.
                return baseDistance;
            }
        }

        private void Awake()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            ActorDataTag dataTag = ActorDataTag.MoveDistance;

            baseDistance = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, dataTag);
        }
    }
}
