using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;
using APP.Enums;

namespace APP.UseCases
{
    public class CreateContractUseCase{
        private readonly IContractRepository _contractRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserAuthorizationService _userAuthorizationService;

        public CreateContractUseCase(IContractRepository contractRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService)
        {
            _contractRepository = contractRepository;
            _authenticationService = authenticationService;
            _userAuthorizationService = userAuthorizationService;
        }

        // public async Task<Contract> Execute(Guid userId, string token, Contract newContract){
        public async Task<Contract> Execute(Contract newContract){
            // User user = await _authenticationService.Authenticate(userId, token);

            // if(user == null)
            // {
            //     throw CommonExceptions.Unauthorized("Unauthorized: credentials are not valid");
            // }

            // if(!_userAuthorizationService.AuthorizeCreateContracts(user))
            // {
            //     throw CommonExceptions.Forbidden("Forbidden: you don't have permission to create Contracts");
            // }


            if(newContract.StakeholderId == null || newContract.Title == null || newContract.InitialAmount == null){
                throw CommonExceptions.BadRequest("Bad Request: Stakeholder Id, Title and Initial Amount are required");
            }

            if(newContract.PaymentStatus == null) Console.WriteLine("payment status null");
            if(newContract.ContractStatus == null) newContract.ContractStatus = ContractStatus.ACTIVE;
            if(newContract.StartDate == null) newContract.StartDate = new DateTime();


            Contract createdContract = await _contractRepository.Create(newContract);
            
            // return createdContract;
            
            return newContract;
        }
    }
}