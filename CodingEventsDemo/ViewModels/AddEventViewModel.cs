using System;
using System.ComponentModel.DataAnnotations;

namespace CodingEventsDemo.ViewModels
{
    public class AddEventViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description too long!")]
        public string Description { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Number of attendees is required.")]
        [Range(0, 100000, ErrorMessage = "Number of attendees must be between 0 and 100,000.")]
        public int NumberOfAttendees { get; set; }

        [Required]
        public bool IsRegistered { get; set; }

        [Compare(nameof(IsRegistered), ErrorMessage = "Please register before adding your event.")]
        public bool IsTrue { get { return true; } }

    }
}
