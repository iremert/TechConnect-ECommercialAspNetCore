﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.DtoUI.ContactDtos
{
    public class GetByIdContactDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
