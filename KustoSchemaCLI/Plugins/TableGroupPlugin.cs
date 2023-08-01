using KustoSchemaTools.Helpers;
using KustoSchemaTools.Model;
using KustoSchemaTools.Plugins;

namespace KustoSchemaCLI.Plugins
{

    public class TableGroupPlugin : IYamlSchemaPlugin
    {
        public TableGroupPlugin(string subFolder = "table_groups")
        {
            SubFolder = subFolder;
        }

        public string SubFolder { get; }

        public async Task OnLoad(Database existingDatabase, string basePath)
        {
            var dict = existingDatabase.Tables;

            var path = Path.Combine(basePath, SubFolder);
            if (Directory.Exists(path) == false) return;
            var files = Directory.GetFiles(path, "*.yml");
            foreach (var filePath in files)
            {
                var tgFile = await File.ReadAllTextAsync(filePath);
                var tg = Serialization.YamlPascalCaseDeserializer.Deserialize<TableGroup>(tgFile);

                foreach (var entity in tg.AppliesTo)
                {
                    dict.Merge(tg.Table, entity);
                }
            }
        }


        public Task OnWrite(Database existingDatabase, string path)
        {
            return Task.CompletedTask;
        }
    }
}
