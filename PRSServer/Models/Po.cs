namespace PRSServer.Models
{
    public class Po
    {

        public Vendor vendor { get; set; }
        public IEnumerable<Poline> Polines { get; set; }
        public decimal PoTotal { get; set; }
    }
}
