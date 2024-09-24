namespace ECC_APP_2.Models
{
    public class FundingGuideTemplate
    {
        // Parameterless constructor
        public FundingGuideTemplate() { }

        public FundingGuideTemplate(int studentNum, int fundingGuideId, string fundingPurpose, int amountRequested, string bussinessOverview, string bussinessName, string mission, string bussinessModel, string totalFunding, string useOfFunds, string expenses, string profitability, string industry, string competitors, string marketTrends, string keyMembersandRoles, string keyMilestones, string timeline, string risks, string riskPlan, string summary, string name, string email, string phoneNumber)
        {
            StudentNum = studentNum;
            FundingGuideId = fundingGuideId;
            FundingPurpose = fundingPurpose;
            AmountRequested = amountRequested;
            BussinessOverview = bussinessOverview;
            BussinessName = bussinessName;
            Mission = mission;
            BussinessModel = bussinessModel;
            TotalFunding = totalFunding;
            UseOfFunds = useOfFunds;
            this.expenses = expenses;
            Profitability = profitability;
            this.industry = industry;
            Competitors = competitors;
            this.marketTrends = marketTrends;
            this.keyMembersandRoles = keyMembersandRoles;
            this.keyMilestones = keyMilestones;
            Timeline = timeline;
            Risks = risks;
            RiskPlan = riskPlan;
            this.summary = summary;
            this.name = name;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }



        // Parameterized constructor


        public int StudentNum { get; set; } // Change from string to int

        public int FundingGuideId { get; set; } // Add this property
        public string FundingPurpose { get; set; }
        public int AmountRequested { get; set; }
        public String BussinessOverview { get; set; }
        public string BussinessName { get; set; }
        public string Mission { get; set; }
        public String BussinessModel { get; set; }
        public String TotalFunding { get; set; }
        public string UseOfFunds { get; set; }
        public string expenses { get; set; }
        public string Profitability { get; set; }
        public string industry { get; set; }
        public string Competitors { get; set; }
        public string marketTrends { get; set; }
        public string keyMembersandRoles { get; set; }
        public string keyMilestones { get; set; }
        public string Timeline { get; set; }
        public string Risks { get; set; }
        public string RiskPlan { get; set; }
        public string summary { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
}
