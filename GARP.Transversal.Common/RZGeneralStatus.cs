namespace GARP.Transversal.Common
{
    public class RZGeneralStatus
    {
        public bool Ok { get; set; }
        public string Mensaje { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public RZGeneralStatus()
        {
            Ok = false;
        }
    }
}
