using Avalon.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Core.Interfaces
{
    public interface IRolesGenerator
    {
        IEnumerable<Role> Generate(int count);
    }
}
