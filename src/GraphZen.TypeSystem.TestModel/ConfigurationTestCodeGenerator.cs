﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Immutable;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;

namespace GraphZen
{
    public class ConfigurationTestCodeGenerator
    {
        public static void Generate()
        {
            var g = new MetaModelTestCaseGenerator(true);
            var genDir = CodeGenHelpers.GetTargetDirectory();
            CodeGenHelpers.DeleteGeneratedFiles(genDir);
            // ReSharper disable once AssignNullToNotNullAttribute
            g.GetTemplateModels(ImmutableArray<Element>.Empty, GraphQLMetaModel.Schema());
        }
    }
}