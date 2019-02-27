using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using System.Reflection;

namespace FishFramework
{
    public class HarmonyInitalizer
    {
        public static void HarmonyInit()
        {
            HarmonyInstance harm = HarmonyInstance.Create("customfish");
            harm.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
