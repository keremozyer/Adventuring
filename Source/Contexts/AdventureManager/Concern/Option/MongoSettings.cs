using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Architecture.Model.Interface.Validation;

namespace Adventuring.Contexts.AdventureManager.Concern.Option;

/// <summary></summary>
public class MongoSettings : IOptionService, IValidatableSetting
{
    /// <summary>
    /// Mongo host's connection string.
    /// </summary>
    public required string ConnectionString { get; set; }
    /// <summary>
    /// Database to use.
    /// </summary>
    public required string DatabaseName { get; set; }
    /// <summary>
    /// Flag indicating whether the current application environment supports transactions or not.
    /// </summary>
    public required bool? DoesSupportTransactions { get; set; }

    /// <summary>
    /// Validates the setting instance and returns the validation result without throwing it.
    /// </summary>
    /// <returns></returns>
    public InvalidReferenceDataException? Validate()
    {
        List<InvalidReferenceDataExceptionMessage>? errors = null;

        if (String.IsNullOrWhiteSpace(this.ConnectionString))
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(MongoSettings)}/{nameof(this.ConnectionString)}"));
        }

        if (String.IsNullOrWhiteSpace(this.DatabaseName))
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(MongoSettings)}/{nameof(this.DatabaseName)}"));
        }

        if (this.DoesSupportTransactions is null)
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(MongoSettings)}/{nameof(this.DoesSupportTransactions)}"));
        }

        return errors!.HasElements() ? new InvalidReferenceDataException(errors!) : null;
    }
}