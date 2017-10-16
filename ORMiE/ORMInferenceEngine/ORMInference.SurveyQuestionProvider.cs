﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ORMSolutions.ORMArchitect.Framework.Shell;
using ORMSolutions.ORMArchitect.Framework.Shell.DynamicSurveyTreeGrid;
namespace unibz.ORMInferenceEngine
{
	partial class ORMInferenceEngineDomainModel : ISurveyQuestionProvider<Microsoft.VisualStudio.Modeling.Store>
	{
		private static readonly ISurveyQuestionTypeInfo<Microsoft.VisualStudio.Modeling.Store>[] mySurveyQuestionTypeInfo1 = new ISurveyQuestionTypeInfo<Microsoft.VisualStudio.Modeling.Store>[]{
			ProvideSurveyQuestionForSurveyRootElementType.Instance};
		/// <summary>Implements <see cref="ISurveyQuestionProvider{Object}.GetSurveyQuestions"/></summary>
		protected static IEnumerable<ISurveyQuestionTypeInfo<Microsoft.VisualStudio.Modeling.Store>> GetSurveyQuestions(Microsoft.VisualStudio.Modeling.Store surveyContext, object expansionKey)
		{
			if (expansionKey == null)
			{
				return mySurveyQuestionTypeInfo1;
			}
			return null;
		}
		IEnumerable<ISurveyQuestionTypeInfo<Microsoft.VisualStudio.Modeling.Store>> ISurveyQuestionProvider<Microsoft.VisualStudio.Modeling.Store>.GetSurveyQuestions(Microsoft.VisualStudio.Modeling.Store surveyContext, object expansionKey)
		{
			return GetSurveyQuestions(surveyContext, expansionKey);
		}
		/// <summary>Implements <see cref="ISurveyQuestionProvider{Object}.GetSurveyQuestionImageLists"/></summary>
		protected ImageList[] GetSurveyQuestionImageLists(Microsoft.VisualStudio.Modeling.Store surveyContext)
		{
			return null;
		}
		ImageList[] ISurveyQuestionProvider<Microsoft.VisualStudio.Modeling.Store>.GetSurveyQuestionImageLists(Microsoft.VisualStudio.Modeling.Store surveyContext)
		{
			return this.GetSurveyQuestionImageLists(surveyContext);
		}
		/// <summary>Implements <see cref="ISurveyQuestionProvider{Object}.GetErrorDisplayTypes"/></summary>
		protected static IEnumerable<Type> GetErrorDisplayTypes()
		{
			return null;
		}
		IEnumerable<Type> ISurveyQuestionProvider<Microsoft.VisualStudio.Modeling.Store>.GetErrorDisplayTypes()
		{
			return GetErrorDisplayTypes();
		}
		private sealed class ProvideSurveyQuestionForSurveyRootElementType : ISurveyQuestionTypeInfo, ISurveyQuestionTypeInfo<Microsoft.VisualStudio.Modeling.Store>
		{
			private ProvideSurveyQuestionForSurveyRootElementType()
			{
			}
			public static readonly ISurveyQuestionTypeInfo<Microsoft.VisualStudio.Modeling.Store> Instance = new ProvideSurveyQuestionForSurveyRootElementType();
			public Type QuestionType
			{
				get
				{
					return typeof(SurveyRootElementType);
				}
			}
			public ISurveyDynamicValues DynamicQuestionValues
			{
				get
				{
					return null;
				}
			}
			public int AskQuestion(object data, object contextElement)
			{
				IAnswerSurveyQuestion<SurveyRootElementType> typedData = data as IAnswerSurveyQuestion<SurveyRootElementType>;
				if (typedData != null)
				{
					return typedData.AskQuestion(contextElement);
				}
				return -1;
			}
			public int MapAnswerToImageIndex(int answer)
			{
				return -1;
			}
			public IFreeFormCommandProvider<Microsoft.VisualStudio.Modeling.Store> GetFreeFormCommands(Microsoft.VisualStudio.Modeling.Store surveyContext, int answer)
			{
				switch ((SurveyRootElementType)answer)
				{
					case SurveyRootElementType.InferredConstraint:
						return FreeFormInferredConstraintsCommands;
				}
				return null;
			}
			public bool ShowEmptyGroup(Microsoft.VisualStudio.Modeling.Store surveyContext, int answer)
			{
				switch ((SurveyRootElementType)answer)
				{
					case SurveyRootElementType.InferredConstraint:
					case SurveyRootElementType.TopLevelObjectType:
						return true;
					default:
						return false;
				}
			}
			public SurveyQuestionDisplayData GetDisplayData(int answer)
			{
				SurveyRootElementType typedAnswer = (SurveyRootElementType)answer;
				System.Drawing.Color foreColor = System.Drawing.Color.Empty;
				System.Drawing.Color backColor = System.Drawing.Color.Empty;
				GetItemColor(typedAnswer, ref foreColor, ref backColor);
				return new SurveyQuestionDisplayData(foreColor, backColor);
			}
			public SurveyQuestionUISupport UISupport
			{
				get
				{
					return SurveyQuestionUISupport.Grouping | SurveyQuestionUISupport.Sorting | SurveyQuestionUISupport.DisplayData | SurveyQuestionUISupport.EmptyGroups;
				}
			}
			public static int QuestionPriority
			{
				get
				{
					return 0;
				}
			}
			int ISurveyQuestionTypeInfo.QuestionPriority
			{
				get
				{
					return QuestionPriority;
				}
			}
		}
	}
}