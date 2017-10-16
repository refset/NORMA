﻿<?xml version="1.0" encoding="utf-8"?>
<!--
	Natural Object-Role Modeling Architect for Visual Studio

	Copyright © Neumont University. All rights reserved.
	Copyright © ORM Solutions, LLC. All rights reserved.

	The use and distribution terms for this software are covered by the
	Common Public License 1.0 (http://opensource.org/licenses/cpl) which
	can be found in the file CPL.txt at the root of this distribution.
	By using this software in any fashion, you are agreeing to be bound by
	the terms of this license.

	You must not remove this notice, or any other, from this software.
-->
<Dsl
	xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel"
	PackageGuid="76D8E8CE-B44D-46B9-A37C-4E3094288830"
	Id="24FA0419-71BE-41FF-AE7C-80DD60E35532"
	Namespace="unibz.ORMInferenceEngine"
	PackageNamespace="unibz.ORMInferenceEngine"
	Name="ORMInferenceEngine"
	DisplayName="Inference Engine"
	Description="Inference Engine for ORM Model"
	CompanyName="Free University of Bozen-Bolzano"
	ProductName="Inference Engine for NORMA"
	MajorVersion="1" MinorVersion="0" Build="0" Revision="0">

	<Attributes>
		<ClrAttribute Name="DslModeling::ExtendsDomainModel">
			<Parameters>
				<AttributeParameter Value="&quot;3EAE649F-E654-4D04-8289-C25D2C0322D8&quot;/*ORMSolutions.ORMArchitect.Core.ObjectModel.ORMCoreDomainModel*/"/>
			</Parameters>
		</ClrAttribute>
		<ClrAttribute Name="ORMSolutions.ORMArchitect.Core.Load.NORMAExtensionLoadKey">
			<Parameters>
				<AttributeParameter Value="&quot;ly4276soTIbFjWepNqXV+Kz83T9EDlRB8zByGpXLakP5hWEFIcWB0L6P97ZeGbgMASEKYS/e+bVpcCq0MkIExQ==&quot;"/>
			</Parameters>
		</ClrAttribute>	</Attributes>

	<Classes>
		<DomainClass Name="Hierarchy" Namespace="unibz.ORMInferenceEngine" Id="E276F940-1082-4526-89F8-4CA88374121A" DisplayName="Hierarchy" Description="">
		</DomainClass>
		<DomainClass Name="UnsatisfiableDomain" Namespace="unibz.ORMInferenceEngine" Id="F65DD9E9-770B-4A9C-B041-795A8DECB9A9" DisplayName="Unsatisfiable Domain" Description="">
		</DomainClass>
		<DomainClass Name="InferenceResult" Namespace="unibz.ORMInferenceEngine" Id="8B15C2B7-6E85-4D80-A2CB-17C522D93315" DisplayName="Inference Result" Description="">
			<Properties>
				<DomainProperty Name="IsProcessed" DisplayName="IsProcessed" Id="27D2E4BA-A8E9-48E3-AFB8-E76924DDB14C" Description="Is it already processed?">
					<Type>
						<ExternalTypeMoniker Name="/System/Boolean"/>
					</Type>
				</DomainProperty>
			</Properties>
		</DomainClass>
    <!-- InferredUnsatisfiableDomain is a container playing a role of an alternate owner for all the object and fact types in a calculated UnsatisfiableDomain, is bound to the UnsatisfiableDomain node in the Model Browser -->
    <DomainClass Name="InferredUnsatisfiableDomain" Namespace="unibz.ORMInferenceEngine" Id="3fa70fa8-022e-4bc1-b63d-503e8e9df582" DisplayName="Inferred Unsatisfiable Domain" InheritanceModifier="Sealed" Description="">
    </DomainClass>
    <!-- InferredHierarchy is a container playing a role of an alternate owner for all the object types in a calculated hierarchy, is bound to the Object Type Hierarchy node in the Model Browser -->
    <DomainClass Name="InferredHierarchy" Namespace="unibz.ORMInferenceEngine" Id="98804ec9-a915-45e2-bb74-ffd4bcdef12c" DisplayName="Inferred Hierarchy" InheritanceModifier="Sealed" Description="">
    </DomainClass>
    <DomainClass Name="InferredTopObjectType" Namespace="unibz.ORMInferenceEngine" Id="24d946b1-d2a9-40a0-b0eb-a9acf56468c4" DisplayName="Inferred Top Object Type" Description="">
      <BaseClass>
        <DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ObjectType"/>
      </BaseClass>
    </DomainClass>
    <!-- Inferred Constraints is a container playing a role of an alternate owner for all the inferred constraints, is bound to the Inferred Constraints node in the Model Browser -->
		<DomainClass Name="InferredConstraints" Namespace="unibz.ORMInferenceEngine" Id="73E2969E-CBA9-4E83-B739-30319BF31769" DisplayName="Inferred Constraints" InheritanceModifier="Sealed" Description="">
		</DomainClass>
		<DomainClass Name="InferredSubsetConstraint" Namespace="unibz.ORMInferenceEngine" Id="6F23683C-A3F2-4683-B478-25F2811A1A83" DisplayName="Inferred Subset Constraint" Description="">
			<BaseClass>
				<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/SubsetConstraint"/>
			</BaseClass>		
		</DomainClass>
		<DomainClass Name="InferredEqualityConstraint" Namespace="unibz.ORMInferenceEngine" Id="C068B9F9-C6A8-4C32-A99E-6E991A3AF7B0" DisplayName="Inferred Exclusion Constraint" Description="">
			<BaseClass>
				<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/EqualityConstraint"/>
			</BaseClass>
		</DomainClass>
		<DomainClass Name="InferredExclusionConstraint" Namespace="unibz.ORMInferenceEngine" Id="94F7DB54-3D8A-41E6-87E6-E5ACB4EF0CA9" DisplayName="Inferred Exclusion Constraint" Description="">
			<BaseClass>
				<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ExclusionConstraint"/>
			</BaseClass>
		</DomainClass>
		<DomainClass Name="InferredSubtypeFact" Namespace="unibz.ORMInferenceEngine" Id="B13FDF84-9B05-48AF-A953-5DD5928F122A" DisplayName="Inferred Subtype Fact" Description="">
			<BaseClass>
				<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/SubtypeFact"/>
			</BaseClass>
		</DomainClass>
		<DomainClass Name="InferredMandatoryConstraint" Namespace="unibz.ORMInferenceEngine" Id="A4258B02-F416-468A-BF09-414AED389473" DisplayName="Inferred Mandatory Constraint" Description="">
			<BaseClass>
				<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/MandatoryConstraint"/>
			</BaseClass>
		</DomainClass>
		<DomainClass Name="InferredFrequencyConstraint" Namespace="unibz.ORMInferenceEngine" Id="5422777B-6C79-428D-9531-9717CC04A9E9" DisplayName="Inferred Frequency Constraint" Description="">
			<BaseClass>
				<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/FrequencyConstraint"/>
			</BaseClass>
		</DomainClass>
		<DomainClass Name="InferredUniquenessConstraint" Namespace="unibz.ORMInferenceEngine" Id="53C264AE-CD29-4F7E-8577-648530ED18A4" DisplayName="Inferred Uniqueness Constraint" Description="">
			<BaseClass>
				<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/UniquenessConstraint"/>
			</BaseClass>
		</DomainClass>
	</Classes>

	<Relationships>
		<DomainRelationship Name="UnsatisfiableDomainIsForORMModel" Namespace="unibz.ORMInferenceEngine" AllowsDuplicates="false" Id="BAF92B0A-6E47-41B5-905C-07C903044B11">
			<Source>
				<DomainRole Name="UnsatisfiableDomain" PropertyName="Model" Multiplicity="One" PropagatesDelete="true" IsPropertyGenerator="true" DisplayName="Unsatisfiable Domain" Id="FCDD864D-2E66-4FA6-AAFC-BC18685D2677">
					<RolePlayer>
						<DomainClassMoniker Name="UnsatisfiableDomain"/>
					</RolePlayer>
				</DomainRole>
			</Source>
			<Target>
				<DomainRole Name="ORMModel" PropertyName="UnsatisfiableDomain" Multiplicity="ZeroOne" PropagatesDelete="false" IsPropertyGenerator="false" DisplayName="Model" Id="F49D0190-1B54-411A-8F09-B3FBB7D046F4">
					<RolePlayer>
						<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ORMModel"/>
					</RolePlayer>
				</DomainRole>
			</Target>
		</DomainRelationship>

    <DomainRelationship Name="InferredUnsatisfiableDomainIsForORMModel" Namespace="unibz.ORMInferenceEngine" AllowsDuplicates="false" Id="1aaa5bad-1f1a-4c6f-94d3-48e07fac803c">
      <Source>
        <DomainRole Name="InferredUnsatisfiableDomain" PropertyName="Model" Multiplicity="One" PropagatesDelete="true" IsPropertyGenerator="true" DisplayName="Inferred Unsatisfiable Domain" Id="649191ad-bf6d-458f-a1b6-c8091726144b">
          <RolePlayer>
            <DomainClassMoniker Name="InferredUnsatisfiableDomain"/>
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Name="ORMModel" PropertyName="InferredUnsatisfiableDomain" Multiplicity="ZeroOne" PropagatesDelete="false" IsPropertyGenerator="false" DisplayName="Model" Id="596fd48f-7a44-4dea-aeaa-fa5229ef32d1">
          <RolePlayer>
            <DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ORMModel"/>
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>

    <DomainRelationship Name="UnsatisfiableObjectType" Namespace="unibz.ORMInferenceEngine" AllowsDuplicates="false" InheritanceModifier="Sealed" Id="94F223CD-95FA-487A-B21B-846AD7F931D8">
			<Source>
				<DomainRole Name="InferredUnsatisfiableDomain" PropertyName="ObjectTypeCollection" Multiplicity="ZeroMany" PropagatesDelete="false" IsPropertyGenerator="true" DisplayName="Inferred Unsatisfiable Domain" Id="F140A7DF-A42A-4E7C-8C4B-08EBFAB878CE">
					<RolePlayer>
						<DomainClassMoniker Name="InferredUnsatisfiableDomain"/>
					</RolePlayer>
				</DomainRole>
			</Source>
			<Target>
				<DomainRole Name="ObjectType" PropertyName="InferredUnsatisfiableDomain" Multiplicity="ZeroOne" PropagatesDelete="false" IsPropertyGenerator="false" DisplayName="ObjectType" Id="A40A2716-B654-4364-8A5F-B64C928F3AB6">
					<RolePlayer>
						<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ObjectType"/>
					</RolePlayer>
				</DomainRole>
			</Target>
		</DomainRelationship>

		<DomainRelationship Name="UnsatisfiableFactType" Namespace="unibz.ORMInferenceEngine" AllowsDuplicates="false" InheritanceModifier="Sealed" Id="077F9B3C-9A7A-42E3-89DA-AA388C58E7FC">
			<Source>
				<DomainRole Name="InferredUnsatisfiableDomain" PropertyName="FactTypeCollection" Multiplicity="ZeroMany" PropagatesDelete="false" IsPropertyGenerator="true" DisplayName="Inferred Unsatisfiable Domain" Id="9CABD542-D7DA-47C6-9373-B7184B3F723C">
					<RolePlayer>
						<DomainClassMoniker Name="InferredUnsatisfiableDomain"/>
					</RolePlayer>
				</DomainRole>
			</Source>
			<Target>
				<DomainRole Name="FactType" PropertyName="InferredUnsatisfiableDomain" Multiplicity="ZeroOne" PropagatesDelete="false" IsPropertyGenerator="false" DisplayName="FactType" Id="ACC6E62E-ECE1-49BD-90D6-C39DCB95A177">
					<RolePlayer>
						<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/FactType"/>
					</RolePlayer>
				</DomainRole>
			</Target>
		</DomainRelationship>

    <DomainRelationship Name="HierarchyIsForORMModel" Namespace="unibz.ORMInferenceEngine" AllowsDuplicates="false" Id="1E1CAC8E-D4DC-401C-99C9-3FFDD81FA940">
      <Source>
        <DomainRole Name="Hierarchy" PropertyName="Model" Multiplicity="One" PropagatesDelete="true" IsPropertyGenerator="true" DisplayName="Hierarchy" Id="B2927FC6-04B5-408A-B71B-1DF0B276A786">
          <RolePlayer>
            <DomainClassMoniker Name="Hierarchy"/>
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Name="ORMModel" PropertyName="Hierarchy" Multiplicity="ZeroOne" PropagatesDelete="false" IsPropertyGenerator="false" DisplayName="Model" Id="71F2482D-CB7C-4ED0-ADA7-2797A3D9A16D">
          <RolePlayer>
            <DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ORMModel"/>
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>

    <DomainRelationship Name="InferredHierarchyIsForORMModel" Namespace="unibz.ORMInferenceEngine" AllowsDuplicates="false" Id="f991efc0-1f21-4ded-ab72-c2ddbdb5e8cd">
      <Source>
        <DomainRole Name="InferredHierarchy" PropertyName="Model" Multiplicity="One" PropagatesDelete="true" IsPropertyGenerator="true" DisplayName="InferredHierarchy" Id="825a2143-3847-419d-9a81-38dd1408fe06">
          <RolePlayer>
            <DomainClassMoniker Name="InferredHierarchy"/>
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Name="ORMModel" PropertyName="InferredHierarchy" Multiplicity="ZeroOne" PropagatesDelete="false" IsPropertyGenerator="false" DisplayName="Model" Id="3c04fc1c-fc7b-4d28-abbe-64444055daae">
          <RolePlayer>
            <DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ORMModel"/>
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>

    <DomainRelationship Name="TopLevelObjectType" Namespace="unibz.ORMInferenceEngine" AllowsDuplicates="false"  InheritanceModifier="Sealed" Id="77204642-1413-45B5-8BD7-549F49B73C50">
      <Source>
        <DomainRole Name="InferredHierarchy" PropertyName="TopObjectTypeCollection" Multiplicity="ZeroMany" PropagatesDelete="false" IsPropertyGenerator="true" DisplayName="InferredHierarchy" Id="7425127E-6CFF-4CCE-B404-32DE9D1434F1">
          <RolePlayer>
            <DomainClassMoniker Name="InferredHierarchy"/>
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Name="ObjectType" PropertyName="InferredHierarchy" Multiplicity="ZeroOne" PropagatesDelete="false" IsPropertyGenerator="false" DisplayName="ObjectType" Id="B455862C-55EE-48EA-9DD0-4AED5ADCB312">
          <RolePlayer>
            <DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ObjectType"/>
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>

    <DomainRelationship Name="ObjectTypeContainment" Namespace="unibz.ORMInferenceEngine" AllowsDuplicates="false" InheritanceModifier="Sealed" Id="8B2B52BC-7F06-48E2-AFF3-DA8A20DF854B">
      <Source>
        <DomainRole Name="Parent" PropertyName="ChildCollection" Multiplicity="ZeroMany" PropagatesDelete="false" IsPropertyGenerator="true" DisplayName="Parent" Id="E3CEE9FB-F40E-4CEC-8366-B4C0EFF5FADC">
          <RolePlayer>
            <DomainRelationshipMoniker Name="TopLevelObjectType"/>
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Name="Child" PropertyName="ParentCollection" Multiplicity="ZeroMany" PropagatesDelete="false" IsPropertyGenerator="false" DisplayName="Child" Id="1E354DC0-A4BE-47C8-B872-6C4E0E461DB9">
          <RolePlayer>
            <DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ObjectType"/>
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>

    <DomainRelationship Name="InferenceResultTargetsORMModel" Namespace="unibz.ORMInferenceEngine" AllowsDuplicates="false" Id="89794B64-FA2A-40EB-BB70-ED24B737788F">
			<Source>
				<DomainRole Name="InferenceResult" PropertyName="Model" Multiplicity="One" PropagatesDelete="true" IsPropertyGenerator="true" DisplayName="InferenceResult" Id="8B85F422-91D5-412C-909C-200941064A1A">
					<RolePlayer>
						<DomainClassMoniker Name="InferenceResult"/>
					</RolePlayer>
				</DomainRole>
			</Source>
			<Target>
				<DomainRole Name="ORMModel" PropertyName="InferenceResult" Multiplicity="ZeroMany" PropagatesDelete="false" IsPropertyGenerator="false" DisplayName="Model" Id="4BFA395C-A15E-4160-9E29-CDE658D76D88">
					<RolePlayer>
						<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ORMModel"/>
					</RolePlayer>
				</DomainRole>
			</Target>
		</DomainRelationship>

		<DomainRelationship Name="InferredConstraintsTargetORMModel" Namespace="unibz.ORMInferenceEngine" AllowsDuplicates="false" Id="12F9390D-0B1B-4C20-8AE9-CD31A24638F4">
			<Source>
				<DomainRole Name="InferredConstraints" PropertyName="Model" Multiplicity="One" PropagatesDelete="true" IsPropertyGenerator="true" DisplayName="InferredConstraints" Id="44AF5552-43E4-4159-A044-FF3F018A8F09">
					<RolePlayer>
						<DomainClassMoniker Name="InferredConstraints"/>
					</RolePlayer>
				</DomainRole>
			</Source>
			<Target>
				<DomainRole Name="ORMModel" PropertyName="InferredConstraints" Multiplicity="ZeroOne" PropagatesDelete="false" IsPropertyGenerator="false" DisplayName="Model" Id="BC2CF0CD-AFD2-428B-B47A-FEA53BB011E8">
					<RolePlayer>
						<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/ORMModel"/>
					</RolePlayer>
				</DomainRole>
			</Target>
		</DomainRelationship>

		<DomainRelationship Name="SetConstraintIsInferred" Namespace="unibz.ORMInferenceEngine" IsEmbedding="true" AllowsDuplicates="false" Id="D4B6F130-6A77-4454-9C45-0088626D2843">
			<Source>
				<DomainRole Name="InferredConstraints" PropertyName="SetConstraintCollection" Multiplicity="ZeroMany" PropagatesDelete="false" IsPropertyGenerator="true" DisplayName="InferredConstraints" Id="938497E3-C3A4-4754-9382-233243CAE527">
					<RolePlayer>
						<DomainClassMoniker Name="InferredConstraints"/>
					</RolePlayer>
				</DomainRole>
			</Source>
			<Target>
				<DomainRole Name="SetConstraint" PropertyName="InferredConstraints" Multiplicity="ZeroOne" PropagatesDelete="true" IsPropertyGenerator="false" DisplayName="SetConstraint" Id="D10E1F9C-4626-47BB-9A60-02B856B37E33">
					<RolePlayer>
						<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/SetConstraint"/>
					</RolePlayer>
				</DomainRole>
			</Target>
		</DomainRelationship>

		<DomainRelationship Name="SetComparisonConstraintIsInferred" Namespace="unibz.ORMInferenceEngine" IsEmbedding="true" AllowsDuplicates="false" Id="FE83FC21-1A41-4298-945F-A0670C64E2A0">
			<Source>
				<DomainRole Name="InferredConstraints" PropertyName="SetComparisonConstraintCollection" Multiplicity="ZeroMany" PropagatesDelete="false" IsPropertyGenerator="true" DisplayName="InferredConstraints" Id="6A7B85BA-A86F-416A-B5B3-FC8C9226D0E9">
					<RolePlayer>
						<DomainClassMoniker Name="InferredConstraints"/>
					</RolePlayer>
				</DomainRole>
			</Source>
			<Target>
				<DomainRole Name="SetComparisonConstraint" PropertyName="InferredConstraints" Multiplicity="ZeroOne" PropagatesDelete="true" IsPropertyGenerator="false" DisplayName="SetConstraint" Id="8A8C4D99-02F4-4851-90E0-CC443F8C119B">
					<RolePlayer>
						<DomainClassMoniker Name="/ORMSolutions.ORMArchitect.Core.ObjectModel/SetComparisonConstraint"/>
					</RolePlayer>
				</DomainRole>
			</Target>
		</DomainRelationship>
		<DomainRelationship Name="SubtypeFactIsInferred" Namespace="unibz.ORMInferenceEngine" IsEmbedding="true" AllowsDuplicates="false" Id="14ED9F6A-2DCA-4255-A204-E77C52C0CAE2">
			<Source>
				<DomainRole Name="InferredConstraints" PropertyName="SubtypeFactCollection" Multiplicity="ZeroMany" PropagatesDelete="false" IsPropertyGenerator="true" DisplayName="InferredConstraints" Id="3BB67A2F-5158-4EAD-9D4B-67E7F8FB2D24">
					<RolePlayer>
						<DomainClassMoniker Name="InferredConstraints"/>
					</RolePlayer>
				</DomainRole>
			</Source>
			<Target>
				<DomainRole Name="SubtypeFact" PropertyName="InferredConstraints" Multiplicity="ZeroOne" PropagatesDelete="true" IsPropertyGenerator="false" DisplayName="SubtypeFact" Id="5D8AD920-CFA3-45B4-BD90-A2025AA66939">
					<RolePlayer>
						<DomainClassMoniker Name="InferredSubtypeFact"/>
					</RolePlayer>
				</DomainRole>
			</Target>
		</DomainRelationship>
	</Relationships>

	<Types>
	</Types>

	<XmlSerializationBehavior Name="ORMInferenceEngineDomainModelSerializationBehavior" Namespace="unibz.ORMInferenceEngine"/>

	<DslLibraryImports>
		<DslLibraryImport FilePath="..\..\ORMModel\Framework\SystemCore.dsl"/>
		<DslLibraryImport FilePath="..\..\ORMModel\ObjectModel\ORMCore.dsl"/>
	</DslLibraryImports>

</Dsl>