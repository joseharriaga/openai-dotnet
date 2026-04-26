using OpenAI.Responses;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;

namespace Azure.AI.Extensions.OpenAI;

/// <summary> The input definition information for a bing grounding search tool as used to configure an agent. </summary>
public partial class BingGroundingTool : ResponseTool, IJsonModel<BingGroundingTool>
{
    /// <summary> Initializes a new instance of <see cref="BingGroundingTool"/>. </summary>
    /// <param name="bingGrounding"> The bing grounding search tool parameters. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="bingGrounding"/> is null. </exception>
    public BingGroundingTool(BingGroundingOptions bingGrounding) : base(ResponseToolKind.BingGrounding())
    {
        Argument.AssertNotNull(bingGrounding, nameof(bingGrounding));

        BingGroundingOptions = bingGrounding;
    }

    /// <summary> Initializes a new instance of <see cref="BingGroundingTool"/>. </summary>
    /// <param name="type"> The response tool kind representing the tool type. </param>
    /// <param name="additionalBinaryDataProperties"> Keeps track of any properties unknown to the library. </param>
    /// <param name="bingGrounding"> The bing grounding search tool parameters. </param>
    internal BingGroundingTool(ResponseToolKind @type, IDictionary<string, BinaryData> additionalBinaryDataProperties, BingGroundingOptions bingGrounding) : base(@type)
    {
        _additionalBinaryDataProperties = additionalBinaryDataProperties;
        BingGroundingOptions = bingGrounding;
    }

    /// <summary> Keeps track of any properties unknown to the library. </summary>
    private readonly IDictionary<string, BinaryData> _additionalBinaryDataProperties;

    /// <summary> The bing grounding search tool parameters. </summary>
    public BingGroundingOptions BingGroundingOptions { get; set; }

    // *****************************************************************


    /// <summary> Initializes a new instance of <see cref="BingGroundingTool"/> for deserialization. </summary>
    internal BingGroundingTool() : base(ResponseToolKind.BingGrounding())
    {
    }

    /// <param name="data"> The data to parse. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    protected override ResponseTool PersistableModelCreateCore(BinaryData data, ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingTool>)this).GetFormatFromOptions(options) : options.Format;
        switch (format)
        {
            case "J":
                using (JsonDocument document = JsonDocument.Parse(data, ModelSerializationExtensions.JsonDocumentOptions))
                {
                    return DeserializeBingGroundingTool(document.RootElement, options);
                }
            default:
                throw new FormatException($"The model {nameof(BingGroundingTool)} does not support reading '{options.Format}' format.");
        }
    }

    /// <param name="options"> The client options for reading and writing models. </param>
    protected override BinaryData PersistableModelWriteCore(ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingTool>)this).GetFormatFromOptions(options) : options.Format;
        switch (format)
        {
            case "J":
                return ModelReaderWriter.Write(this, options, AzureAIExtensionsOpenAIContext.Default);
            default:
                throw new FormatException($"The model {nameof(BingGroundingTool)} does not support writing '{options.Format}' format.");
        }
    }

    /// <param name="options"> The client options for reading and writing models. </param>
    BinaryData IPersistableModel<BingGroundingTool>.Write(ModelReaderWriterOptions options) => PersistableModelWriteCore(options);

    /// <param name="data"> The data to parse. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    BingGroundingTool IPersistableModel<BingGroundingTool>.Create(BinaryData data, ModelReaderWriterOptions options) => (BingGroundingTool)PersistableModelCreateCore(data, options);

    /// <param name="options"> The client options for reading and writing models. </param>
    string IPersistableModel<BingGroundingTool>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";

    /// <param name="writer"> The JSON writer. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    void IJsonModel<BingGroundingTool>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        writer.WriteStartObject();
        JsonModelWriteCore(writer, options);
        writer.WriteEndObject();
    }

    /// <param name="writer"> The JSON writer. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    protected override void JsonModelWriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingTool>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(BingGroundingTool)} does not support writing '{format}' format.");
        }
        base.JsonModelWriteCore(writer, options);
        if (options.Format != "W" && _additionalBinaryDataProperties != null)
        {
            foreach (var item in _additionalBinaryDataProperties)
            {
                writer.WritePropertyName(item.Key);
#if NET6_0_OR_GREATER
                writer.WriteRawValue(item.Value);
#else
                using (JsonDocument document = JsonDocument.Parse(item.Value))
                {
                    JsonSerializer.Serialize(writer, document.RootElement);
                }
#endif
            }
        }
        writer.WritePropertyName("bing_grounding"u8);
        writer.WriteObjectValue(BingGroundingOptions, options);
    }

    /// <param name="reader"> The JSON reader. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    BingGroundingTool IJsonModel<BingGroundingTool>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options) => (BingGroundingTool)JsonModelCreateCore(ref reader, options);

    /// <param name="reader"> The JSON reader. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    protected override ResponseTool JsonModelCreateCore(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingTool>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(BingGroundingTool)} does not support reading '{format}' format.");
        }
        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return DeserializeBingGroundingTool(document.RootElement, options);
    }

    /// <param name="element"> The JSON element to deserialize. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    internal static BingGroundingTool DeserializeBingGroundingTool(JsonElement element, ModelReaderWriterOptions options)
    {
        if (element.ValueKind == JsonValueKind.Null)
        {
            return null;
        }
        ResponseToolKind @type = default;
        IDictionary<string, BinaryData> additionalBinaryDataProperties = new ChangeTrackingDictionary<string, BinaryData>();
        BingGroundingOptions bingGrounding = default;
        foreach (var prop in element.EnumerateObject())
        {
            if (prop.NameEquals("type"u8))
            {
                @type = new ResponseToolKind(prop.Value.GetString());
                continue;
            }
            if (prop.NameEquals("bing_grounding"u8))
            {
                bingGrounding = BingGroundingOptions.DeserializeBingGroundingOptions(prop.Value, options);
                continue;
            }
            if (options.Format != "W")
            {
                additionalBinaryDataProperties.Add(prop.Name, BinaryData.FromString(prop.Value.GetRawText()));
            }
        }
        return new BingGroundingTool(@type, additionalBinaryDataProperties, bingGrounding);
    }
}
