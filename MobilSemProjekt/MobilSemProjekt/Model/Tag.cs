﻿using System.Collections.Generic;

namespace MobilSemProjekt.Model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public List<Location> Locations { get; set; }
    }
}
