

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;


#nullable enable

namespace GraphZen.LanguageModel {
    public static partial class SyntaxFactory {
        public static ArgumentSyntax Argument(GraphZen.LanguageModel.NameSyntax name , GraphZen.LanguageModel.StringValueSyntax description , GraphZen.LanguageModel.ValueSyntax value , GraphZen.LanguageModel.SyntaxLocation location  = null) => new ArgumentSyntax(GraphZen.LanguageModel.NameSyntax name , GraphZen.LanguageModel.StringValueSyntax description , GraphZen.LanguageModel.ValueSyntax value , GraphZen.LanguageModel.SyntaxLocation location  = null);
    }
}
