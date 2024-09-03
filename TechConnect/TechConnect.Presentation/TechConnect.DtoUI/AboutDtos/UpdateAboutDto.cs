﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.DtoUI.AboutDtos
{
    public class UpdateAboutDto
    {
        public string ID { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public bool Status { get; set; }
    }
}
