using Luluco.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Data;

namespace Luluco.MVVM.ViewModel
{

    class VariableViewModel : ObservableObject
    {
        private ObservableCollection<LulucoPair> _variableDict = new ObservableCollection<LulucoPair>();
        public ObservableCollection<LulucoPair> Variabledict => _variableDict;


        //private Dictionary<string, string> _vars = new Dictionary<string, string>();
        //public Dictionary<string, string> Vars
        //{
        //    get
        //    {
        //        return _vars;
        //    }
        //    set
        //    {
        //        _vars = value;
        //        OnPropertyChanged();
        //    }
        //}

        public void LoadData()
        {
            string filePath = LulucoPath.VarsConfigPath;
            if(!File.Exists(filePath))
            {
                return;
            }

            StreamReader streamReaderMain = new StreamReader(filePath, Encoding.UTF8);
            string jsonText = streamReaderMain.ReadToEnd();
            JsonTextReader reader = new JsonTextReader(new StringReader(jsonText));
            JObject jObject = JObject.Load(reader);

            _variableDict.Clear();
            foreach (var item in jObject)
            {
                //_mydict[$"[{item.Key}]"] = item.Value.ToString();
                //_mydict[$"[{item.Key}]"] = item.Value.ToString();
                _variableDict.Add(new LulucoPair { Key = $"{item.Key}", Value = $"{item.Value}" });
            }

            //_vars = new Dictionary<string, string>();
            //_vars["abc"] = "123";
            //_vars["def"] = "456";
            //_vars["hit"] = "789";

            //_mydict = new ObservableCollection<MyCustomClass>();
            //_mydict.Add(new MyCustomClass { Key = "def", Value = "1234" });

            //OnPropertyChanged();
        }
    }
}
