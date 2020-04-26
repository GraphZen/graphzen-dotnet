// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel.Validation.Rules
{
    public class SchemaMustHaveRootObjectTypes : DocumentValidationRuleVisitor
    {
        private SchemaDefinitionSyntax _schema;

        public SchemaMustHaveRootObjectTypes(DocumentValidationContext context) : base(context)
        {
            _schema = null!;
        }

        public override VisitAction EnterSchemaDefinition(SchemaDefinitionSyntax node)
        {
            _schema = node;

            return false;
        }

        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            var queryRootOpeartionTypeDef =
                _schema?.RootOperationTypes.FirstOrDefault(_ => _.OperationType == OperationType.Query);
            var queryTypeName = queryRootOpeartionTypeDef?.Type.Name.Value ?? "Query";
            var queryType = node.Definitions.OfType<TypeDefinitionSyntax>()
                .FirstOrDefault(_ => _.Name.Value == queryTypeName);
            var queryRootOperationType = (SyntaxNode?)queryRootOpeartionTypeDef?.Type ?? queryType;
            if (queryType == null)
            {
                ReportError("Query root type must be provided.", _schema!);
            }
            else if (!(queryType is ObjectTypeDefinitionSyntax))
            {
                ReportError($"Query root type must be Object type, it cannot be {queryType.Name.Value}.",
                    queryRootOperationType);
            }

            var mutationRootOpeartionTypeDef =
                _schema?.RootOperationTypes.FirstOrDefault(_ => _.OperationType == OperationType.Mutation);
            var mutationTypeName = mutationRootOpeartionTypeDef?.Type.Name.Value ?? "Mutation";
            var mutationType = node.Definitions.OfType<TypeDefinitionSyntax>()
                .FirstOrDefault(_ => _.Name.Value == mutationTypeName);
            var mutationRootOperationType = (SyntaxNode?)mutationRootOpeartionTypeDef?.Type ?? mutationType;
            if (mutationType != null && !(mutationType is ObjectTypeDefinitionSyntax))
            {
                ReportError(
                    $"Mutation root type must be Object type if provided, it cannot be {mutationType.Name.Value}.",
                    mutationRootOperationType);
            }

            var subscriptionRootOpeartionTypeDef =
                _schema?.RootOperationTypes.FirstOrDefault(_ => _.OperationType == OperationType.Subscription);
            var subscriptionTypeName = subscriptionRootOpeartionTypeDef?.Type.Name.Value ?? "Subscription";
            var subscriptionType = node.Definitions.OfType<TypeDefinitionSyntax>()
                .FirstOrDefault(_ => _.Name.Value == subscriptionTypeName);
            var subscriptionRootOperationType =
                (SyntaxNode?)subscriptionRootOpeartionTypeDef?.Type ?? subscriptionType;

            if (subscriptionType != null && !(subscriptionType is ObjectTypeDefinitionSyntax))
            {
                ReportError(
                    $"Subscription root type must be Object type if provided, it cannot be {subscriptionType.Name.Value}.",
                    subscriptionRootOperationType);
            }

            return true;
        }
    }
}