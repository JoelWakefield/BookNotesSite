﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookNotesSite.Models
{
    public class BlobInfo
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }
    }
}