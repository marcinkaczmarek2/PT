namespace ModelViewModel.Model.API
{
    public interface IBookModelData
    {
        int id { get; set; }
        string title { get; set; }
        string publisher { get; set; }
        string author { get; set; }
        int numberOfPages { get; set; }
        string genre { get; set; }
    }
}
