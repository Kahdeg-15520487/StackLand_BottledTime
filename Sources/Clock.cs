using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SandClock.Sources
{
    public class Clock : CardData
    {
        class TimerData
        {
            public TimerData(string name, string status, TimerAction timerAction, string timerActionId, string timerBlueprintId, int timerSubprintIndex, bool timerRunning, float currentTimerTime, float targetTimerTime)
            {
                Name = name;
                Status = status;
                TimerAction = timerAction;
                TimerActionId = timerActionId;
                TimerBlueprintId = timerBlueprintId;
                TimerSubprintIndex = timerSubprintIndex;
                TimerRunning = timerRunning;
                CurrentTimerTime = currentTimerTime;
                TargetTimerTime = targetTimerTime;
            }

            public string Name { get; }
            public string Status { get; }
            public TimerAction TimerAction { get; }
            public string TimerActionId { get; }
            public string TimerBlueprintId { get; }
            public int TimerSubprintIndex { get; }
            public bool TimerRunning { get; }
            public float CurrentTimerTime { get; }
            public float TargetTimerTime { get; }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine($"Name: {Name}");
                sb.AppendLine($"Status: {Status}");
                sb.AppendLine($"TimerActionId: {TimerActionId}");
                sb.AppendLine($"TimerBlueprintId: {TimerBlueprintId}");
                sb.AppendLine($"TimerSubprintIndex: {TimerSubprintIndex}");
                sb.AppendLine($"TimerRunning: {TimerRunning}");
                sb.AppendLine($"CurrentTimerTime: {CurrentTimerTime}");
                sb.AppendLine($"TargetTimerTime: {TargetTimerTime}");
                return sb.ToString();
            }
        }
        private string txt;
        private TimerData timerInfo;

        public override void UpdateCard()
        {
            var child = GetFirstChildNotClock();
            if (child != null)
            {
                this.timerInfo = new TimerData(
                    child.CardData.Name,
                    child.Status,
                    child.TimerAction,
                    child.TimerActionId,
                    child.TimerBlueprintId,
                    child.TimerSubprintIndex,
                    child.TimerRunning,
                    child.CurrentTimerTime,
                    child.TargetTimerTime
                );
                var newtxt = $"root: {timerInfo} ";
                if (!newtxt.Equals(txt))
                {
                    this.txt = newtxt;
                    Plugin.L.LogInfo(this.txt);
                }
                if (child.TimerRunning)
                {
                    child.CurrentTimerTime += Time.deltaTime * WorldManager.instance.TimeScale;
                }
            }
            else
            {
                this.timerInfo = null;
                this.txt = string.Empty;
            }
            base.UpdateCard();
        }

        private GameCard GetFirstChildNotClock()
        {
            var result = this.MyGameCard.Child;
            while (result != null && result.CardData.Id == this.Id)
            {
                result = result.Child;
            }
            return result;
        }

        protected override bool CanHaveCard(CardData otherCard)
        {
            return true;
        }
    }
}
