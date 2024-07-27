using BepInEx;
using HarmonyLib;
using Utilla;

namespace GorillaInvisibility
{
    [ModdedGamemode, BepInDependency("org.legoandmars.gorillatag.utilla")]
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;

        public static bool IsAllowed => Instance && Instance.enabled && Instance.InModdedRoom;

        public bool InModdedRoom;

        public void Awake()
        {
            Instance = this;
            Harmony.CreateAndPatchAll(typeof(Plugin).Assembly, Constants.GUID);
        }

        public void OnEnable()
        {

        }

        public void OnDisable()
        {

        }

        [ModdedGamemodeJoin]
        public void OnModdedJoin()
        {
            InModdedRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnModdedLeave()
        {
            InModdedRoom = false;
        }
    }
}
