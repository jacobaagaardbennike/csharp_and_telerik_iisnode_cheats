
        /// <summary>
        /// Tjekker om telefonnmmer er et gyldigt telefonnummer
        /// http://erhvervsstyrelsen.dk/nummerplan
        /// </summary>
        /// <param name="tlf"></param>
        /// <returns>boolean</returns>
        public bool IsPhoneValid(string phonenumber)
        {
            //Regex regex = new Regex("^(?:2[0-9]{7}|3[0|1][0-9]{6}|4[0|1|2][0-9]{6}|5[0|1|2|3][0-9]{6}|6[0|1][0-9]{6}|71[0-9]{6}|81[0-9]{6}|9[1|2|3][0-9]{6})$"); <- forældet
            Regex regex = new Regex("^([0-9]{8})$");
            Match match = regex.Match(phonenumber);
            return match.Success;
}
