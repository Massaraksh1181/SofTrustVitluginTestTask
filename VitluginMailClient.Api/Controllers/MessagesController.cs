using VitluginMailClientApi.Dto;
using VitluginMailClientDomain;
using VitluginMailClientDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace VitluginMailClientApi.Controllers
{
    public class MessagesController : ControllerBaseApi
    {
        public MessagesController(AppDbContext context) : base(context)
        {
        }

        [HttpPost("[action]")]
        public IActionResult Create(CreateMessageDto input)
        {

            if (string.IsNullOrEmpty(input.Name) ||
               string.IsNullOrEmpty(input.Email) ||
               string.IsNullOrEmpty(input.Phone) ||
               input.TopicId <= 0 ||
               string.IsNullOrEmpty(input.Text))
            return BadRequest("Не все поля заполнены.");

            if (!Regex.IsMatch(input.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return BadRequest("Неверный формат электронной почты.");

            if (!Regex.IsMatch(input.Phone, @"^(\+\d{1,3}[- ]?)?\d{10}$"))
                return BadRequest("Неверный номер телефона.");

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
