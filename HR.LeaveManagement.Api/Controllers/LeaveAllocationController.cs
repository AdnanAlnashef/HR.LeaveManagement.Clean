using HR.LeaveManagement.Application.features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveAllocationController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListQuery());
            return Ok(leaveAllocations);
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
        {
           var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailsQuery(id));
            return Ok(leaveAllocation);
        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(CreateLeaveAllocationCommand createLeaveAllocationCommand)
        {
            var response = await _mediator.Send(createLeaveAllocationCommand);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<LeaveAllocationController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(UpdateLeaveAllocationCommand updateLeaveAllocationCommand)
        {
            await _mediator.Send(updateLeaveAllocationCommand);
            return NoContent();
        }

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand() { Id = id};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
