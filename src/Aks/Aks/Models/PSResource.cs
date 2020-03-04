namespace Microsoft.Azure.Commands.Aks.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The Resource model definition.
    /// </summary>
    public partial class PSResource
    {
        /// <summary>
        /// Initializes a new instance of the Resource class.
        /// </summary>
        public PSResource(){}

        /// <summary>
        /// Initializes a new instance of the Resource class.
        /// </summary>
        /// <param name="location">Resource location</param>
        /// <param name="id">Resource Id</param>
        /// <param name="name">Resource name</param>
        /// <param name="type">Resource type</param>
        /// <param name="tags">Resource tags</param>
        public PSResource(string location, string id = default(string), string name = default(string), string type = default(string), IDictionary<string, string> tags = default(IDictionary<string, string>))
        {
            Id = id;
            Name = name;
            Type = type;
            Location = location;
            Tags = tags;
        }

        /// <summary>
        /// Gets resource Id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets resource name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets resource type
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Gets or sets resource location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets resource tags
        /// </summary>
        public IDictionary<string, string> Tags { get; set; }
    }
}