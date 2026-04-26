using OpenAI.Responses;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Azure.AI.Extensions.OpenAI;

/// <summary> The input definition information for a bing grounding search tool as used to configure an agent. </summary>
public partial class BingGroundingAzureTool : AzureResponsesTool, IJsonModel<BingGroundingAzureTool>
{
    /// <summary> Initializes a new instance of <see cref="BingGroundingAzureTool"/>. </summary>
    /// <param name="bingGrounding"> The bing grounding search tool parameters. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="bingGrounding"/> is null. </exception>
    public BingGroundingAzureTool(BingGroundingOptions bingGrounding) : base(AzureResponsesToolKind.BingGrounding)
    {
        Argument.AssertNotNull(bingGrounding, nameof(bingGrounding));

        BingGroundingOptions = bingGrounding;
    }

    /// <summary> Initializes a new instance of <see cref="BingGroundingAzureTool"/>. </summary>
    /// <param name="type"></param>
    /// <param name="additionalBinaryDataProperties"> Keeps track of any properties unknown to the library. </param>
    /// <param name="bingGrounding"> The bing grounding search tool parameters. </param>
    internal BingGroundingAzureTool(AzureResponsesToolKind @type, IDictionary<string, BinaryData> additionalBinaryDataProperties, BingGroundingOptions bingGrounding) : base(@type, additionalBinaryDataProperties)
    {
        BingGroundingOptions = bingGrounding;
    }

    /// <summary> The bing grounding search tool parameters. </summary>
    public BingGroundingOptions BingGroundingOptions { get; set; }

    // *****************************************************************


    /// <summary> Initializes a new instance of <see cref="BingGroundingAzureTool"/> for deserialization. </summary>
    internal BingGroundingAzureTool()
    {
    }

    /// <param name="data"> The data to parse. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    protected override AzureResponsesTool PersistableModelCreateCore(BinaryData data, ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingAzureTool>)this).GetFormatFromOptions(options) : options.Format;
        switch (format)
        {
            case "J":
                using (JsonDocument document = JsonDocument.Parse(data, ModelSerializationExtensions.JsonDocumentOptions))
                {
                    return DeserializeBingGroundingAzureTool(document.RootElement, options);
                }
            default:
                throw new FormatException($"The model {nameof(BingGroundingAzureTool)} does not support reading '{options.Format}' format.");
        }
    }

    /// <param name="options"> The client options for reading and writing models. </param>
    protected override BinaryData PersistableModelWriteCore(ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingAzureTool>)this).GetFormatFromOptions(options) : options.Format;
        switch (format)
        {
            case "J":
                return ModelReaderWriter.Write(this, options, AzureAIExtensionsOpenAIContext.Default);
            default:
                throw new FormatException($"The model {nameof(BingGroundingAzureTool)} does not support writing '{options.Format}' format.");
        }
    }

    /// <param name="options"> The client options for reading and writing models. </param>
    BinaryData IPersistableModel<BingGroundingAzureTool>.Write(ModelReaderWriterOptions options) => PersistableModelWriteCore(options);

    /// <param name="data"> The data to parse. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    BingGroundingAzureTool IPersistableModel<BingGroundingAzureTool>.Create(BinaryData data, ModelReaderWriterOptions options) => (BingGroundingAzureTool)PersistableModelCreateCore(data, options);

    /// <param name="options"> The client options for reading and writing models. </param>
    string IPersistableModel<BingGroundingAzureTool>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";

    /// <param name="writer"> The JSON writer. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    void IJsonModel<BingGroundingAzureTool>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        writer.WriteStartObject();
        JsonModelWriteCore(writer, options);
        writer.WriteEndObject();
    }

    /// <param name="writer"> The JSON writer. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    protected override void JsonModelWriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingAzureTool>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(BingGroundingAzureTool)} does not support writing '{format}' format.");
        }
        base.JsonModelWriteCore(writer, options);
        writer.WritePropertyName("bing_grounding"u8);
        writer.WriteObjectValue(BingGroundingOptions, options);
    }

    /// <param name="reader"> The JSON reader. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    BingGroundingAzureTool IJsonModel<BingGroundingAzureTool>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options) => (BingGroundingAzureTool)JsonModelCreateCore(ref reader, options);

    /// <param name="reader"> The JSON reader. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    protected override AzureResponsesTool JsonModelCreateCore(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingAzureTool>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(BingGroundingAzureTool)} does not support reading '{format}' format.");
        }
        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return DeserializeBingGroundingAzureTool(document.RootElement, options);
    }

    /// <param name="element"> The JSON element to deserialize. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    internal static BingGroundingAzureTool DeserializeBingGroundingAzureTool(JsonElement element, ModelReaderWriterOptions options)
    {
        if (element.ValueKind == JsonValueKind.Null)
        {
            return null;
        }
        AzureResponsesToolKind @type = default;
        IDictionary<string, BinaryData> additionalBinaryDataProperties = new ChangeTrackingDictionary<string, BinaryData>();
        BingGroundingOptions bingGrounding = default;
        foreach (var prop in element.EnumerateObject())
        {
            if (prop.NameEquals("type"u8))
            {
                @type = new AzureResponsesToolKind(prop.Value.GetString());
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
        return new BingGroundingAzureTool(@type, additionalBinaryDataProperties, bingGrounding);
    }
}