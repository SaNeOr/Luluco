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
        private ObservableCollection<LulucoDict> _templateDict = new ObservableCollection<LulucoDict>();
        public ObservableCollection<LulucoDict> TemplateDict => _templateDict;

        public void LoadData()
        {
            string filePath = PathManager.TemplateConfigPath;

            StreamReader streamReaderMain = new StreamReader(filePath, Encoding.UTF8);
            string jsonText = streamReaderMain.ReadToEnd();
            JsonTextReader reader = new JsonTextReader(new StringReader(jsonText));
            JObject jObject = JObject.Load(reader);

            foreach (var item in jObject)
            {
                _templateDict.Add(new LulucoDict { Key = $"{item.Key}", Value = $"{item.Value}" });
            }

        }

    }
}
