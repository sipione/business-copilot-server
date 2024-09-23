using APP.Entities;

namespace APP.Interfaces.Services;
public interface IUserAuthorizationService{
    bool AuthorizeViewUsers(User user, Guid? userId);
    bool AuthorizeCreateUsers(User user);
    bool AuthorizeUpdateUsers(User user, Guid? userId);
    bool AuthorizeDeleteUsers(User user, Guid? userId);
    bool AuthorizeViewVouchers(User user);
    bool AuthorizeCreateVouchers(User user);
    bool AuthorizeUpdateVouchers(User user);
    bool AuthorizeDeleteVouchers(User user);
    bool AuthorizeViewCashFlows(User user);
    bool AuthorizeCreateCashFlows(User user);
    bool AuthorizeUpdateCashFlows(User user);
    bool AuthorizeDeleteCashFlows(User user);
    bool AuthorizeViewSubAccounts(User user);
    bool AuthorizeCreateSubAccounts(User user);
    bool AuthorizeUpdateSubAccounts(User user);
    bool AuthorizeDeleteSubAccounts(User user);
    bool AuthorizeViewStakeholders(User user);
    bool AuthorizeCreateStakeholders(User user);
    bool AuthorizeUpdateStakeholders(User user);
    bool AuthorizeDeleteStakeholders(User user);
    bool AuthorizeViewProgresses(User user);
    bool AuthorizeCreateProgresses(User user);
    bool AuthorizeUpdateProgresses(User user);
    bool AuthorizeDeleteProgresses(User user);
    bool AuthorizeViewContracts(User user);
    bool AuthorizeCreateContracts(User user);
    bool AuthorizeUpdateContracts(User user);
    bool AuthorizeDeleteContracts(User user);
}