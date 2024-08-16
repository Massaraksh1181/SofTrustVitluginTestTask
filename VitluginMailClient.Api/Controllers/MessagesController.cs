using MailClientTestApi.Dto;
using MailClientTestDomain;
using MailClientTestDomain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MailClientTestApi.Controllers
{
    public class MessagesController : ControllerBaseApi
    {
        public MessagesController(AppDbContext context) : base(context)
        {
        }

        [HttpPost("[action]")]
        public IActionResult Create(CreateMessageDto input)
        {
            var contact = Context.Contacts.FirstOrDefault(c => c.Email == input.Email && c.Phone == input.Phone);
            if (contact == null)
            {
                contact = new Contact
                {
                    Name = input.Name,
                    Email = input.Email,
                    Phone = input.Phone,
                };

                Context.Contacts.Add(contact);
                Context.SaveChanges();
            }

            var message = new Message
            {
                ContactId = contact.Id,
                Text = input.Text,
                TopicId = input.TopicId,
            };

            Context.Messages.Add(message);
            Context.SaveChanges();
            return Created(nameof(Create), message);
        }

        [HttpGet("[action]/{messageId}")]
        public IActionResult GetMessageDetails(int messageId)
        {
            var messageDetails = Context.Messages
                .Where(m => m.Id == messageId)
                .Select(m => new
                {
                    MessageText = m.Text,
                    TopicName = m.Topic.Name,
                    ContactName = m.Contact.Name
                })
                .FirstOrDefault();

            if (messageDetails == null)
            {
                return NotFound();
            }

            return Ok(messageDetails);
        }
    }
}
