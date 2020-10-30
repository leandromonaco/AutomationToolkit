﻿using System.Collections.Generic;

namespace AutomationToolkit.SonaType.Model
{
    public class SonaTypeComponentScanResult
    {
        public string Coordinates { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public List<SonaTypeVulnerability> Vulnerabilities { get; set; }
    }
}