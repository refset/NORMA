﻿using Microsoft.VisualStudio.Modeling;
using ORMSolutions.ORMArchitect.Core.ObjectModel;

namespace unibz.ORMInferenceEngine
{
	partial class ORM2OWLTranslationManager
	{
		SetComparisonConstraint CreateInferredSubset(InferredConstraints container, string first, string second)
		{
			
			Partition outputPartition = container.Partition;
			ORMModel model = container.Model;


			SetComparisonConstraint targetConstraint = new InferredSubsetConstraint(outputPartition);
			targetConstraint.Name = "Subset " + first + " - " + second;
			//new SetComparisonConstraintIsInferred(container, targetConstraint);

			//SetComparisonConstraintRoleSequence roleseq1 = new SetComparisonConstraintRoleSequence(outputPartition);
			//SetComparisonConstraintRoleSequence roleseq2 = new SetComparisonConstraintRoleSequence(outputPartition);

			//roleseq1.RoleCollection.Add(role);
			//roleseq2.RoleCollection.Add(role2);

			//targetConstraint.RoleSequenceCollection.Add(roleseq1);
			//targetConstraint.RoleSequenceCollection.Add(roleseq2);

			return targetConstraint;
		}
	}
}