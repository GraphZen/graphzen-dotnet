// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public partial interface IMutableFields : IBuildableFields
    {
        IField? GetOrAddField(string name, Type clrType, ConfigurationSource configurationSource);
        IField? GetOrAddField(string name, string type, ConfigurationSource configurationSource);
        bool RemoveField(IField field);
        bool AddField(IField field);
        ConfigurationSource? FindIgnoredFieldConfigurationSource(string name);
    }
}