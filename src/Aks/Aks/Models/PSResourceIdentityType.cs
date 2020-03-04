using System.Runtime.Serialization;

namespace Microsoft.Azure.Commands.Aks.Models
{
    public enum PSResourceIdentityType
    {
        [EnumMember(Value = "SystemAssigned")]
        SystemAssigned,

        [EnumMember(Value = "None")]
        None
    }
}
