using HR.LeaveManagement.Application.features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.features.LeaveRequest.Queries.GetLeaveRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool IsLoggedInUser = false)
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListQuery());
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailsQuery() { Id = id});
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(CreateLeaveRequestCommand createLeaveRequestCommand)
        {
            var response = await _mediator.Send(createLeaveRequestCommand);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveRequestCommand updateLeaveRequestCommand)
        {
            await _mediator.Send(updateLeaveRequestCommand);
            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [Route("CancelRequest")]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequestCommand)
        {
            await _mediator.Send(cancelLeaveRequestCommand);
            return NoContent();
        }

        [HttpPut]
        [Route("UpdateApproval")]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand changeLeaveRequestApprovalCommand)
        {
            await _mediator.Send(changeLeaveRequestApprovalCommand);
            return NoContent();
        }
    }
}
