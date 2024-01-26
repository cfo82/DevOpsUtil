namespace GitLabApiClient.Models.ToDoList.Responses;

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

public class ToDoItemConverter : CustomCreationConverter<IToDo>
{
    public override IToDo Create(Type objectType)
    {
        throw new NotImplementedException();
    }

    public IToDo Create(JObject jObject)
    {
        string type = (string)jObject.Property("target_type");
        switch (type)
        {
            case "Issue":
                return new ToDoIssue();
            case "MergeRequest":
                return new ToDoMergeRequest();
            default:
                throw new ApplicationException($"ToDo target not supported.");
        }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);
        var target = Create(jObject);
        serializer.Populate(jObject.CreateReader(), target);
        return target;
    }
}
