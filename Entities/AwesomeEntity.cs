using Azure;
using Azure.Data.Tables;
using System.Diagnostics.CodeAnalysis;

namespace TableInputIssue.Entities;

// Adding serializable to make sure the entity is just that.
[Serializable]
public class AwesomeEntity : ITableEntity
{
    // Explicitly added default, SetsRequiredMembers and parameter constructors.
    #region Constructor

    public AwesomeEntity()
    {
    }

    [SetsRequiredMembers]
    public AwesomeEntity(string partitionKey, string rowKey, string whatMakesThisSpecial)
    {
        PartitionKey = partitionKey;
        RowKey = rowKey;
        WhatMakesThisSpecial = whatMakesThisSpecial;
    }

    [SetsRequiredMembers]
    public AwesomeEntity(string partitionKey, string rowKey, DateTimeOffset? timestamp, ETag eTag, string whatMakesThisSpecial) : this(partitionKey, rowKey, whatMakesThisSpecial)
    {
        Timestamp = timestamp;
        ETag = eTag;
    }

    #endregion

    public required string PartitionKey { get; set; } = string.Empty;

    public required string RowKey { get; set; } = string.Empty;

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }

    public required string WhatMakesThisSpecial { get; set; } = string.Empty;
}