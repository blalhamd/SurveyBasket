namespace Survey.Shared.Interfaces
{
    public interface IEntityModificationUser
    {
        public int? ModificatedByUserId { get; set; }
        public DateTime? FirstModificationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }

    }
}
