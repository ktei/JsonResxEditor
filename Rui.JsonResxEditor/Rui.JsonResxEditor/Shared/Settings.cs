using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Shared
{
    public static class Settings
    {
        public static readonly string DataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData,
            Environment.SpecialFolderOption.Create), "JsonResxEditor");

        public static readonly string DatabasePath = Path.Combine(DataFolderPath, "data.jrxe");
    }
}
