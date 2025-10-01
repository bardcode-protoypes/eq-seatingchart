using UnityEngine;
using System.Collections.Generic;

public class RuleChecker : MonoBehaviour
{
    public RuleResult Validate(List<GuestCardController> guestCards, List<TableController> tables)
    {
        var result = new RuleResult();

        foreach (var card in guestCards)
        {
            if (card.CurrentSeat == null) continue;

            foreach (var rule in card.GuestData.rules) 
            {
                if (!CheckRule(card, rule))
                {
                    string error = $"Rule violated: {card.GuestData.GuestNameRef} - {rule.type}";
                    result.Errors.Add(error);
                }
            }
        }

        return result;
    }

    private bool CheckRule(GuestCardController card, GuestRule rule)
    {
        var seat = card.CurrentSeat;

        switch (rule.type)
        {
            case RuleType.MustBeNextTo:
                return seat.nextTo.Exists(s => s.CurrentGuestCard?.GuestData == rule.targetGuest);

            case RuleType.MustNotBeNextTo:
                return !seat.nextTo.Exists(s => s.CurrentGuestCard?.GuestData == rule.targetGuest);

            case RuleType.MustBeOpposite:
                return seat.oppositeTo != null &&
                       seat.oppositeTo.CurrentGuestCard?.GuestData == rule.targetGuest;

            case RuleType.MustNotBeOpposite:
                return !(seat.oppositeTo != null &&
                         seat.oppositeTo.CurrentGuestCard?.GuestData == rule.targetGuest);

            case RuleType.MustBeAtSameTable:
                return seat.sameTable.Exists(s => s.CurrentGuestCard?.GuestData == rule.targetGuest);

            case RuleType.MustNotBeAtSameTable:
                return !seat.sameTable.Exists(s => s.CurrentGuestCard?.GuestData == rule.targetGuest);

            case RuleType.MustBeInZone:
                return seat.roomZones.Contains(rule.targetZone);

            case RuleType.MustNotBeInZone:
                return !seat.roomZones.Contains(rule.targetZone);

            default:
                return true;
        }
    }
}
