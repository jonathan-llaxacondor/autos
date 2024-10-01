namespace GARP.Transversal.Common
{
    public class RZGeneralResponse<T> : RZGeneralStatus
    {
        public IEnumerable<T> Data { get; set; }
    }
}
