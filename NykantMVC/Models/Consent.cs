﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NykantMVC.Models
{
    public class Consent
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public ConsentType Type { get; set; }
        public string ConsentText { get; set; }
        public ConsentHow How { get; set; }
        public string ButtonText { get; set; }
        public DateTime Date { get; set; }
        public ConsentStatus Status { get; set; }
    }
    public enum ConsentType
    {
        Cookie,
        TermsAndConditions,
        PrivacyPolicy,
        Newsletter
    }
    public enum ConsentHow
    {
        Button,
        Checkbox
    }
    public enum ConsentStatus
    {
        Given,
        Retrieved
    }
}
