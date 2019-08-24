// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public class DefaultIDirectiveAnnotationSyntaxConverter : SyntaxConverter
    {
        public override bool CanRead { get; } = true;
        public override bool CanWrite { get; } = true;

        public override object FromSyntax(SyntaxNode node)
        {
            if (node is DirectiveSyntax directive)
            {
                return new DirectiveAnnotation(directive.Name.Value, directive);
            }

            return null;
        }

        public override SyntaxNode ToSyntax(object value)
        {
            if (value is IDirectiveAnnotation annotation)
            {
                if (annotation.Value is DirectiveSyntax syntax)
                {
                    return syntax;
                }
                // TODO: lookup directive in schema based on name, get values from value, create syntax (AstFromValue)

                // Default: return simple directive annotation w/name only
                return new DirectiveSyntax(SyntaxFactory.Name(annotation.Name));
            }

            return null;
        }
    }
}