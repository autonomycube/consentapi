using Consent.Common.Repository.SQL.Models;
using System;

namespace Consent.Common.EnityFramework.Entities.Identity
{
    public class UserRolePermission : EntityBase<string>
    {
        public UserRolePermission()
        {
            Id = Guid.NewGuid().ToString();
        }

        public virtual string RoleId { get; set; }
        public virtual string PermissionId { get; set; }
        public virtual bool? IsActive { get; set; }

        public virtual UserPermissions UserPermissions { get; set; }
    }
}
