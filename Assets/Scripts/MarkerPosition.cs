using AxeMan.DungeonObject;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IMarkerPosition
    {
        void MoveMarkerToPC(GameObject marker);

        void ResetMarkerPosition(GameObject marker);
    }

    public class MarkerPosition : MonoBehaviour, IMarkerPosition
    {
        private MetaInfo pcMetaInfo;

        public void MoveMarkerToPC(GameObject marker)
        {
            int[] position = pcMetaInfo.Position;

            marker.GetComponent<LocalManager>().SetPosition(position);
            GetComponent<TileOverlay>().TryHideTile(position);
        }

        public void ResetMarkerPosition(GameObject marker)
        {
            int invalid = -999;
            int[] outOfScreen = new int[] { invalid, invalid };
            int[] current = marker.GetComponent<MetaInfo>().Position;

            marker.GetComponent<LocalManager>().SetPosition(outOfScreen);
            GetComponent<TileOverlay>().TryHideTile(current);
        }

        private void MarkerPosition_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            GameObject pc = e.PC;
            pcMetaInfo = pc.GetComponent<MetaInfo>();
        }

        private void Start()
        {
            GetComponent<Wizard>().SettingReference
                += MarkerPosition_SettingReference;
        }
    }
}
