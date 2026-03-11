using HarmonyLib;
using Outerm;
using RimWorld;
using Verse;

namespace OutermEnglishTranslation
{
	[StaticConstructorOnStartup]
	public static class Bootstrap
	{
		static Bootstrap()
		{
			new Harmony("codex.outermenglishtranslation").PatchAll();
		}
	}

	[HarmonyPatch(typeof(HAR_OT_Comp_GrowingWeapon), nameof(HAR_OT_Comp_GrowingWeapon.Notify_KilledEnemy))]
	public static class HAR_OT_Comp_GrowingWeapon_Notify_KilledEnemy_Patch
	{
		public static bool Prefix(HAR_OT_Comp_GrowingWeapon __instance)
		{
			__instance.extraDamage += 0.05f;
			Messages.Message(__instance.parent.LabelCap + " has grown. (Bonus: +" + __instance.extraDamage.ToString("F2") + ")", MessageTypeDefOf.PositiveEvent, true);
			return false;
		}
	}
}
