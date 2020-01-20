using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class BuildSkillDataBind : MonoBehaviour
    {
        private CanvasTag[] columns;
        private Dictionary<CommandTag, int> command2int;
        private Dictionary<CommandTag, SkillNameTag> command2skillName;
        private CanvasTag currentCanvas;
        private CommandTag currentCommand;
        private UITag currentMiddleRow;
        private UITag currentRightRow;
        private SkillNameTag currentSkillName;
        private Dictionary<CanvasTag, UITag[]> rows;

        private Dictionary<CanvasTag, Dictionary<UITag, SkillSlotTag>>
            ui2skillSlot;

        public void TryMoveUIFocus(CommandTag command)
        {
            Debug.Log(command);
        }

        private void Awake()
        {
            columns = new CanvasTag[]
            {
                CanvasTag.Canvas_BuildSkill_Middle,
                CanvasTag.Canvas_BuildSkill_Right
            };

            rows = new Dictionary<CanvasTag, UITag[]>()
            {
                {
                    CanvasTag.Canvas_BuildSkill_Middle,
                    new UITag[]
                    {
                        UITag.Text2, UITag.Text4, UITag.Text6, UITag.Text8,
                        UITag.Text10, UITag.Text12, UITag.Text14,
                    }
                },
                {
                    CanvasTag.Canvas_BuildSkill_Right,
                    new UITag[]
                    {
                        UITag.Text2, UITag.Text4, UITag.Text6, UITag.Text8,
                        UITag.Text10,
                    }
                }
            };

            command2int = new Dictionary<CommandTag, int>()
            {
                { CommandTag.Left, -1 },
                { CommandTag.Right, 1 },
                { CommandTag.Up, -1 },
                { CommandTag.Down, 1 },
            };

            command2skillName = new Dictionary<CommandTag, SkillNameTag>()
            {
                { CommandTag.SkillQ, SkillNameTag.SkillQ },
                { CommandTag.SkillW, SkillNameTag.SkillW },
                { CommandTag.SkillE, SkillNameTag.SkillE },
                { CommandTag.SkillR, SkillNameTag.SkillR },
            };

            ui2skillSlot
                = new Dictionary<CanvasTag, Dictionary<UITag, SkillSlotTag>>()
                {
                    { CanvasTag.Canvas_BuildSkill_Middle,
                        new Dictionary<UITag, SkillSlotTag>()
                        {
                            { UITag.Text2, SkillSlotTag.SkillType },
                            { UITag.Text4, SkillSlotTag.Merit1 },
                            { UITag.Text6, SkillSlotTag.Merit2 },
                            { UITag.Text8, SkillSlotTag.Merit3 },
                            { UITag.Text10, SkillSlotTag.Flaw1 },
                            { UITag.Text12, SkillSlotTag.Flaw2 },
                            { UITag.Text14, SkillSlotTag.Flaw3 },
                        }
                    },
                    { CanvasTag.Canvas_BuildSkill_Right,
                        new Dictionary<UITag, SkillSlotTag>()
                        {
                            { UITag.Text2, SkillSlotTag.Component1 },
                            { UITag.Text4, SkillSlotTag.Component2 },
                            { UITag.Text6, SkillSlotTag.Component3 },
                            { UITag.Text8, SkillSlotTag.Component4 },
                            { UITag.Text10, SkillSlotTag.Component5 },
                        }
                    }
                };
        }
    }
}
