namespace FAQ.DAL.BaseModels
{
    public abstract class BaseInheritancable
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}