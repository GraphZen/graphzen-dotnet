// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.CodeGenFx.Generators
{
    public class GeneratedCode
    {
        public GeneratedCode(string path, string contents)
        {
            Path = path;
            Contents = contents;
        }

        public string Path { get; }
        public string Contents { get; }

        public void WriteToFile()
        {
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Path));
            try
            {
                File.WriteAllText(Path, Contents);
            }
            catch (Exception e)
            {
                throw new Exception($"Error writing contents to file: '{Path}'. See inner exception for details.", e);
            }

            Console.WriteLine($"Generated file: {Path}");
        }
    }
}