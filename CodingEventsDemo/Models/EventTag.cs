namespace CodingEventsDemo.Models
{
    public class EventTag
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public EventTag()
        {
        }

        //no separate ID column as a primary key
        //since this is a join table, we want the primary Id to be a pair consisting of EventId and TagId
    }
}
