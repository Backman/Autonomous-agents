using SharpSteer2.WinDemo.PlugIns.Ctf;

namespace SharpSteer2.WinDemo.PlugIns.Arrival
{
    public class ArrivalPlugIn
        : CtfPlugIn
    {
        public override string Name
        {
            get { return "Arrival"; }
        }

        public ArrivalPlugIn(IAnnotationService annotations)
            :base(annotations, 0, true, 0.5f, 100)
        {
        }
    }
}
