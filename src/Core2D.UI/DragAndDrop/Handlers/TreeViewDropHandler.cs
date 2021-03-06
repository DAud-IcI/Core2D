﻿using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using Core2D.Containers;
using Core2D.Editor;

namespace Core2D.UI.DragAndDrop.Handlers
{
    /// <summary>
    /// Project tree view drop handler.
    /// </summary>
    public class ProjectTreeViewDropHandler : DefaultDropHandler
    {
        private bool ValidateContainer(TreeView treeView, DragEventArgs e, object sourceContext, object targetContext, bool bExecute)
        {
            if (!(sourceContext is IBaseContainer sourceItem)
                || !(targetContext is IProjectContainer)
                || !(treeView.GetVisualAt(e.GetPosition(treeView)) is IControl targetControl)
                || !(treeView.GetVisualRoot() is IControl rootControl)
                || !(rootControl.DataContext is IProjectEditor editor)
                || !(targetControl.DataContext is IBaseContainer targetItem))
            {
                return false;
            }

            Debug.WriteLine($"{sourceItem} -> {targetItem}");

            switch (sourceItem)
            {
                case ILayerContainer sourceLayer:
                    {
                        switch (targetItem)
                        {
                            case ILayerContainer targetLayer:
                                {
                                    if (bExecute)
                                    {
                                        // TODO:
                                    }
                                    return true;
                                }
                            case IPageContainer targetPage:
                                {
                                    if (e.DragEffects == DragDropEffects.Copy)
                                    {
                                        if (bExecute)
                                        {
                                            var layer = editor?.Clone(sourceLayer);
                                            editor?.Project.AddLayer(targetPage, layer);
                                        }
                                        return true;
                                    }
                                    else if (e.DragEffects == DragDropEffects.Move)
                                    {
                                        if (bExecute)
                                        {
                                            editor?.Project?.RemoveLayer(sourceLayer);
                                            editor?.Project.AddLayer(targetPage, sourceLayer);
                                        }
                                        return true;
                                    }
                                    else if (e.DragEffects == DragDropEffects.Link)
                                    {
                                        if (bExecute)
                                        {
                                            editor?.Project.AddLayer(targetPage, sourceLayer);
                                            e.DragEffects = DragDropEffects.None;
                                        }
                                        return true;
                                    }
                                    return false;
                                }
                            case IDocumentContainer targetDocument:
                                {
                                    return false;
                                }
                        }

                        return false;
                    }
                case IPageContainer sourcePage:
                    {
                        switch (targetItem)
                        {
                            case ILayerContainer targetLayer:
                                {
                                    return false;
                                }
                            case IPageContainer targetPage:
                                {
                                    if (bExecute)
                                    {
                                        // TODO:
                                    }
                                    return true;
                                }
                            case IDocumentContainer targetDocument:
                                {
                                    if (e.DragEffects == DragDropEffects.Copy)
                                    {
                                        if (bExecute)
                                        {
                                            var page = editor?.Clone(sourcePage);
                                            editor?.Project.AddPage(targetDocument, page);
                                            editor?.Project?.SetCurrentContainer(page);
                                        }
                                        return true;
                                    }
                                    else if (e.DragEffects == DragDropEffects.Move)
                                    {
                                        if (bExecute)
                                        {
                                            editor?.Project?.RemovePage(sourcePage);
                                            editor?.Project.AddPage(targetDocument, sourcePage);
                                            editor?.Project?.SetCurrentContainer(sourcePage);
                                        }
                                        return true;
                                    }
                                    else if (e.DragEffects == DragDropEffects.Link)
                                    {
                                        if (bExecute)
                                        {
                                            editor?.Project.AddPage(targetDocument, sourcePage);
                                            editor?.Project?.SetCurrentContainer(sourcePage);
                                        }
                                        return true;
                                    }
                                    return false;
                                }
                        }

                        return false;
                    }
                case IDocumentContainer sourceDocument:
                    {
                        switch (targetItem)
                        {
                            case ILayerContainer targetLayer:
                                {
                                    return false;
                                }
                            case IPageContainer targetPage:
                                {
                                    return false;
                                }
                            case IDocumentContainer targetDocument:
                                {
                                    if (bExecute)
                                    {
                                        // TODO:
                                    }
                                    return true;
                                }
                        }
                        return false;
                    }
            }

            return false;
        }

        /// <inheritdoc/>
        public override bool Validate(object sender, DragEventArgs e, object sourceContext, object targetContext, object state)
        {
            if (e.Source is IControl && sender is TreeView treeView)
            {
                return ValidateContainer(treeView, e, sourceContext, targetContext, false);
            }
            return false;
        }

        /// <inheritdoc/>
        public override bool Execute(object sender, DragEventArgs e, object sourceContext, object targetContext, object state)
        {
            if (e.Source is IControl && sender is TreeView treeView)
            {
                return ValidateContainer(treeView, e, sourceContext, targetContext, true);
            }
            return false;
        }
    }
}
