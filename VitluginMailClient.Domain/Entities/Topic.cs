using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClientTestDomain.Entities
{
    public class Topic : Entity
    {
        public required string Name { get; set; }
    }
}
