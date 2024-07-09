namespace ProyectoDeSeguridad.Models
{
    public class SSLCertificateResponse
    {

        public class Rootobject
        {
            public string auditCreated { get; set; }
            public string domain { get; set; }
            public string ip { get; set; }
            public int port { get; set; }
            public Certificate[] certificates { get; set; }
        }

        public class Certificate
        {
            public string chainHierarchy { get; set; }
            public string validationType { get; set; }
            public string validFrom { get; set; }
            public string validTo { get; set; }
            public string serialNumber { get; set; }
            public string signatureAlgorithm { get; set; }
            public Subject subject { get; set; }
            public Issuer issuer { get; set; }
            public string pem { get; set; }
            public Extensions extensions { get; set; }
            public Publickey publicKey { get; set; }
        }

        public class Subject
        {
            public string country { get; set; }
            public string organization { get; set; }
            public string locality { get; set; }
            public string province { get; set; }
            public string commonName { get; set; }
        }

        public class Issuer
        {
            public string country { get; set; }
            public string organization { get; set; }
            public string commonName { get; set; }
        }

        public class Extensions
        {
            public string authorityKeyIdentifier { get; set; }
            public string subjectKeyIdentifier { get; set; }
            public string[] keyUsage { get; set; }
            public string[] extendedKeyUsage { get; set; }
            public string[] crlDistributionPoints { get; set; }
            public Authorityinfoaccess authorityInfoAccess { get; set; }
            public Subjectalternativenames subjectAlternativeNames { get; set; }
            public Certificatepolicy[] certificatePolicies { get; set; }
        }

        public class Authorityinfoaccess
        {
            public string[] issuers { get; set; }
            public string[] ocsp { get; set; }
        }

        public class Subjectalternativenames
        {
            public string[] dnsNames { get; set; }
        }

        public class Certificatepolicy
        {
            public string policyIdentifier { get; set; }
            public Policyqualifier[] policyQualifiers { get; set; }
        }

        public class Policyqualifier
        {
            public string policyQualifierId { get; set; }
            public string cpsUri { get; set; }
        }

        public class Publickey
        {
            public string type { get; set; }
            public int bits { get; set; }
            public string pem { get; set; }
        }

    }
}
