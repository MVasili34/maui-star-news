using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using EntityModels;
using Services;

namespace ArticleODataAPI.Controllers;

public class SectionsController : ODataController
{
    private readonly IArticleService _articleService;

    public SectionsController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [EnableQuery]
    public async Task<IEnumerable<Section>> GetSections() => await _articleService.RetrieveSectionsAsync();

    //POST: api/Sections
    //BODY: Section (JSON)
    [HttpPost]
    [ProducesResponseType(201,Type = typeof(Section))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PostSection([FromBody] Section section)
    {
        if(section is null)
        {
            return BadRequest();
        }

        Section? addedSection = await _articleService.AddSectionAsync(section);
        if(addedSection is null)
        {
            return BadRequest("Сервис не смог добавить секцию");
        }
        return Created(addedSection);
    }

    //PUT: api/Sections/[id]
    //BODY: Section (JSON)
    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> PutSection(int key, [FromBody] Section section)
    {
        if(section is null || section.SectionId != key)
        {
            return BadRequest();
        }

        Section? updated = await _articleService.UpdateSectionAsync(key, section);
        if(updated is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    //DELETE: api/Sections/[key]
    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteSection(int key)
    {
        bool? status = await _articleService.DeleteSectionAsync(key);

        if(!status.HasValue)
        {
            return NotFound();
        }

        if(status.Value)
        {
            return NoContent();
        }

        return BadRequest();
    }
}
