using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_API_GeneralCatalog_1721030646.DTO;
using Repo_API_GeneralCatalog_1721030646.Models;
using Repo_API_1721030646.Repo.Generic_W1;

namespace Repo_API_GeneralCatalog_1721030646.Controllers.Generic_Repo_W1
{
    [Route("GeneralCatalog/Generic_Repo/Way_1/[controller]/[action]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        //First approach (way 1) about Repository Patten in Powerpoint file
        private readonly IRepo<School> _SchoolService;
        private readonly IMapper _mapper;
        public SchoolController(IRepo<School> SchoolService, IMapper mapper)
        {
            _SchoolService = SchoolService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDTO>> Get(int id)
        {
            var entity = await _SchoolService.GetAsync(id);
            if (entity != null)
            {
                var model = new SchoolDTO();
                _mapper.Map(entity, model);
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<School>> GetFull(int id)
        {
            // Returns the School Entity with full information such as: Country, Province, District, Ward...
            return await _SchoolService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDTO>>> GetList()
        {
            var entityList = await _SchoolService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<SchoolDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _SchoolService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<SchoolDTO>> Create(SchoolDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _SchoolService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new School();
            _mapper.Map(model, newModel);
            if (await _SchoolService.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<SchoolDTO>> Update(SchoolDTO model)
        {
            if (_SchoolService.CheckExists(model.Id))
            {
                var entity = new School();
                _mapper.Map(model, entity);
                if (await _SchoolService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
