// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class CodeGenHelpers
    {
        public static void WriteFile(string path, string contents)
        {
            Console.Write($"Generating file: {path} ");
            if (File.Exists(path))
            {
                Console.Write("(deleted existing)");
                File.Delete(path);
            }

            Console.Write(" (writing ...");
            File.AppendAllText(path, contents);
            Console.WriteLine(" done)");
        }
    }
}