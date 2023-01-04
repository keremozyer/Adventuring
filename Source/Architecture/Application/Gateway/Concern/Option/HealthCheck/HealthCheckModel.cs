using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.Validation;

namespace Adventuring.Architecture.Application.Gateway.Concern.Option.HealthCheck;

public class HealthCheckModel : IValidatableSetting
{
    public required string URL { get; set; }
    public required int? Retries { get; set; }
    public required int? IntervalInMilliseconds { get; set; }

    public InvalidReferenceDataException? Validate()
    {
        List<InvalidReferenceDataExceptionMessage>? errors = null;

        if (String.IsNullOrWhiteSpace(this.URL))
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(HealthCheckModel)}/{nameof(this.URL)}"));
        }

        if (this.Retries is null)
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(HealthCheckModel)}/{nameof(this.Retries)}"));
        }
        else if (this.Retries.Value < 0)
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.InvalidConfigurationValue, $"{nameof(HealthCheckModel)}/{nameof(this.Retries)}"));
        }

        if (this.IntervalInMilliseconds is null)
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(HealthCheckModel)}/{nameof(this.IntervalInMilliseconds)}"));
        }
        else if (this.IntervalInMilliseconds.Value < 0)
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.InvalidConfigurationValue, $"{nameof(HealthCheckModel)}/{nameof(this.IntervalInMilliseconds)}"));
        }

        return errors!.HasElements() ? new InvalidReferenceDataException(errors!) : null;
    }
}