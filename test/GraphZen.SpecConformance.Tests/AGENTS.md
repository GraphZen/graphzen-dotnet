# AGENTS.md

This directory contains GraphZen's specification conformance suite.

The goal is not just "more tests." The goal is an executable conformance statement that makes it easy to answer:

- what part of the GraphQL spec is represented
- what part is enforced
- what part is still a known gap
- what part is not applicable to GraphZen

## Primary Quality Attributes

Every conformance test should optimize for:

- **Traceability**: each class and test maps to a concrete spec section and, where possible, a specific normative statement
- **Readability**: the tests should read close to the spec and close to GraphQL examples, not like framework plumbing
- **Normative fidelity**: assertions should reflect required observable behavior, not inferred internal implementation details
- **Gap visibility**: missing coverage, skipped cases, spec ambiguities, and implementation gaps must be explicit
- **Determinism**: failures and ordering should be stable and easy to compare over time
- **Reviewability**: diffs should be small, obvious, and clearly tied to spec behavior
- **Versioned conformance**: the suite must make clear which spec draft and reference cases it aligns with
- **Implementation independence**: tests should verify externally visible GraphQL behavior, not GraphZen internals

## Project Intent

This project should become the canonical home for spec conformance coverage.

- Reusing existing test classes from other test projects is acceptable as an intermediate step
- The long-term target is a self-contained conformance suite with minimal dependence on legacy test organization
- Existing tests may be wrapped here for traceability, but new conformance work should prefer native conformance classes in this project

## Specification Sources

When mapping tests to the GraphQL specification, use these sources explicitly:

- website draft: `https://spec.graphql.org/draft/`
- website index: `https://spec.graphql.org/`
- local spec repository clone: `~/Code/graphql/graphql-spec`
- upstream spec repository: `https://github.com/graphql/graphql-spec`
- local `graphql-js` repository clone: `~/Code/graphql/graphql-js`
- upstream `graphql-js` repository: `https://github.com/graphql/graphql-js`

Use them for different purposes:

- prefer the spec website for canonical section numbers, headings, and navigation
- prefer the local `graphql-spec` clone for source markdown, exact wording, and repository history
- use the upstream GitHub repository when linking source files, issues, pull requests, or commits for reviewer context
- prefer the local `graphql-js` clone when tracing reference implementation behavior, locating upstream tests, or checking how a spec rule is exercised in practice
- use the upstream `graphql-js` repository when linking reference implementation source files, test cases, issues, pull requests, or commits for reviewer context

Do not invent section names or subsection structure from memory when the website or spec repo can be checked directly.

## Folder and Namespace Layout

Mirror the GraphQL specification hierarchy as directly as possible.

- Prefer `Section2_Language`, `Section3_TypeSystem`, `Section4_Introspection`, `Section5_Validation`, `Section6_Execution`, and `Section7_Response`
- Within each section, mirror the spec subsection structure in folders, namespaces, and class names
- Prefer one conformance class per spec subsection
- Prefer one test method per normative rule, allowance, prohibition, or example

If a subsection is not yet implemented, represent it explicitly with a placeholder class or coverage manifest entry instead of leaving it invisible.

### Required Mapping Rules

The mapping from specification to code should be explicit and predictable:

- folder maps to spec chapter or subsection group
- namespace maps to the same chapter or subsection group as the folder
- class maps to one exact spec subsection
- method maps to one exact normative statement, allowance, prohibition, algorithm branch, or worked example

The intended shape is:

- folder: `Section5_Validation/Fields/`
  maps to spec Chapter 5 and the `Fields` subsection group
- namespace: `GraphZen.SpecConformance.Tests.Section5_Validation.Fields`
  maps to the same group as the folder
- class: `FieldSelectionsConformanceTests`
  maps to one exact subsection such as `5.3.1 Field Selections`
- method: `object_field_selection_is_valid()`
  maps to one exact rule or example inside that subsection

### Naming Rules

Use names that make the spec correspondence obvious without opening the body:

- folders should use spec-oriented group names, not GraphZen implementation names
- namespaces should match folders exactly
- classes should be named after the subsection heading, suffixed with `ConformanceTests`
- methods should read like executable spec prose and describe one specific claim

Examples:

- `Section2_Language/SelectionSets/SelectionSetConformanceTests.cs`
- `GraphZen.SpecConformance.Tests.Section2_Language.SelectionSets`
- `[SpecSection("2.5", "Selection Sets")]`
- `selection_set_may_contain_fields_and_fragments()`

- `Section6_Execution/FieldExecution/FieldExecutionConformanceTests.cs`
- `GraphZen.SpecConformance.Tests.Section6_Execution.FieldExecution`
- `[SpecSection("6.4", "Executing Fields")]`
- `field_errors_produce_null_at_the_response_position()`

Avoid names like:

- `ExecutorTests`
- `ParserTests`
- `KnownTypeNamesTests`

unless they are temporary wrappers around legacy tests. Native conformance classes should prefer spec language over implementation rule names.

### Wrapper vs Native Rule

If a class is only wrapping an existing legacy test class:

- the wrapper class still must map to one exact spec subsection
- the wrapper class name should be spec-oriented even if the inherited class is not
- the wrapper should be treated as transitional structure, not the desired end state

For new work, prefer native conformance classes in this project over wrappers.

## Metadata and Traceability

Every conformance class must declare explicit spec metadata.

- Use `SpecSection` attributes for the exact subsection being represented
- Preserve hierarchical filtering support so broader section filters still work
- Keep the current spec draft version centralized in shared infrastructure
- Where a case comes from `graphql-js`, preserve the upstream case intent and, when practical, the original case name
- If a test is skipped, it must state why and should reference a follow-up issue

Preferred metadata to preserve in code or adjacent documentation:

- spec draft version
- exact spec subsection
- exact subsection heading from the spec website or source markdown
- upstream `graphql-js` source file
- upstream test case name
- status: `implemented`, `known_gap`, `not_applicable`, or `spec_ambiguity`

### Concrete Metadata Guidance

Use the smallest number of locations that keep the metadata obvious at review time.

- before naming a class or assigning a `SpecSection`, confirm the subsection heading in the local spec source, not from memory
- for example, `~/Code/graphql/graphql-spec/spec/Section 5 -- Validation.md` currently contains:
- `### Field Selections`
- `### Field Selection Merging`
- `### Leaf Field Selections`
- keep the spec draft version centralized in `GraphZen.SpecConformance.Tests.Infrastructure.SpecMetadata.Version`
- keep the exact subsection number and heading on the class with `SpecSection`
- keep `graphql-js` source file, upstream case name, and status either:
- in a short class header comment when the whole class shares the same provenance and status
- in an adjacent manifest or coverage file when the class mixes multiple upstream cases or statuses
- in a method-level comment only when one method materially differs from the rest of the class

Preferred future-state shape for one exact subsection:

```csharp
// Spec draft: draft-2026-04-02 (see SpecMetadata.Version)
// Spec subsection: 5.3.3
// Spec heading: Leaf Field Selections
// Spec source: spec/Section 5 -- Validation.md
// graphql-js source: src/validation/rules/ScalarLeafsRule.ts
// graphql-js tests: src/validation/__tests__/ScalarLeafsRule-test.ts
// graphql-js case(s): "valid scalar selection", "object type missing selection"
// Status: implemented

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fields;

[SpecSection("5.3.3", "Leaf Field Selections")]
public class LeafFieldSelectionsConformanceTests : SpecValidationRuleHarness
{
    // ...
}
```

If the class is a transitional wrapper around an existing legacy rule suite, be explicit about that instead of pretending it is already in final form:

```csharp
// Transitional wrapper around an implementation-named legacy suite.
// Spec draft: draft-2026-04-02 (see SpecMetadata.Version)
// Spec subsection: 5.3.3
// Spec heading: Leaf Field Selections
// Spec source: spec/Section 5 -- Validation.md
// graphql-js source: src/validation/rules/ScalarLeafsRule.ts
// graphql-js tests: src/validation/__tests__/ScalarLeafsRule-test.ts
// Status: known_gap

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fields;

[SpecSection("5.3.3", "Leaf Field Selections")]
public class ScalarLeafsConformanceTests : SpecValidationRuleHarness
{
    // ...
}
```

The point is not the exact comment format. The point is that a reviewer should be able to answer all six metadata questions without searching the whole repository.

Every represented subsection should be traceable in all four places:

- the file path
- the namespace
- the class name
- the `SpecSection` attribute

### Concrete Traceability Example

For spec subsection `5.3.3 Leaf Field Selections`, the preferred mapping is:

- file path: `test/GraphZen.SpecConformance.Tests/Section5_Validation/Fields/LeafFieldSelectionsConformanceTests.cs`
- namespace: `GraphZen.SpecConformance.Tests.Section5_Validation.Fields`
- class name: `LeafFieldSelectionsConformanceTests`
- attribute: `[SpecSection("5.3.3", "Leaf Field Selections")]`
- spec source to verify the heading: `~/Code/graphql/graphql-spec/spec/Section 5 -- Validation.md`
- upstream reference tests for case names: `~/Code/graphql/graphql-js/src/validation/__tests__/ScalarLeafsRule-test.ts`

That gives a reviewer a straight path:

- `Section5_Validation` tells them the spec chapter
- `Fields` tells them the subsection group
- `LeafFieldSelectionsConformanceTests` tells them the exact subsection heading
- `SpecSection("5.3.3", "Leaf Field Selections")` confirms the canonical subsection number and heading

If one of those four says `FieldsOnCorrectType`, another says `LeafFieldSelections`, and the attribute says `5.3.3`, the structure is wrong even if the assertions are useful. Rename or split the tests until the mapping is unambiguous.

### Transitional Multi-Section Classes

Some current wrapper classes still represent more than one subsection by carrying multiple `SpecSection` attributes. Treat that as temporary migration structure, not the desired steady state.

- acceptable short term: one wrapper class with multiple `SpecSection` attributes while coverage is being lifted into this project
- preferred end state: split into one class per exact subsection
- when touching a multi-section wrapper for substantive new conformance work, prefer splitting it rather than adding more mixed coverage

Adjacent documentation or coverage manifests should follow the same exact subsection identity as the class they describe.

If those four representations disagree, fix the structure rather than relying on comments to explain it.

## Readability Standards

Conformance tests should read like executable spec text.

- Name classes and methods in language that matches the spec, not local implementation jargon
- Keep GraphQL documents formatted as real GraphQL, not compressed string blobs
- Prefer short, section-local helpers over deep abstraction stacks
- Avoid helpers that hide the actual GraphQL input or expected outcome
- Repeat simple setup if it makes the rule easier to understand
- Comments should explain intent only when the spec nuance is not obvious from the test itself

The reader should usually understand the rule and expected behavior without opening product code.

Where practical, a reviewer should also be able to line up:

- the subsection heading in the spec website
- the conformance class name
- the individual method names

without translation.

## Test Design Rules

Prefer tests with a single clear responsibility.

- A positive test should usually prove one thing is allowed
- A negative test should usually prove one thing is rejected
- Avoid broad "kitchen sink" tests unless the spec itself is testing composition
- Separate positive, negative, edge-case, and not-applicable coverage clearly
- Assert observable GraphQL behavior only: results, errors, nullability, ordering guarantees if required, and schema shape

Do not write conformance tests that assert internal GraphZen classes, visitors, or intermediate structures unless the section is explicitly about language or syntax objects.

## Shared Fixtures and Harnesses

Harnesses should compress repetition without obscuring meaning.

- Prefer canonical shared schemas only when they are already widely recognized and documented
- Keep shared schemas minimal and spec-oriented
- Introduce section-local schemas when a rule needs a smaller or clearer setup
- Do not force every section through one global mega-schema if it hurts readability
- Failure diagnostics should include the spec section and preserve enough context to understand the mismatch quickly

Long term:

- Section-specific harnesses are preferred over one catch-all harness
- The conformance project should own its own minimal fixtures where possible

## Coverage and Reporting

Coverage must be machine-reportable.

The suite should be able to distinguish:

- represented and passing
- represented and failing
- represented but skipped
- not yet represented
- not applicable

The coverage manifest should never silently drift away from the actual test surface.

- Keep section manifests explicit
- Prefer generated reports over hand-maintained checklists when practical
- A reviewer should be able to answer "what percent of Section X is enforced?" without manual counting

## Skips, Gaps, and Follow-Up Work

Skips are allowed only when they make the gap clearer, not easier to ignore.

- Every skip must describe whether it is an implementation gap, incomplete port, spec ambiguity, or non-applicable case
- Every intentional skip should link to a follow-up issue
- Placeholder tests should be used to represent known uncovered subsections, not to fake conformance
- Do not change GraphZen implementation solely to make a weak conformance test pass
- Do not weaken a test just to get green CI if the spec behavior is still missing

Green with skips is acceptable only when the skips are explicit and actionable.

## Upstream Porting Guidance

When porting from `graphql-js`:

- preserve the original intent before adapting style
- keep one C# test close to one upstream case unless combining them materially improves clarity
- keep case names recognizable
- do not port reference implementation quirks that are not required by the spec
- mark cases that are reference-only or not applicable to GraphZen

## Review Standards

A conformance PR should be easy to review by section.

- organize changes by spec area, not by incidental helper edits
- avoid mixing large unrelated refactors into conformance work
- keep new infrastructure small and general
- prefer obvious test placement over clever reuse

The desired end state is:

- the directory reads like an executable appendix to the GraphQL spec
- the coverage report reads like a conformance statement
- gaps are impossible to miss
