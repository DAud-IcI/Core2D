﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;

namespace Core2D
{
    /// <summary>
    /// Poly line path segment.
    /// </summary>
    public class XPolyLineSegment : XPathSegment
    {
        /// <summary>
        /// Gets or sets points array.
        /// </summary>
        public IList<XPoint> Points { get; set; }

        /// <inheritdoc/>
        public override IEnumerable<XPoint> GetPoints()
        {
            return Points;
        }

        /// <summary>
        /// Creates a new <see cref="XPolyLineSegment"/> instance.
        /// </summary>
        /// <param name="points">The points array.</param>
        /// <param name="isStroked">The flag indicating whether shape is stroked.</param>
        /// <param name="isSmoothJoin">The flag indicating whether shape is smooth join.</param>
        /// <returns>The new instance of the <see cref="XPolyLineSegment"/> class.</returns>
        public static XPolyLineSegment Create(IList<XPoint> points, bool isStroked, bool isSmoothJoin)
        {
            return new XPolyLineSegment()
            {
                Points = points,
                IsStroked = isStroked,
                IsSmoothJoin = isSmoothJoin
            };
        }
    }
}
