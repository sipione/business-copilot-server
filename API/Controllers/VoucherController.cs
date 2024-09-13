using Microsoft.AspNetCore.Mvc;
using APP.Interfaces.Repository;
using APP.Entities;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class VoucherController : ControllerBase{
    private readonly ILogger<VoucherController> _logger;
    private readonly IVoucherRepository _voucherRepository;

    public VoucherController(ILogger<VoucherController> logger, IVoucherRepository voucherRepository)
    {
        _logger = logger;
        _voucherRepository = voucherRepository;
    }

    [HttpGet(Name = "GetVouchers")]
    public async Task<IEnumerable<Voucher>> Get()
    {
        return await _voucherRepository.GetAll();
    }

    [HttpGet("{id}", Name = "GetVoucher")]
    public async Task<Voucher> Get(Guid id)
    {
        return await _voucherRepository.GetById(id);
    }

    [HttpPost(Name = "CreateVoucher")]
    public async Task<Voucher> Create(Voucher voucher)
    {
        return await _voucherRepository.Create(voucher);
    }

    [HttpPut(Name = "UpdateVoucher")]
    public async Task<Voucher> Update(Voucher voucher)
    {
        return await _voucherRepository.Update(voucher);
    }

    [HttpDelete("{id}", Name = "DeleteVoucher")]
    public async Task<Voucher> Delete(Guid id)
    {
        return await _voucherRepository.Delete(id);
    }
}