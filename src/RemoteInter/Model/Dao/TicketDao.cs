using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RemoteInter.Enum;
using RemoteInter.Repository.RepositoryBase;
using RemoteInter.Repository.RepositoryBase.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RemoteInter.Model.Dao
{
    [BsonIgnoreExtraElements]
    [MongoCollection(Name = "ticket")]
    public class TicketDao : IDocument<ObjectId>
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public TicketType Type { get; set; }
        public TicketStatus Status { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Description { get; set; }

        public int? Severity { get; set; }
        public int? Priority { get; set; }
    }
}