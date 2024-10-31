namespace Survey.Shared.Interfaces
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedAt { get; set; }
        public int? DeletedByUserId { get; set; }

    }
}
