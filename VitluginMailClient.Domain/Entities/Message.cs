using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitluginMailClientDomain.Entities
{
    public class Message : Entity
    {
        public required string Text { get; set; }

        public required int TopicId { get; set; }
        [ForeignKey(nameof(TopicId))]
        public Topic? Topic { get; set; }

        public required int ContactId { get; set; }
        [ForeignKey(nameof(ContactId))]
        public Contact? Contact { get; set; }
    }
}
