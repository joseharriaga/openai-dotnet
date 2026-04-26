using Azure.AI.Extensions.OpenAI;
using NUnit.Framework;
using OpenAI;
using OpenAI.Responses;
using System;
using System.ClientModel.Primitives;
using System.Linq;
using System.Text.Json;

namespace OpenAI.Tests.Responses;

[Category("Responses")]
[Category("Smoke")]
public partial class ResponsesExtensibilityTests
{
    [Test]
    public void AzureBingGroundingToolUsesAzureToolAdapterExperience()
    {
        ResponseResult response = ReadResponseWithFunctionAndBingGroundingTools();
        FunctionTool functionTool = AssertNativeFunctionToolExperience(response.Tools[0]);
        BinaryData bingGroundingJson = AssertUnknownBingGroundingResponseTool(response.Tools[1]);

        AzureResponsesTool azureTool = ModelReaderWriter.Read<AzureResponsesTool>(
            bingGroundingJson,
            ModelSerializationExtensions.WireOptions,
            AzureAIExtensionsOpenAIContext.Default);
        BingGroundingAzureTool bingGroundingTool = (BingGroundingAzureTool)azureTool;
        AssertBingGroundingOptions(bingGroundingTool.BingGroundingOptions);

        CreateResponseOptions options = new();
        options.Tools.Add(functionTool);
        options.Tools.Add(bingGroundingTool);

        Assert.Multiple(() =>
        {
            Assert.That(options.Tools[0], Is.SameAs(functionTool));
            Assert.That(options.Tools[1], Is.InstanceOf<UnknownTool>());
            Assert.That(options.Tools[1].Kind, Is.EqualTo(ResponseToolKind.BingGrounding()));
        });
    }

    [Test]
    public void DirectBingGroundingToolFeelsLikeNativeResponseToolExperience()
    {
        ResponseResult response = ReadResponseWithFunctionAndBingGroundingTools();
        FunctionTool functionTool = AssertNativeFunctionToolExperience(response.Tools[0]);
        BinaryData bingGroundingJson = AssertUnknownBingGroundingResponseTool(response.Tools[1]);

        BingGroundingTool bingGroundingTool = ModelReaderWriter.Read<BingGroundingTool>(
            bingGroundingJson,
            ModelSerializationExtensions.WireOptions,
            AzureAIExtensionsOpenAIContext.Default);
        AssertBingGroundingOptions(bingGroundingTool.BingGroundingOptions);

        CreateResponseOptions options = new();
        options.Tools.Add(functionTool);
        options.Tools.Add(bingGroundingTool);

        Assert.Multiple(() =>
        {
            Assert.That(options.Tools[0], Is.SameAs(functionTool));
            Assert.That(options.Tools[1], Is.SameAs(bingGroundingTool));
            Assert.That(options.Tools[1].Kind, Is.EqualTo(ResponseToolKind.BingGrounding()));
        });
    }

    private static ResponseResult ReadResponseWithFunctionAndBingGroundingTools()
        => ModelReaderWriter.Read<ResponseResult>(ResponseWithFunctionAndBingGroundingTools);

    private static FunctionTool AssertNativeFunctionToolExperience(ResponseTool tool)
    {
        FunctionTool functionTool = (FunctionTool)tool;

        using JsonDocument parameters = JsonDocument.Parse(functionTool.FunctionParameters.ToString());
        JsonElement root = parameters.RootElement;

        Assert.Multiple(() =>
        {
            Assert.That(functionTool.Kind, Is.EqualTo(ResponseToolKind.Function));
            Assert.That(functionTool.FunctionName, Is.EqualTo("get_weather_at_location"));
            Assert.That(functionTool.FunctionDescription, Is.EqualTo("Gets the weather at a specified location, optionally specifying units for temperature"));
            Assert.That(functionTool.StrictModeEnabled, Is.False);
            Assert.That(root.GetProperty("type").GetString(), Is.EqualTo("object"));
            Assert.That(root.GetProperty("properties").TryGetProperty("location", out _), Is.True);
            Assert.That(root.GetProperty("required").EnumerateArray().Select(item => item.GetString()), Does.Contain("location"));
        });

        return functionTool;
    }

    private static BinaryData AssertUnknownBingGroundingResponseTool(ResponseTool tool)
    {
        Assert.Multiple(() =>
        {
            Assert.That(tool, Is.InstanceOf<UnknownTool>());
            Assert.That(tool.Kind, Is.EqualTo(ResponseToolKind.BingGrounding()));
        });

        return tool.Patch.GetJson("$."u8);
    }

    private static void AssertBingGroundingOptions(BingGroundingOptions options)
    {
        Assert.That(options.SearchConfigurations, Has.Count.EqualTo(1));
        BingGroundingSearchConfiguration searchConfiguration = options.SearchConfigurations[0];

        Assert.Multiple(() =>
        {
            Assert.That(searchConfiguration.ProjectConnectionId, Is.EqualTo("bing-project-connection-id"));
            Assert.That(searchConfiguration.Count, Is.EqualTo(7));
            Assert.That(searchConfiguration.Market, Is.EqualTo("en-US"));
            Assert.That(searchConfiguration.SetLang, Is.EqualTo("en"));
            Assert.That(searchConfiguration.Freshness, Is.EqualTo("7d"));
        });
    }

    private static readonly BinaryData ResponseWithFunctionAndBingGroundingTools = BinaryData.FromString("""
        {
           "id": "resp_09163d12481226a80068c9ef4ce6848194bc5be7e0c05c5f93",
           "object": "response",
           "created_at": 1758064460,
           "status": "completed",
           "background": false,
           "error": null,
           "incomplete_details": null,
           "instructions": null,
           "max_output_tokens": null,
           "max_tool_calls": null,
           "model": "gpt-4o-mini-2024-07-18",
           "output": [
             {
               "id": "msg_09163d12481226a80068c9ef4d4ba081949d77a49fd01389f3",
               "type": "message",
               "status": "completed",
               "content": [
                 {
                   "type": "output_text",
                   "annotations": [],
                   "logprobs": [],
                   "text": "Hello."
                 }
               ],
               "role": "assistant"
             }
           ],
           "parallel_tool_calls": true,
           "previous_response_id": null,
           "prompt_cache_key": null,
           "reasoning": {
             "effort": null,
             "summary": null
           },
           "safety_identifier": null,
           "service_tier": "default",
           "store": true,
           "temperature": 1.0,
           "text": {
             "format": {
               "type": "text"
             },
             "verbosity": "medium"
           },
           "tool_choice": "auto",
           "tools": [
             {
               "type": "function",
               "description": "Gets the weather at a specified location, optionally specifying units for temperature",
               "name": "get_weather_at_location",
               "parameters": {
                 "type": "object",
                 "properties": {
                   "location": {
                     "type": "string"
                   },
                   "unit": {
                     "type": "string",
                     "enum": [
                       "C",
                       "F",
                       "K"
                     ]
                   }
                 },
                 "required": [
                   "location"
                 ]
               },
               "strict": false
             },
             {
               "type": "bing_grounding",
               "bing_grounding": {
                 "search_configurations": [
                   {
                     "project_connection_id": "bing-project-connection-id",
                     "count": 7,
                     "market": "en-US",
                     "set_lang": "en",
                     "freshness": "7d"
                   }
                 ]
               }
             }
           ],
           "top_logprobs": 0,
           "top_p": 1.0,
           "truncation": "disabled",
           "usage": {
             "input_tokens": 14,
             "input_tokens_details": {
               "cached_tokens": 0
             },
             "output_tokens": 3,
             "output_tokens_details": {
               "reasoning_tokens": 0
             },
             "total_tokens": 17
           },
           "user": null,
           "metadata": {}
        }
        """);
}
