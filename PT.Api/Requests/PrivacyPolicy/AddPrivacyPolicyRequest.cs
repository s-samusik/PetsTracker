using PT.Domain.Enums;

namespace PT.Api.Requests.PrivacyPolicy;

public sealed record AddPrivacyPolicyRequest(string Text, UserType UserType);
