#region Common Public License Copyright Notice
/**************************************************************************\
* Natural Object-Role Modeling Architect for Visual Studio                 *
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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.VirtualTreeGrid;
using System.Reflection;
using ORMSolutions.ORMArchitect.Framework.Shell;

#if VISUALSTUDIO_9_0
using VirtualTreeInPlaceControlFlags = Microsoft.VisualStudio.VirtualTreeGrid.VirtualTreeInPlaceControls;
#endif //VISUALSTUDIO_9_0

namespace ORMSolutions.ORMArchitect.Framework.Design
{
	/// <summary>
	/// A base class used to display a simple list of elements
	/// the property grid. Derived classes override the GetContentList
	/// method to return items, and alternately the LastControlSize and
	/// NullItemText getters to control the list contents.
	/// </summary>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class ElementPicker<T> : SizePreservingEditor<T>
		where T : ElementPicker<T>
	{
		#region DropDownTreeControl class. Handles the Escape key for the dropdown
		private sealed class DropDownTreeControl : StandardVirtualTreeControl, INotifyEscapeKeyPressed
		{
			private EventHandler myEscapePressed;
			private int myInitialIndex = -1;
			public event DoubleClickEventHandler AfterDoubleClick;
			public DropDownTreeControl()
			{
				HasRootLines = false;
				FullCellSelect = true;
			}
			public int InitialSelectionIndex
			{
				set
				{
					myInitialIndex = value;
				}
			}
			protected sealed override CreateParams CreateParams
			{
				[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					CreateParams @params = base.CreateParams;
					@params.ExStyle &= ~0x200; // Turn off Fixed3D border style
					return @params;
				}
			}
			/// <summary>
			/// Set an initial selection in the control
			/// </summary>
			protected override int InitialIndex
			{
				get
				{
					return myInitialIndex;
				}
			}
			protected sealed override void OnDoubleClick(DoubleClickEventArgs e)
			{
				base.OnDoubleClick(e);
				if (AfterDoubleClick != null)
				{
					AfterDoubleClick(this, e);
				}
			}
			/// <summary>
			/// Track the escape key before the control closes
			/// </summary>
			protected sealed override bool IsInputKey(Keys keyData)
			{
				if ((keyData & Keys.KeyCode) == Keys.Escape)
				{
					EventHandler escapePressed;
					if (null != (escapePressed = myEscapePressed))
					{
						escapePressed(this, EventArgs.Empty);
					}
				}
				return base.IsInputKey(keyData);
			}
			event EventHandler INotifyEscapeKeyPressed.EscapePressed
			{
				add { myEscapePressed += value; }
				remove { myEscapePressed -= value; }
			}
		}
		#endregion DropDownTreeControl class. Handles the Escape key for the dropdown
		#region SimpleBranch class
		private class SimpleBranch : IBranch
		{
			#region Member Variables
			private IList myItems;
			private string myNullItemText;
			private ElementPicker<T> myPicker;
			#endregion // Member Variables
			#region Constructor
			public SimpleBranch(ElementPicker<T> picker, IList items, string nullItemText)
			{
				myPicker = picker;
				myItems = items;
				myNullItemText = nullItemText;
			}
			#endregion // Constructor
			#region IBranch Implementation
			string IBranch.GetText(int row, int column)
			{
				string nullItemText = myNullItemText;
				if (nullItemText != null)
				{
					if (row == 0)
					{
						return nullItemText;
					}
					--row;
				}
				return myPicker.GetDisplayString(myItems[row]);
			}
			int IBranch.VisibleItemCount
			{
				get
				{
					int retVal = 0;
					IList items = myItems;
					if (items != null)
					{
						retVal += items.Count;
					}
					if (myNullItemText != null)
					{
						retVal += 1;
					}
					return retVal;
				}
			}
			#region Irrelevant methods
			BranchFeatures IBranch.Features
			{
				get { return BranchFeatures.None; }
			}
			VirtualTreeLabelEditData IBranch.BeginLabelEdit(int row, int column, VirtualTreeLabelEditActivationStyles activationStyle)
			{
				return VirtualTreeLabelEditData.Invalid;
			}
			LabelEditResult IBranch.CommitLabelEdit(int row, int column, string newText)
			{
				return LabelEditResult.CancelEdit;
			}
			VirtualTreeAccessibilityData IBranch.GetAccessibilityData(int row, int column)
			{
				return VirtualTreeAccessibilityData.Empty;
			}
			VirtualTreeDisplayData IBranch.GetDisplayData(int row, int column, VirtualTreeDisplayDataMasks requiredData)
			{
				return VirtualTreeDisplayData.Empty;
			}
			object IBranch.GetObject(int row, int column, ObjectStyle style, ref int options)
			{
				return null;
			}
			string IBranch.GetTipText(int row, int column, ToolTipType tipType)
			{
				return null;
			}
			bool IBranch.IsExpandable(int row, int column)
			{
				return false;
			}
			LocateObjectData IBranch.LocateObject(object obj, ObjectStyle style, int locateOptions)
			{
				return default(LocateObjectData);
			}
			event BranchModificationEventHandler IBranch.OnBranchModification
			{
				add { }
				remove { }
			}
			void IBranch.OnDragEvent(object sender, int row, int column, DragEventType eventType, DragEventArgs args)
			{
			}
			void IBranch.OnGiveFeedback(GiveFeedbackEventArgs args, int row, int column)
			{
			}
			void IBranch.OnQueryContinueDrag(QueryContinueDragEventArgs args, int row, int column)
			{
			}
			VirtualTreeStartDragData IBranch.OnStartDrag(object sender, int row, int column, DragReason reason)
			{
				return VirtualTreeStartDragData.Empty;
			}
			StateRefreshChanges IBranch.SynchronizeState(int row, int column, IBranch matchBranch, int matchRow, int matchColumn)
			{
				return StateRefreshChanges.None;
			}
			StateRefreshChanges IBranch.ToggleState(int row, int column)
			{
				return StateRefreshChanges.None;
			}
			int IBranch.UpdateCounter
			{
				get { return 0; }
			}
			#endregion // Irrelevant methods
			#endregion // IBranch Implementation
		}
		#endregion // SimpleBranch class
		#region UITypeEditor overrides
		/// <summary>
		/// Required UITypeEditor override. Opens dropdown modally
		/// and waits for user input.
		/// </summary>
		/// <param name="context">The descriptor context. Used to retrieve
		/// the live instance and other data.</param>
		/// <param name="provider">The service provider for the given context.</param>
		/// <param name="value">The current property value</param>
		/// <returns>The updated property value, or the orignal value to effect a cancel</returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService editor = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
			if (editor != null)
			{
				object newObject = value;
				// Get the list contents and add a null handler if needed
				IList elements = GetContentList(context, value);
				string nullText = NullItemText;
				// Proceed if there is anything to show
				if (nullText != null || (elements != null && elements.Count != 0))
				{
					// Create a tree control
					using (DropDownTreeControl treeControl = new DropDownTreeControl())
					{
#if VISUALSTUDIO_9_0 // MSBUG: Hack workaround crashing bug in VirtualTreeControl.OnToggleExpansion
						treeControl.ColumnPermutation = new ColumnPermutation(1, new int[]{0}, false);
#endif
						// Close the dropdown after a double click
						treeControl.AfterDoubleClick += delegate(object sender, DoubleClickEventArgs e)
						{
							if (e.Button == MouseButtons.Left)
							{
								editor.CloseDropDown();
							}
						};

						// Create a tree for the control
						ITree tree = new VirtualTree();
						tree.Root = new SimpleBranch(this, elements, nullText);
						treeControl.Tree = tree;

						// Manage the size of the control
						Size lastSize = LastControlSize;
						if (!lastSize.IsEmpty)
						{
							treeControl.Size = lastSize;
						}

						int initialIndex = -1;
						if (value != null)
						{
							if (elements != null)
							{
								initialIndex = elements.IndexOf(TranslateToDisplayObject(value, elements));
								if (nullText != null)
								{
									++initialIndex;
								}
							}
						}
						else if (nullText != null)
						{
							initialIndex = 0;
						}
						if (initialIndex != -1)
						{
							treeControl.InitialSelectionIndex = initialIndex;
						}
						Control adornedControl = SetTreeControlDisplayOptions(treeControl) ?? treeControl;
						bool escapePressed = false;
						EditorUtility.AttachEscapeKeyPressedEventHandler(
							adornedControl,
							delegate(object sender, EventArgs e)
							{
								escapePressed = true;
							});

						// Make sure keystrokes are not forwarded while the modal dropdown is open
						IVirtualTreeInPlaceControl virtualTreeInPlaceControl = editor as IVirtualTreeInPlaceControl;
						VirtualTreeInPlaceControlFlags flags = virtualTreeInPlaceControl != null ? virtualTreeInPlaceControl.Flags : 0;
						if (0 != (flags & VirtualTreeInPlaceControlFlags.ForwardKeyEvents))
						{
							virtualTreeInPlaceControl.Flags = flags & ~VirtualTreeInPlaceControlFlags.ForwardKeyEvents;
						}

						// Show the dropdown. This is modal.
						editor.DropDownControl(adornedControl);

						// Restore keystroke forwarding
						if (0 != (flags & VirtualTreeInPlaceControlFlags.ForwardKeyEvents))
						{
							virtualTreeInPlaceControl.Flags = flags;
						}

						// Record the final size, we'll use it next time for this type of control
						LastControlSize = treeControl.Size;

						// Make sure the user didn't cancel, and translate the null placeholder
						// back to null if necessary
						if (!escapePressed)
						{
							int lastIndex = treeControl.AnchorIndex;
							if (lastIndex != -1)
							{
								if (nullText != null)
								{
									--lastIndex;
									if (lastIndex == -1)
									{
										newObject = null;
									}
									else
									{
										newObject = elements[lastIndex];
									}
								}
								else
								{
									newObject = elements[lastIndex];
								}
								// Give the caller the chance to change the type of the chosen object
								newObject = TranslateFromDisplayObject(lastIndex, newObject);
							}
						}
					}
				}
				return newObject;
			}
			return value;
		}
		#endregion // UITypeEditor overrides
		#region ElementPicker Specifics
		/// <summary>
		/// Override and set a text value to allow a null element to be returned
		/// </summary>
		protected virtual string NullItemText
		{
			get
			{
				return null;
			}
		}
		/// <summary>
		/// Generate and return a list to display in the item picker. The initial
		/// value (if it is not null) must be included in the returned set. If null values
		/// are allowed, the NullItemText getter should also be overridden.
		/// </summary>
		/// <param name="context">ITypeDescriptorContext passed in by the system</param>
		/// <param name="value">The current value</param>
		/// <returns>A list. An empty list will</returns>
		protected abstract IList GetContentList(ITypeDescriptorContext context, object value);
		/// <summary>
		/// Give a derived class with an opportunity to change the
		/// chosen object from the dropdown list into a different type
		/// of object. This allows the passed in candidate list to contain
		/// objects that have a different type than the type of the property.
		/// TranslateFromDisplayObject is called when the user selects an item
		/// in the dropdown.
		/// </summary>
		/// <param name="newIndex">The index chosen from the list. If the null
		/// item was chosen, then the newIndex will be -1</param>
		/// <param name="newObject">The chosen object. Can be null.</param>
		/// <returns>Default implementation returns newObject</returns>
		protected virtual object TranslateFromDisplayObject(int newIndex, object newObject)
		{
			return newObject;
		}
		/// <summary>
		/// Give a derived class with an opportunity to change the
		/// chosen object from the dropdown list into a different type
		/// of object. This allows the passed in candidate list to contain
		/// objects that have a different type than the type of the property.
		/// TranslateToDisplayObject is called to determine the initial selection
		/// in the dropdown.
		/// </summary>
		/// <param name="initialObject">The starting value of the property</param>
		/// <param name="contentList">The list returned from GetContentList</param>
		/// <returns>Default implementation returns initialObject</returns>
		protected virtual object TranslateToDisplayObject(object initialObject, IList contentList)
		{
			return initialObject;
		}
		private static TypeConverter myStringConverter;
		/// <summary>
		/// Convert an object returned by <see cref="GetContentList"/> to
		/// a displayable string value.
		/// </summary>
		/// <param name="displayObject">An object from the content list.</param>
		/// <returns>A displayable string.</returns>
		protected virtual string GetDisplayString(object displayObject)
		{
			TypeConverter converter = myStringConverter;
			if (converter == null)
			{
				System.Threading.Interlocked.CompareExchange<TypeConverter>(ref myStringConverter, TypeDescriptor.GetConverter(typeof(string)), null);
				converter = myStringConverter;
			}
			return converter.ConvertToString(displayObject);
		}
		/// <summary>
		/// Set display options on the tree control. The list is implemented as
		/// a tree control with tree-style display settings turned off, so changes
		/// should be limited in most cases to setting tree headers
		/// </summary>
		/// <param name="treeControl">A <see cref="VirtualTreeControl"/></param>
		/// <returns>Optionally return an alternate control with adornments that contains the <paramref name="treeControl"/>.
		/// The size of the tree control should be maintained during this request.</returns>
		protected virtual Control SetTreeControlDisplayOptions(VirtualTreeControl treeControl)
		{
			// Empty default implementation
			return null;
		}
		#endregion // ElementPicker Specifics
	}
}
