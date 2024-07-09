namespace ProyectoDeSeguridad.Models
{
    public class WebCategorizationResponse
    {
        public class Rootobject
        {
            public As _as { get; set; }
            public string domainName { get; set; }
            public Category[] categories { get; set; }
            public DateTime createdDate { get; set; }
            public bool websiteResponded { get; set; }
        }

        public class As
        {
            public int asn { get; set; }
            public string domain { get; set; }
            public string name { get; set; }
            public string route { get; set; }
            public string type { get; set; }
        }

        public class Category
        {
            public float confidence { get; set; }
            public int id { get; set; }
            public string name { get; set; }
        }


    }
}
