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
        public virtual TEntityKey Id { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
    }
}
