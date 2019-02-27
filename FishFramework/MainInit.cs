using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using System.Reflection;

namespace FishFramework
{
    public class MainInit
    {
        public static void Init()
        {
            HarmonyInstance harm = HarmonyInstance.Create("Kylinator25-FishFramework");
            harm.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
