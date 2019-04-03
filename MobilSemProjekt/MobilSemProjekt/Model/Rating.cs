namespace MobilSemProjekt.Model
{
    public class Rating
    {
        public int RatingId { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
    }
}