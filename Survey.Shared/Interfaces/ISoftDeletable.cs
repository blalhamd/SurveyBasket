

namespace Survey.Shared.Interfaces
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedAt { get; set; }
        public int? DeletedByUserId { get; set; }

        //public Task Delete()
        //{
        //    IsDeleted = true;
        //    DeletedAt = DateTime.Now;

        //    return Task.CompletedTask;
        //}

        //public Task UndoDelete()
        //{
        //    IsDeleted = false;
        //    DeletedAt = null;

        //    return Task.CompletedTask;
        //}
    }
}
