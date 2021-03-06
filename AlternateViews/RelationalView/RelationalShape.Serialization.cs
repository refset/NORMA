using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.VisualStudio.Modeling.Diagrams;
using ORMSolutions.ORMArchitect.Framework.Design;
using System.ComponentModel;
using ORMSolutions.ORMArchitect.Core.ObjectModel;
using Microsoft.VisualStudio.Modeling;
using ORMSolutions.ORMArchitect.RelationalModels.ConceptualDatabase;
using ORMSolutions.ORMArchitect.ORMAbstraction;
using ORMSolutions.ORMArchitect.ORMAbstractionToConceptualDatabaseBridge;
using ORMSolutions.ORMArchitect.ORMToORMAbstractionBridge;
using ORMSolutions.ORMArchitect.Framework.Shell;

namespace ORMSolutions.ORMArchitect.Views.RelationalView
{
	partial class RelationalDiagram : IXmlSerializable
	{
		/// <summary>
		/// The local name of a TableShape element.
		/// </summary>
		private const string TableShapeElementName = "TableShape";
		/// <summary>
		/// The local name of an ObjectTypeRef attribute.
		/// </summary>
		private const string ObjectTypeRefAttributeName = "ObjectTypeRef";
		/// <summary>
		/// The local name of a Location attribute.
		/// </summary>
		private const string LocationAttributeName = "Location";
		/// <summary>
		/// The local name of an Id attribute.
		/// </summary>
		private const string IdAttributeName = "id";
		/// <summary>
		/// The local name of a SubjectRef attribute
		/// </summary>
		private const string SubjectRefAttributeName = "SubjectRef";
		/// <summary>
		/// The local name of a RelationalDiagram element.
		/// </summary>
		private const string RelationalDiagramElementName = "RelationalDiagram";
		/// <summary>
		/// DisplayDataTypes attribute name.
		/// </summary>
		private const string DisplayDataTypesAttributeName = "DisplayDataTypes";
		/// <summary>
		/// Gets the <see cref="T:System.Xml.Schema.XmlSchema"/> that the custom serialization should conform to.
		/// </summary>
		/// <returns><see langword="null" />.</returns>
		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}
		/// <summary>
		/// Processes the XML that this <see cref="T:ORMSolutions.ORMArchitect.Views.RelationalView.RelationalDiagram"/> has
		/// written.
		/// </summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader"/> that will read the XML associated with the
		/// serialization of this object.</param>
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string rvNamespace = RelationalShapeDomainModel.XmlNamespace;

			Dictionary<object, object> context = Store.TransactionManager.CurrentTransaction.TopLevelTransaction.Context.ContextInfo;
			ISerializationContext serializationContext = ((ISerializationContextHost)Store).SerializationContext;
			object tablePositionsObject;
			Dictionary<Guid, PointD> tablePositions;
			if (!context.TryGetValue(TablePositionDictionaryKey, out tablePositionsObject) ||
				(tablePositions = tablePositionsObject as Dictionary<Guid, PointD>) == null)
			{
				context[TablePositionDictionaryKey] = tablePositions = new Dictionary<Guid, PointD>();
			}
			while (reader.NodeType != XmlNodeType.Element && reader.Read()) ;
			if (Subject == null)
			{
				serializationContext.RealizeElementLink(
					null,
					this,
					serializationContext.RealizeElement(reader.GetAttribute(SubjectRefAttributeName), Catalog.DomainClassId, true),
					PresentationViewsSubject.SubjectDomainRoleId,
					null);
			}
			this.DisplayDataTypes = XmlConvert.ToBoolean(reader.GetAttribute(DisplayDataTypesAttributeName));

			TypeConverter pointConverter = TypeDescriptor.GetConverter(typeof(PointD));
			while (reader.Read())
			{
				string objectType = null;
				PointD? location = null;
				if (reader.NodeType == XmlNodeType.Element)
				{
					if (reader.LocalName == TableShapeElementName)
					{
						while (reader.MoveToNextAttribute())
						{
							if (reader.LocalName == ObjectTypeRefAttributeName)
							{
								objectType = reader.Value;
							}
							else if (reader.LocalName == LocationAttributeName)
							{
								location = (PointD)pointConverter.ConvertFromInvariantString(reader.Value);
							}
						}
						if (objectType == null || location == null)
						{
							throw new InvalidOperationException();
						}
						tablePositions[serializationContext.ResolveElementIdentifier(objectType)] = location.Value;
					}
				}
			}
		}
		/// <summary>
		/// Serializes this <see cref="T:ORMSolutions.ORMArchitect.Views.RelationalView.RelationalDiagram"/>.
		/// </summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter"/> that will write the custom serialization contents.</param>
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			string rvNamespace = RelationalShapeDomainModel.XmlNamespace;
			// <RelationalDiagram>
			//    <TableShape ObjectTypeRef="" Location="x, y" />
			//    ...
			// </RelationalDiagram>
			ISerializationContext serializationContext = ((ISerializationContextHost)Store).SerializationContext;
			writer.WriteStartElement(RelationalDiagramElementName, rvNamespace);
			writer.WriteAttributeString(IdAttributeName, serializationContext.GetIdentifierString(Id));
			writer.WriteAttributeString(DisplayDataTypesAttributeName, XmlConvert.ToString(DisplayDataTypes));
			writer.WriteAttributeString(SubjectRefAttributeName, serializationContext.GetIdentifierString(this.ModelElement.Id));
			TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(PointD));
			foreach (ShapeElement shapeElement in this.NestedChildShapes)
			{
				TableShape tableShape = shapeElement as TableShape;
				if (tableShape != null)
				{
					ConceptType conceptType;
					ObjectType objectType;
					if (null != (conceptType = TableIsPrimarilyForConceptType.GetConceptType((Table)tableShape.ModelElement)) &&
						null != (objectType = ConceptTypeIsForObjectType.GetObjectType(conceptType)))
					{
						writer.WriteStartElement(TableShapeElementName, rvNamespace);
						writer.WriteAttributeString(ObjectTypeRefAttributeName, serializationContext.GetIdentifierString(objectType.Id));
						writer.WriteAttributeString(LocationAttributeName, typeConverter.ConvertToInvariantString(tableShape.Location));
						writer.WriteEndElement();
					}
				}
			}
			writer.WriteEndElement();
		}
	}
}
