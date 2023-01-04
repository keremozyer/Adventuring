using Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Adventuring.Architecture.Application.Web.Concern.Filter.StartupFilters;

/// <summary>
/// Startup filter to validate configurable settings.
/// </summary>
public class SettingValidationStartupFilter : IStartupFilter
{
    private readonly IEnumerable<IValidatableSetting> ValidatableSettings;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="validatableSettings"></param>
    public SettingValidationStartupFilter(IEnumerable<IValidatableSetting> validatableSettings)
    {
        this.ValidatableSettings = validatableSettings;
    }

    /// <summary>
    /// When added to the pipeline this method will call <see cref="IValidatableSetting.Validate"/> method of all registered <see cref="IValidatableSetting"/> in the dependency injection container and throw the result if there are any errors.
    /// </summary>
    /// <param name="next"></param>
    /// <returns></returns>
    /// <exception cref="InvalidReferenceDataException">All validation errors in the configurable settings.</exception>
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        List<InvalidReferenceDataExceptionMessage>? errors = null;
        foreach (IValidatableSetting validatableObject in this.ValidatableSettings ?? Array.Empty<IValidatableSetting>())
        {
            errors = errors.AddRangeSafe(validatableObject.Validate()?.Messages?.Cast<InvalidReferenceDataExceptionMessage>());
        }

        return errors!.IsNullOrEmpty() ? next : throw new InvalidReferenceDataException(errors!);
    }
}