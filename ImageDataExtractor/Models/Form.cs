﻿using System.Collections.Generic;

namespace ImageDataExtractor.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Field> Fields { get; set; }
    }
}
