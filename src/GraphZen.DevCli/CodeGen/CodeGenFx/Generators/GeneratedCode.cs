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

        public void WriteToFile(bool quick)
        {
            try
            {
                var hashMark = $"// Source Hash Code: {CalculateHash(Contents)}";

                if (quick && File.Exists(Path))
                {
                    if (File.ReadAllText(Path).Contains(hashMark))
                    {
                        Console.WriteLine($"{Path}: detected identical output, skipping");
                        return;
                    }
                }

                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Path));
                File.WriteAllText(Path, Contents + hashMark);
                Console.WriteLine($"{Path}: generated");
            }
            catch (Exception e)
            {
                throw new Exception($"Error writing contents to file: '{Path}'. See inner exception for details.", e);
            }
        }


        private static ulong CalculateHash(string read)
        {
            var hashedValue = 3074457345618258791ul;
            for (var i = 0; i < read.Length; i++)
            {
                hashedValue += read[i];
                hashedValue *= 3074457345618258799ul;
            }

            return hashedValue;
        }
    }
}