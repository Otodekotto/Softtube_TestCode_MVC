using Newtonsoft.Json;

namespace Softtube_TestCode_MVC.Models
{
    public class ProductViewModel
    {
        public int TotalResults { get; set; }
        public List<ProductItem> Result { get; set; }

        public class ProductItem
        {
            public string Name { get; set; }
            public Images Images { get; set; }
        }

        public class Images
        {
            [JsonProperty("240w")] // Map the JSON property "240w" to this C# property
            public string W240 { get; set; }
        }
    }
}
