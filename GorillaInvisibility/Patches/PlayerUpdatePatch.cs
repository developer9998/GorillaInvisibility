using HarmonyLib;

namespace GorillaInvisibility.Patches
{
    [HarmonyPatch(typeof(VRRig), "LateUpdate")]
    public class PlayerUpdatePatch
    {
        public static void Postfix(VRRig __instance)
        {
            if (__instance.isOfflineVRRig && Plugin.Instance)
            {
                bool canChangeHiddenState = !NetworkSystem.Instance.InRoom || Plugin.IsAllowed;

                bool isHiddenAllowed = canChangeHiddenState && __instance.mainSkin.enabled == Plugin.IsAllowed;

                if (isHiddenAllowed)
                {
                    __instance.SetPlayerMeshHidden(Plugin.IsAllowed);
                    return;
                }

                if (!Plugin.IsAllowed && __instance.mainSkin.enabled && !__instance.faceSkin.enabled)
                {
                    __instance.SetPlayerMeshHidden(false);
                }
            }
        }
    }
}
