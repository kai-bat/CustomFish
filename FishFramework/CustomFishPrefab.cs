using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMLHelper.V2.Assets;
using UnityEngine;

namespace FishFramework
{
    public class CustomFishPrefab : ModPrefab
    {
        public AssetBundle bundle;
        public float scale;
        public bool pickupable;

        public CustomFishPrefab(AssetBundle modelbundle, string classId, string prefabFileName, float scale, bool pickupable, TechType techType = TechType.None) : base(classId, prefabFileName, techType)
        {
            bundle = modelbundle;
            this.scale = scale;
            this.pickupable = pickupable;
            TechType = techType;
        }

        public override GameObject GetGameObject()
        {
            Console.WriteLine("[FishFramework] Getting object from asset bundle");
            GameObject mainObj = bundle.LoadAsset<GameObject>(PrefabFileName);

            mainObj.AddComponent<ScaleFixer>().scale = scale;

            Console.WriteLine("[FishFramework] Setting correct shaders on renderers");
            Renderer[] renderers = mainObj.GetComponentsInChildren<Renderer>();
            foreach(Renderer rend in renderers)
            {
                rend.material.shader = Shader.Find("MarmosetUBER");
            }

            Console.WriteLine("[FishFramework] Adding essential components to object");
            WorldForces forces = mainObj.AddOrGet<WorldForces>();

            forces.useRigidbody = mainObj.AddOrGet<Rigidbody>();
            forces.aboveWaterDrag = 0f;
            forces.aboveWaterGravity = 9.81f;
            forces.handleDrag = true;
            forces.handleGravity = true;
            forces.underwaterDrag = 0.5f;
            forces.underwaterGravity = 0;

            mainObj.AddOrGet<SkyApplier>().renderers = renderers;
            mainObj.AddOrGet<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
            mainObj.AddOrGet<LiveMixin>();

            Creature creature = mainObj.AddOrGet<Creature>();

            mainObj.AddOrGet<TechTag>().type = TechType;

            Console.WriteLine("[FishFramework] Adding pickupable component");
            if (pickupable)
            {
                mainObj.AddOrGet<Pickupable>();
            }

            return mainObj;
        }
    }
}
