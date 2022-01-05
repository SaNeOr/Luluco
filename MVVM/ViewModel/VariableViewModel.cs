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

    class VariableViewModel : ObservableObject
    {
        private ObservableCollection<LulucoDict> _variableDict = new ObservableCollection<LulucoDict>();
        public ObservableCollection<LulucoDict> Variabledict => _variableDict;


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
            string filePath = PathManager.VarsConfigPath;

            StreamReader streamReaderMain = new StreamReader(filePath, Encoding.UTF8);
            string jsonText = streamReaderMain.ReadToEnd();
            JsonTextReader reader = new JsonTextReader(new StringReader(jsonText));
            JObject jObject = JObject.Load(reader);

            foreach (var item in jObject)
            {
                //_mydict[$"[{item.Key}]"] = item.Value.ToString();
                //_mydict[$"[{item.Key}]"] = item.Value.ToString();
                _variableDict.Add(new LulucoDict { Key = $"{item.Key}", Value = $"{item.Value}" });
            }
            //_vars = new Dictionary<string, string>();
            //_vars["abc"] = "123";
            //_vars["def"] = "456";
            //_vars["hit"] = "789";

            //_mydict = new ObservableCollection<MyCustomClass>();
            //_mydict.Add(new MyCustomClass { Key = "def", Value = "1234" });
        }
    }
}
