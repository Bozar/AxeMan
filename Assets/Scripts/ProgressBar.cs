using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PrototypeFactory;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class ProgressBar : MonoBehaviour
    {
        private int maxX;
        private int minX;

        public int XCoordinate { get; private set; }

        public int YCoordinate { get; private set; }

        private void Awake()
        {
            minX = 0;
            maxX = 5;

            XCoordinate = minX;
            YCoordinate = 9;
        }

        private void CreateBar()
        {
            IPrototype[] protoBar = GetComponent<Blueprint>().GetBlueprint(
                BlueprintTag.ProgressBar);
            GetComponent<CreateObject>().Create(protoBar);
        }

        private int GetXCoordinate(int current, int min, int max)
        {
            int result = ++current;
            if (result > max)
            {
                result = min;
            }
            return result;
        }

        private void ProgressBar_EndingTurn(object sender, EndingTurnEventArgs e)
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            LocalManager manager = pc.GetComponent<LocalManager>();
            if (!manager.MatchID(e.ObjectID))
            {
                return;
            }

            RemoveBar();
            XCoordinate = GetXCoordinate(XCoordinate, minX, maxX);
            CreateBar();
        }

        private void RemoveBar()
        {
            GameObject[] bar = GetComponent<SearchObject>().Search(
                SubTag.ProgressBar);
            foreach (GameObject b in bar)
            {
                b.GetComponent<LocalManager>().Remove();
            }
        }

        private void Start()
        {
            GetComponent<TurnManager>().EndingTurn
                += ProgressBar_EndingTurn;
        }
    }
}
