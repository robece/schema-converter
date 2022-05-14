using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace SchemaConverter.Commands
{
    internal class ConvertCommand
    {
        enum ConvertResultEnum
        {
            Success,
            Failed
        }

        public Program Parent { get; set; } = default!;

        [Option(ShortName = "d", LongName = "direction")]
        [Required]
        public string Direction { get; set; } = String.Empty;

        [Option(ShortName = "f", LongName = "file-path")]
        [Required]
        public string FilePath { get; set; } = String.Empty;

        [Option(ShortName = "o", LongName = "output-directory")]
        [Required]
        public string OutputDirectory { get; set; } = String.Empty;

        private int OnExecuteAsync(IConsole console, CommandLineApplication app)
        {
            try
            {
                var result = string.Empty;

                if (!File.Exists(FilePath))
                {
                    console.WriteLine("File not found.", ConsoleColor.Yellow);
                }

                if (!Directory.Exists(OutputDirectory))
                {
                    console.WriteLine("Directory not found.", ConsoleColor.Yellow);
                }

                var name = Path.GetFileName(FilePath);
                var fullname = Path.Combine(OutputDirectory, name);

                switch (Direction.ToLower())
                {
                    case "eg2ce":
                        ISchemaConverter schemaConverterEGToCE = new EGToCEConverter();
                        schemaConverterEGToCE.Convert(console, FilePath, OutputDirectory);
                        break;
                    case "ce2eg":
                        ISchemaConverter schemaConverterCEToEG = new CEToEGConverter();
                        schemaConverterCEToEG.Convert(console, FilePath, OutputDirectory);
                        break;
                    default:
                        console.WriteLine("Direction not supported.", ConsoleColor.Yellow);
                        break;
                }
            }
            catch (JsonException ex)
            {
                console.WriteLine(ex.Message, ConsoleColor.Red);
                return (int)ConvertResultEnum.Failed;
            }
            catch (Exception ex)
            {
                console.WriteLine(ex.Message, ConsoleColor.Red);
                return (int)ConvertResultEnum.Failed;
            }

            return (int)ConvertResultEnum.Success;
        }
    }
}
