using System;

namespace Consent.Common.Repository.SQL.Models.Abstract
{
    public interface IEntity<TEntityKey>
    {
        /// <summary>
        /// Unique id of the entity
        /// </summary>
        TEntityKey Id { get; set; }
        /// <summary>
        /// Entity created by
        /// </summary>
        string CreatedBy { get; set; }
        /// <summary>
        /// Entity created on
        /// </summary>
        DateTime CreatedDate { get; set; }
        /// <summary>
        /// Entity updated by
        /// </summary>
        string UpdatedBy { get; set; }
        /// <summary>
        /// Entity updated on
        /// </summary>
        DateTime UpdatedDate { get; set; }
    }
}
