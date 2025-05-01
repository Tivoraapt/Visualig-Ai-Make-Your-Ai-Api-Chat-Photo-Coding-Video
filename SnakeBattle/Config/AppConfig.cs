using System.Collections.Generic;

namespace OnAirLight.CommandLine.Config
{
    public class AppConfig
    {
        public Azure Azure { get; set; }
        public Hubitat Hubitat { get; set; }
    }

    public class Azure
    {
        public string AppId { get; set; }
        public string TenantId { get; set; }
        public string Scopes { get; set; }
        public string UserId { get; set; }
        public string[] AllScopes => Scopes.Split(';');
    }

    public class Hubitat
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string NameOrIp { get; set; }
        public string DeviceName { get; set; }
        public Dictionary<string, string> StatusMap { get; set; }
    }
}
