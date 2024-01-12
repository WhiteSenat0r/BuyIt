using Application.Responses.Common.Classes;
using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Entities.IdentityRelated;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BuyIt.Presentation.WebAPI.Controllers.IdentityRelated;

public class RoleController : BaseApiController
{
    private readonly RoleManager<UserRole> _roleManager;

    public RoleController(RoleManager<UserRole> roleManager)
    {
        _roleManager = roleManager;
    }
    
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserRole>> Login([FromQuery]string roleName)
    {
        if (roleName.IsNullOrEmpty()) return BadRequest(new ApiResponse(400));
        
        var role = new UserRole(roleName);

        var creationResult = await _roleManager.CreateAsync(role);
        
        return creationResult.Succeeded
            ? Ok(role) 
            : BadRequest(new ApiResponse(400));
    }
}