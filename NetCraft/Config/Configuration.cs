using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace NetCraft.Config
{
    public class Configuration
    {
        private Dictionary<string, string> _values;
        private string _filePath;

        public Configuration(string path)
        {
            _values = new Dictionary<string, string>();
            _filePath = path;
        }

        public void Load()
        {
            var fileContent = File.ReadAllText(_filePath);
            _values = JsonConvert.DeserializeObject<Dictionary<string, string>>(fileContent);
        }

        public void Save()
        {
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(_values, Formatting.Indented));
        }

        public string GetValue(string key, string defaultValue)
        {
            if (_values.ContainsKey(key))
                return _values[key];
            _values.Add(key, defaultValue);
            return defaultValue;
        }

        public void SetValue(string key, string value)
        {
            if (_values.ContainsKey(key))
                _values[key] = value;
            else
                _values.Add(key, value);
        }
    }
}
