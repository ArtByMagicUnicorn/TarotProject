using System.Text.Json.Serialization;

namespace Tarot.Data
{
    // DTO = Data Transfer Object. En enkel "container".
    public class MeaningSeedDto
    {
        // Denna "attribut" talar om för C# att
        // "cardName" i JSON-filen ska paras ihop med
        // 'CardName'-egenskapen i C#.
        [JsonPropertyName("cardName")]
        public string CardName { get; set; }

        [JsonPropertyName("upright")]
        public string Upright { get; set; }

        [JsonPropertyName("reversed")]
        public string Reversed { get; set; }
    }
}
