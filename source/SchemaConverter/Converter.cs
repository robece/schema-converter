using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace SchemaConverter
{
    internal interface ISchemaConverter
    {
        void Convert(IConsole console, string filePath, string outputDirectory);
    }

    internal class EGToCEConverter : ISchemaConverter
    {
        public void Convert(IConsole console, string filePath, string outputDirectory)
        {
            var name = Path.GetFileName(filePath);
            var fullname = Path.Combine(outputDirectory, name);

            string content = File.ReadAllText(filePath);
            var token = JToken.Parse(content);

            if (token is JArray)
            {
                var eg_events = token.ToObject<List<ExpandoObject>>();
                var ce_events = new List<ExpandoObject>();

                if (eg_events != null)
                    if (eg_events.Count > 0)
                        foreach (dynamic e in eg_events)
                        {
                            dynamic cloudEvent = Map(e);
                            ce_events.Add(cloudEvent);
                        }

                string json = JsonConvert.SerializeObject(ce_events, Formatting.Indented);

                console.WriteLine(json, ConsoleColor.Green);

                File.WriteAllText(fullname, json);
            }
            else if (token is JObject)
            {
                var e = token.ToObject<ExpandoObject>();

                if (e != null)
                {
                    dynamic cloudEvent = Map(e);

                    string json = JsonConvert.SerializeObject(cloudEvent, Formatting.Indented);

                    console.WriteLine(json, ConsoleColor.Green);

                    File.WriteAllText(fullname, json);
                }
            }
        }

        private dynamic Map(dynamic e)
        {
            string topic = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("topic"))
                topic = e.topic;

            string subject = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("subject"))
                subject = e.subject;

            string eventType = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("eventType"))
                eventType = e.eventType;

            string eventTime = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("eventTime"))
                eventTime = e.eventTime.ToString("o");

            string id = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("id"))
                id = e.id;

            string dataVersion = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("dataVersion"))
                dataVersion = e.dataVersion;

            string metadataVersion = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("metadataVersion"))
                metadataVersion = e.metadataVersion;

            dynamic data = new ExpandoObject();
            if (((IDictionary<String, object>)e).ContainsKey("data"))
                data = e.data;

            dynamic cloudEvent = new ExpandoObject();
            cloudEvent.source = topic;
            cloudEvent.subject = subject;
            cloudEvent.type = eventType;
            cloudEvent.time = eventTime;
            cloudEvent.id = id;
            cloudEvent.data = data;
            cloudEvent.specVersion = "1.0";

            return cloudEvent;
        }
    }

    internal class CEToEGConverter : ISchemaConverter
    {
        public void Convert(IConsole console, string filePath, string outputDirectory)
        {
            var name = Path.GetFileName(filePath);
            var fullname = Path.Combine(outputDirectory, name);

            string content = File.ReadAllText(filePath);
            var token = JToken.Parse(content);

            if (token is JArray)
            {
                var ce_events = token.ToObject<List<ExpandoObject>>();
                var eg_events = new List<ExpandoObject>();

                if (ce_events != null)
                    if (ce_events.Count > 0)
                        foreach (dynamic e in ce_events)
                        {
                            dynamic eventGridEvent = Map(e);
                            eg_events.Add(eventGridEvent);
                        }

                string json = JsonConvert.SerializeObject(eg_events, Formatting.Indented);

                console.WriteLine(json, ConsoleColor.Green);

                File.WriteAllText(fullname, json);
            }
            else if (token is JObject)
            {
                var e = token.ToObject<ExpandoObject>();

                if (e != null)
                {
                    dynamic eventGridEvent = Map(e);

                    string json = JsonConvert.SerializeObject(eventGridEvent, Formatting.Indented);

                    console.WriteLine(json, ConsoleColor.Green);

                    File.WriteAllText(fullname, json);
                }
            }
        }

        private dynamic Map(dynamic e)
        {
            string source = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("source"))
                source = e.source;

            string subject = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("subject"))
                subject = e.subject;

            string type = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("type"))
                type = e.type;

            string time = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("time"))
                time = e.time.ToString("o");

            string id = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("id"))
                id = e.id;

            string specVersion = string.Empty;
            if (((IDictionary<String, object>)e).ContainsKey("specVersion"))
                specVersion = e.specVersion;

            dynamic data = new ExpandoObject();
            if (((IDictionary<String, object>)e).ContainsKey("data"))
                data = e.data;

            dynamic eventGridEvent = new ExpandoObject();
            eventGridEvent.topic = source;
            eventGridEvent.subject = subject;
            eventGridEvent.eventType = type;
            eventGridEvent.eventTime = time;
            eventGridEvent.id = id;
            eventGridEvent.data = data;
            eventGridEvent.dataVersion = "1.0";
            eventGridEvent.metadataVersion = "1.0";

            return eventGridEvent;
        }
    }
}
