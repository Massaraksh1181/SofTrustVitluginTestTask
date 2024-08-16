using MailClientTestDomain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailClientTestApi.Dto
{
    public class CreateMessageDto
    {
        public required string Text { get; set; }
        public required int TopicId { get; set; }

        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
