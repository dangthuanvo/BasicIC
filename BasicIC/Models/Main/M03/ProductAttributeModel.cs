﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class ProductAttributeModel : BaseModel
    {
        [Required]
        public Guid attribute_id { get; set; }
        [Required]
        public Guid product_id { get; set; }
        public string attribute_value { get; set; }
    }
}