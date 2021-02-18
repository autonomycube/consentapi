using Consent.Common.Repository.SQL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consent.Common.EnityFramework.Entities.Identity
{
    public class UserPermissions : EntityBase<string>
    {
        public UserPermissions()
        {
            Id = Guid.NewGuid().ToString();
        }

        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }
        public virtual string GroupName { get; set; }
        public virtual bool? IsActive { get; set; }

        [ForeignKey("PermissionId")]
        public virtual ICollection<UserRolePermission> UserRolePermission { get; set; }
    }
}
