using MelonLoader;
using BTD_Mod_Helper;
using TeslaTower;

[assembly: MelonInfo(typeof(TeslaTowerMod), "Tesla Tower", "1.0.0", "YourName")]
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
}
