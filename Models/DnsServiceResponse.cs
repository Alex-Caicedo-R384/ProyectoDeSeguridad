namespace ProyectoDeSeguridad.Models
{
    public class DnsServiceResponse
    {

        public class Rootobject
        {
            public string status { get; set; }
            public string hostname { get; set; }
            public Records records { get; set; }
        }

        public class Records
        {
            public A[] A { get; set; }
            public object[] CNAME { get; set; }
            public MX[] MX { get; set; }
            public N[] NS { get; set; }
            public SOA[] SOA { get; set; }
            public string[] TXT { get; set; }
        }

        public class A
        {
            public string address { get; set; }
            public int ttl { get; set; }
        }

        public class MX
        {
            public string exchange { get; set; }
            public int priority { get; set; }
        }

        public class N
        {
            public string nameserver { get; set; }
        }

        public class SOA
        {
            public string nameserver { get; set; }
            public string hostmaster { get; set; }
        }
    }
}
