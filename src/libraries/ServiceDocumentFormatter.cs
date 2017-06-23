// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.ServiceModel.Syndication
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Xml;

    [TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    [DataContract]
    public abstract class ServiceDocumentFormatter
    {
        private ServiceDocument _document;

        protected ServiceDocumentFormatter()
        {
        }
        protected ServiceDocumentFormatter(ServiceDocument documentToWrite)
        {
            if (documentToWrite == null)
            {
                throw new ArgumentNullException("documentToWrite");
            }
            _document = documentToWrite;
        }

        public ServiceDocument Document
        {
            get { return _document; }
        }

        public abstract string Version
        { get; }

        public abstract bool CanRead(XmlReader reader);
        public abstract void ReadFrom(XmlReader reader);
        public abstract void WriteTo(XmlWriter writer);

        internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, CategoriesDocument categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException("categories");
            }
            Atom10FeedFormatter.CloseBuffer(buffer, writer);
            categories.LoadElementExtensions(buffer);
        }

        internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, ResourceCollectionInfo collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            Atom10FeedFormatter.CloseBuffer(buffer, writer);
            collection.LoadElementExtensions(buffer);
        }

        internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, Workspace workspace)
        {
            if (workspace == null)
            {
                throw new ArgumentNullException("workspace");
            }
            Atom10FeedFormatter.CloseBuffer(buffer, writer);
            workspace.LoadElementExtensions(buffer);
        }

        internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, ServiceDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            Atom10FeedFormatter.CloseBuffer(buffer, writer);
            document.LoadElementExtensions(buffer);
        }

        protected static SyndicationCategory CreateCategory(InlineCategoriesDocument inlineCategories)
        {
            if (inlineCategories == null)
            {
                throw new ArgumentNullException("inlineCategories");
            }
            return inlineCategories.CreateCategory();
        }

        protected static ResourceCollectionInfo CreateCollection(Workspace workspace)
        {
            if (workspace == null)
            {
                throw new ArgumentNullException("workspace");
            }
            return workspace.CreateResourceCollection();
        }

        protected static InlineCategoriesDocument CreateInlineCategories(ResourceCollectionInfo collection)
        {
            return collection.CreateInlineCategoriesDocument();
        }

        protected static ReferencedCategoriesDocument CreateReferencedCategories(ResourceCollectionInfo collection)
        {
            return collection.CreateReferencedCategoriesDocument();
        }

        protected static Workspace CreateWorkspace(ServiceDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            return document.CreateWorkspace();
        }

        protected static void LoadElementExtensions(XmlReader reader, CategoriesDocument categories, int maxExtensionSize)
        {
            if (categories == null)
            {
                throw new ArgumentNullException("categories");
            }
            categories.LoadElementExtensions(reader, maxExtensionSize);
        }

        protected static void LoadElementExtensions(XmlReader reader, ResourceCollectionInfo collection, int maxExtensionSize)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            collection.LoadElementExtensions(reader, maxExtensionSize);
        }

        protected static void LoadElementExtensions(XmlReader reader, Workspace workspace, int maxExtensionSize)
        {
            if (workspace == null)
            {
                throw new ArgumentNullException("workspace");
            }
            workspace.LoadElementExtensions(reader, maxExtensionSize);
        }

        protected static void LoadElementExtensions(XmlReader reader, ServiceDocument document, int maxExtensionSize)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            document.LoadElementExtensions(reader, maxExtensionSize);
        }

        protected static bool TryParseAttribute(string name, string ns, string value, ServiceDocument document, string version)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            return document.TryParseAttribute(name, ns, value, version);
        }

        protected static bool TryParseAttribute(string name, string ns, string value, ResourceCollectionInfo collection, string version)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return collection.TryParseAttribute(name, ns, value, version);
        }

        protected static bool TryParseAttribute(string name, string ns, string value, CategoriesDocument categories, string version)
        {
            if (categories == null)
            {
                throw new ArgumentNullException("categories");
            }
            return categories.TryParseAttribute(name, ns, value, version);
        }

        protected static bool TryParseAttribute(string name, string ns, string value, Workspace workspace, string version)
        {
            if (workspace == null)
            {
                throw new ArgumentNullException("workspace");
            }
            return workspace.TryParseAttribute(name, ns, value, version);
        }

        protected static bool TryParseElement(XmlReader reader, ResourceCollectionInfo collection, string version)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return collection.TryParseElement(reader, version);
        }

        protected static bool TryParseElement(XmlReader reader, ServiceDocument document, string version)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            return document.TryParseElement(reader, version);
        }

        protected static bool TryParseElement(XmlReader reader, Workspace workspace, string version)
        {
            if (workspace == null)
            {
                throw new ArgumentNullException("workspace");
            }
            return workspace.TryParseElement(reader, version);
        }

        protected static bool TryParseElement(XmlReader reader, CategoriesDocument categories, string version)
        {
            if (categories == null)
            {
                throw new ArgumentNullException("categories");
            }
            return categories.TryParseElement(reader, version);
        }

        protected static void WriteAttributeExtensions(XmlWriter writer, ServiceDocument document, string version)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            document.WriteAttributeExtensions(writer, version);
        }

        protected static void WriteAttributeExtensions(XmlWriter writer, Workspace workspace, string version)
        {
            if (workspace == null)
            {
                throw new ArgumentNullException("workspace");
            }
            workspace.WriteAttributeExtensions(writer, version);
        }

        protected static void WriteAttributeExtensions(XmlWriter writer, ResourceCollectionInfo collection, string version)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            collection.WriteAttributeExtensions(writer, version);
        }

        protected static void WriteAttributeExtensions(XmlWriter writer, CategoriesDocument categories, string version)
        {
            if (categories == null)
            {
                throw new ArgumentNullException("categories");
            }
            categories.WriteAttributeExtensions(writer, version);
        }

        protected static void WriteElementExtensions(XmlWriter writer, ServiceDocument document, string version)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            document.WriteElementExtensions(writer, version);
        }

        protected static void WriteElementExtensions(XmlWriter writer, Workspace workspace, string version)
        {
            if (workspace == null)
            {
                throw new ArgumentNullException("workspace");
            }
            workspace.WriteElementExtensions(writer, version);
        }

        protected static void WriteElementExtensions(XmlWriter writer, ResourceCollectionInfo collection, string version)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            collection.WriteElementExtensions(writer, version);
        }

        protected static void WriteElementExtensions(XmlWriter writer, CategoriesDocument categories, string version)
        {
            if (categories == null)
            {
                throw new ArgumentNullException("categories");
            }
            categories.WriteElementExtensions(writer, version);
        }

        protected virtual ServiceDocument CreateDocumentInstance()
        {
            return new ServiceDocument();
        }

        protected virtual void SetDocument(ServiceDocument document)
        {
            _document = document;
        }
    }
}
