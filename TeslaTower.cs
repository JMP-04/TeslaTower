using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using TeslaTower;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;

[assembly: MelonInfo(typeof(TeslaTower.TeslaTowerMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TeslaTower
{
    public class TeslaTowerMod : BloonsTD6Mod
    {
        public override void OnApplicationStart()
        {
            ModHelper.Msg<TeslaTowerMod>("TeslaTower mod loaded!");
        }
    }

    public class TeslaTower : ModTower
    {
        public override string BaseTower => TowerType.TackShooter;
        public override int Cost => 500;
        public override TowerSet TowerSet => TowerSet.Magic;
        public override int TopPathUpgrades => 5;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 5;
        public override string DisplayName => "Tesla Tower";
        public override string Description => "A tower that uses electricity to destroy Bloons.";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.range = 30;
            var attackModel = towerModel.GetBehavior<AttackModel>();
            attackModel.weapons[0].projectile.pierce = 5;
        }
    }
}