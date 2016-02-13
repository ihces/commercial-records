using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Storage;

namespace CommercialRecordSystem.DataLayer
{
    class ScriptLoader
    {
        private ScriptLoader()
        {
            Scripts = new SortedDictionary<string, string>();

            loadScripts();
        }

        private SortedDictionary<string, string> Scripts = null;

        private static ScriptLoader instance = null;

        public static ScriptLoader getInstance()
        {
            if (null == instance)
            {
                instance = new ScriptLoader();
            }

            return instance;
        }

        private async void loadScripts()
        {
            StorageFolder folder = await (
                await Package.Current.InstalledLocation.
                GetFolderAsync("DataLayer")).
                GetFolderAsync("Scripts");

            IReadOnlyList<StorageFile> allfiles =
                await folder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);

            XDocument loadedData = XDocument.Load(allfiles[0].Path);


            foreach (XElement scriptElement in loadedData.Descendants("script"))
            {
                Scripts.Add(scriptElement.Attribute("id").Value,
                    string.Join(" ", scriptElement.Value.Split('\n').Select(s => s.Trim())));
            }
        }

        internal string getScript(string query, object[] args)
        {
            string scriptBuff = null;
            if (Scripts.ContainsKey(query))
            {
                scriptBuff = string.Format(Scripts[query], args);
            }

            return scriptBuff;
        }
    }
}
