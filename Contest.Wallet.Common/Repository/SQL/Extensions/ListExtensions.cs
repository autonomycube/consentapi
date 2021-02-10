﻿using Consent.Common.Repository.Helpers;
using System.Collections.Generic;
using System.ComponentModel;

namespace Consent.Common.Repository.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ListExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IList<T> list,
            int pageIndex, int pageSize, int total)
        {
            return new PaginatedList<T>(list, pageIndex, pageSize, total);
        }
    }
}
