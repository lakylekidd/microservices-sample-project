using System;
using MediatR;

namespace Ordering.API.Application.Commands
{
    public class IdentifiedCommand<T, R> : IRequest<R>
        where T : IRequest<R>
    {
        /// <summary>
        /// The identified command
        /// </summary>
        public T Command { get; }

        /// <summary>
        /// The request id
        /// </summary>
        public Guid Id { get; }

        // The constructor
        public IdentifiedCommand(T command, Guid id)
        {
            Command = command;
            Id = id;
        }
    }
}
