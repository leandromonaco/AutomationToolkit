﻿using System.Collections.Generic;

namespace Octopus.Repository.Model
{
    public class OctopusScope
    {
        public List<string> Environment { get; set; }
        public List<string> Machine { get; set; }
        public List<string> Action { get; set; }
        public List<string> Role { get; set; }
        public List<string> Channel { get; set; }
    }
}