namespace NotesAPI___with_Repository_Pattern_and_Dtos.Models
{
    public class Note
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

    }
}
