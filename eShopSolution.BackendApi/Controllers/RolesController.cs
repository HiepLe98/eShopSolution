﻿using eShopSolution.Application.System.Roles;
using eShopSolution.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleManager)
        {
            _roleService = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }
    }
}