using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Unity.Display;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAssets.Scripts.Models.Towers.Filters;

namespace TeslaTower
{
    public class BiggerTesla : ModUpgrade<TeslaTower>
    {
        public override string Name => "Bigger Tesla";
        public override string Description => "Increases the range and damage of the Tesla Tower.";
        public override int Cost => 750;
        public override int Path => TOP;
        public override int Tier => 1;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 10;

            var attackModel = towerModel.GetBehavior<AttackModel>();
            attackModel.weapons[0].projectile.GetDamageModel().damage += 2;
        }
    }

    public class PowerfulStrikes : ModUpgrade<TeslaTower>
    {
        public override string Name => "Powerful Strikes";
        public override string Description => "Further increases the range and damage of the Tesla Tower.";
        public override int Cost => 1500;
        public override int Path => TOP;
        public override int Tier => 2;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 15;

            var attackModel = towerModel.GetBehavior<AttackModel>();
            attackModel.weapons[0].projectile.GetDamageModel().damage += 10;
        }
    }

    public class ElectrifyingSurge : ModUpgrade<TeslaTower>
    {
        public override string Name => "Electrifying Surge";
        public override string Description => "Increases attack speed and pierce of the Tesla Tower.";
        public override int Cost => 3500;
        public override int Path => TOP;
        public override int Tier => 3;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetBehavior<AttackModel>();

            attackModel.weapons[0].Rate *= 0.7f;
            attackModel.weapons[0].projectile.pierce += 10;
        }
    }

    public class IonosphericAmplifier : ModUpgrade<TeslaTower>
    {
        public override string Name => "Ionospheric Amplifier";
        public override string Description => "Applies burn damage to all non-MOAB-class bloons. Adds pierce and increases attack speed.";
        public override int Cost => 5000;
        public override int Path => TOP;
        public override int Tier => 4;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var weapon = attackModel.weapons[0];

            var burnEffect = new DamageOverTimeModel();
            burnEffect.name = "BurnEffect";
            burnEffect.damage = 1;
            burnEffect.interval = 1.0f;
            burnEffect.immuneBloonProperties = BloonProperties.Lead | BloonProperties.White;
            burnEffect.displayPath = null;
            burnEffect.displayLifetime = 1.0f;

            weapon.projectile.AddBehavior(burnEffect);

            weapon.projectile.pierce += 15;
            weapon.Rate *= 0.6f;
        }
    }

    public class TowerOfTheHeavens : ModUpgrade<TeslaTower>
    {
        public override string Name => "Tower of the Heavens";
        public override string Description => "Deals monstrous damage, now applies burn to all bloon types including MOABs, and additionally stuns all non-MOAB-class bloons.";
        public override int Cost => 60000;
        public override int Path => TOP;
        public override int Tier => 5;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var weapon = attackModel.weapons[0];

            var burnEffect = new DamageOverTimeModel();
            burnEffect.name = "HeavensBurnEffect";
            burnEffect.damage = 3;
            burnEffect.interval = 1.0f;
            burnEffect.immuneBloonProperties = BloonProperties.None;
            burnEffect.displayPath = null;
            burnEffect.displayLifetime = 2.0f;

            weapon.projectile.AddBehavior(burnEffect);

            var stunEffect = new SlowModel(
                name: "HeavensStunEffect",
                multiplier: 0.0f,
                lifespan: 1.0f,
                mutationId: "HeavensStunMutation",
                layers: 99999,
                overlayType: null,
                isUnique: false,
                dontRefreshDuration: false,
                effectModel: null,
                cascadeMutators: false,
                removeMutatorIfNotMatching: false,
                countGlueAchievement: false,
                glueLevel: 0,
                matchLayersWithDamage: false
            );
            weapon.projectile.AddBehavior(stunEffect);

            weapon.projectile.pierce += 40;
            weapon.projectile.GetDamageModel().damage += 50;
        }
    }

    public class EnhancedOptics : ModUpgrade<TeslaTower>
    {
        public override string Name => "Enhanced Optics";
        public override string Description => "Allows the Tesla Tower to detect and hit camo bloons.";
        public override int Cost => 500;
        public override int Path => MIDDLE;
        public override int Tier => 1;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetection", true));
        }
    }

    public class AmplifiedCurrent : ModUpgrade<TeslaTower>
    {
        public override string Name => "Amplified Current";
        public override string Description => "Increases the Tesla Tower’s range and attack frequency. Bloons hit by the Tesla Tower are de-camoed.";
        public override int Cost => 1000;
        public override int Path => MIDDLE;
        public override int Tier => 2;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range *= 1.2f;
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].Rate *= 0.8f;  

            attackModel.weapons[0].projectile.AddBehavior(new RemoveBloonModifiersModel(
                name: "RemoveCamoOnHit",
                cleanseRegen: false,
                cleanseCamo: true,
                cleanseLead: false,
                cleanseFortified: false,
                cleanseOnlyIfDamaged: false,
                bloonTagExcludeList: null,
                bloonTagExplicitList: null
            ));
        }
    }

    public class StaticField : ModUpgrade<TeslaTower>
    {
        public override string Name => "Static Field";
        public override string Description => "Creates an electrical field that stuns non-MOAB-class bloons if they remain inside for longer than 0.5 seconds.";
        public override int Cost => 3000;
        public override int Path => MIDDLE;
        public override int Tier => 3;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            // Create a SlowModel that stuns bloons for 1 second after 0.5 seconds in the range
            var stunEffect = new SlowModel(
                name: "StaticFieldStun",
                lifespan: 1.0f,  // Stun lasts for 1 second
                multiplier: 0.0f,  // Full stop (stun)
                mutationId: "StaticFieldMutation",
                layers: 99999,  // Applies to all layers
                overlayType: null,
                isUnique: false,
                dontRefreshDuration: false,
                effectModel: null,
                cascadeMutators: false,
                removeMutatorIfNotMatching: false,
                countGlueAchievement: false,
                glueLevel: 0,
                matchLayersWithDamage: false
            );

            // Add the behavior to the Tesla Tower to apply the effect in its range
            towerModel.AddBehavior(stunEffect);
        }
    }
}