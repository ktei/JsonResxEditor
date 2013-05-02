using Caliburn.Micro;
using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Rui.JsonResxEditor
{
    public class MefBootstrapper : Bootstrapper<IShell>
    {
        private CompositionContainer container;

        protected override void Configure()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            container = new CompositionContainer(catalog);
            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);

            container.Compose(batch);
            MefSupport.OnSatisfyImports = target => container.ComposeParts(target);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            CheckDatabase();
            base.OnStartup(sender, e);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            IoC.Get<IShell>().PersistPreference();
            base.OnExit(sender, e);
        }

        void CheckDatabase()
        {
            if (!Directory.Exists(Settings.DataFolderPath))
            {
                Directory.CreateDirectory(Settings.DataFolderPath);
            }
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.CreateTable<Project>();
                db.CreateTable<Source>();
                db.CreateTable<Item>();
                db.CreateTable<Translation>();
                db.CreateTable<Preference>();
            }
        }
    }
}
