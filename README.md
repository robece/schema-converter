# Schema Converter

SchemaConverter it's an small tool project that helps to convert event files from Event Grid schema to Cloud Events schema or vice versa.

The project it's build using .NET 6.0 LTS and the McMaster.Extensions packages for CLI purposes.

## Parameters

| Attribute        | Shortname | Longname          | Detail                     |
|------------------|-----------|-------------------|----------------------------|
| Direction        | -d        | -direction        | eg2ce \| ce2eg             |
| File Path        | -f        | -file-path        | Full file path             |
| Output Directory | -o        | -output-directory | Full output directory path |

eg2ce: from Event Grid schema to Cloud Events schema
ce2eg: from Cloud Events schema to Event Grid schema

## Sample command

./SchemaConverter -d eg2ce -f "C:\Users\robece\Desktop\source\blob_created.json" -o "C:\Users\robece\Desktop\output"

## License
The MIT License (MIT) Copyright © 2022 Roberto Cervantes
