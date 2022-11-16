using System.Collections.Generic;

namespace CodingEventsDemo.Models
{
    public class EventCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Event> Events { get; set; }  //a collection of Event objects --> an EventCategory may be related to multiple Event objects
        //For categories to be aware of the events that they relate to, we must add an Event collection property to EventCategory

        public EventCategory()
        {
        }

        public EventCategory(string name)
        {
            Name = name;
        }
    }
}
