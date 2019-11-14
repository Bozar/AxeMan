using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SchedulingSystem;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class AnnouncePCDeath : MonoBehaviour
    {
        private bool announce;
        private SpriteRenderer pcRenderer;

        private void AnnouncePCDeath_BuryingPC(object sender, EventArgs e)
        {
            announce = true;
        }

        private void AnnouncePCDeath_EndingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (announce)
            {
                GetComponent<ColorManager>().SetColor(
                    pcRenderer, ColorTag.Orange);

                GetComponent<LogManager>().Add(new LogMessage(
                    LogCategoryTag.GameProgress, LogMessageTag.PCDeath));

                announce = false;
            }
        }

        private void AnnouncePCDeath_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            GameObject pc = e.PC;
            pcRenderer = pc.GetComponent<SpriteRenderer>();
        }

        private void Awake()
        {
            announce = false;
        }

        private void Start()
        {
            GetComponent<BuryPC>().BuryingPC += AnnouncePCDeath_BuryingPC;
            GetComponent<TurnManager>().EndingTurn += AnnouncePCDeath_EndingTurn;
            GetComponent<InitializeMainGame>().SettingReference
                += AnnouncePCDeath_SettingReference;
        }
    }
}
