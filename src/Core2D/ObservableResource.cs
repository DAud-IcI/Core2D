﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Immutable;
using Core2D.Attributes;

namespace Core2D
{
    /// <summary>
    /// Observable resources base class.
    /// </summary>
    public abstract class ObservableResource : ObservableObject
    {
        private ImmutableArray<ObservableObject> _resources;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableResource"/> class.
        /// </summary>
        public ObservableResource() : base() => Resources = ImmutableArray.Create<ObservableObject>();

        /// <summary>
        /// Gets or sets shape resources.
        /// </summary>
        [Content]
        public ImmutableArray<ObservableObject> Resources
        {
            get => _resources;
            set => Update(ref _resources, value);
        }

        /// <summary>
        /// Check whether the <see cref="Resources"/> property has changed from its default value.
        /// </summary>
        /// <returns>Returns true if the property has changed; otherwise, returns false.</returns>
        public bool ShouldSerializeResources() => _resources.IsEmpty == false;
    }
}
