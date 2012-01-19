using System;
using System.Configuration;
using Microsoft.Win32;

namespace Dragon
{
    internal static class ConnectionStringHelper
    {
        public static string GetConnectionString(string name)
        {
            Guard.ArgumentNotNullOrEmptyString(name, "GetConnectionString(string >>> name <<<)");
            string lowername = name.ToLower();
            if (lowername.IndexOf("server=") >= 0 || lowername.IndexOf("data source=") >= 0)
            {
                return name;
            }
            else if (lowername.IndexOf("\\") > 0)
            {
                return GetConnectionStringFromRegistry(name);
            }
            else
            {
                return GetConnectionStringFromConfigFile(name);
            }
        }

        public static string GetConnectionStringFromConfigFile(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string GetConnectionStringFromRegistry(string registryPath)
        {
            Guard.ArgumentNotNullOrEmptyString(registryPath, "registry path");
            int pos = registryPath.LastIndexOf('\\');

            string path = registryPath.Substring(0, pos);
            string name = registryPath.Substring(pos + 1);
            return GetRegKeyValue(path, name);
        }

        private static string GetRegKeyValue(string key, string name)
        {
            RegistryKey regkey = Registry.LocalMachine;
            using (regkey = regkey.OpenSubKey(key))
            {
                var conn = regkey.GetValue(name);
                return conn == null ? null : conn.ToString();
            }
        }
    }
}
