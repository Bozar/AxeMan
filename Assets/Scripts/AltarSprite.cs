using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class AltarSprite : MonoBehaviour
    {
        private Sprite[] altarSprites;
        private int upgradeCount;

        private void AltarSprite_UpgradingAltar(object sender, EventArgs e)
        {
            GameObject[] altars;

            if (upgradeCount < altarSprites.Length)
            {
                altars = GetComponent<SearchObject>().Search(SubTag.LifeAltar);
                foreach (GameObject go in altars)
                {
                    go.GetComponent<SpriteRenderer>().sprite
                        = altarSprites[upgradeCount];
                }

                upgradeCount++;
            }
        }

        private void Awake()
        {
            upgradeCount = 0;

            // We have one large sprite map and a single sprite is sliced from
            // it. So we need to call `LoadAll()` instead of `Load()` here.
            Sprite[] sprites = Resources.LoadAll<Sprite>("curses_vector_32x48");
            altarSprites = new Sprite[]
            {
                // Diamond
                sprites[3],
                // Heart
                sprites[2],
            };
        }

        private void Start()
        {
            GetComponent<UpgradeAltar>().UpgradingAltar
                += AltarSprite_UpgradingAltar;
        }
    }
}
