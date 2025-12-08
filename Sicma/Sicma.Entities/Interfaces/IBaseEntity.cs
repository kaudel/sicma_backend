namespace Sicma.Entities.Interfaces
{
    public interface IBaseEntity
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; } 

        public DateTime? UpdatedDate { get; set; }

        public string CreatedUserId { get; set; }
    }
}
