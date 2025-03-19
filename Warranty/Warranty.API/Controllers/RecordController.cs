using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warranty.API.PostModels;
using Warranty.Core.DTOs;
using Warranty.Core.Interfaces.Services;
using Warranty.Core.Models;
using Warranty.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Warranty.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordServices _iService;
        private readonly IMapper _mapper;

        public RecordController(IRecordServices iService, IMapper mapper)
        {
            _iService = iService;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordDto>>> Get()
        {
            var records = _iService.GetAllRecords();
            return records == null ? NotFound() : Ok(records);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordDto>> Get(int id)
        {
            var record = await _iService.GetRecordById(id);
            return record == null ? NotFound() : Ok(record);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<RecordDto>> GetbyUserId(int userId)
        {
            var record = await _iService.GetRecordsByUserId(userId);
            return Ok(record);
            //return record == null ? NotFound() : Ok(record);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost]
        public async Task<ActionResult<RecordDto>> Post([FromBody] RecordPostModel recordPostModel)
        {
            var recordDto = _mapper.Map<RecordDto>(recordPostModel);
            recordDto = await _iService.AddRecord(recordDto);
            return recordDto == null ? NotFound() : Ok(recordDto);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] RecordPostModel recordPostModel)
        {
            var recordDto = _mapper.Map<RecordDto>(recordPostModel);
            recordDto = await  _iService.UpdateRecord(id, recordDto);
            return recordDto == null ? NotFound() : Ok(recordDto);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _iService.DeleteRecord(id) ? Ok(true) : NotFound();
        }
    }
}
