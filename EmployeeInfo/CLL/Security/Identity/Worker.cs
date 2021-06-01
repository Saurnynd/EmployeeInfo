using System;
using System.Collections.Generic;
using System.Text;

namespace CLL.Security.Identity
{
    public class Worker
    : User
    {
        public Worker(int userId, string name)
            : base(userId, name, nameof(Worker))
        {
        }
    }
}
