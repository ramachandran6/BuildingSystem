﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.BS.Model
{
    public class WorkerDetails
    {
        [Key]
        public Guid Id { get; set; }
        public string BuildingName { get; set; }
        public int weight { get; set; }
    }
}
