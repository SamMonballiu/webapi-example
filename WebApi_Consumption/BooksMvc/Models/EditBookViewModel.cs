using DataLayer.Models;
using System.Web.Mvc;

namespace BooksMvc.Models
{
    public class EditBookViewModel
    {
        public Book SelectedBook { get; set; }
        public SelectList AvailableAuthors { get; set; }

        public int SelectedAuthorId { get; set; }
    }
}