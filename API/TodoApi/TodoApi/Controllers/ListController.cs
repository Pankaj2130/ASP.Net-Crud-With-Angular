using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListController : Controller
    {
        private readonly ApiDBContext apiDBContext;

        public ListController(ApiDBContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        //get all the list
        [HttpGet]
        public async Task<IActionResult> GetAllList()
        {
            var list = await apiDBContext.Lists.ToListAsync();
            return Ok(list);
        }


        //get singnal list list
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetList")]
        public async Task<IActionResult> GetList([FromRoute] Guid id)
        {
            var list = await apiDBContext.Lists.FirstOrDefaultAsync(x =>x.Id == id);
            if (list != null)
            {
                return Ok(list);
            }
            return NotFound("List is not found");
        }


        //get singnal list list
        [HttpPost]
        public async Task<IActionResult> AddList([FromBody] List list)
        {
            list.Id = Guid.NewGuid();
            await apiDBContext.Lists.AddAsync(list);
            await apiDBContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetList), new { id = list.Id}, list);
        }

        //updating a list
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateList([FromRoute] Guid id, [FromBody] List list)
        {
            var existingList = await apiDBContext.Lists.FirstOrDefaultAsync(x => x.Id == id);
            if (existingList != null)
            {
                existingList.Title = list.Title;
                await apiDBContext.SaveChangesAsync();
                return Ok(existingList);
            }
            return NotFound("list not found");
        }



        //delete a list
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteList([FromRoute] Guid id)
        {
            var existingList = await apiDBContext.Lists.FirstOrDefaultAsync(X => X.Id == id);
            if (existingList != null)
            {
                apiDBContext.Remove(existingList);
                await apiDBContext.SaveChangesAsync();
                return Ok(existingList);
            }
            return NotFound("list not found");
        }



    }
}