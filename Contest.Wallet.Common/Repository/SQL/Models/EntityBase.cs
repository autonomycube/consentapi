using Contest.Wallet.Common.Repository.SQL.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contest.Wallet.Common.Repository.SQL.Models
{
    public class EntityBase<TEntityKey> : IEntity<TEntityKey>
    {
        [Key]
        [Required]
        [Column("id")]
        public TEntityKey Id { get; set; }

        [Key]
        [Column("tenant_id")]
        public TEntityKey TenantId { get; set; }

        [Required]
        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        [Required]
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }
    }
}
