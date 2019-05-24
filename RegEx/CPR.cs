
public bool IsValidCpr(string cpr)
        {
        //regegsuden bindestreg
            Regex regex = new Regex("^(?:(?:31(?:0[13578]|1[02])|(?:30|29)(?:0[13-9]|1[0-2])|(?:0[1-9]|1[0-9]|2[0-8])(?:0[1-9]|1[0-2]))[0-9]{3}|290200[4-9]|2902(?:(?!00)[02468][048]|[13579][26])[0-3])[0-9]{3}|0000000000$");
            Match match = regex.Match(cpr);
            return match.Success;
}
