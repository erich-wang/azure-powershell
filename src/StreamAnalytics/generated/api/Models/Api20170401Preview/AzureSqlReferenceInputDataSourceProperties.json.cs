namespace Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Models.Api20170401Preview
{
    using static Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Extensions;

    public partial class AzureSqlReferenceInputDataSourceProperties
    {

        /// <summary>
        /// <c>AfterFromJson</c> will be called after the json deserialization has finished, allowing customization of the object
        /// before it is returned. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="json">The JsonNode that should be deserialized into this object.</param>

        partial void AfterFromJson(Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject json);

        /// <summary>
        /// <c>AfterToJson</c> will be called after the json erialization has finished, allowing customization of the <see cref="Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject"
        /// /> before it is returned. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="container">The JSON container that the serialization result will be placed in.</param>

        partial void AfterToJson(ref Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject container);

        /// <summary>
        /// <c>BeforeFromJson</c> will be called before the json deserialization has commenced, allowing complete customization of
        /// the object before it is deserialized.
        /// If you wish to disable the default deserialization entirely, return <c>true</c> in the <see "returnNow" /> output parameter.
        /// Implement this method in a partial class to enable this behavior.
        /// </summary>
        /// <param name="json">The JsonNode that should be deserialized into this object.</param>
        /// <param name="returnNow">Determines if the rest of the deserialization should be processed, or if the method should return
        /// instantly.</param>

        partial void BeforeFromJson(Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject json, ref bool returnNow);

        /// <summary>
        /// <c>BeforeToJson</c> will be called before the json serialization has commenced, allowing complete customization of the
        /// object before it is serialized.
        /// If you wish to disable the default serialization entirely, return <c>true</c> in the <see "returnNow" /> output parameter.
        /// Implement this method in a partial class to enable this behavior.
        /// </summary>
        /// <param name="container">The JSON container that the serialization result will be placed in.</param>
        /// <param name="returnNow">Determines if the rest of the serialization should be processed, or if the method should return
        /// instantly.</param>

        partial void BeforeToJson(ref Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject container, ref bool returnNow);

        /// <summary>
        /// Deserializes a Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject into a new instance of <see cref="AzureSqlReferenceInputDataSourceProperties" />.
        /// </summary>
        /// <param name="json">A Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject instance to deserialize from.</param>
        internal AzureSqlReferenceInputDataSourceProperties(Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject json)
        {
            bool returnNow = false;
            BeforeFromJson(json, ref returnNow);
            if (returnNow)
            {
                return;
            }
            {_server = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString>("server"), out var __jsonServer) ? (string)__jsonServer : (string)Server;}
            {_database = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString>("database"), out var __jsonDatabase) ? (string)__jsonDatabase : (string)Database;}
            {_user = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString>("user"), out var __jsonUser) ? (string)__jsonUser : (string)User;}
            {_password = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString>("password"), out var __jsonPassword) ? (string)__jsonPassword : (string)Password;}
            {_table = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString>("table"), out var __jsonTable) ? (string)__jsonTable : (string)Table;}
            {_refreshType = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString>("refreshType"), out var __jsonRefreshType) ? (string)__jsonRefreshType : (string)RefreshType;}
            {_refreshRate = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString>("refreshRate"), out var __jsonRefreshRate) ? (string)__jsonRefreshRate : (string)RefreshRate;}
            {_fullSnapshotQuery = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString>("fullSnapshotQuery"), out var __jsonFullSnapshotQuery) ? (string)__jsonFullSnapshotQuery : (string)FullSnapshotQuery;}
            {_deltaSnapshotQuery = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString>("deltaSnapshotQuery"), out var __jsonDeltaSnapshotQuery) ? (string)__jsonDeltaSnapshotQuery : (string)DeltaSnapshotQuery;}
            AfterFromJson(json);
        }

        /// <summary>
        /// Deserializes a <see cref="Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode"/> into an instance of Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Models.Api20170401Preview.IAzureSqlReferenceInputDataSourceProperties.
        /// </summary>
        /// <param name="node">a <see cref="Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode" /> to deserialize from.</param>
        /// <returns>
        /// an instance of Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Models.Api20170401Preview.IAzureSqlReferenceInputDataSourceProperties.
        /// </returns>
        public static Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Models.Api20170401Preview.IAzureSqlReferenceInputDataSourceProperties FromJson(Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode node)
        {
            return node is Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject json ? new AzureSqlReferenceInputDataSourceProperties(json) : null;
        }

        /// <summary>
        /// Serializes this instance of <see cref="AzureSqlReferenceInputDataSourceProperties" /> into a <see cref="Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode"
        /// />.
        /// </summary>
        /// <param name="container">The <see cref="Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject"/> container to serialize this object into. If the caller
        /// passes in <c>null</c>, a new instance will be created and returned to the caller.</param>
        /// <param name="serializationMode">Allows the caller to choose the depth of the serialization. See <see cref="Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.SerializationMode"/>.</param>
        /// <returns>
        /// a serialized instance of <see cref="AzureSqlReferenceInputDataSourceProperties" /> as a <see cref="Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode"
        /// />.
        /// </returns>
        public Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode ToJson(Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject container, Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.SerializationMode serializationMode)
        {
            container = container ?? new Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonObject();

            bool returnNow = false;
            BeforeToJson(ref container, ref returnNow);
            if (returnNow)
            {
                return container;
            }
            AddIf( null != (((object)this._server)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString(this._server.ToString()) : null, "server" ,container.Add );
            AddIf( null != (((object)this._database)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString(this._database.ToString()) : null, "database" ,container.Add );
            AddIf( null != (((object)this._user)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString(this._user.ToString()) : null, "user" ,container.Add );
            AddIf( null != (((object)this._password)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString(this._password.ToString()) : null, "password" ,container.Add );
            AddIf( null != (((object)this._table)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString(this._table.ToString()) : null, "table" ,container.Add );
            AddIf( null != (((object)this._refreshType)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString(this._refreshType.ToString()) : null, "refreshType" ,container.Add );
            AddIf( null != (((object)this._refreshRate)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString(this._refreshRate.ToString()) : null, "refreshRate" ,container.Add );
            AddIf( null != (((object)this._fullSnapshotQuery)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString(this._fullSnapshotQuery.ToString()) : null, "fullSnapshotQuery" ,container.Add );
            AddIf( null != (((object)this._deltaSnapshotQuery)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.StreamAnalytics.Runtime.Json.JsonString(this._deltaSnapshotQuery.ToString()) : null, "deltaSnapshotQuery" ,container.Add );
            AfterToJson(ref container);
            return container;
        }
    }
}