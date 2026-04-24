# AGENTS.md

This directory contains GraphZen's specification conformance suite -- an executable conformance statement that makes it easy to answer what part of the GraphQL spec is represented, enforced, a known gap, or not applicable.

## Current State

- Only **Chapter 5 (Validation)** has conformance structure. No other chapters have classes or manifest entries yet.
- **29 validation subsections** are listed in `SpecCoverageManifest.ValidationSections`.
- All conformance classes are **native conformance classes** extending `SpecValidationRuleHarness` with inline GraphQL and TheoryData.
- **All negative validation tests are currently skipped** -- the validation rule implementations do not yet reject invalid queries. Positive tests pass.
- The project depends on `GraphZen.Tests` for `ValidationRuleHarness` and its shared `TestSchema`.
- Spec draft version is centralized in `Infrastructure/SpecMetadata.cs`.

## Running Tests

```sh
# all conformance tests
dotnet test --project test/GraphZen.SpecConformance.Tests/

# all Chapter 5 tests
dotnet test --project test/GraphZen.SpecConformance.Tests/ --filter "SpecSection=5"

# all Fields subsection tests (5.3.x)
dotnet test --project test/GraphZen.SpecConformance.Tests/ --filter "SpecSection=5.3"

# one exact subsection
dotnet test --project test/GraphZen.SpecConformance.Tests/ --filter "SpecSection=5.3.3"
```

Hierarchical filtering works because `SpecSectionDiscoverer` expands `"5.3.3"` into traits for `"5"`, `"5.3"`, and `"5.3.3"`.

## Adding a Conformance Class (Step by Step)

This is the most common task. Follow this checklist:

1. **Verify the heading** in the local spec source (e.g., `~/Code/graphql/graphql-spec/spec/Section 5 -- Validation.md`). Do not invent headings from memory.
2. **Derive the section number** from the spec website at `https://spec.graphql.org/draft/`. The source markdown uses `##`/`###`/`####` headings without explicit numbers -- the website renders them. Count headings to derive numbers like `5.3.3`.
3. **Identify the graphql-js validation rule and test file** in `~/Code/graphql/graphql-js/src/validation/` for upstream case names and intent.
4. **Create the class** in the correct folder and namespace (see Mapping Rules below).
5. **Set `RuleUnderTest`** to the appropriate `QueryValidationRules.*` value (see Available Rules below).
6. **Add valid and invalid query cases** using the TheoryData pattern (see Test Patterns below).
7. **Add the section number** to `SpecCoverageManifest.ValidationSections` if not already present.
8. **Run the section tests** to verify: `dotnet test --filter "SpecSection=X.Y.Z"`
9. **Run the coverage test** to check manifest consistency: `dotnet test --filter "FullyQualifiedName~ValidationCoverageTests"`

## Specification Sources

This suite tracks the **working draft** of the GraphQL specification, not a published edition. The latest published edition is September 2025, but we target the draft so conformance work stays current with spec evolution. The spec draft version is recorded in `Infrastructure/SpecMetadata.cs`.

- **Spec website (draft):** `https://spec.graphql.org/draft/` -- canonical section numbers, headings, and deep links
- **Spec website (index):** `https://spec.graphql.org/`
- **Local spec clone:** `~/Code/graphql/graphql-spec` -- source markdown, exact wording, repository history
- **Upstream spec repo:** `https://github.com/graphql/graphql-spec` -- for linking in PRs
- **Local graphql-js clone:** `~/Code/graphql/graphql-js` -- reference implementation behavior, upstream tests
- **Upstream graphql-js repo:** `https://github.com/graphql/graphql-js` -- for linking in PRs

Use them for different purposes:

- Prefer the spec website for canonical section numbers, headings, and deep-link URLs.
- Prefer the local spec clone for exact wording and markdown source.
- Prefer the local graphql-js clone for tracing how a spec rule is exercised in practice.
- Use the upstream GitHub repositories when linking source files or issues for reviewer context.

**Section numbering note:** The spec source markdown does not contain explicit numeric section numbers. Numbers like `5.3.3` are derived from heading order within each chapter file (first `##` = X.1, second `##` = X.2; first `###` under X.3 = X.3.1, etc.). The spec website renders these numbers. Always verify against the website or by counting headings in the source -- do not guess.

### Spec Website Deep Links

Every conformance class should include a direct URL to its spec subsection. The URL format is:

```
https://spec.graphql.org/draft/#sec-{Heading-With-Hyphens}
```

Spaces in the heading become hyphens. When a subsection heading is ambiguous (appears under multiple parents), the anchor is prefixed with the parent section name using a dot separator.

Examples:

| Spec heading | URL |
|---|---|
| Leaf Field Selections | `https://spec.graphql.org/draft/#sec-Leaf-Field-Selections` |
| Field Selection Merging | `https://spec.graphql.org/draft/#sec-Field-Selection-Merging` |
| All Variable Usages Are Allowed | `https://spec.graphql.org/draft/#sec-All-Variable-Usages-Are-Allowed` |
| Fragments (under Language) | `https://spec.graphql.org/draft/#sec-Language.Fragments` |
| Custom Scalars (under Scalars) | `https://spec.graphql.org/draft/#sec-Scalars.Custom-Scalars` |
| Input Coercion (under Input Objects) | `https://spec.graphql.org/draft/#sec-Input-Objects.Input-Coercion` |

When in doubt, check the cross-references in the local spec source (e.g., `grep '#sec-' ~/Code/graphql/graphql-spec/spec/*.md`) or navigate to the heading on the spec website and copy the anchor from the URL bar.

## Mapping Rules

The mapping from specification to code must be explicit and predictable:

| Artifact | Maps to |
|---|---|
| folder | spec chapter or subsection group |
| namespace | same as folder |
| class | one exact spec subsection |
| method | one normative statement, allowance, prohibition, or example |

### Concrete Example

For spec subsection `5.3.3 Leaf Field Selections`:

| Artifact | Value |
|---|---|
| spec URL | `https://spec.graphql.org/draft/#sec-Leaf-Field-Selections` |
| file path | `Section5_Validation/Fields/LeafFieldSelectionsConformanceTests.cs` |
| namespace | `GraphZen.SpecConformance.Tests.Section5_Validation.Fields` |
| class name | `LeafFieldSelectionsConformanceTests` |
| attribute | `[SpecSection("5.3.3", "Leaf Field Selections")]` |
| spec source | `~/Code/graphql/graphql-spec/spec/Section 5 -- Validation.md` |
| graphql-js tests | `~/Code/graphql/graphql-js/src/validation/__tests__/ScalarLeafsRule-test.ts` |

If the file path, namespace, class name, and attribute disagree about which subsection is represented, fix the structure.

### Naming

- Chapter folders: `Section2_Language`, `Section3_TypeSystem`, `Section4_Introspection`, `Section5_Validation`, `Section6_Execution`, `Section7_Response`
- Subsection folders: spec-oriented group names (e.g., `Fields/`, `Arguments/`), not implementation names
- Classes: named after the subsection heading, suffixed with `ConformanceTests`
- Methods: snake_case, read like executable spec prose (e.g., `object_field_selection_is_valid()`)

Avoid implementation-oriented names like `ExecutorTests` or `ParserTests`.

### `SpecSection` Attribute

```csharp
[SpecSection("5.3.3", "Leaf Field Selections")]
```

The first parameter is the section number. The second parameter is the spec heading. (Note: the second parameter is named `rule` in the attribute source code, but is used for the heading in practice throughout the codebase.)

### Header Comment

Every conformance class must include a header comment block before the namespace declaration with these fields:

- spec URL (deep link to the exact subsection on the spec website)
- graphql-js source file and test file

The spec draft version is centralized in `Infrastructure/SpecMetadata.cs` -- reference it via `see SpecMetadata.Version` rather than repeating the version per-class.

```csharp
// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Leaf-Field-Selections
// graphql-js source: src/validation/rules/ScalarLeafsRule.ts
// graphql-js tests: src/validation/__tests__/ScalarLeafsRule-test.ts
```

A reviewer should be able to click the spec URL and land on the exact subsection, without searching the repository or manually navigating the spec website.

### Multi-Section Classes

Do not use multiple `[SpecSection]` attributes on a single class. Each conformance class must map to exactly one spec subsection. If an existing class has multiple attributes, split it into separate classes.

### Sub-Subsection Handling

Some spec subsections contain further sub-subsections. For example, section 5.5.2.3 "Fragment Spread Is Possible" has sub-subsections 5.5.2.3.1 through 5.5.2.3.5 ("Object Spreads In Object Scope", "Abstract Spreads in Object Scope", etc.). The manifest lists only the parent subsection (`5.5.2.3`), and a single conformance class covers the entire subsection including all of its children. Do not create separate manifest entries or conformance classes for individual sub-subsections -- fold their test cases into the parent class.

## Test Patterns

### Native Conformance Class

The standard pattern for a native conformance class with multiple test cases:

```csharp
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Leaf-Field-Selections
// graphql-js source: src/validation/rules/ScalarLeafsRule.ts
// graphql-js tests: src/validation/__tests__/ScalarLeafsRule-test.ts

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fields;

[SpecSection("5.3.3", "Leaf Field Selections")]
public class LeafFieldSelectionsConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.ScalarLeafs;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "valid_scalar_selection",
            """
            fragment scalarSelection on Dog {
              barks
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "object_type_missing_selection",
            """
            query directQueryOnObjectWithoutSubFields {
              human
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_scalar_leaf_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative scalar leaf validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_scalar_leaf_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}
```

Key conventions:

- `TheoryData<string, string>` for valid queries: `(caseName, query)`
- `TheoryData<string, string, int>` for invalid queries: `(caseName, query, errorCount)`
- `[Theory] [MemberData(nameof(ValidQueries))]` to wire up test data
- For subsections with only 1-2 cases, use `[Fact]` instead of TheoryData
- Keep GraphQL documents as formatted raw string literals, not compressed single-line strings

### Gap Placeholder

When a subsection is in the manifest but not yet implemented:

```csharp
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Field-Selection-Merging
// graphql-js source: src/validation/rules/OverlappingFieldsCanBeMergedRule.ts
// graphql-js tests: src/validation/__tests__/OverlappingFieldsCanBeMergedRule-test.ts

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fields;

[SpecSection("5.3.2", "Field Selection Merging")]
public class FieldSelectionMergingConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.OverlappingFieldsCanBeMerged;

    [Fact(Skip = "Broader graphql-js overlap-port remains a conformance gap; tracked via follow-up issue.")]
    public void graphql_js_overlap_matrix_is_not_yet_ported()
    {
    }
}
```

### Skip Message Convention

Every skipped test must explain the gap. Use this pattern:

```
[Fact(Skip = "Description of the gap; tracked via follow-up issue.")]
[Theory(Skip = "Negative X validation cases are a conformance gap tracked in follow-up issue.")]
```

The message should describe whether the gap is an implementation gap, incomplete port, spec ambiguity, or non-applicable case.

### Harness API

`SpecValidationRuleHarness` extends `ValidationRuleHarness` and provides these methods:

| Method | Use when |
|---|---|
| `QueryShouldPass(string query)` | Query should produce zero validation errors against TestSchema |
| `QueryShouldPass(Schema schema, string query)` | Same, but against a custom schema |
| `QueryShouldFail(string query)` | Query should produce at least one error against TestSchema |
| `QueryShouldFail(string query, int errorCount)` | Query should produce exactly N errors against TestSchema |
| `QueryShouldFail(Schema schema, string query)` | At least one error against a custom schema |
| `QueryShouldFail(Schema schema, string query, int errorCount)` | Exactly N errors against a custom schema |

The inherited `RuleUnderTest` property (abstract on `ValidationRuleHarness`) determines which single validation rule is exercised. Set it via:
```csharp
public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.ScalarLeafs;
```

### Available Validation Rules

These are the `QueryValidationRules.*` values available for `RuleUnderTest`:

`ExecutableDefinitions`, `FieldsOnCorrectType`, `FragmentsOnCompositeTypes`, `KnownArgumentNames`, `KnownDirectives`, `KnownFragmentNames`, `KnownTypeNames`, `LoneAnonymousOperation`, `NoFragmentCycles`, `NoUndefinedVariables`, `NoUnusedFragments`, `NoUnusedVariables`, `OverlappingFieldsCanBeMerged`, `PossibleFragmentSpreads`, `ProvidedRequiredArguments`, `ScalarLeafs`, `SingleFieldSubscriptions`, `UniqueArgumentNames`, `UniqueDirectivesPerLocation`, `UniqueFragmentNames`, `UniqueInputFieldNames`, `UniqueOperationNames`, `UniqueVariableNames`, `ValuesOfCorrectType`, `VariablesAreInputTypes`, `VariablesInAllowedPosition`

## Coverage Manifest

`Infrastructure/SpecCoverageManifest.cs` lists every spec subsection that should have a conformance class. Currently it contains only `ValidationSections` (Chapter 5).

`Infrastructure/ValidationCoverageTests.cs` uses reflection to discover all `[SpecSection]` attributes in the assembly and fails the build if any manifest entry lacks a corresponding class.

When adding a new subsection:

1. Add the section number to `SpecCoverageManifest.ValidationSections`
2. Create the conformance class (native conformance class or gap placeholder)
3. Run `dotnet test --filter "FullyQualifiedName~ValidationCoverageTests"` to verify consistency

The manifest must never silently drift from the actual test surface.

### Known Exclusions

Some spec subsections are intentionally absent from the manifest because GraphZen does not have a corresponding validation rule. These are documented here so agents do not attempt to add them:

- **5.2.1.1 "Operation Type Existence"** -- GraphZen does not implement this as a standalone validation rule.

If you encounter a spec subsection without a manifest entry, check this list before adding it. If the subsection is not listed here and is genuinely missing, add it to the manifest and create a conformance class or gap placeholder.

## Quality Standards

- **Traceability**: every class and test maps to a concrete spec section
- **Readability**: tests should read like the spec, not framework plumbing
- **Normative fidelity**: assert observable GraphQL behavior (results, errors, nullability), not internal implementation
- **Gap visibility**: missing coverage must be explicit -- never silently absent
- **Determinism**: failures must be stable and reproducible
- **Implementation independence**: tests verify GraphQL behavior, not GraphZen internals

### Test Design

- One test should prove one thing (one rule allowed, one rule rejected)
- Avoid "kitchen sink" tests unless the spec itself tests composition
- Separate positive, negative, and edge-case coverage clearly
- Do not assert internal GraphZen classes or visitors in conformance tests
- Keep GraphQL inputs visible in the test -- avoid helpers that hide the query or expected outcome
- Repeat simple setup if it makes the rule easier to understand

### Fixtures and Schemas

Most validation tests use the shared `TestSchema` from `ValidationRuleHarness`. Introduce a section-local schema when a rule needs a smaller or clearer setup. Do not force every section through one global mega-schema if it hurts readability. Long-term, the conformance project should own its own minimal fixtures.

`TestSchema` (defined in `ValidationRuleHarness`) includes:

- **Interfaces**: Being, Pet, Canine, Intelligent
- **Objects**: Dog, Cat, Human, Alien, ComplicatedArgs, QueryRoot
- **Unions**: CatOrDog, DogOrHuman, HumanOrAlien
- **Enums**: DogCommand, FurColor
- **Input Objects**: ComplexInput
- **Custom Scalars**: Invalid, Any
- **Directives**: onQuery, onMutation, onSubscription, onField, onFragmentDefinition, onFragmentSpread, onInlineFragment

QueryRoot exposes fields for `human`, `alien`, `cat`, `pet`, `catOrDog`, `dogOrHuman`, `humanOrAlien`, `complicatedArgs`, `invalidArg`, and `anyArg`.

### Review Standards

- Organize PR changes by spec area, not by incidental helper edits
- Avoid mixing large unrelated refactors into conformance work
- Keep infrastructure small and general

## Porting from graphql-js

When porting upstream test cases:

- Preserve the original intent before adapting style
- Keep one C# test close to one upstream case unless combining materially improves clarity
- Keep case names recognizable
- Do not port reference implementation quirks that aren't required by the spec
- Mark cases that are reference-only or not applicable to GraphZen

## Project Intent

This project should become the canonical home for spec conformance coverage. The desired end state:

- The directory reads like an executable appendix to the GraphQL spec
- The coverage report reads like a conformance statement
- Gaps are impossible to miss
- One conformance class per exact spec subsection
- Every subsection not yet implemented is represented as an explicit placeholder or manifest entry
- Chapters beyond Validation (Language, Type System, Introspection, Execution, Response) are represented with the same structure
