using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Azure.AI.Extensions.OpenAI;

/// <summary> The bing grounding search tool parameters. </summary>
public partial class BingGroundingOptions : IJsonModel<BingGroundingOptions>
{
    /// <summary> Keeps track of any properties unknown to the library. </summary>
    private protected readonly IDictionary<string, BinaryData> _additionalBinaryDataProperties;

    /// <summary> Initializes a new instance of <see cref="BingGroundingOptions"/>. </summary>
    /// <param name="searchConfigurations">
    /// The search configurations attached to this tool. There can be a maximum of 1
    /// search configuration resource attached to the tool.
    /// </param>
    /// <exception cref="ArgumentNullException"> <paramref name="searchConfigurations"/> is null. </exception>
    public BingGroundingOptions(IEnumerable<BingGroundingSearchConfiguration> searchConfigurations)
    {
        Argument.AssertNotNull(searchConfigurations, nameof(searchConfigurations));

        SearchConfigurations = searchConfigurations.ToList();
    }

    /// <summary> Initializes a new instance of <see cref="BingGroundingOptions"/>. </summary>
    /// <param name="searchConfigurations">
    /// The search configurations attached to this tool. There can be a maximum of 1
    /// search configuration resource attached to the tool.
    /// </param>
    /// <param name="additionalBinaryDataProperties"> Keeps track of any properties unknown to the library. </param>
    internal BingGroundingOptions(IList<BingGroundingSearchConfiguration> searchConfigurations, IDictionary<string, BinaryData> additionalBinaryDataProperties)
    {
        SearchConfigurations = searchConfigurations;
        _additionalBinaryDataProperties = additionalBinaryDataProperties;
    }

    /// <summary>
    /// The search configurations attached to this tool. There can be a maximum of 1
    /// search configuration resource attached to the tool.
    /// </summary>
    public IList<BingGroundingSearchConfiguration> SearchConfigurations { get; }

    // *****************************************************************


    /// <summary> Initializes a new instance of <see cref="BingGroundingOptions"/> for deserialization. </summary>
    internal BingGroundingOptions()
    {
    }

    /// <param name="data"> The data to parse. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    protected virtual BingGroundingOptions PersistableModelCreateCore(BinaryData data, ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingOptions>)this).GetFormatFromOptions(options) : options.Format;
        switch (format)
        {
            case "J":
                using (JsonDocument document = JsonDocument.Parse(data, ModelSerializationExtensions.JsonDocumentOptions))
                {
                    return DeserializeBingGroundingOptions(document.RootElement, options);
                }
            default:
                throw new FormatException($"The model {nameof(BingGroundingOptions)} does not support reading '{options.Format}' format.");
        }
    }

    /// <param name="options"> The client options for reading and writing models. </param>
    protected virtual BinaryData PersistableModelWriteCore(ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingOptions>)this).GetFormatFromOptions(options) : options.Format;
        switch (format)
        {
            case "J":
                return ModelReaderWriter.Write(this, options, AzureAIExtensionsOpenAIContext.Default);
            default:
                throw new FormatException($"The model {nameof(BingGroundingOptions)} does not support writing '{options.Format}' format.");
        }
    }

    /// <param name="options"> The client options for reading and writing models. </param>
    BinaryData IPersistableModel<BingGroundingOptions>.Write(ModelReaderWriterOptions options) => PersistableModelWriteCore(options);

    /// <param name="data"> The data to parse. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    BingGroundingOptions IPersistableModel<BingGroundingOptions>.Create(BinaryData data, ModelReaderWriterOptions options) => PersistableModelCreateCore(data, options);

    /// <param name="options"> The client options for reading and writing models. </param>
    string IPersistableModel<BingGroundingOptions>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";

    /// <param name="writer"> The JSON writer. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    void IJsonModel<BingGroundingOptions>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        writer.WriteStartObject();
        JsonModelWriteCore(writer, options);
        writer.WriteEndObject();
    }

    /// <param name="writer"> The JSON writer. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    protected virtual void JsonModelWriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingOptions>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(BingGroundingOptions)} does not support writing '{format}' format.");
        }
        writer.WritePropertyName("search_configurations"u8);
        writer.WriteStartArray();
        foreach (BingGroundingSearchConfiguration item in SearchConfigurations)
        {
            writer.WriteObjectValue(item, options);
        }
        writer.WriteEndArray();
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
    }

    /// <param name="reader"> The JSON reader. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    BingGroundingOptions IJsonModel<BingGroundingOptions>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options) => JsonModelCreateCore(ref reader, options);

    /// <param name="reader"> The JSON reader. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    protected virtual BingGroundingOptions JsonModelCreateCore(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
    {
        string format = options.Format == "W" ? ((IPersistableModel<BingGroundingOptions>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(BingGroundingOptions)} does not support reading '{format}' format.");
        }
        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return DeserializeBingGroundingOptions(document.RootElement, options);
    }

    /// <param name="element"> The JSON element to deserialize. </param>
    /// <param name="options"> The client options for reading and writing models. </param>
    internal static BingGroundingOptions DeserializeBingGroundingOptions(JsonElement element, ModelReaderWriterOptions options)
    {
        if (element.ValueKind == JsonValueKind.Null)
        {
            return null;
        }
        IList<BingGroundingSearchConfiguration> searchConfigurations = default;
        IDictionary<string, BinaryData> additionalBinaryDataProperties = new ChangeTrackingDictionary<string, BinaryData>();
        foreach (var prop in element.EnumerateObject())
        {
            if (prop.NameEquals("search_configurations"u8))
            {
                List<BingGroundingSearchConfiguration> array = new List<BingGroundingSearchConfiguration>();
                foreach (var item in prop.Value.EnumerateArray())
                {
                    array.Add(BingGroundingSearchConfiguration.DeserializeBingGroundingSearchConfiguration(item, options));
                }
                searchConfigurations = array;
                continue;
            }
            if (options.Format != "W")
            {
                additionalBinaryDataProperties.Add(prop.Name, BinaryData.FromString(prop.Value.GetRawText()));
            }
        }
        return new BingGroundingOptions(searchConfigurations, additionalBinaryDataProperties);
    }
}