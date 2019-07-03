using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus : MonoBehaviour
    {
        private void Canvas_PCStatus_ChangedHP(object sender,
            ChangedHPEventArgs e)
        {
            if (e.STag != SubTag.PC)
            {
                return;
            }
            UpdateHP();
        }

        private void Canvas_PCStatus_CreatedWorld(object sender,
            EventArgs e)
        {
            UpdateHP();
        }

        private void Start()
        {
            GetComponent<Wizard>().CreatedWorld
                += Canvas_PCStatus_CreatedWorld;
            GetComponent<PublishHP>().ChangedHP
                += Canvas_PCStatus_ChangedHP;
        }

        private void UpdateHP()
        {
            Text ui = GetComponent<SearchUI>().SearchText(
                CanvasTag.Canvas_PCStatus_Left, UITag.HP);
            HP hp = GetComponent<SearchObject>().Search(SubTag.PC)[0]
                .GetComponent<HP>();
            ui.text = "HP: " + hp.Current + "/" + hp.Max;
        }
    }
}
