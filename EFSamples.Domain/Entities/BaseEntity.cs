﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSamples.Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public bool IsRemoved { get; set; }
    }
}