using System;
using System.Collections.Generic;
using System.Text;

namespace TestDAL.Model
{
    public class Client : User
    {
        public int Level { get; set; }
        public virtual Manager Manager { get; set; }

    }
}
