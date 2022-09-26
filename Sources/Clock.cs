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
            public TimerData(string id, string name, string status, TimerAction timerAction, string timerActionId, string timerBlueprintId, int timerSubprintIndex, bool timerRunning, float currentTimerTime, float targetTimerTime)
            {
                Id = id;
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

            public string Id { get; }
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
                sb.AppendLine($"Id: {Id}");
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
            var child = GetFirstChild();
            if (child != null)
            {
                this.timerInfo = new TimerData(
                    child.CardData.Id,
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
                // var newtxt = $"root: {timerInfo} ";
                // if (!newtxt.Equals(txt))
                // {
                    // this.txt = newtxt;
                    // Plugin.L.LogInfo(this.txt);
                    // Plugin.L.LogInfo(child.CardData.Id);
                // }
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

        private GameCard GetFirstChild()
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
            switch (otherCard.Id)
            {
                case "berrybush":
                case "apple_tree":
                case "banana_tree":
                case "coconut_tree":
                case "cotton_plant":
                case "sugar_cane":
                case "tree":
                case "lumbercamp":
                case "rock":
                case "quarry":
                case "sand_quarry":
                case "iron_deposit":
                case "iron_mine":
                case "gold_deposit":
                case "gold_mine":
                case "cave":
                case "catacombs":
                case "forest":
                case "plains":
                case "graveyard":
                case "jungle":
                case "mountain":
                case "old_village":
				case "bottled_time":
                    return true;
                default:
                    return false;
            }
        }
    }
}
