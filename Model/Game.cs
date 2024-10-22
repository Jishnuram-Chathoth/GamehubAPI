namespace GamehubAPI.Model
{
    public class Game
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int StockQty { get; set; }

    }
}
