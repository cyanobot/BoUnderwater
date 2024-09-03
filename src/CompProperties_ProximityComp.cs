using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace BoUnderwater
{
    public class CompProperties_ProximityComp : CompProperties
    {
        public float ProximityTriggerDistance = 10f;
    }

    public class ProximityComp : ThingComp
    {
        private bool IsThingInProximity = false;
        public CompProperties_ProximityComp Props => (CompProperties_ProximityComp)props;

        public override void CompTick()
        {
            base.CompTick();
            if (parent.IsHashIntervalTick(250))
            {
                IEnumerable<Pawn> PawnsInRange = GetPawnsInRange(this.parent.Position, this.parent.MapHeld, Props.ProximityTriggerDistance);

                Log.Message($"Things near {PawnsInRange.Count()}"); 
                if (PawnsInRange.Any())
                {
                    if (!IsThingInProximity)
                    {
                        OnProximityEnter(PawnsInRange.First());  
                    }
                }
                else if (IsThingInProximity)
                {
                    OnProximityLeave();
                }
            }
        }

        public virtual void OnProximityEnter(Thing Thing)
        {
            IsThingInProximity = true;
        }

        public virtual void OnProximityLeave()
        {
            IsThingInProximity = false;
        }

        private IEnumerable<Pawn> GetPawnsInRange(IntVec3 Center, Map Map, float Radius)
        {
            return GenRadial.RadialCellsAround(Center, Radius, true)
                .SelectMany(C => C.GetThingList(Map))
                .OfType<Pawn>();
        }

        public override string CompInspectStringExtra()
        {
            return base.CompInspectStringExtra() + (IsThingInProximity ? "something in proximity" : "nothing near");
        }
    }

    public class CompProperties_Proximity_ShowMote : CompProperties_ProximityComp
    {
        //public float ProximityTriggerDistance = 1f;
        public EffecterDef EffectorToShow;

        public CompProperties_Proximity_ShowMote()
        {
            compClass = typeof(Proximity_ShowMoteComp);
        }
    }

    public class Proximity_ShowMoteComp : ProximityComp
    {
        public new CompProperties_Proximity_ShowMote Props => (CompProperties_Proximity_ShowMote)props;

        private Effecter EffectorInstance;

        public override void OnProximityEnter(Thing Thing)
        {
            base.OnProximityEnter(Thing);

            CompGlower compGlower = this.parent.TryGetComp<CompGlower>();

            if (compGlower != null)
            {
                compGlower.GlowColor = ColorInt.FromHdrColor(Color.red);
            }


            if (Props.EffectorToShow != null)
            {

                if (EffectorInstance != null)
                {
                    EffectorInstance.Cleanup();
                    EffectorInstance = null;
                }

                EffectorInstance = Props.EffectorToShow.SpawnMaintained(this.parent.Position, this.parent.MapHeld);

            }
        }

        public override void OnProximityLeave()
        {
            base.OnProximityLeave();

            if (EffectorInstance != null)
            {
                EffectorInstance.Cleanup();
                EffectorInstance = null;
            }


            CompGlower compGlower = this.parent.TryGetComp<CompGlower>();

            if (compGlower != null)
            {
                compGlower.GlowColor = ColorInt.FromHdrColor(Color.green);
            }

        }

        public override void CompTick()
        {
            base.CompTick();

            if (EffectorInstance != null)
            {
                EffectorInstance.EffectTick(parent, parent);
            }
        }
    }
}
