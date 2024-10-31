namespace Survey.Entities.entities
{
    public class BaseEntity : IEntityCreationTime, IEntityCreatedByUser,IEntityModificationUser,ISoftDeletable
    {
        public int Id { get; set; }
        public DateTime CreationTime {get; set;}
        public int CreatedByUserId {get; set;}
        public int? ModificatedByUserId {get; set;}
        public DateTime? FirstModificationDate {get; set;}
        public DateTime? LastModificationDate {get; set;}
        public bool IsDeleted {get; set;}
        public DateTime? DeletedAt {get; set;}
        public int? DeletedByUserId { get; set; }

    }
}
