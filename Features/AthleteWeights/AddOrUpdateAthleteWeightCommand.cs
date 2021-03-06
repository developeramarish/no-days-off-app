using MediatR;
using NoDaysOffApp.Data;
using NoDaysOffApp.Model;
using NoDaysOffApp.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace NoDaysOffApp.Features.AthleteWeights
{
    public class AddOrUpdateAthleteWeightCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public AthleteWeightApiModel AthleteWeight { get; set; }            
			public Guid CorrelationId { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(NoDaysOffAppContext context, IEventBus bus)
            {
                _context = context;
                _bus = bus;
            }

            public async Task<Response> Handle(Request request)
            {
                var entity = await _context.AthleteWeights
                    .Include(x => x.Tenant)
                    .Include(x => x.Athlete)
                    .SingleOrDefaultAsync(x => x.Id == request.AthleteWeight.Id 
                    && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    var athlete = await _context.Athletes.SingleAsync(x => x.Username == request.Username);
                    _context.AthleteWeights.Add(entity = new AthleteWeight() { TenantId = tenant.Id, AthleteId = athlete.Id });
                }

                entity.WeightInKgs = request.AthleteWeight.WeightInKgs;

                entity.WeighedOn = request.AthleteWeight.WeighedOn;

                await _context.SaveChangesAsync();

                _bus.Publish(new AddedOrUpdatedAthleteWeightMessage(AthleteWeightApiModel.FromAthleteWeight(entity), request.CorrelationId, request.TenantUniqueId));

                return new Response();
            }

            private readonly NoDaysOffAppContext _context;
            private readonly IEventBus _bus;
        }
    }
}
