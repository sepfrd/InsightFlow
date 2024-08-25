namespace InsightFlow.Common.Dtos.Configurations;

public class CaptchaConfigurationDto
{
    public required string NonceKey { get; init; }

    public required string HiddenInputName { get; init; }

    public required string EncryptionKey { get; init; }

    public required string HiddenTokenName { get; init; }

    public required string InputName { get; init; }

    public required string Identifier { get; init; }

    public required int ExpirationPeriodMinutes { get; init; }
}