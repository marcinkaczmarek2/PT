namespace Data.API
{
    public interface IBook
    {
        public int id { get; set; }
        public string title { set; get; }
        public string publisher { set; get; }
        public string author { set; get; }
        public int numberOfPages { set; get; }
        public string genre { set; get; }
    }
}
