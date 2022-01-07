using Luluco.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Luluco.MVVM.ViewModel
{
    class TemplateModel: ObservableObject
    {
        private ObservableCollection<LulucoPair> _templateDict = new ObservableCollection<LulucoPair>();
        public ObservableCollection<LulucoPair> TemplateDict => _templateDict;


        public void LoadData()
        {
            string filePath = LulucoPath.TemplateConfigPath;
            if (!File.Exists(filePath))
            {
                return;
            }
            _templateDict.Clear();
            StreamReader streamReaderMain = new StreamReader(filePath, Encoding.UTF8);
            string jsonText = streamReaderMain.ReadToEnd();
            JsonTextReader reader = new JsonTextReader(new StringReader(jsonText));
            JObject jObject = JObject.Load(reader);

            foreach (var item in jObject)
            {
                _templateDict.Add(new LulucoPair { Key = $"{item.Key}", Value = $"{item.Value}" });
            }
        }

    }
}
