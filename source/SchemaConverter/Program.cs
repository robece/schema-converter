using Microsoft.Extensions.Hosting;
using SchemaConverter.Commands;

Host.CreateDefaultBuilder().RunCommandLineApplicationAsync<ConvertCommand>(args);

// Uncomment to test:
//using McMaster.Extensions.CommandLineUtils;
//using SchemaConverter;

//var FilePath = @"C:\Users\robece\Desktop\source\blob_created.json";
//var OutputDirectory = @"C:\Users\robece\Desktop\output";

//ISchemaConverter schemaConverterEGToCE = new CEToEGConverter();
//schemaConverterEGToCE.Convert(PhysicalConsole.Singleton, FilePath, OutputDirectory);
