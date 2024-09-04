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

                //Log.Message($"Things near {PawnsInRange.Count()}"); 
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

        protected IEnumerable<Pawn> GetPawnsInRange(IntVec3 Center, Map Map, float Radius)
        {
            return GenRadial.RadialCellsAround(Center, Radius, true)
                .Where(C => C.InBounds(Map)) 
                .SelectMany(C => C.GetThingList(Map))
                .OfType<Pawn>();
        }

        public override string CompInspectStringExtra()
        {
            return base.CompInspectStringExtra() + (IsThingInProximity ? "something in proximity" : "nothing near");
        }
    }

}
