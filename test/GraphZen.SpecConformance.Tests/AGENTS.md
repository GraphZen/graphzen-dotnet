# AGENTS.md

GraphZen's specification conformance suite -- an executable conformance statement mapping the GraphQL spec to test coverage. Each class maps to one spec subsection, each method proves one normative statement, and gaps are always explicit. The directory should read like an executable appendix to the GraphQL spec.

## Coverage Principles

The spec is the sole source of truth for what this suite covers. graphql-js is a supplement for example test cases, not a coverage guide.

- **Every spec test case gets written** with its query and expected outcome, regardless of whether GraphZen can pass it today. The test body should contain the actual GraphQL query and assertion call.
- **Skip the test, not the work.** When GraphZen lacks an implementation, the test is skipped with a reason pointing at the implementation gap. The skip message describes what GraphZen needs to implement, not that the test is deferred.
- **Empty placeholders are temporary.** A skipped `[Fact]` with an empty body is acceptable only while actively porting test cases. The goal state is a fully-written test that's skipped because the implementation isn't there yet — not a placeholder that says "we'll write this later."
- **The manifest is driven by the spec**, not by GraphZen's capabilities. Every validation subsection in the spec gets a manifest entry.
- **The suite is agnostic of GraphZen internals.** Conformance classes should not reference GraphZen implementation details. The harness provides `ExpectValid`, `ExpectErrors`, and `ToDeepEqual` — that is the full API surface for test authors.

## Running Tests

```sh
dotnet test --project test/GraphZen.SpecConformance.Tests/                             # all conformance tests
dotnet test --project test/GraphZen.SpecConformance.Tests/ --filter "SpecSection=5"     # Chapter 5
dotnet test --project test/GraphZen.SpecConformance.Tests/ --filter "SpecSection=5.3"   # Fields (5.3.x)
dotnet test --project test/GraphZen.SpecConformance.Tests/ --filter "SpecSection=5.3.3" # one subsection
```

Hierarchical filtering works because `SpecSectionDiscoverer` expands `"5.3.3"` into traits for `"5"`, `"5.3"`, and `"5.3.3"`.

Section 2 (Language) tests follow the same pattern:

```sh
dotnet test --project test/GraphZen.SpecConformance.Tests/ --filter "SpecSection=2"     # Chapter 2
dotnet test --project test/GraphZen.SpecConformance.Tests/ --filter "SpecSection=2.10"   # Input Values (2.10.x)
dotnet test --project test/GraphZen.SpecConformance.Tests/ --filter "SpecSection=2.10.4" # one subsection
```

## Adding a Conformance Class

1. **Verify the heading** in the local spec source (e.g., `~/Code/graphql/graphql-spec/spec/Section 5 -- Validation.md` or `Section 2 -- Language.md`). Do not invent headings from memory.
2. **Derive the section number** from `https://spec.graphql.org/draft/`. The source markdown has no explicit numbers -- count headings to derive numbers like `5.3.3`.
3. **Check graphql-js** for upstream test cases and intent. This is a supplement, not the source of truth.
   - Section 5: `~/Code/graphql/graphql-js/src/validation/`
   - Section 2: `~/Code/graphql/graphql-js/src/language/__tests__/` (primarily `parser-test.ts` and `lexer-test.ts`)
4. **Add the section number** to the appropriate manifest list in `SpecCoverageManifest` (`ValidationSections` or `LanguageSections`).
5. **Create the class** in the correct folder and namespace (see Structure below).
6. **Add test cases** as individual `[Fact]` methods -- one method per distinct spec scenario (see Test Patterns below).
7. **Run the section tests**: `dotnet test --filter "SpecSection=X.Y.Z"`
8. **Run the coverage test**: `dotnet test --filter "FullyQualifiedName~ValidationCoverageTests"` or `"FullyQualifiedName~LanguageCoverageTests"`

## Specification Sources

This suite tracks the **working draft** of the GraphQL specification, not a published edition.

| Source | Use for |
|---|---|
| **Spec website:** `https://spec.graphql.org/draft/` | Canonical section numbers, headings, deep-link URLs |
| **Local spec clone:** `~/Code/graphql/graphql-spec` | Exact wording, markdown source |
| **Local graphql-js clone:** `~/Code/graphql/graphql-js` | Reference implementation behavior, upstream tests |
| **Upstream repos:** `github.com/graphql/graphql-spec`, `github.com/graphql/graphql-js` | Linking in PRs |

**Section numbering:** The spec source markdown has no explicit section numbers. Numbers like `5.3.3` are derived from heading order (first `##` = X.1, first `###` under X.3 = X.3.1, etc.). Always verify against the website or by counting headings -- do not guess.

## Structure

The mapping from specification to code is explicit and predictable:

| Artifact | Maps to |
|---|---|
| folder | spec chapter or subsection group |
| namespace | same as folder |
| file | one class, named to match the class it contains |
| class | one exact spec subsection |
| method | one normative statement, allowance, prohibition, or example |

If the file path, namespace, class name, and attribute disagree about which subsection is represented, fix the structure. Do not put multiple conformance classes in one file.

### Examples

For spec subsection `5.3.3 Leaf Field Selections`:

| Artifact | Value |
|---|---|
| file path | `Section5_Validation/Fields/LeafFieldSelectionsConformanceTests.cs` |
| namespace | `GraphZen.SpecConformance.Tests.Section5_Validation.Fields` |
| class name | `LeafFieldSelectionsConformanceTests` |
| XML doc comment | `/// <seealso href="https://spec.graphql.org/draft/#sec-Leaf-Field-Selections"/>` |
| attribute | `[SpecSection("5.3.3", "Leaf Field Selections")]` |

For spec subsection `2.10.4 String Value`:

| Artifact | Value |
|---|---|
| file path | `Section2_Language/Values/StringValueConformanceTests.cs` |
| namespace | `GraphZen.SpecConformance.Tests.Section2_Language.Values` |
| class name | `StringValueConformanceTests` |
| XML doc comment | `/// <seealso href="https://spec.graphql.org/draft/#sec-String-Value"/>` |
| attribute | `[SpecSection("2.10.4", "String Value")]` |

### Naming

- Chapter folders: `Section2_Language`, `Section3_TypeSystem`, `Section4_Introspection`, `Section5_Validation`, `Section6_Execution`, `Section7_Response`
- Subsection folders: spec-oriented group names (`Fields/`, `Arguments/`), not implementation names
- Classes: named after the subsection heading, suffixed with `ConformanceTests`
- Methods: snake_case, read like executable spec prose (`object_field_selection_is_valid()`)

### `SpecSection` Attribute

```csharp
[SpecSection("5.3.3", "Leaf Field Selections")]
```

First parameter is the section number, second is the spec heading. Each class must have exactly one `[SpecSection]` attribute -- do not use multiple attributes on a single class.

### XML Doc Comments

Use XML doc comments for spec traceability. Unlike `//` comments, they are semantically bound to their declaration and won't be separated by formatters.

**Class level** -- always include a `<seealso href="..."/>` with the spec deep link:

```csharp
/// <seealso href="https://spec.graphql.org/draft/#sec-Leaf-Field-Selections"/>
[SpecSection("5.3.3", "Leaf Field Selections")]
public class LeafFieldSelectionsConformanceTests
```

Do not add a `<summary>` that restates the class name or attribute.

**Method level** -- only when the comment adds information the method name and query don't already carry:

```csharp
/// <remarks>
/// The spec explicitly allows fields defined on implementors to appear
/// in inline fragments on the interface, even though the interface
/// itself does not declare the field.
/// </remarks>
[Fact]
public void valid_field_in_inline_fragment_on_implementor()
```

Do not use `//` comments for spec traceability (URLs, version references, graphql-js paths). The spec version is in `SpecMetadata.cs`.

#### Spec URL Format

```
https://spec.graphql.org/draft/#sec-{Heading-With-Hyphens}
```

Spaces become hyphens. Ambiguous headings are prefixed with the parent section using a dot separator:

| Heading | URL |
|---|---|
| Leaf Field Selections | `...#sec-Leaf-Field-Selections` |
| Fragments (under Language) | `...#sec-Language.Fragments` |
| Input Coercion (under Input Objects) | `...#sec-Input-Objects.Input-Coercion` |

When in doubt, navigate to the heading on the spec website and copy the anchor.

### Sub-Subsection Handling

Some subsections contain further children (e.g., 5.5.2.3 "Fragment Spread Is Possible" has 5.5.2.3.1 through 5.5.2.3.5). The manifest lists only the parent subsection, and a single conformance class covers it including all children. Do not create separate manifest entries or classes for sub-subsections.

## Test Patterns

### Conformance Class

Each test case is an individual `[Fact]` method. The method name reads like an executable spec statement, and the body contains the query and assertion.

```csharp
using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fields;

/// <seealso href="https://spec.graphql.org/draft/#sec-Leaf-Field-Selections"/>
[SpecSection("5.3.3", "Leaf Field Selections")]
public class LeafFieldSelectionsConformanceTests
{
    [Fact]
    public void valid_scalar_selection()
    {
        ExpectValid(ScalarLeafs, """
            fragment scalarSelection on Dog {
              barks
            }
            """);
    }

    [Fact(Skip = "GraphZen does not reject queries selecting sub-fields on scalar types.")]
    public void object_type_missing_selection_is_rejected()
    {
        ExpectErrors(ScalarLeafs, """
            query directQueryOnObjectWithoutSubFields {
              human
            }
            """).ToDeepEqual(
            new("Field \"human\" of type \"Human\" must have a selection of subfields. Did you mean \"human { ... }\"?",
                Line: 2, Column: 15));
    }
}
```

Conventions:

- One `[Fact]` per distinct spec scenario -- do not group unrelated cases into `TheoryData`
- Use `[Theory]` only for genuine parametric variations of the same scenario
- Keep GraphQL documents as formatted raw string literals
- Every skipped test gets its own `Skip` message describing the specific gap

### Implementation Gaps

When GraphZen lacks an implementation, write the test case with the query and expected behavior, then throw `NotImplementedException`:

```csharp
[Fact]
public void query_type_must_be_defined()
{
    var query = """
        { field }
        """;
    throw new NotImplementedException(
        "GraphZen does not implement Operation Type Existence validation.");
}
```

When the implementation lands, replace the throw with the assertion and the test is ready to run.

### Assertion Helpers

`SpecValidation` (imported via `using static`) provides:

| Method | Use when |
|---|---|
| `ExpectValid(rule, query)` | Query should produce zero errors from this rule |
| `ExpectValid(schema, rule, query)` | Same, against a custom schema |
| `ExpectErrors(rule, query)` | Returns errors from a specific rule against TestSchema -- chain with `.ToDeepEqual(...)` |
| `ExpectErrors(schema, rule, query)` | Same, against a custom schema |

Every test explicitly names the rule it exercises. `ExpectValid` is sugar for `ExpectErrors(rule, query).ToDeepEqual()` (no args = empty error list). This mirrors graphql-js's per-file `expectValid`/`expectErrors` harness pattern.

Do not use `QueryShouldPass` or `QueryShouldFail` -- they don't bind to a specific rule and `QueryShouldFail` only checks error count.

#### `ToDeepEqual` and `ExpectedError`

`.ToDeepEqual(...)` asserts the exact error list -- message, line, and column for each error:

```csharp
ExpectErrors(FieldsOnCorrectType, """
    fragment typeKnownAgain on Pet {
      unknown_pet_field {
        ... on Cat {
          unknown_cat_field
        }
      }
    }
    """).ToDeepEqual(
    new("Cannot query field \"unknown_pet_field\" on type \"Pet\".", Line: 3, Column: 7),
    new("Cannot query field \"unknown_cat_field\" on type \"Cat\".", Line: 5, Column: 11));
```

`ExpectedError` is a positional record: `new(message, Line: line, Column: column)`. Always assert both the message and the location -- never assert only the error count. Calling `.ToDeepEqual()` with no arguments asserts zero errors (this is what `ExpectValid` does).

### Section 2 — Language Tests

Section 2 conformance tests use `SpecParsing` (imported via `using static`) instead of `SpecValidation`. The fundamental difference: Section 5 tests bind to a named validation rule; Section 2 tests exercise the parser directly.

#### Assertion Helpers (SpecParsing)

`SpecParsing` provides:

| Method | Use when |
|---|---|
| `ExpectValidSyntax(source)` | Source should parse as a valid document; returns `DocumentSyntax` for further assertions |
| `ExpectSyntaxError(source)` | Source should fail to parse -- chain with `.ToEqual(...)` for error details |
| `ExpectValidValue(source)` | Source should parse as a valid value literal; returns `ValueSyntax` |
| `ExpectValidType(source)` | Source should parse as a valid type reference; returns `TypeSyntax` |

`.ToEqual(...)` asserts the error message and location:

```csharp
ExpectSyntaxError("""
    { field( }
    """).ToEqual(
    new("expected message", Line: 1, Column: 10));
```

`ExpectedSyntaxError` is a positional record: `new(message, Line: line, Column: column)`. Unlike validation (which can produce multiple errors), parse errors are singular -- the parser stops at the first error -- so the method is `ToEqual` not `ToDeepEqual`.

When AST shape matters, use the returned syntax node:

```csharp
var doc = ExpectValidSyntax("{ field }");
var op = Assert.Single(doc.Definitions);
Assert.IsType<OperationDefinitionSyntax>(op);
```

#### Error Message Assertions

The spec does not mandate error message text, so exact wording is implementation-dependent. Use `ToEqual` for messages that are semantically significant. When only the location matters (or the message is a Superpower parser artifact), use `WithMessageContaining` for substring matching:

```csharp
ExpectSyntaxError("{ 1.0e }")
    .WithMessageContaining("unexpected")
    .AtLocation(Line: 1, Column: 7);
```

#### Test Patterns by Category

**Lexical sections (2.1.x)** -- test through document parsing. The suite is agnostic of GraphZen internals, so use `ExpectValidSyntax`/`ExpectValidValue` rather than the tokenizer directly.

```csharp
[Fact]
public void horizontal_tab_is_valid_whitespace()
{
    ExpectValidSyntax("{\tfield\t}");
}

[Fact]
public void commas_are_insignificant()
{
    var withCommas = ExpectValidSyntax("{ a, b, c }");
    var withoutCommas = ExpectValidSyntax("{ a b c }");
    Assert.Equal(withCommas.ToSyntaxString(), withoutCommas.ToSyntaxString());
}
```

**Syntactic sections (2.3--2.9, 2.11--2.13)** -- test grammar productions via document parsing.

```csharp
[Fact]
public void query_shorthand()
{
    var doc = ExpectValidSyntax("{ field }");
    var op = Assert.IsType<OperationDefinitionSyntax>(Assert.Single(doc.Definitions));
    Assert.Null(op.Name);
}
```

**Value sections (2.10.x)** -- test via `ExpectValidValue`.

```csharp
[Fact]
public void integer_value_parses()
{
    var value = ExpectValidValue("42");
    var intValue = Assert.IsType<IntValueSyntax>(value);
    Assert.Equal("42", intValue.Value);
}
```

**Type reference section (2.12)** -- test via `ExpectValidType`.

```csharp
[Fact]
public void non_null_list_of_non_null_type()
{
    var type = ExpectValidType("[String!]!");
    Assert.IsType<NonNullTypeSyntax>(type);
}
```

#### Relationship to Existing Parser Tests

Existing tests in `test/GraphZen.Tests/LanguageModel/` are implementation tests -- they verify GraphZen's parser using internal APIs (`ParserTestBase`, `SyntaxFactory`, `SuperPowerTokenizer`). The conformance suite is additive and does not replace them.

| | Existing parser tests | Spec conformance tests |
|---|---|---|
| Location | `test/GraphZen.Tests/LanguageModel/` | `test/GraphZen.SpecConformance.Tests/Section2_Language/` |
| Purpose | Verify parser implementation | Prove spec compliance |
| API surface | Internal (`ParserTestBase`, `SyntaxFactory`) | Public (`SpecParsing` harness only) |
| Organized by | Implementation module | Spec subsection |
| Gaps | Absent = not tested | Absent = never; gaps are explicit |

### TestSchema (Section 5)

Most Section 5 tests use the shared `TestSchema` provided by the harness. Introduce a section-local schema when a test needs a smaller or clearer setup.

- **Interfaces**: Being, Pet, Canine, Intelligent
- **Objects**: Dog, Cat, Human, Alien, ComplicatedArgs, QueryRoot
- **Unions**: CatOrDog, DogOrHuman, HumanOrAlien
- **Enums**: DogCommand, FurColor
- **Input Objects**: ComplexInput
- **Custom Scalars**: Invalid, Any
- **Directives**: onQuery, onMutation, onSubscription, onField, onFragmentDefinition, onFragmentSpread, onInlineFragment

QueryRoot exposes: `human`, `alien`, `cat`, `pet`, `catOrDog`, `dogOrHuman`, `humanOrAlien`, `complicatedArgs`, `invalidArg`, `anyArg`.

## Coverage Manifest

`Infrastructure/SpecCoverageManifest.cs` lists every spec subsection that should have a conformance class. Coverage test classes use reflection to verify every manifest entry has a corresponding `[SpecSection]` class.

| Manifest list | Coverage test | Spec chapter |
|---|---|---|
| `SpecCoverageManifest.LanguageSections` | `LanguageCoverageTests` | Section 2 -- Language |
| `SpecCoverageManifest.ValidationSections` | `ValidationCoverageTests` | Section 5 -- Validation |

When adding a new subsection:

1. Add the section number to the appropriate manifest list (`LanguageSections` or `ValidationSections`)
2. Create the conformance class (or gap placeholder)
3. Run the matching coverage test: `dotnet test --filter "FullyQualifiedName~LanguageCoverageTests"` or `"FullyQualifiedName~ValidationCoverageTests"`

## Quality Standards

- One test proves one thing
- Tests read like the spec, not framework plumbing
- Assert observable GraphQL behavior, not internal implementation
- Gaps are always explicit -- never silently absent
- Keep GraphQL inputs visible in the test -- avoid helpers that hide the query
- Repeat simple setup if it aids readability

### Porting from graphql-js

- Preserve the original intent before adapting style
- Keep one C# test close to one upstream case
- Keep case names recognizable
- Do not port reference implementation quirks not required by the spec
- Mark cases that are reference-only or not applicable to GraphZen
