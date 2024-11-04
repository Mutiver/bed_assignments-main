using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2_BED.Data;
using Assignment2_BED.Models;
using Mapster;

namespace Assignment2_BED.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ModelManageDB _context;

        public ModelsController(ModelManageDB context)
        {
            _context = context;
        }

        // POST: api/Models : Opret model - Kun Grunddata - ikke jobs og udgifter
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModelBase>> PostModel(ModelBase modelCreate)
        {
            //_context.model.Add(model.Adapt<Model>());
            //await _context.SaveChangesAsync();

            //var dbModel = await _context.model.ToListAsync();

            //return Accepted(dbModel.Adapt<List<ModelBase>>());


            //var tempModel = model.Adapt<Model>();
            //_context.model.Add(tempModel);
            //await _context.SaveChangesAsync();

            //return Ok(tempModel.Adapt<ModelBase>());

            // use Mapster to map modelCreate to a Model
            var model = modelCreate.Adapt<Model>();

            // add the model to the database and save changes
            _context.model.Add(model);
            await _context.SaveChangesAsync();

            // return the model as a ModelBaseData
            return Ok(model.Adapt<ModelBase>());
        }

        // DELETE: api/Models/5 : Slet Model
        [HttpDelete("{modelId}")]
        public async Task<IActionResult> DeleteModel(long modelId)
        {
            var model = await _context.model.FindAsync(modelId);
            
            if(model == null)
            {
                return NotFound("ID not found");
            }

            _context.model.Remove(model);
            await _context.SaveChangesAsync();

            return Ok(await _context.model.ToListAsync());
        }

        // PUT: api/Models/5 : Opdatere model - Kun grunddata - Ikke jobs og udgifter
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{modelId}")]
        public async Task<IActionResult> PutModel(long modelId, ModelBase modelBase)
        {
            var model = await _context.model.FindAsync(modelId);
            if (model == null) return NotFound("Model not found");

            var tempModel = modelBase.Adapt(model);
            _context.model.Update(tempModel);

            await _context.SaveChangesAsync();

            return Ok(tempModel.Adapt<ModelBase>());
        }

        // GET: api/Models : Hente en liste med alle modeller - Uden data for deres jobs og udgifter
        //[HttpGet]
        //public async Task<ActionResult<List<ModelBase>>> Getmodels()
        //{
        //    if (_context.model == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.model.ToListAsync();
        //}
        [HttpGet]
        public async Task<ActionResult<List<ModelBase>>> Getmodels()
        {
            var dbModel = await _context.model.ToListAsync();
            if (dbModel == null)
            {
                return NotFound();
            }
            foreach (var model in dbModel)
            {
                _context.Entry(model)
                    .Collection(m => m.Jobs)
                    .Load();
            }

            return Ok(dbModel.Adapt<List<ModelBase>>());
        }

        // Get model from modelID with jobs and expenses
        [HttpGet("{modelId}")]
        public async Task<ActionResult<Model>> GetModelWithJobAndExpense(long modelId)
        {
            var dbModel = await _context.model.FindAsync(modelId);
            if (dbModel == null) { return NotFound("Could not find model with id " + modelId); }

            _context.Entry(dbModel).Collection(m => m.Expenses).Load();
            _context.Entry(dbModel).Collection(m => m.Jobs).Load();

            return Ok(dbModel);

        }

        //// GET: api/Models/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Model>> GetModel(long id)
        //{
        //    if (_context.model == null)
        //    {
        //        return NotFound();
        //    }
        //    var model = await _context.model.FindAsync(id);

        //    if (model == null)
        //    {
        //        return NotFound();
        //    }

        //    return model;
        //}

        //        private bool ModelExists(long id)
        //        {
        //            return (_context.model?.Any(e => e.ModelId == id)).GetValueOrDefault();
        //        }
    }
}
