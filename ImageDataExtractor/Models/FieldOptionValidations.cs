namespace ImageDataExtractor.Models
{
    public class FieldOptionValidations
    {
        public bool Required { get; set; } = false;
        public int? MaxLength { get; set; }
        public bool IsNumbric { get; set; } = false;
        public string? CascadingFieldOptionsValue { get; set; }
        public int? FieldId { get; set; }
    }
}
