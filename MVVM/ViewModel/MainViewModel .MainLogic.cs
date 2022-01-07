using Luluco.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace Luluco.MVVM.ViewModel
{
    partial class MainViewModel
    {

        #region Luluco Main Logic
        Dictionary<string, string> Vars = new Dictionary<string, string>();
        Dictionary<string, string> Templates = new Dictionary<string, string>();

        string TargetExcelPath = "";
        string TemplateExcelPath = "";


        public void Apply()
        {
            LoadVars();
            LoadTemplate();

            if (Luluco())
            {

                MessageBox.Show($"Success!");
            }
            else
            {
                MessageBox.Show($"Error!");
            }

        }


        public void Save()
        {

            JObject templateConfigJobject = new JObject();

            foreach (var item in TemplateVM.TemplateDict)
            {
                JProperty jProperty = new JProperty(item.Key, item.Value.ToString());
                templateConfigJobject.Add(jProperty);
            }

            JObject variableConfigJobject = new JObject();

            foreach (var item in VariableVM.Variabledict)
            {
                JProperty jProperty = new JProperty(item.Key, item.Value.ToString());
                variableConfigJobject.Add(jProperty);
            }


            File.WriteAllText(LulucoPath.TemplateConfigPath, templateConfigJobject.ToString());
            File.WriteAllText(LulucoPath.VarsConfigPath, variableConfigJobject.ToString());
        }


        void LoadVars()
        {
            foreach (var item in VariableVM.Variabledict)
            {
                Vars[$"[{item.Key}]"] = item.Value.ToString();
            }
        }

        void LoadTemplate()
        {
            foreach (var item in TemplateVM.TemplateDict)
            {
                if (!item.IsDisable)
                {
                    Templates[item.Key] = item.Value.ToString();
                }
            }

            //TargetExcelPath = Templates["TargetExcelPath"];
            //TemplateExcelPath = Templates["TemplateExcelPath"];

            TargetExcelPath = Templates["TargetExcelPath"] = LulucoPath.ExportDirPath;
            TemplateExcelPath = Templates["TemplateExcelPath"] = LulucoPath.LulucoConfiDirgPath;
        }




        bool Luluco()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            foreach (var temp in Templates)
            {
                if (temp.Key == "TargetExcelPath") continue;
                if (temp.Key == "TemplateExcelPath") continue;

                string templatePath = TemplateExcelPath + "/" + temp.Value;

                if (!File.Exists(templatePath))
                {
                    Console.WriteLine($"Error: {templatePath} Not Exists!");
                    continue;
                }

                var file = new FileInfo(templatePath);


                if (!temp.Key.Contains('.'))
                {
                    //  gen sub-excel
                    var heroID = Vars["[HeroID]"];
                    var subExcelPath = $"{TargetExcelPath}/{temp.Key}/{temp.Key}_{heroID}.xlsx";
                    using (var subExcel = new ExcelPackage(file))
                    {
                        var ws = subExcel.Workbook.Worksheets[0];
                        ws.Name = $"{temp.Key}_{heroID}";
                        HandleSheet(ws);
                        subExcel.SaveAs(subExcelPath);
                        Console.WriteLine($"{subExcelPath} Save Sucess!\n");

                    }
                    continue;
                }

                var excelName = temp.Key.Split('.')[0];
                var sheetName = temp.Key.Split('.')[1];


                using (var excel = new ExcelPackage(file))
                {
                    var ws = excel.Workbook.Worksheets[0];
                    int RowLength = ws.Dimension.End.Row;

                    int ColumLength = ws.Dimension.End.Column;
                    HandleSheet(ws);

                    var targetExcelPath = TargetExcelPath + "/" + excelName + ".xlsx";

                    if (!File.Exists(targetExcelPath))
                    {
                        Console.WriteLine($"Error: {targetExcelPath} Not Exists!");
                        continue;
                    }

                    var targetFile = new FileInfo(targetExcelPath);
                    var targetExcel = new ExcelPackage(targetFile);

                    int insertRowCount = RowLength - 4 + 1;
                    var targetWs = targetExcel.Workbook.Worksheets[sheetName];

                    int startRow = 4;
                    int targetRowLength = targetWs.Dimension.End.Row;

                    //bool find = false;
                    var insertBefore = Vars["[InsertBefore]"];

                    for (int r = 4; r <= targetRowLength; r++)
                    {

                        startRow = r;
                        var cell = targetWs.Cells[r, 1];
                        if (cell == null || cell.Value == null) continue;
                        var cellStringValue = cell.Value.ToString();
                        if (cellStringValue == null) continue;

                        if (cellStringValue.StartsWith(insertBefore))
                        {
                            //startRow = r;
                            break;
                        }
                    }
                    //startRow++;
                    targetWs.InsertRow(startRow, insertRowCount);

                    ws.Cells[4, 1, RowLength, ColumLength].Copy(targetWs.Cells[startRow, 1, 6, ColumLength]);
                    //await targetExcel.SaveAsync();
                    targetExcel.Save();
                    Console.WriteLine($"{targetExcelPath} Save Sucess!\n");
                }
            }

            return true;
        }


        void HandleSheet(ExcelWorksheet sheet)
        {
            var ws = sheet;

            int RowLength = ws.Dimension.End.Row;
            int ColumLength = ws.Dimension.End.Column;

            for (int r = 4; r <= RowLength; r++)
            {
                for (int c = 1; c <= ColumLength; c++)
                {
                    var cell = ws.Cells[r, c];
                    if (cell == null || cell.Value == null) continue;
                    var cellStringValue = cell.Value.ToString();
                    if (cellStringValue == null) continue;
                    cellStringValue = ReplaceVar(cellStringValue);
                    cell.Value = cellStringValue;
                }
            }
        }

        string ReplaceVar(string cellValue)
        {
            foreach (var v in Vars)
            {
                if (cellValue.Contains(v.Key))
                {
                    cellValue = cellValue.Replace(v.Key, v.Value);
                }
            }
            return cellValue;
        }

        #endregion
    }
}
