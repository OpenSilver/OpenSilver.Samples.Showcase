using System;

namespace DotNetForHtml5.Showcase.SampleRestWebService.Models
{
    public class ToDoItem
    {
        public ToDoItem() { }

        public ToDoItem(string description, int ownerId)
        {
            Description = description;
        }

        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Description { get; set; }
    }
}