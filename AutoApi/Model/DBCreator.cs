using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace AutoApi.Model
{
    public static class DB
    {
        public static string DBStore;
        public static string DBTable;
        public static string DataBases = $@"{Directory.GetCurrentDirectory()}\DataBase";
        public static string Store = $@"{DataBases}\{DBStore}";
        public static class Database
        {
            public static void Create(string Database)
            {

                if (!Directory.Exists(DB.DataBases))
                {
                    Directory.CreateDirectory(DB.DataBases);
                    System.IO.File.Create($@"{DB.DataBases}\DataBasesInfo.json").Dispose();
                    var DataBaseInfo = File.ReadAllLines($@"{DB.DataBases}\DataBasesInfo.json").ToList();
                    DataBaseInfo.Insert(0, "[\n]");
                    File.WriteAllLines($@"{DB.DataBases}\DataBasesInfo.json", DataBaseInfo.ToArray());

                }
                
                /*var DatabaseinfoUpdate = File.ReadAllLines($@"{DB.DataBases}\DataBasesInfo.json").ToList();
                DatabaseinfoUpdate.RemoveAt(1);
                DatabaseinfoUpdate.Insert(1, "{");
                File.WriteAllLines($@"{DB.DataBases}\DataBasesInfo.json", DatabaseinfoUpdate.ToArray());*/


                if (!Directory.Exists($@"{DB.DataBases}\{Database}"))
                {
                    JObject obj = new JObject();
                    obj["DataBase"] = Database;
                    var Databaseinfo = File.ReadAllLines($@"{DB.DataBases}\DataBasesInfo.json").ToList();
                    string DBinfo = JsonConvert.SerializeObject(obj, Formatting.Indented);
                    Databaseinfo.Insert(1, "," + DBinfo);
                    File.WriteAllLines($@"{DB.DataBases}\DataBasesInfo.json", Databaseinfo.ToArray());

                    Directory.CreateDirectory($@"{DB.DataBases}\{Database}");
                    System.IO.File.Create($@"{DB.DataBases}\{Database}\Store.json").Dispose();
                    var StoreInfo = File.ReadAllLines($@"{DB.DataBases}\{Database}\Store.json").ToList();
                    StoreInfo.Insert(0, "[\n]");
                    File.WriteAllLines($@"{DB.DataBases}\{Database}\Store.json", StoreInfo.ToArray());

                    //Table
                    if (!Directory.Exists($@"{DB.DataBases}\{Database}\Table"))
                    {
                        Directory.CreateDirectory($@"{DB.DataBases}\{Database}\Table");
                        System.IO.File.Create($@"{DB.DataBases}\{Database}\Table\Table.json").Dispose();
                        var TableInfo = File.ReadAllLines($@"{DB.DataBases}\{Database}\Table\Table.json").ToList();
                        TableInfo.Insert(0, "[\n]");
                        File.WriteAllLines($@"{DB.DataBases}\{Database}\Table\Table.json", TableInfo.ToArray());
                    }
                    //Model
                    if (!Directory.Exists($@"{DB.DataBases}\{Database}\Model"))
                    {
                        Directory.CreateDirectory($@"{DB.DataBases}\{Database}\Model");
                        System.IO.File.Create($@"{DB.DataBases}\{Database}\Model\Model.json").Dispose();
                        var ModelInfo = File.ReadAllLines($@"{DB.DataBases}\{Database}\Model\Model.json").ToList();
                        ModelInfo.Insert(0, "[\n]");
                        File.WriteAllLines($@"{DB.DataBases}\{Database}\Model\Model.json", ModelInfo.ToArray());

                    }
                    //Controller
                    if (!Directory.Exists($@"{DB.DataBases}\{Database}\Controller"))
                    {
                        Directory.CreateDirectory($@"{DB.DataBases}\{Database}\Controller");
                        System.IO.File.Create($@"{DB.DataBases}\{Database}\Controller\Controller.json").Dispose();
                        var ControllerInfo = File.ReadAllLines($@"{DB.DataBases}\{Database}\Controller\Controller.json").ToList();
                        ControllerInfo.Insert(0, "[\n]");
                        File.WriteAllLines($@"{DB.DataBases}\{Database}\Controller\Controller.json", ControllerInfo.ToArray());

                    }
                    //Function
                    if (!Directory.Exists($@"{DB.DataBases}\{Database}\Function"))
                    {
                        Directory.CreateDirectory($@"{DB.DataBases}\{Database}\Function");
                        File.Create($@"{DB.DataBases}\{Database}\Function\Function.json").Dispose();
                        var FunctionInfo = File.ReadAllLines($@"{DB.DataBases}\{Database}\Function\Function.json").ToList();
                        FunctionInfo.Insert(0, "[\n]");
                        File.WriteAllLines($@"{DB.DataBases}\{Database}\Function\Function.json", FunctionInfo.ToArray());
                    }

                    
                }
              
            }
        }
        public static class Model
        {
            public static dynamic Create(string ModelName)
            {
               string path = $@"{DB.DataBases}\{DB.DBStore}\Model\";
                if (!File.Exists($@"{path}{ModelName}.cs"))
                {
                    File.Create($@"{path}{ModelName}.cs").Dispose();
                }
                var list = File.ReadAllLines($@"{path}{ModelName}.cs").ToList();
                list.Insert(0, $@"namespace NewJsonCrud.DataBase.{DB.DBStore}.Model" + "\n"
+ "{"
+ "\n"
+ $"public class {ModelName}" + "\n"
+ "{"
+ "\n"
+ "\n"
+ "}"
+ "\n"
+ "}");

                File.WriteAllLines($@"{path}{ModelName}.cs", list);

                DB.Controller.Create(ModelName);
                DB.Tables.Create(ModelName);
                return "create successfully";

            }
            public static dynamic AddProperty(string ModelName,string type, string name)
            {
                //AddProperty
                string path = $@"{DB.DataBases}\{DB.DBStore}\Model\";
                var Fileread = File.ReadAllLines($@"{path}{ModelName}.cs").ToList();
                Fileread.Insert(4, $@"public {type} {name}" + "{get; set; }");
                File.WriteAllLines($@"{path}{ModelName}.cs", Fileread.ToArray());
                JObject obj = new JObject();
                obj["User"] = DB.DBStore;
                obj["Model"] = ModelName;
                obj["Property"] = $@"public {type} {name}" + "{get; set; }";
                var json = JsonConvert.SerializeObject(obj,Formatting.Indented);
                var ModelJson = File.ReadAllLines($@"{DB.DataBases}\{DB.DBStore}\Model\Model.json").ToList();
                ModelJson.Insert(1,json);
                File.WriteAllLines($@"{DB.DataBases}\{DB.DBStore}\Model\Model.json", ModelJson);

                // Add Property on JsonFile
                return "Property";
            }
        }
        public static class Controller
        {
            public static dynamic Create(string ControllerName)
            {
                string path = $@"{DB.DataBases}\{DB.DBStore}\Controller\";
                if (!File.Exists($@"{path}{ControllerName}Conroller.cs"))
                {
                    File.Create($@"{path}{ControllerName}Conroller.cs").Dispose();
                }
                var list = File.ReadAllLines($@"{path}{ControllerName}Conroller.cs").ToList();
                list.Insert(0, $@"using Microsoft.AspNetCore.Http;"
 + "\n"
 + "using Microsoft.AspNetCore.Mvc;"
 + "\n"
 + $@"using NewJsonCrud.DataBase.{DB.DBStore}.Model;"
 + "\n\n"
 + $@"namespace NewJsonCrud.DataBase.{DB.DBStore}.Controller"
 + "\n"
 + "{"
 + "\n\n"
 + "[Route" + "(" + $@"""api/{DB.DBStore}/[controller]""" + ")" + "]"
 + "\n"
 + "[ApiController]"
 + "\n"
 + $@"public class {ControllerName}Controller : ControllerBase"
 + "\n"
 + "{"
 + "\n"
 + @"[HttpPost(""created"")]"
+"\n"
        +$@"public dynamic hey({ControllerName} u)"
                +"{"
                +"\n"
                +"\n"
                    + $@"return ""dfd"";"
                    +"\n"
               +"}" 
                
 + "\n"
 + "}"
 + "\n"
 + "}"
 );
                File.WriteAllLines($@"{path}{ControllerName}Conroller.cs", list);
                return "success";

            }
        }
        public static class Tables
        {
            public static dynamic Create(string TableName)
            {
                string path = $@"{DB.DataBases}\{DB.DBStore}\Table\";
                if (!File.Exists($@"{path}{TableName}.json"))
                {
                    File.Create($@"{path}{TableName}.json").Dispose();
                    var Table = File.ReadAllLines($@"{path}{TableName}.json").ToList();
                    Table.Insert(0, "[\n]");
                    File.WriteAllLines($@"{path}{TableName}.json", Table.ToArray());
                    JObject obj = new JObject();
                    obj["TableName"] = TableName;
                    var TblJson = JsonConvert.SerializeObject(obj, Formatting.Indented);
                    var pathread = $@"{DB.DataBases}\{DB.DBStore}\Table\Table.json";

                    var pathlist = File.ReadAllLines($@"{pathread}").ToList();
                    pathlist.Insert(1,","+TblJson);
                    File.WriteAllLines(pathread,pathlist.ToArray());
                    

                }

                return "fdf";
            }
        }
    }
}
