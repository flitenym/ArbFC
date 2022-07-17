﻿using System.ComponentModel.DataAnnotations;

namespace Storage.Module.Entities
{
    public class Ticker
    {
        [Key]
        public long Id { get; set; }
        public string ToAsset { get; set; }
        public string FromAsset { get; set; }
    }
}