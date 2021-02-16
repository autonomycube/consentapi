using Consent.Common.Repository.SQL.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consent.Common.Repository.SQL.Models
{
    public class EntityBase<TEntityKey> : IEntity<TEntityKey>
    {
        [Key]
        [Required]
        public TEntityKey Id { get; set; }

        public TEntityKey TenantId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
