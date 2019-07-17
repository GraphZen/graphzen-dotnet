﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.IO;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen
{
    public static class CodeGenHelpers
    {
        [NotNull]
        public static DirectoryInfo GetSolutionDirectory(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory() ?? throw new Exception("unable to get directory"));
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            return directory ?? throw new Exception("Could not find solution directory");
        }
    }
}