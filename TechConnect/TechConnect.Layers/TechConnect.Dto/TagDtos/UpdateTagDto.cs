﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.Dto.TagDtos
{
    public class UpdateTagDto
    {
        public string ID { get; set; }
        public string TagName { get; set; }
        public bool Status { get; set; }
    }
}