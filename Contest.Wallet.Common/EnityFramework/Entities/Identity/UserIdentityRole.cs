using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consent.Common.EnityFramework.Entities.Identity
{
    public class UserIdentityRole : IdentityRole
	{
        public UserIdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public virtual string TenantId { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }


        [ForeignKey("RoleId")]
        public virtual ICollection<UserRolePermission> UserRolePermission { get; set; }
    }
}
