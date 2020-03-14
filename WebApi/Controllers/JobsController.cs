using System.Threading.Tasks;
using AutoMapper;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repos.Abstract;
using WebApi.Filters;
using WebApi.Helpers;
using WebApi.Requests;
using WebApi.Responses;
using X.PagedList;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobsController(IJobRepository jobRepository, IMapper mapper)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<JobIndexResponse>> Index(int page = 1)
        {
            var pagedList = await _jobRepository.All.ToPagedListAsync(page, 5);

            var res = Json.GeneratePagedListAnswer(pagedList);
            
            return res;
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Job>> Create(JobRequest request)
        {
            var job = _mapper.Map<JobRequest, Job>(request);

            await _jobRepository.SaveAsync(job);

            return job;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [JobExistsFilter]
        public async Task<ActionResult<JobItemResponse>> Read(int id)
        {
            var job = await _jobRepository.All.FirstOrDefaultAsync(x => x.JobId == id);

            var jobResponse = _mapper.Map<Job, JobItemResponse>(job);

            return jobResponse;
        }

        [HttpPatch]
        [Route("[action]/{id}")]
        [JobExistsFilter]
        public async Task<ActionResult<Job>> Update(int id, [FromBody] JobRequest request)
        {
            var job = _mapper.Map<JobRequest, Job>(request);
            job.JobId = id;

            await _jobRepository.SaveAsync(job);

            return job;
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        [JobExistsFilter]
        public async Task<ActionResult<object>> Delete(int id)
        {
            await _jobRepository.DeleteAsync(id);
            return Http.GenerateOkAnswer($"Job with id {id} was deleted");
        }
    }
}