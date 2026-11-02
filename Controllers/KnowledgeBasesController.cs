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
    public async Task<ActionResult<IEnumerable<KnowledgeBaseDto>>> GetAll()
    {
        var result = await _service.GetAll();
        if (result is null) return NotFound();
        return Ok(result);
    }

    // GET api/knowledge-bases/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<KnowledgeBaseDto>> GetById(Guid id)
    {
        var dto = await _service.GetById(id);
        if (dto is null) return NotFound();
        return dto;
    }

    [HttpPost]
    public async Task<ActionResult<KnowledgeBaseDto>> Create(CreateKnowledgeBaseDto row)
    {
        var dto = await _service.Create(row);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateKnowledgeBaseDto input)
    {
        return await _service.Update(id, input) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await _service.Delete(id) ? NoContent() : NotFound();
    }
}