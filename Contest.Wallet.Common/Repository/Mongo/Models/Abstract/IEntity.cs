using MongoDB.Bson;
using System;

namespace Contest.Wallet.Common.Repository.Mongo.Models.Abstract
{
    public interface IEntity
    {
        /// <summary>
        /// Unique id of the entity
        /// </summary>
        string Id { get; set; }
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
