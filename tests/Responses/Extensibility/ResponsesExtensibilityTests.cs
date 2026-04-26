using NUnit.Framework;
using OpenAI.Responses;
using System;
using System.ClientModel.Primitives;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace OpenAI.Tests.Responses;

[Category("Responses")]
[Category("Smoke")]
public partial class ResponsesExtensibilityTests
{
    [Test]
    public void CustomTools()
    {
        BinaryData data = BinaryData.FromString("""
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
                         "project_connection_id": "'$BING_PROJECT_CONNECTION_ID'",
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

        ResponseResult response = ModelReaderWriter.Read<ResponseResult>(data);
        ResponseTool tool = response.Tools[0];
        string foo = tool.Patch.GetString("$.foo"u8);

        string json = tool.Patch.GetJson("$."u8).ToString();

        Console.WriteLine(response.GetOutputText());
    }
}
