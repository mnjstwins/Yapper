﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Augment;

namespace Yapper.Dialects
{
    /// <summary>
    /// Provides functionality common to all supported SQL Server versions
    /// </summary>
    public class SqlServerDialect : SqlDialect
    {
        /// <summary>
        /// Provides the items used to construct a Select statement given a <see cref="ISqlDialect"/>
        /// </summary>
        /// <param name="selection">Select Clause</param>
        /// <param name="source">From Clause</param>
        /// <param name="conditions">Where Clause</param>
        /// <param name="order">Order By Clause</param>
        /// <param name="grouping">Group By Clause</param>
        /// <param name="limit">Top Clause</param>
        /// <param name="offset">Page offset index (zero based)</param>
        /// <param name="fetch">Page number of records to return</param>
        /// <returns>A SQL Select statement specific to a given <see cref="ISqlDialect"/></returns>
        public override string SelectStatement(string selection, string source, string conditions, string order, string grouping, int limit, int offset, int fetch)
        {
            if (limit > 0)
            {
                return "select top({0}) {1} from {2} {3} {4} {5}"
                    .FormatArgs(limit, selection, source, conditions, grouping, order);
            }

            return base.SelectStatement(selection, source, conditions, order, grouping, limit, offset, fetch);
        }

        /// <summary>
        /// Gets the identity value on insert
        /// </summary>
        public override string SelectIdentity { get { return StatementSeparator + "select SCOPE_IDENTITY()"; } }
    }
}
