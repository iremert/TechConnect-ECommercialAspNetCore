﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.DtoUI.TagDtos
{
    public class UpdateTagDto
    {
         public string ID { get; set; }
        public string TagName { get; set; }
        public string Url { get; set; }
        public bool Status { get; set; }
    }
}
