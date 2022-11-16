using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEventsDemo.Data;
using CodingEventsDemo.Models;
using CodingEventsDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coding_events_practice.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext context;

        public EventsController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Event> events = context.Events.Include(e => e.Category).ToList();
            //context.Events --> query the Events table
            //include references to the Category table (foreign key --> from Events into Categories)
            //then list all of those out
            return View(events);
        }

        public IActionResult Add()  //handles GET requests, renders the form
        {
            List<EventCategory> categories = context.Categories.ToList();  //query the database for all categories in the database

            AddEventViewModel addEventViewModel = new AddEventViewModel(context.Categories.ToList());  //and then pass them into the view model

            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)  //handles POST requests, processes the form
        {
            if (ModelState.IsValid)
            {                               //NOTE: DbContext is how we query the database
                EventCategory category = context.Categories.Find(addEventViewModel.CategoryId);
                //will look for the one specific EventCategory object that has the specific CategoryId listed. That ID will be in the form submission. Then that object will be pulled out by EntityFramework and put into local variable category. We can then use that local variable category to assign the Category for my newEvent object (below).

                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Category = category
                };

                //EventData.Add(newEvent);
                context.Events.Add(newEvent);

                context.SaveChanges();

                return Redirect("/Events");
            }

            return View(addEventViewModel);
        }

        public IActionResult Delete()
        {
            //ViewBag.events = EventData.GetAll();
            ViewBag.events = context.Events.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                //EventData.Remove(eventId);
                Event theEvent = context.Events.Find(eventId);
                context.Events.Remove(theEvent);
            }

            context.SaveChanges();

            return Redirect("/Events");
        }

        //Will handle events at route "/Events/Detail/x" (x is the ID of an event) (x is a path parameter)
        public IActionResult Detail(int id)
        {
            Event theEvent = context.Events.Include(e => e.Category).Single(e => e.Id == id);

            //we have a specific ID for the single event we want to display
            //can use that ID to go fetch it out of the database
            //fetches the single event from the database with the category entact
            //have to EagerLoad child objects of our persistant classes
            //context.Events = collection of all events in the database
            //.Include(lambda expression) = want to include the category child object
            //.Single() = filter out the one event that matches the boolean expression (single ID) from the entire collection

            //NOTE: Can't use .Find() with .Include() so have to use method chaining .Include().Single()


            List<EventTag> eventTags = context.EventTags
                .Where(et => et.EventId == id)
                .Include(et => et.Tag)
                .ToList();


            EventDetailViewModel viewModel = new EventDetailViewModel(theEvent, eventTags);
            return View(viewModel);
        }

    }
}
