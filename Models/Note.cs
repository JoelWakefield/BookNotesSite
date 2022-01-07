using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookNotesSite.Models
{
    public class Note
    {
        public string Text { get; set; }
        public string Heading { get; set; }
        public bool BoldHeading { get; set; }
        public List<Note> Notes { get; set; }
    }
}
