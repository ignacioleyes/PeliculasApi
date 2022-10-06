namespace PeliculasApi.DTOs
{
    public class BaseFilter
    {
        public string Filters { get; set; }
        public Range Range { get; set; } = new Range() { Start = 0, End = 9 };
        public Sort Sort { get; set; }
    }
    public class FilterValue
    {
        public string Field { get; set; }
        public string Value { get; set; }
    }

    public class Range
    {
        public int Start { get; set; }
        public int End { get; set; }
    }

    public class Sort
    {
        public string Field { get; set; }
        public bool IsAscending { get; set; } = true;
    }
}
