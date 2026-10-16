using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(HostilityResponseModeUtility), "DrawResponseButton_GenerateMenu")]
    public static class HostilityResponseModeUtility_DrawResponseButton_GenerateMenu_Patch
    {
        public static bool Prefix(ref IEnumerable<Widgets.DropdownMenuElement<HostilityResponseMode>> __result, Pawn p)
        {
            if (p.HasTrait(ST_DefOf.ST_Daredevil))
            {
                var list = new List<Widgets.DropdownMenuElement<HostilityResponseMode>>();
                var responses = new List<HostilityResponseMode> { HostilityResponseMode.Attack, HostilityResponseMode.Ignore };
                foreach (var response in responses)
                {
                    if (response == HostilityResponseMode.Attack && p.WorkTagIsDisabled(WorkTags.Violent))
                    {
                        continue;
                    }
                    list.Add(new Widgets.DropdownMenuElement<HostilityResponseMode>
                    {
                        option = new FloatMenuOption(response.GetLabel(), delegate
                        {
                            p.playerSettings.hostilityResponse = response;
                        }, response.GetIcon(), Color.white),
                        payload = response
                    });
                }
                __result = list;
                return false;
            }
            return true;
        }
    }
}
