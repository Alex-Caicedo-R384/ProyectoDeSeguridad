namespace ProyectoDeSeguridad.Models
{
    public class DomainReputationResponse
    {
        public class Rootobject
        {
            public string mode { get; set; }
            public float reputationScore { get; set; }
            public Testresult[] testResults { get; set; }
        }

        public class Testresult
        {
            public string test { get; set; }
            public int testCode { get; set; }
            public Warning[] warnings { get; set; }
        }

        public class Warning
        {
            public string warningDescription { get; set; }
            public int warningCode { get; set; }
        }
    }
}
