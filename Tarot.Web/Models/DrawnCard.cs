namespace Tarot.Web.Models 
{ 

    using Tarot.Data;

    public class DrawnCard
    {
        public TarotCard Card { get; set; }
        public bool IsReversed { get; set; }
    }
}
