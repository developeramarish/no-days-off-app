using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoDaysOffApp.Features.Core;

namespace NoDaysOffApp.Features.ScheduledExercises
{
    [Authorize]
    [RoutePrefix("api/scheduledExercises")]
    public class ScheduledExerciseController : BaseApiController
    {
        public ScheduledExerciseController(IMediator mediator)
            :base(mediator) { }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateScheduledExerciseCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateScheduledExerciseCommand.Request request) => Ok(await Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateScheduledExerciseCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateScheduledExerciseCommand.Request request) => Ok(await Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetScheduledExercisesQuery.Response))]
        public async Task<IHttpActionResult> Get() => Ok(await Send(new GetScheduledExercisesQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetScheduledExerciseByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri]GetScheduledExerciseByIdQuery.Request request) => Ok(await Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveScheduledExerciseCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveScheduledExerciseCommand.Request request) => Ok(await Send(request));

    }
}
