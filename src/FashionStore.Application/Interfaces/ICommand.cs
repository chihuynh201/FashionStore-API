using FashionStore.Application.Common.DTOs.Response;
using MediatR;

namespace FashionStore.Application.Interfaces;
public interface ICommand : IRequest<ActionResponse>
{
}

public interface ICommand<TResponse> : IRequest<ActionResponse<TResponse>>
{
}


public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ActionResponse>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, ActionResponse<TResponse>>
    where TCommand : ICommand<TResponse>
{
}