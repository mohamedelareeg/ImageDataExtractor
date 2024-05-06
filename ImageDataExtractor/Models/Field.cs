using ImageDataExtractor.Enums;
using System.Collections.Generic;

namespace ImageDataExtractor.Models
{
    public class Field
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool Required { get; set; }
        public string InputValue { get; set; }
        public FieldType FieldType { get; set; }
        public int? CascadingFieldId { get; set; }
        public bool Unique { get; set; } = false;
        public List<FieldOptionLists>? FieldOptionLists { get; set; }
    }
}
