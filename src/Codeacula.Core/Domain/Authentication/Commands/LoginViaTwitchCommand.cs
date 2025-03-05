namespace Codeacula.Core.Domain.Authentication.Commands;

using Codeacula.Core.Common.CQRS;
using Codeacula.Core.Domain.Authentication.Results;

public record LoginViaTwitchCommand(string Code, string State) : CommandBase<LoginViaTwitchResult>;
