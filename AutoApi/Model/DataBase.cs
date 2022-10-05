/*
using NewJsonCrud.Models.CrudClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Linq.Expressions;

namespace NewJsonCrud.Models.CrudClass
{
    public static class DBs
    {
        public static string DBStore;
        public static string CreateTable;
        public static string database = Directory.GetCurrentDirectory();
        public static string counterval = "0";

        public static dynamic Create()
        {
            string newdb = $@"{database}\{DBStore}\";
            bool check = false;
            if (!Directory.Exists(newdb))
            {
                Directory.CreateDirectory(newdb);
            }
            if (!System.IO.File.Exists($@"{newdb}TableInfo.json"))
            {
                System.IO.File.Create($@"{newdb}TableInfo.json").Dispose();
            }
            var TableInfo = File.ReadAllLines($@"{newdb}TableInfo.json").ToList();
            for (int i = 0; i < TableInfo.Count; i++)
            {
                if (TableInfo[i] == CreateTable)
                {
                    check = true;
                    break;
                }
            }
            if (check == false)
            {
                if (!Directory.Exists($@"{newdb}{CreateTable}"))
                {
                    Directory.CreateDirectory($@"{newdb}{CreateTable}");
                }
                if (!File.Exists($@"{newdb}{CreateTable}\{CreateTable}.json"))
                {
                    File.Create($@"{newdb}{CreateTable}\{CreateTable}.json").Dispose();
                }
                if (!System.IO.File.Exists($@"{newdb}{CreateTable}\JKey.json"))
                {
                    System.IO.File.Create($@"{newdb}{CreateTable}\JKey.json").Dispose();
                }
                if (!System.IO.File.Exists($@"{newdb}{CreateTable}\Function.json"))
                {
                    System.IO.File.Create($@"{newdb}{CreateTable}\Function.json").Dispose();
                }
                if (!System.IO.File.Exists($@"{newdb}{CreateTable}\Property.json"))
                {
                    System.IO.File.Create($@"{newdb}{CreateTable}\Property.json").Dispose();
                }
                TableInfo.Insert(0, $@"{CreateTable}");
                File.WriteAllLines($@"{newdb}TableInfo.json", TableInfo.ToArray());
            }
            var line = File.ReadAllLines($@"{newdb}{CreateTable}\{CreateTable}.json").ToList();
            if (line.Count() <= 0)
            {
                System.IO.File.WriteAllText($@"{newdb}{CreateTable}\{CreateTable}.json", "[\n]");
            }
            return "gh";
        }
        public static dynamic Post(object u)
        {
            string Main = @"D:\JsonCrud\NewJsonCrud\NewJsonCrud\Models\CrudClass\DataBase.cs";
            var property = File.ReadAllLines($@"{database}\{DBStore}\{CreateTable}\Property.json").ToList();
            if (u != null)
            {
                if (property.Count() <= 0)
                {
                    foreach (var prop in u.GetType().GetProperties())
                    {
                        if (property.Count <= 0)
                        {
                            property.Insert(0, $@"if (json[i].{prop.Name} == numericValue)");
                        }
                        property.Insert(0, $@"json[i].{prop.Name} = u.{prop.Name};");
                        File.WriteAllLines($@"{database}\{DBStore}\{CreateTable}\Property.json", property.ToArray());
                    }
                }
            }
            string Path = $@"{database}\{DBStore}\{CreateTable}\{CreateTable}.json";
            string Key = $@"{database}\{DBStore}\{CreateTable}\JKey.json";
            string lineContents = ReadSpecificLine(2);
            var f = File.ReadAllLines(Path).ToList();
            var ja = JsonConvert.SerializeObject(u, Formatting.Indented);

            f.Insert(1, "," + ja);
            System.IO.File.WriteAllLines(Path, f);
            return "successs";
        }
        public static dynamic Get()
        {
            string Path = $@"{database}\{DBStore}\{CreateTable}\{CreateTable}.json";
            string Key = $@"{database}\{DBStore}\{CreateTable}\JKey.json";
            string Function = $@"{database}\{DBStore}\{CreateTable}\Function.json";
            string Main = @"D:\JsonCrud\NewJsonCrud\NewJsonCrud\Models\CrudClass\DataBase.cs";
            string lineContents = ReadSpecificLine(1);
            dynamic data = System.IO.File.ReadAllText(Path);
            var json = JsonConvert.DeserializeObject<List<User>>(data);

            if (json != null)
            {
                for (int i = 0; i < json.Count; i++)
                {

                    User u = json[i];

                }
            }
            return json;
        }
        public static dynamic FindById(string id)
        {
            string Path = $@"{database}\{DBStore}\{CreateTable}\{CreateTable}.json";
            string Key = $@"{database}\{DBStore}\{CreateTable}\JKey.json";
            string cs = @"D:\JsonCrud\NewJsonCrud\NewJsonCrud\Models\CrudClass\DataBase.cs";
            var property = File.ReadAllLines($@"{database}\{DBStore}\{CreateTable}\Property.json").ToList();
            List<dynamic> objectnames = new List<dynamic>();
            dynamic data = System.IO.File.ReadAllText(Path);
            ReadSpecificLine(1);


            var json = JsonConvert.DeserializeObject<List<User>>(data);

            int numericValue;
            bool isNumber = int.TryParse(id, out numericValue);
            if (isNumber == true)
            {
                if (json != null)
                {

                    List<string> list = new List<string>();
                    for (int k = 0; k < property.Count; k++)
                    {

                        list.Add(property[k]);

                    }


                    for (int i = 0; i < json.Count; i++)
                    {

                        if (json[i].U_id == numericValue)

                        {
                            objectnames.Add(json[i]);
                            break;
                        }
                    }
                }
            }
            return objectnames;
        }
        public static dynamic Update(User u, string id)
        {
            string Path = $@"{database}\{DBStore}\{CreateTable}\{CreateTable}.json";
            string Key = $@"{database}\{DBStore}\{CreateTable}\JKey.json";

            string mpath = @"D:\JsonCrud\NewJsonCrud\NewJsonCrud\Models\CrudClass\DataBase.cs";


            var property = File.ReadAllLines($@"{database}\{DBStore}\{CreateTable}\Property.json").ToList();
            dynamic data = System.IO.File.ReadAllText(Path);
            ReadSpecificLine(1);
            var json = JsonConvert.DeserializeObject<List<User>>(data);
            int numericValue;
            bool isNumber = int.TryParse(id, out numericValue);
            if (isNumber == true)
            {
                if (json != null)
                {
                    for (int i = 0; i < json.Count; i++)
                    {
                        if (json[i].U_id == numericValue)
                        {

                            json[i].U_Disignation = u.U_Disignation;
                            json[i].U_Salary = u.U_Salary;
                            json[i].U_Age = u.U_Age;
                            json[i].U_Email = u.U_Email;
                            json[i].U_Name = u.U_Name;
                            string output = Newtonsoft.Json.JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented);
                            System.IO.File.WriteAllText(Path, output);
                            return json[i];

                        }
                    }
                }
            }
            else
            {
                return "invalid id";
            }

            return "hey";
        }

        public static dynamic JKey()
        {

            string Path = $@"{database}\{DBStore}\{CreateTable}\{CreateTable}.json";
            string Key = $@"{database}\{DBStore}\{CreateTable}\JKey.json";
            var line = File.ReadAllLines(Key).ToList();
            if (line.Count <= 0)
            {
                System.IO.File.WriteAllText(Key, counterval);
            }
            var Inc = File.ReadAllLines(Key).ToList();
            var value = int.Parse(Inc[0]);
            var data = value + 1;
            *//*Inc.Insert(1, data.ToString());*//*
            System.IO.File.WriteAllText(Key, data.ToString());
            return data;
        }
        public static dynamic ReadSpecificLine(int count)
        {
            string Path = $@"{database}\{DBStore}\{CreateTable}\{CreateTable}.json";
            string Key = $@"{database}\{DBStore}\{CreateTable}\JKey.json";
            if (count == 2)
            {
                var linesList = File.ReadAllLines(Path).ToList();
                if (linesList.Count > 6)
                {
                    linesList.RemoveAt(1);
                    linesList.Insert(1, ",{");
                    File.WriteAllLines(Path, linesList.ToArray());
                }
            }
            else
            {
                var linesList = File.ReadAllLines(Path).ToList();
                if (linesList.Count > 6)
                {
                    linesList.RemoveAt(1);
                    linesList.Insert(1, "{");
                    File.WriteAllLines(Path, linesList.ToArray());
                }
            }
            return "success";
        }
    }
}

*/