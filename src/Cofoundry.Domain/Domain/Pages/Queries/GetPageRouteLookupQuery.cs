﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Returns a collection of page routing data for all pages. The 
    /// PageRoute projection is a small page object focused on providing 
    /// routing data only. Data returned from this query is cached by 
    /// default as it's core to routing and often incorporated in more detailed
    /// page projections.
    /// </summary>
    public class GetPageRouteLookupQuery : IQuery<IDictionary<int, PageRoute>>
    {
    }
}
