using Consent.Common.Repository.Mongo.Models.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Consent.Common.Repository.Mongo.Models
{
    public class EntityBase : IEntity
    {
        public EntityBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; }

        [Required]
        [BsonElement("created_by")]
        public string CreatedBy { get; set; }

        [Required]
        [BsonElement("created_date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [BsonElement("updated_by")]
        public string UpdatedBy { get; set; }

        [Required]
        [BsonElement("updated_date")]
        public DateTime UpdatedDate { get; set; }
    }
}