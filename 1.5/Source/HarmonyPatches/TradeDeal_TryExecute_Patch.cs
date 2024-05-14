using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(TradeDeal), "TryExecute")]
    public static class TradeDeal_TryExecute_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var reset = AccessTools.Method(typeof(TradeDeal), nameof(TradeDeal.Reset));
            foreach (var code in codeInstructions)
            {
                if (code.Calls(reset))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(TradeDeal_TryExecute_Patch), "ResolveTrade"));
                }
                yield return code;
            }
        }

        public static void ResolveTrade(TradeDeal __instance, ref bool actuallyTraded)
        {
            if (actuallyTraded && TradeSession.playerNegotiator.HasTrait(ST_DefOf.ST_Materialistic))
            {
                if (__instance.tradeables.Any(x => x.ActionToDo == TradeAction.PlayerBuys && x.CountToTransfer > 0))
                {
                    TradeSession.playerNegotiator.needs?.mood?.thoughts?.memories.TryGainMemory(ST_DefOf.ST_NewStuff);
                }
            }
        }
    }
}
