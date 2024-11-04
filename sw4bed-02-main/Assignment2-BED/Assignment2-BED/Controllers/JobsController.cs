using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2_BED.Data;
using Assignment2_BED.Models;
using AutoMapper;
using Mapster;
using Assignment2_BED.Properties.Models;

namespace Assignment2_BED.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ModelManageDB _context;

        public JobsController(ModelManageDB context)
        {
            _context = context;
        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobCreated>> PostJob(JobCreate jobCreate)
        {
            _context.job.Add(jobCreate.Adapt<Job>());
            await _context.SaveChangesAsync();

            var dbJobs = await _context.job.ToListAsync();

            return Accepted(dbJobs.Adapt<List<JobCreated>>());
        }

        // DELETE: 
        [HttpDelete("{jobId}")]
        public async Task<IActionResult> DeleteJob(long jobId)
        {
            var job = await _context.job.FindAsync(jobId);

            if (job == null)
            {

                return NotFound("ID not found");
            }

            _context.job.Remove(job);
            await _context.SaveChangesAsync();

            return Ok(await _context.job.ToListAsync());
        }

        // PUT: api/Jobs/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{jobId}")]
        public async Task<IActionResult> PutJob(long jobId, JobUpdate jobUpdate)
        {
            var Dbjob = await _context.job.FindAsync(jobId);
            if (Dbjob == null) { return NotFound("job not found"); }

            var job = jobUpdate.Adapt(Dbjob);
            _context.job.Update(job);

            await _context.SaveChangesAsync();

            return Ok(Dbjob);
        }

        // PUT: api/Jobs/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //tilføj model til job
        [HttpPut("{jobId}/AddModel/{modelId}")]
        public async Task<IActionResult> PutModelOnJob(long jobId, long modelId)
        {
            var Dbjob = await _context.job.FindAsync(jobId);
            if (Dbjob == null) { return NotFound("job not found"); }

            var Dbmodel = await _context.model.FindAsync(modelId);
            if (Dbmodel == null) { return NotFound("model not found"); }

            _context.Entry(Dbjob).Collection(j => j.Models).Load();

            _context.Entry(Dbmodel).Collection(m => m.Jobs).Load();

            if (Dbjob.Models.Contains(Dbmodel)) { return Conflict("model already assigned to this job"); }
            Dbjob.Models.Add(Dbmodel);

            await _context.SaveChangesAsync();

            return Accepted(Dbmodel.Adapt<JobModel>());
        }

        [HttpDelete("{jobId}/RemoveModel/{modelId}\"")]
        public async Task<IActionResult> DeleteModelFromJob(long jobId, long modelId)
        {
            var Dbjob = await _context.job.FindAsync(jobId);
            if (Dbjob == null) { return NotFound("job not found"); }

            var Dbmodel = await _context.model.FindAsync(modelId);
            if (Dbmodel == null) { return NotFound("model not found"); }

            _context.Entry(Dbjob).Collection(j => j.Models).Load();

            _context.Entry(Dbmodel).Collection(m => m.Jobs).Load();

            if (!Dbjob.Models.Contains(Dbmodel)) { return Conflict("model not assigned to this job"); }
            Dbjob.Models.Remove(Dbmodel);

            await _context.SaveChangesAsync();

            return Accepted(Dbmodel.Adapt<JobModel>());
        }

        // GET: api/Jobs
        [HttpGet("getModelNames/{modelId}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetjobWithModelName()
        {
            List<JobModelName> AllJobsWithModels = new List<JobModelName>();


            foreach (var job in _context.job)
            {

                await _context.Entry(job).Collection(j => j.Models).LoadAsync();

                var aJobWithModelNames = job.Adapt<JobModelName>();
                aJobWithModelNames.ModelNames = new List<string>();

                foreach (var model in job.Models)
                {
                    aJobWithModelNames.ModelNames.Add($"{model.FirstName} {model.LastName}");
                }

                AllJobsWithModels.Add(aJobWithModelNames);
            }

            return Ok(AllJobsWithModels);
        }

        // GET {model.id} get a lsit of all jobs for a model
        [HttpGet("WithModel/{modelId}")]
        public async Task<ActionResult<List<JobModel>>> GetJobsWithModels(long modelId)
        {
            var dbJobs = await _context.job.ToListAsync();


            dbJobs.ForEach(async j => await _context.Entry(j).Collection(j => j.Models).LoadAsync());


            dbJobs = dbJobs.Where(j => j.Models.Any(m => m.ModelId == modelId)).ToList();


            return Ok(dbJobs.Adapt<List<JobModel>>());
        }

        // GET {job.id} JobWithExpenses
        [HttpGet("WithExpenses/{jobId}")]
        public async Task<ActionResult<JobExpenses>> GetJobWithExpenses(long jobId)
        {
            var dbJob = await _context.job.FindAsync(jobId);

            if (dbJob == null) { return NotFound("Could not find job with id " + jobId); }

            _context.Entry(dbJob).Collection(j => j.Expenses).Load();
            _context.Entry(dbJob).Collection(j => j.Models).Load();

            // map the job to a jobWithExpenses and assign a new list of expenses to the jobWithExpenses list of expenses
            var jobWithExpenses = dbJob.Adapt<JobExpenses>();
            jobWithExpenses.Expenses = new List<ExpenseCreated>();

            if (dbJob == null) { return NotFound("Could not find job with id " + jobId); }

            // add all expenses related to the job to the jobWithExpenses object
            foreach (var expense in dbJob.Expenses)
            {
                jobWithExpenses.Expenses.Add(expense.Adapt<ExpenseCreated>());
            }

            return Ok(jobWithExpenses);
        }
    }
}





        //}
        //// GET: api/Models/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Model>> Getjob(long id)
        //{

        //}



        //private bool ModelExists(long id)
        //{

        //}
    
