public string GetSamAccountName()
    {
      if (HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.Name.ToString().Length > 4)
      {
        return HttpContext.Current.User.Identity.Name.ToString().Remove(0, 4);
      }
      else
        throw new Exception("ERROR: UserService - user not found");
    }
