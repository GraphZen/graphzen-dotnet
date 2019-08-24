#nullable disable
using GraphZen.Infrastructure;

namespace GraphZen
{
    public class InterfaceTypeFieldViaClrProperty_ConfigurationTests
    {
        // Convention:
        // can_be_added_by_convention
        // ignored_by_convention?

        // Data Annotation
        // can_be_ignored_by_data_annotation

        // Explicit
        // added_by_convention_can_be_ignored_by_explicit_configuration
        // ignored_by_data_annotation_can_be_ignored_by_explicit_configuration
        // ignored_by_data_annotation_can_be_unignored_by_explicit_configuration
    }
}