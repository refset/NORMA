#region Common Public License Copyright Notice
/**************************************************************************\
* Neumont Object-Role Modeling Architect for Visual Studio                 *
*                                                                          *
* Copyright � Neumont University. All rights reserved.                     *
*                                                                          *
* The use and distribution terms for this software are covered by the      *
* Common Public License 1.0 (http://opensource.org/licenses/cpl) which     *
* can be found in the file CPL.txt at the root of this distribution.       *
* By using this software in any fashion, you are agreeing to be bound by   *
* the terms of this license.                                               *
*                                                                          *
* You must not remove this notice, or any other, from this software.       *
\**************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling;

namespace Neumont.Tools.Modeling.Shell.DynamicSurveyTreeGrid
{
	/// <summary>
	/// wrapper for objects to be dispalyed in the Survey Tree
	/// </summary>
	public struct SampleDataElementNode : ISurveyName, IEquatable<SampleDataElementNode>
	{
		/// <summary>
		/// public constructor
		/// </summary>
		public SampleDataElementNode(object element)
			: this(element, 0)
		{
		}
		/// <summary>
		/// public constructor
		/// </summary>
		public SampleDataElementNode(object element, int index)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			this.myElement = element;
			this.myIndex = index;
			this.myName = element as ISurveyName;
			this.myNodeData = 0;
		}
		private readonly object myElement;
		private readonly ISurveyName myName;
		/// <summary>
		/// returns object wrapped by this node
		/// </summary>
		public object Element
		{
			get
			{
				return myElement;
			}
		}
		private int myIndex;
		/// <summary>
		/// returns nodes index in container, must be set by container to work
		/// </summary>
		public int Index
		{
			get 
			{ 
				return myIndex; 
			}
			set 
			{
				myIndex = value;
			}
		}
		private int myNodeData;
		/// <summary>
		/// gets or sets the integer that holds the answers to all questions in myNodeCachedQuestions
		/// </summary>
		public int NodeData
		{
			get
			{
				return myNodeData;
			}
			set
			{
				myNodeData = value;
			}
		}

		#region ISurveyName property wrappers
		/// <summary>
		/// returns name of the wrapped element
		/// </summary>
		public string SurveyName
		{
			get
			{
				if (myName != null)
				{
					return myName.SurveyName;
				}
				return myElement.ToString();
			}
		}
		/// <summary>
		/// for edit mode returns the name the user can change
		/// </summary>
		public string EditableSurveyName
		{
			get
			{
				if (myName != null)
				{
					return myName.EditableSurveyName;
				}
				return myElement.ToString();
			}
			set
			{
				if (myName != null)
				{
					myName.EditableSurveyName = value;
				}
			}
		}
		/// <summary>
		/// returns whether or not this object can be edited.
		/// </summary>
		public bool IsEditable
		{
			get
			{
				return myName != null && myName.IsEditable;
			}
		}
		#endregion

		#region Infrastructure Methods

		/// <summary>See <see cref="Object.GetHashCode"/>.</summary>
		public override int GetHashCode()
		{
			return ((this.myElement != null) ? this.myElement.GetHashCode() : 0) ^ this.myIndex ^ this.myNodeData; 
		}
		/// <summary>See <see cref="Object.Equals(Object)"/>.</summary>
		public override bool Equals(object obj)
		{
			return obj is SampleDataElementNode && this.Equals((SampleDataElementNode)obj);
		}
		/// <summary>See <see cref="IEquatable{SampleDataElementNode}.Equals"/>.</summary>
		public bool Equals(SampleDataElementNode other)
		{
			return this.myElement == other.myElement && this.myIndex == other.myIndex && this.myNodeData == other.myNodeData;
		}
		/// <summary>
		/// Returns whether <param name="left"/> is equal to <param name="right"/>, based on the
		/// <see cref="Equals(SampleDataElementNode)"/> method.
		/// </summary>
		public static bool operator ==(SampleDataElementNode left, SampleDataElementNode right)
		{
			return left.Equals(right);
		}
		/// <summary>
		/// Returns whether <param name="left"/> is not equal to <param name="right"/>, based on the
		/// <see cref="Equals(SampleDataElementNode)"/> method.
		/// </summary>
		public static bool operator !=(SampleDataElementNode left, SampleDataElementNode right)
		{
			return !left.Equals(right);
		}

		#endregion // Infrastructure Methods
	}
}