﻿using System.Text.Json.Serialization;

namespace SellBroCRMWPF
{
    public class Item
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return "Id - " + Id + " Value - " + Value;
        }
    }
}