using KbApi.Dtos;
using KbApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KbApi.Controllers;

[ApiController]
[Route("api/knowledge-bases")]
public class KnowledgeBasesController : ControllerBase
{
    // 暂时用内存列表当数据库，static 保证每次请求共用同一份数据
    private static readonly List<KnowledgeBase> _store = new();

    // GET api/knowledge-bases
    [HttpGet]
    public ActionResult<IEnumerable<KnowledgeBaseDto>> GetAll()
    {
        var result = _store.Select(kb => new KnowledgeBaseDto(kb.Id, kb.Name, kb.Description, kb.CreatedAt));
        return Ok(result);
    }

    // GET api/knowledge-bases/{id}
    [HttpGet("{id:guid}")]
    public ActionResult<KnowledgeBaseDto> GetById(Guid id)
    {
        var kb = _store.FirstOrDefault(x => x.Id == id);
        if (kb is null) return NotFound();
        return Ok(new KnowledgeBaseDto(kb.Id, kb.Name, kb.Description, kb.CreatedAt));
    }

    [HttpPost]
    public ActionResult<KnowledgeBaseDto> Create(CreateKnowledgeBaseDto row)
    {
        var kb = new KnowledgeBase
        {
            Name = row.Name,
            Description = row.Description
        };

        // 存进内存
        _store.Add(kb);

        var dto = new KnowledgeBaseDto(kb.Id, kb.Name, kb.Description, kb.CreatedAt);
        return CreatedAtAction(nameof(GetById), new { id = kb.Id }, dto);
        //return Ok(dto);
    }

    [HttpPost("{id:guid}")]
    public IActionResult Update(Guid id, UpdateKnowledgeBaseDto input)
    {
        var kb = _store.FirstOrDefault(x => x.Id == id);
        if (kb is null) return NotFound();
        kb.Name = input.Name;
        kb.Description = input.Description;
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var kb = _store.FirstOrDefault(x => x.Id == id);
        if(kb is null) return NotFound();
        _store.Remove(kb);

        return Ok();
    }
}