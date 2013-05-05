using Newtonsoft.Json;
using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;

namespace Rui.JsonResxEditor.Services.Sqlite
{
    [Export(typeof(IProjectService))]
    public class ProjectService : IProjectService
    {
        public void Save(Project model)
        {
            if (model.Id == 0)
            {
                Insert(model);
            }
            else
            {
                Update(model);
            }
        }


        public IEnumerable<Project> FindAll()
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                var enu = db.Table<Project>().GetEnumerator();
                while (enu.MoveNext())
                {
                    yield return enu.Current;
                }
            }
        }


        public IEnumerable<Project> FindAllExcept(int projectId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.Query<Project>("select * from Project where Id <> ?", projectId);
            }
        }


        public void Delete(int projectId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Execute("delete from Translation where ProjectId = ?", projectId);
                db.Execute("delete from Item where ProjectId = ?", projectId);
                db.Execute("delete from Source where ProjectId = ?", projectId);
                db.Execute("delete from Project where Id = ?", projectId);
            }
        }


        public Project Get(int projectId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.Find<Project>(projectId);
            }
        }

        private void Insert(Project model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Insert(model);
            }
        }

        private void Update(Project model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Update(model);
            }
        }


        public bool HasProject()
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.ExecuteScalar<int>("select count(Id) from Project") > 0;
            }
        }


        public void WriteToFile(string directory, int projectId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                var sources = db.Query<Source>("select Id, Name from Source where ProjectId = ?", projectId);
                var locales = db.Query<Locale>("select Code from Locale where ProjectId = ?", projectId);
                foreach (var loc in locales)
                {
                    string dir = Path.Combine(directory, loc.Code);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    
                    foreach (var src in sources)
                    {
                        using (var tw = File.CreateText(Path.Combine(dir,  src.Name + ".json")))
                        {
                            using (var jw = new JsonTextWriter(tw))
                            {
                                jw.Formatting = Formatting.Indented;
                                var translations = db.Query<Translation>(@"select i.Token, i.Text as ItemText, t.Text
                                    from Item i left outer join Translation t on (i.Id = t.ItemId and t.Locale = ?)
                                    where i.SourceId = ?", loc.Code, src.Id);
                                jw.WriteStartObject();
                                translations.ForEach(x =>
                                    {
                                        jw.WritePropertyName(x.Token);
                                        jw.WriteValue(string.IsNullOrEmpty(x.Text) ? x.ItemText : x.Text);
                                    });
                                jw.WriteEndObject();
                                jw.Flush();
                            }
                        }
                    }
                }
            }
        }

        private class Locale
        {
            public string Code { get; set; }
        }

        private class Source
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class Translation
        {
            public string Token { get; set; }
            public string ItemText { get; set; }
            public string Text { get; set; }
        }
    }
}
