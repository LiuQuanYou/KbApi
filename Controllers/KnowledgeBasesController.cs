using KbApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using KbApi.Services;

namespace KbApi.Controllers;


[ApiController]
[Route("api/knowledge-bases")]

public class KnowledgeBasesController : ControllerBase
{
    public KnowledgeBasesController(IKnowledgeBaseService service)
    {
        _service = service;
    }

    private readonly IKnowledgeBaseService _service;
    // GET api/knowledge-bases
    [HttpGet]
    public ActionResult<IEnumerable<KnowledgeBaseDto>> GetAll()
    {
        var result = _service.GetAll();
        return Ok(result);
    }

    // GET api/knowledge-bases/{id}
    [HttpGet("{id:guid}")]
    public ActionResult<KnowledgeBaseDto> GetById(Guid id)
    {
        var dto = _service.GetById(id);
        if (dto is null) return NotFound();
        return dto;
    }

    [HttpPost]
    public ActionResult<KnowledgeBaseDto> Create(CreateKnowledgeBaseDto row)
    {
        var dto = _service.Create(row);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateKnowledgeBaseDto input)
    {
        return _service.Update(id, input) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        return _service.Delete(id) ? NoContent() : NotFound();
    }
}