using System.Collections.Generic;

namespace ImageDataExtractor.Models
{
    public class FieldOptionLists
    {
        public string Value { get; set; }
        public FieldOptionLists? CascadingFieldOptions { get; set; }
        public List<FieldOptionValidations>? FieldOptionValidations { get; set; }
    }
}
