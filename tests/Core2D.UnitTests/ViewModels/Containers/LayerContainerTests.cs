﻿using Core2D;
using Core2D.Containers;
using Xunit;

namespace Core2D.UnitTests
{
    public class LayerContainerTests
    {
        private readonly IFactory _factory = new Factory();

        [Fact]
        [Trait("Core2D.Containers", "Project")]
        public void Inherits_From_ObservableObject()
        {
            var target = _factory.CreateLayerContainer();
            Assert.True(target is IObservableObject);
        }

        [Fact]
        [Trait("Core2D.Containers", "Project")]
        public void Shapes_Not_Null()
        {
            var target = _factory.CreateLayerContainer();
            Assert.False(target.Shapes.IsDefault);
        }

        [Fact]
        [Trait("Core2D.Containers", "Project")]
        public void Setting_IsVisible_Should_Invalidate_Layer()
        {
            var target = _factory.CreateLayerContainer("Layer1");

            bool raised = false;

            target.InvalidateLayer += (sender, e) =>
            {
                raised = true;
            };

            target.IsVisible = false;

            Assert.True(raised);
        }

        [Fact]
        [Trait("Core2D.Containers", "Project")]
        public void Invalidate_Raises_InvalidateLayer_Event()
        {
            var target = _factory.CreateLayerContainer("Layer1");

            bool raised = false;

            target.InvalidateLayer += (sender, e) =>
            {
                raised = true;
            };

            target.Invalidate();

            Assert.True(raised);
        }

        [Fact]
        [Trait("Core2D.Containers", "Project")]
        public void Invalidate_Sets_EventArgs()
        {
            var target = _factory.CreateLayerContainer("Layer1");

            InvalidateLayerEventArgs args = null;

            target.InvalidateLayer += (sender, e) =>
            {
                args = e;
            };

            target.Invalidate();

            Assert.NotNull(args);
            Assert.True(args is InvalidateLayerEventArgs);
        }
    }
}
