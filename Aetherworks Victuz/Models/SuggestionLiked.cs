namespace Aetherworks_Victuz.Models
{
    public class SuggestionLiked
    {
        public int Id { get; set; }
        public int SuggestionId { get; set; }
        public Suggestion? Suggestion { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
