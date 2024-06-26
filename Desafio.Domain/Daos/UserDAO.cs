﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Daos
{
    public class UserDAO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime DateRegistered { get; set; }

        public string UserRegistered { get; set; }

        public string RealName { get; set; }
    }
}
