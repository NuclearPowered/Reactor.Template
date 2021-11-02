using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;

namespace Reactor.Template
{
    [BepInAutoPlugin("me.change.please")]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public partial class TemplatePlugin : BasePlugin
    {
        public Harmony Harmony { get; } = new Harmony(Id);

        public ConfigEntry<string> ConfigName { get; private set; }

        public override void Load()
        {
            ConfigName = Config.Bind("Fake", "Name", ":>");

            Harmony.PatchAll();
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
        public static class ExamplePatch
        {
            public static void Postfix(PlayerControl __instance)
            {
                __instance.nameText.text = PluginSingleton<TemplatePlugin>.Instance.ConfigName.Value;
            }
        }
    }
}
