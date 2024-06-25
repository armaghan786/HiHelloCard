using System;
using System.Collections.Generic;

namespace HiHelloCard.Model.Domain;

public partial class Efmigrationshistory
{
    public string MigrationId { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
