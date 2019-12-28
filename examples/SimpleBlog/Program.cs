// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace SimpleBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(log => log.AddSeq())
                .UseStartup<Startup>().Build();

            webHost.Run();
        }
    }
}