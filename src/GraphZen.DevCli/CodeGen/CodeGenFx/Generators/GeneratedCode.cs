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
            try
            {
                var writeFile = !File.Exists(Path) || !File.ReadAllText(Path).Equals(Contents);


                if (writeFile)
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Path));
                    File.WriteAllText(Path, Contents);
                    Console.WriteLine($"Generated file: {Path}");
                }
                else
                {

                    Console.WriteLine($"Generated file already exists: {Path}");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error writing contents to file: '{Path}'. See inner exception for details.", e);
            }

        }
    }
}