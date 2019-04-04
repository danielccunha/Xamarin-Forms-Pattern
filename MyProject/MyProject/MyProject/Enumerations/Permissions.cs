using System;

namespace MyProject.Enumerations
{
    [Flags]
    public enum Permissions
    {
        None = 0x01,
        Read = 0x02,
        Write = 0x04
    }
}
