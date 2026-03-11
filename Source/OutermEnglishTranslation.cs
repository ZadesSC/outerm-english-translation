using HarmonyLib;
using Outerm;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
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

	[HarmonyPatch]
	public static class HAR_OT_SkillGizmoTranslationPatch
	{
		private sealed class SkillTextAccessor
		{
			public Func<object, string> GetDescription;
			public Func<object, int> GetCooldownTicks;
		}

		private static readonly Dictionary<Type, int> RequiredLevels = new Dictionary<Type, int>
		{
			{ typeof(HAR_OT_Comp_Skill_a), 1 },
			{ typeof(HAR_OT_Comp_Skill_b), 2 },
			{ typeof(HAR_OT_Comp_Skill_c), 3 },
			{ typeof(HAR_OT_Comp_Skill_d), 4 },
			{ typeof(HAR_OT_Comp_Skill_e), 5 },
		};

		private static readonly AccessTools.FieldRef<HAR_OT_Comp_Skill_a, string> SkillACommandDesc = AccessTools.FieldRefAccess<HAR_OT_Comp_Skill_a, string>("commandDesc");
		private static readonly AccessTools.FieldRef<HAR_OT_Comp_Skill_b, string> SkillBCommandDesc = AccessTools.FieldRefAccess<HAR_OT_Comp_Skill_b, string>("commandDesc");
		private static readonly AccessTools.FieldRef<HAR_OT_Comp_Skill_c, string> SkillCCommandDesc = AccessTools.FieldRefAccess<HAR_OT_Comp_Skill_c, string>("commandDesc");
		private static readonly AccessTools.FieldRef<HAR_OT_Comp_Skill_d, string> SkillDCommandDesc = AccessTools.FieldRefAccess<HAR_OT_Comp_Skill_d, string>("commandDesc");
		private static readonly AccessTools.FieldRef<HAR_OT_Comp_Skill_e, string> SkillECommandDesc = AccessTools.FieldRefAccess<HAR_OT_Comp_Skill_e, string>("commandDesc");

		private static readonly AccessTools.FieldRef<HAR_OT_Comp_Skill_a, int> SkillACooldownTicks = AccessTools.FieldRefAccess<HAR_OT_Comp_Skill_a, int>("coolDownTick");
		private static readonly AccessTools.FieldRef<HAR_OT_Comp_Skill_b, int> SkillBCooldownTicks = AccessTools.FieldRefAccess<HAR_OT_Comp_Skill_b, int>("coolDownTick");
		private static readonly AccessTools.FieldRef<HAR_OT_Comp_Skill_c, int> SkillCCooldownTicks = AccessTools.FieldRefAccess<HAR_OT_Comp_Skill_c, int>("coolDownTick");
		private static readonly AccessTools.FieldRef<HAR_OT_Comp_Skill_d, int> SkillDCooldownTicks = AccessTools.FieldRefAccess<HAR_OT_Comp_Skill_d, int>("coolDownTick");
		private static readonly AccessTools.FieldRef<HAR_OT_Comp_Skill_e, int> SkillECooldownTicks = AccessTools.FieldRefAccess<HAR_OT_Comp_Skill_e, int>("coolDownTick");

		private static readonly Dictionary<Type, SkillTextAccessor> Accessors = new Dictionary<Type, SkillTextAccessor>
		{
			{
				typeof(HAR_OT_Comp_Skill_a),
				new SkillTextAccessor
				{
					GetDescription = instance => SkillACommandDesc((HAR_OT_Comp_Skill_a)instance),
					GetCooldownTicks = instance => SkillACooldownTicks((HAR_OT_Comp_Skill_a)instance)
				}
			},
			{
				typeof(HAR_OT_Comp_Skill_b),
				new SkillTextAccessor
				{
					GetDescription = instance => SkillBCommandDesc((HAR_OT_Comp_Skill_b)instance),
					GetCooldownTicks = instance => SkillBCooldownTicks((HAR_OT_Comp_Skill_b)instance)
				}
			},
			{
				typeof(HAR_OT_Comp_Skill_c),
				new SkillTextAccessor
				{
					GetDescription = instance => SkillCCommandDesc((HAR_OT_Comp_Skill_c)instance),
					GetCooldownTicks = instance => SkillCCooldownTicks((HAR_OT_Comp_Skill_c)instance)
				}
			},
			{
				typeof(HAR_OT_Comp_Skill_d),
				new SkillTextAccessor
				{
					GetDescription = instance => SkillDCommandDesc((HAR_OT_Comp_Skill_d)instance),
					GetCooldownTicks = instance => SkillDCooldownTicks((HAR_OT_Comp_Skill_d)instance)
				}
			},
			{
				typeof(HAR_OT_Comp_Skill_e),
				new SkillTextAccessor
				{
					GetDescription = instance => SkillECommandDesc((HAR_OT_Comp_Skill_e)instance),
					GetCooldownTicks = instance => SkillECooldownTicks((HAR_OT_Comp_Skill_e)instance)
				}
			},
		};

		private static readonly HediffDef ArtificialRaceDef = DefDatabase<HediffDef>.GetNamedSilentFail("Outerm_Artificial");

		public static IEnumerable<MethodBase> TargetMethods()
		{
			yield return AccessTools.Method(typeof(HAR_OT_Comp_Skill_a), nameof(HAR_OT_Comp_Skill_a.CompGetGizmosExtra));
			yield return AccessTools.Method(typeof(HAR_OT_Comp_Skill_b), nameof(HAR_OT_Comp_Skill_b.CompGetGizmosExtra));
			yield return AccessTools.Method(typeof(HAR_OT_Comp_Skill_c), nameof(HAR_OT_Comp_Skill_c.CompGetGizmosExtra));
			yield return AccessTools.Method(typeof(HAR_OT_Comp_Skill_d), nameof(HAR_OT_Comp_Skill_d.CompGetGizmosExtra));
			yield return AccessTools.Method(typeof(HAR_OT_Comp_Skill_e), nameof(HAR_OT_Comp_Skill_e.CompGetGizmosExtra));
		}

		public static void Postfix(object __instance, ref IEnumerable<Gizmo> __result)
		{
			if (__instance == null || __result == null)
			{
				return;
			}

			SkillTextAccessor accessor;
			int requiredLevel;
			Type instanceType = __instance.GetType();
			if (!RequiredLevels.TryGetValue(instanceType, out requiredLevel) || !Accessors.TryGetValue(instanceType, out accessor))
			{
				return;
			}

			__result = RewriteGizmos(__instance, __result, requiredLevel, accessor);
		}

		private static IEnumerable<Gizmo> RewriteGizmos(object instance, IEnumerable<Gizmo> gizmos, int requiredLevel, SkillTextAccessor accessor)
		{
			foreach (Gizmo gizmo in gizmos)
			{
				Command command = gizmo as Command;
				if (command != null)
				{
					ApplySkillText(command, instance, requiredLevel, accessor);
				}

				yield return gizmo;
			}
		}

		private static void ApplySkillText(Command command, object instance, int requiredLevel, SkillTextAccessor accessor)
		{
			string baseDescription = accessor.GetDescription(instance);
			if (!baseDescription.NullOrEmpty())
			{
				command.defaultDesc = baseDescription.CapitalizeFirst();
			}

			int cooldownTicks = accessor.GetCooldownTicks(instance);
			if (cooldownTicks > 0)
			{
				command.Disable("On cooldown");
				command.defaultDesc = AppendDetail(command.defaultDesc, "Cooldown remaining: " + GenDate.ToStringTicksToPeriod(cooldownTicks, true, false, true, true, false));
				return;
			}

			Pawn pawn = ((ThingComp)instance).parent as Pawn;
			if (pawn == null || ArtificialRaceDef == null)
			{
				return;
			}

			Hediff artificialRace = pawn.health.hediffSet.GetFirstHediffOfDef(ArtificialRaceDef);
			if (artificialRace == null || artificialRace.Severity < requiredLevel)
			{
				string requirementText = "Requires Artificial Race Lv " + requiredLevel;
				command.Disable(requirementText);
				command.defaultDesc = AppendDetail(command.defaultDesc, "Unlock requirement: Artificial Race Lv " + requiredLevel);
			}
		}

		private static string AppendDetail(string baseDescription, string detail)
		{
			if (baseDescription.NullOrEmpty())
			{
				return detail;
			}

			return baseDescription + "\n\n" + detail;
		}
	}
}
