using System;
using System.Collections.Generic;
using System.Text;

namespace TestDAL.Model
{
    public class Manager: User
    {
        public string Position { get; set; }

        public virtual IEnumerable<Client> Clients { get; set; }
    }
}
