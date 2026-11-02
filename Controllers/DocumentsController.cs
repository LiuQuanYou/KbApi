using KbApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using KbApi.Services;

namespace KbApi.Controllers;

[ApiController]
[Route("api/knowledge-bases/{kbId:guid}/documents")]

public class DocumentsController : ControllerBase
{
    public DocumentsController(IDocumentService service)
    {
        _service = service;
    }

    private readonly IDocumentService _service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentDto>>> GetAll(Guid kbId)
    {
        var result = await _service.GetAll(kbId);
        if (result is null) return NotFound();
        return Ok(result);
    }
    [HttpGet("{docId:guid}")]
    public async Task<ActionResult<DocumentDto>> GetById(Guid kbId, Guid docId)
    {
        var dto = await _service.GetById(kbId, docId);
        if (dto is null) return NotFound();
        return dto;
    }

    [HttpPost]
    public async Task<ActionResult<DocumentDto>> Create(Guid kbId, CreateDocumentDto doc)
    {
        var dto = await _service.Create(kbId, doc);
        if (dto is null) return NotFound();
        return CreatedAtAction(nameof(GetById),new { kbId, docId = dto.Id }, dto);
    }
    [HttpDelete("{docId:guid}")]
    public async Task<IActionResult> Delete(Guid kbId, Guid docId)
    {
        return await _service.Delete(kbId, docId) ? NoContent() : NotFound();
    }
}
