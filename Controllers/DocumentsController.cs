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
    public ActionResult<IEnumerable<DocumentDto>> GetAll(Guid kbId)
    {
        var result = _service.GetAll(kbId);
        if (result is null) return NotFound();
        return Ok(result);
    }
    [HttpGet("{docId:guid}")]
    public ActionResult<DocumentDto> GetById(Guid kbId, Guid docId)
    {
        var dto = _service.GetById(kbId, docId);
        if (dto is null) return NotFound();
        return dto;
    }

    [HttpPost]
    public ActionResult<DocumentDto> Create(Guid kbId, CreateDocumentDto doc)
    {
        var dto = _service.Create(kbId, doc);
        if (dto is null) return NotFound();
        return CreatedAtAction(nameof(GetById),new { kbId, docId = dto.Id }, dto);
    }
    [HttpDelete("{docId:guid}")]
    public IActionResult Delete(Guid kbId, Guid docId)
    {
        return _service.Delete(kbId, docId) ? NoContent() : NotFound();
    }
}