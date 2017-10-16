﻿using System;
using System.Drawing;
using ORMSolutions.ORMArchitect.Core.ObjectModel;
using ORMSolutions.ORMArchitect.Core.ShapeModel;
using ORMSolutions.ORMArchitect.Framework.Diagrams;
using ORMSolutions.ORMArchitect.Framework;
using Microsoft.VisualStudio.Modeling;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;

namespace unibz.ORMInferenceEngine
{
	#region NamedElementDictionary and DuplicateNameError integration
    partial class InferredHierarchy : INamedElementDictionaryParent, IAlternateElementOwner<ObjectType>
	{
		#region INamedElementDictionaryParentNode implementation
		[NonSerialized]
		private NamedElementDictionary myInferredHierarchyDictionary;
		/// <summary>
		/// A <see cref="INamedElementDictionary"/> for retrieving any inferred hierarchy by name.
		/// </summary>
		public INamedElementDictionary InferredHierarchyDictionary
		{
			get
			{
				INamedElementDictionary retVal = myInferredHierarchyDictionary;
				if (retVal == null)
				{
					retVal = myInferredHierarchyDictionary = new InferredHierarchyNamedElementDictionary();
				}
				return retVal;
			}
		}
		INamedElementDictionary INamedElementDictionaryParent.GetCounterpartRoleDictionary(Guid parentDomainRoleId, Guid childDomainRoleId)
		{
            if (parentDomainRoleId == TopLevelObjectType.InferredHierarchyDomainRoleId)
			{
				return InferredHierarchyDictionary;
			}
			return null;
		}
		object INamedElementDictionaryParent.GetAllowDuplicateNamesContextKey(Guid parentDomainRoleId, Guid childDomainRoleId)
		{
			return NamedElementDictionary.BlockDuplicateNamesKey;
		}
		#endregion // INamedElementDictionaryParentNode implementation
		#region Relationship-specific NamedElementDictionary implementations
		#region InferredHierarchyNamedElementDictionary class
		/// <summary>
		/// Dictionary used to set the initial names of constraints and to
		/// generate model validation errors and exceptions for duplicate
		/// element names.
		/// </summary>
		private class InferredHierarchyNamedElementDictionary : NamedElementDictionary
		{
			private sealed class DuplicateNameManager : IDuplicateNameCollectionManager
			{
				#region IDuplicateNameCollectionManager Implementation
				ICollection IDuplicateNameCollectionManager.OnDuplicateElementAdded(ICollection elementCollection, ModelElement element, bool afterTransaction, INotifyElementAdded notifyAdded)
				{
					// Nothing to do, we're blocking duplicate elements from being added for now
					return elementCollection;
				}
				ICollection IDuplicateNameCollectionManager.OnDuplicateElementRemoved(ICollection elementCollection, ModelElement element, bool afterTransaction)
				{
					return elementCollection;
				}
                void IDuplicateNameCollectionManager.AfterCollectionRollback(ICollection collection)
                {
                    // No idea what does it mean and what it is useful for... 
                    //TrackingList trackingList;
                    //if (null != (trackingList = collection as TrackingList))
                    //{
                    //    trackingList.Clear();
                    //    foreach (ElementGrouping grouping in trackingList.NativeCollection)
                    //    {
                    //        trackingList.Add(grouping);
                    //    }
                    //}
                }
                #endregion // IDuplicateNameCollectionManager Implementation
			}
			#region Constructors
			/// <summary>
			/// Default constructor for ConstraintNamedElementDictionary
			/// </summary>
			public InferredHierarchyNamedElementDictionary()
				: base(new DuplicateNameManager())
			{
			}
			#endregion // Constructors
			#region Base overrides
			/// <summary>
			/// Raise an exception with text specific to a name in a model
			/// </summary>
			/// <param name="element">Element we're attempting to name</param>
			/// <param name="requestedName">The in-use requested name</param>
			protected override void ThrowDuplicateNameException(ModelElement element, string requestedName)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The inferred hierarchy '{0}' is already defined in this model.", requestedName));
			}
			#endregion // Base overrides
		}
		#endregion // InferredHierarchyNamedElementDictionary class
		#endregion // Relationship-specific NamedElementDictionary implementations
        #region IAlternateElementOwner<TopLevelObjectType> Implementation
        IEnumerable<ObjectType> IAlternateElementOwner<ObjectType>.OwnedElements
		{
			get
			{
				return this.TopObjectTypeCollection;
			}
		}
        bool IAlternateElementOwner<ObjectType>.ValidateErrorFor(ObjectType element, Type modelErrorType)
		{
			return false;
		}
        DomainClassInfo IAlternateElementOwner<ObjectType>.GetOwnedElementClassInfo(Type elementType)
		{
			Type alternateType = null;
            if (elementType == typeof(ObjectType))
			{
                alternateType = typeof(InferredTopObjectType);
			}
			return (alternateType != null) ? Store.DomainDataDirectory.GetDomainClass(alternateType) : null;
        }
        #endregion // IAlternateElementOwner<ObjectType> Implementation
    }

    partial class TopLevelObjectType : INamedElementDictionaryLink
	{
		#region INamedElementDictionaryLink implementation
		/// <summary>
		/// Implements INamedElementDictionaryLink.ParentRolePlayer
		/// Returns Model.
		/// </summary>
		INamedElementDictionaryParentNode INamedElementDictionaryLink.ParentRolePlayer
		{
			get { return InferredHierarchy; }
		}
		/// <summary>
		/// Implements INamedElementDictionaryLink.ChildRolePlayer
		/// Returns SetComparisonConstraintCollection.
		/// </summary>
		INamedElementDictionaryChildNode INamedElementDictionaryLink.ChildRolePlayer
		{
			get { return ObjectType; }
		}
		/// <summary>
		/// Implements <see cref="INamedElementDictionaryLink.DictionaryLinkUse"/>.
		/// This link is used both directly for object type names in the model,
		/// and indirectly for value constraint names on value types.
		/// </summary>
		NamedElementDictionaryLinkUse INamedElementDictionaryLink.DictionaryLinkUse
		{
			get
			{
				return NamedElementDictionaryLinkUse.DirectDictionary | NamedElementDictionaryLinkUse.DictionaryConnector;
			}
		}
        //INamedElementDictionaryRemoteParent INamedElementDictionaryLink.RemoteParentRolePlayer
        //{
        //    get { return RemoteParentRolePlayer; }
        //}
        ///// <summary>
        ///// Implements INamedElementDictionaryLink.RemoteParentRolePlayer
        ///// Returns null.
        ///// </summary>
        //protected static INamedElementDictionaryRemoteParent RemoteParentRolePlayer
        //{
        //    get { return null; }
        //}
		#endregion // INamedElementDictionaryLink implementation
	}

    partial class InferredTopObjectType : INamedElementDictionaryChild, IHasAlternateOwner<ObjectType>
	{
		#region INamedElementDictionaryChild implementation
		void INamedElementDictionaryChild.GetRoleGuids(out Guid parentDomainRoleId, out Guid childDomainRoleId)
		{
			GetRoleGuids(out parentDomainRoleId, out childDomainRoleId);
		}
		/// <summary>
		/// Implementation of INamedElementDictionaryChild.GetRoleGuids. Identifies
		/// this child as participating in the 'SetComparisonConstraintIsInferred' naming set.
		/// </summary>
		/// <param name="parentDomainRoleId">Guid</param>
		/// <param name="childDomainRoleId">Guid</param>
		protected static new void GetRoleGuids(out Guid parentDomainRoleId, out Guid childDomainRoleId)
		{
            parentDomainRoleId = TopLevelObjectType.InferredHierarchyDomainRoleId;
            childDomainRoleId = TopLevelObjectType.ObjectTypeDomainRoleId;
		}
		#endregion // INamedElementDictionaryChild implementation
        #region IHasAlternateOwner<ObjectType> Implementation
        /// <summary>
        /// Implements <see cref="IHasAlternateOwner{ObjectType}.AlternateOwner"/>
		/// </summary>
		protected IAlternateElementOwner<ObjectType> AlternateOwner
		{
			get
			{
                return TopLevelObjectType.GetInferredHierarchy(this);
			}
			set
			{
				InferredHierarchy parent;
				if (null != (parent = value as InferredHierarchy) &&
					AlternateOwner == null)
				{
                    new TopLevelObjectType(parent, this);
				}
			}
		}
        IAlternateElementOwner<ObjectType> IHasAlternateOwner<ObjectType>.AlternateOwner
		{
			get
			{
				return AlternateOwner;
			}
			set
			{
				AlternateOwner = value;
			}
		}
		object IHasAlternateOwner.UntypedAlternateOwner
		{
			get
			{
				return AlternateOwner;
			}
        }
        #endregion // IHasAlternateOwner<ObjectType> Implementation
    }
    #endregion // NamedElementDictionary integration
}