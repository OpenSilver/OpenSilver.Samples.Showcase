using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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