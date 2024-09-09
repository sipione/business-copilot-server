using APP.Entities;

public class AuthorizationService{
    public bool IsUserAuthorized(Permitions permition, User user){
        //fetch user permitions from database
        //to implement after

        //return user.UserPermitionsList.Contains(permition);
        return user.UserPermitionsList.Contains(permition);
    }

    public bool IsSubAccountAuthorized(Permitions permition, SubAccounts subAccount){
        //fetch subAccount permitions from database
        //to implement after

        //return subAccount.PermitionsList.Contains(permition);
        return subAccount.PermitionsList.Contains(permition);
    }
}