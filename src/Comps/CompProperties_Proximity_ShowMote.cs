using UnityEngine;
using Verse;

namespace BoUnderwater
{
    public class CompProperties_Proximity_ShowMote : CompProperties_ProximityComp
    {
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
