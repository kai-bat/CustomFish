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

        public float swimSpeed;
        public Vector3 swimRadius;

        public CustomFishPrefab(string classId, string prefabFileName, TechType techType = TechType.None) : base(classId, prefabFileName, techType)
        {
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

            Rigidbody rb = mainObj.AddOrGet<Rigidbody>();
            rb.useGravity = false;
            rb.angularDrag = 1f;

            WorldForces forces = mainObj.AddOrGet<WorldForces>();
            forces.useRigidbody = rb;
            forces.aboveWaterDrag = 0f;
            forces.aboveWaterGravity = 9.81f;
            forces.handleDrag = true;
            forces.handleGravity = true;
            forces.underwaterDrag = 1f;
            forces.underwaterGravity = 0;
            forces.waterDepth = Ocean.main.GetOceanLevel();
            forces.enabled = false;
            forces.enabled = true;

            mainObj.AddOrGet<EntityTag>().slotType = EntitySlot.Type.Creature;
            mainObj.AddOrGet<PrefabIdentifier>().ClassId = ClassID;
            mainObj.AddOrGet<TechTag>().type = TechType;

            mainObj.AddOrGet<SkyApplier>().renderers = renderers;
            mainObj.AddOrGet<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
            mainObj.AddOrGet<LiveMixin>().health = 10f;

            Creature creature = mainObj.AddOrGet<Creature>();
            creature.initialCuriosity = AnimationCurve.Linear(0f, 0.5f, 1f, 0.5f);
            creature.initialFriendliness = AnimationCurve.Linear(0f, 0.5f, 1f, 0.5f);
            creature.initialHunger = AnimationCurve.Linear(0f, 0.5f, 1f, 0.5f);
            SwimBehaviour behaviour = mainObj.AddOrGet<SwimBehaviour>();
            SwimRandom swim = mainObj.AddOrGet<SwimRandom>();
            swim.swimVelocity = swimSpeed;
            swim.swimRadius = swimRadius;
            swim.swimInterval = 1f;
            Locomotion loco = mainObj.AddOrGet<Locomotion>();
            loco.useRigidbody = rb;
            mainObj.AddOrGet<EcoTarget>().type = EcoTargetType.Peeper;
            mainObj.AddOrGet<CreatureUtils>();
            mainObj.AddOrGet<VFXSchoolFishRepulsor>();
            SplineFollowing spline = mainObj.AddOrGet<SplineFollowing>();
            spline.locomotion = loco;
            spline.levelOfDetail = mainObj.AddOrGet<BehaviourLOD>();
            spline.GoTo(mainObj.transform.position + mainObj.transform.forward, mainObj.transform.forward, 5f);
            behaviour.splineFollowing = spline;
            creature.ScanCreatureActions();

            Console.WriteLine("[FishFramework] Adding pickupable component");
            if (pickupable)
            {
                mainObj.AddOrGet<Pickupable>();
            }

            return mainObj;
        }
    }
}
