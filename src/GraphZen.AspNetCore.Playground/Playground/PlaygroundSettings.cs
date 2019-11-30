// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Playground
{
    public class PlaygroundSettings
    {
        [JsonPropertyName("editor.cursorShape")]
        public CursorShape? EditorCursorShape { get; set; }

        [JsonPropertyName("editor.fontFamily")]
        public string? EditorFontFamily { get; set; }

        [JsonPropertyName("editor.fontSize")] public int? EditorFontSize { get; set; }

        [JsonPropertyName("editor.reuseHeaders")]
        public bool? EditorReuseHeaders { get; set; }

        [JsonPropertyName("editor.theme")] public EditorTheme? EditorTheme { get; set; }

        [JsonPropertyName("general.betaUpdates")]
        public bool? BetaUpdates { get; set; }

        [JsonPropertyName("prettier.printWidth")]
        public int? PrettierPrintWidth { get; set; }

        [JsonPropertyName("prettier.tabWidth")]
        public int? PrettierTabWidth { get; set; }

        [JsonPropertyName("prettier.useTabs")] public bool? PrettierUseTabs { get; set; }

        [JsonPropertyName("request.credentials")]
        public RequestCredentials? RequestCredentials { get; set; }

        [JsonPropertyName("schema.polling.enable")]
        public bool? SchemaPollingEnabled { get; set; }

        [JsonPropertyName("schema.polling.endpointFilter")]
        public string? SchemaPollingEndpointFilter { get; set; }

        [JsonPropertyName("schema.polling.interval")]
        public int? SchemaPollingInterval { get; set; }

        [JsonPropertyName("schema.disableComments")]
        public bool? SchemaCommentsDisabled { get; set; }

        [JsonPropertyName("tracing.hideTracingResponse")]
        public bool? HideTracingResponse { get; set; }
    }
}