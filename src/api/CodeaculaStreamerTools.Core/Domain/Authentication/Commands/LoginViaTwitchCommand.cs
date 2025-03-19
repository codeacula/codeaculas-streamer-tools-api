namespace CodeaculaStreamerTools.Core.Domain.Authentication.Commands;

using CodeaculaStreamerTools.Core.Common.CQRS;
using CodeaculaStreamerTools.Core.Domain.Authentication.Results;

public record LoginViaTwitchCommand(string Code, string State) : CommandBase<LoginViaTwitchResult>;
