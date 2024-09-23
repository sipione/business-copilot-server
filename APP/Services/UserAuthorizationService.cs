using APP.Entities;
using APP.Enums;

namespace APP.Services;
public class UserAuthorizationService : IUserAuthorizationService{
    public bool AuthorizeViewUsers(User user, Guid? userId){
        if(userId != null){
            return user.Id == userId;
        }

        Permitions permition = Permitions.READ_USERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeCreateUsers(User user){
        Permitions permition = Permitions.CREATE_USERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeUpdateUsers(User user, Guid? userId){
        if(userId != null){
            return user.Id == userId;
        }

        Permitions permition = Permitions.UPDATE_USERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeDeleteUsers(User user, Guid? userId){
        if(userId != null){
            return user.Id == userId;
        }

        Permitions permition = Permitions.DELETE_USERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeViewVouchers(User user){
        Permitions permition = Permitions.READ_VOUCHERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeCreateVouchers(User user){
        Permitions permition = Permitions.CREATE_VOUCHERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeUpdateVouchers(User user){
        Permitions permition = Permitions.UPDATE_VOUCHERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeDeleteVouchers(User user){
        Permitions permition = Permitions.DELETE_VOUCHERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeViewCashFlows(User user){
        Permitions permition = Permitions.READ_CASHFLOWS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeCreateCashFlows(User user){
        Permitions permition = Permitions.CREATE_CASHFLOWS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeUpdateCashFlows(User user){
        Permitions permition = Permitions.UPDATE_CASHFLOWS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeDeleteCashFlows(User user){
        Permitions permition = Permitions.DELETE_CASHFLOWS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeViewContracts(User user){
        Permitions permition = Permitions.READ_CONTRACTS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeCreateContracts(User user){
        Permitions permition = Permitions.CREATE_CONTRACTS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeUpdateContracts(User user){
        Permitions permition = Permitions.UPDATE_CONTRACTS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeDeleteContracts(User user){
        Permitions permition = Permitions.DELETE_CONTRACTS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeViewProgresses(User user){
        Permitions permition = Permitions.READ_PROGRESSES;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeCreateProgresses(User user){
        Permitions permition = Permitions.CREATE_PROGRESSES;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeUpdateProgresses(User user){
        Permitions permition = Permitions.UPDATE_PROGRESSES;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeDeleteProgresses(User user){
        Permitions permition = Permitions.DELETE_PROGRESSES;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeViewStakeholders(User user){
        Permitions permition = Permitions.READ_STAKEHOLDERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeCreateStakeholders(User user){
        Permitions permition = Permitions.CREATE_STAKEHOLDERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeUpdateStakeholders(User user){
        Permitions permition = Permitions.UPDATE_STAKEHOLDERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeDeleteStakeholders(User user){
        Permitions permition = Permitions.DELETE_STAKEHOLDERS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeViewSubAccounts(User user){
        Permitions permition = Permitions.READ_SUBACCOUNTS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeCreateSubAccounts(User user){
        Permitions permition = Permitions.CREATE_SUBACCOUNTS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeUpdateSubAccounts(User user){
        Permitions permition = Permitions.UPDATE_SUBACCOUNTS;
        return user.UserPermitionsList.Contains(permition);
    }

    public bool AuthorizeDeleteSubAccounts(User user){
        Permitions permition = Permitions.DELETE_SUBACCOUNTS;
        return user.UserPermitionsList.Contains(permition);
    }
}