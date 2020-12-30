using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;

namespace Example
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class ExamplePlugin : BasePlugin
    {
        public const string Id = "gg.reactor.Example";

        public Harmony Harmony { get; } = new Harmony(Id);

        public ConfigEntry<string> Name { get; private set; }

        public override void Load()
        {
            Name = Config.Bind("Fake", "Name", ":>");

            Harmony.PatchAll();
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
        public static class ExamplePatch
        {
            public static void Postfix(PlayerControl __instance)
            {
                __instance.nameText.Text = PluginSingleton<ExamplePlugin>.Instance.Name.Value;
            }
        }
    }
}
