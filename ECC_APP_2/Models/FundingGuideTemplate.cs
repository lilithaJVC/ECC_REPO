namespace ECC_APP_2.Models
{
    public class FundingGuideTemplate
    {
        // Parameterless constructor
        public FundingGuideTemplate() { }

        public FundingGuideTemplate(int fundingGuideId, int studentNum, string fundingPurpose, int amountRequested, string bussinessOverview, string bussinessName, string mission, string bussinessModel, string totalFunding, string useOfFunds, string expenses, string profitability, string industry, string competitors, string marketTrends, string keyMembersandRoles, string keyMilestones, string timeline, string risks, string riskPlan, string summary, string name, string email, string phoneNumber)
        {
            FundingGuideId = fundingGuideId;
            this.studentNum = studentNum;
            this.fundingPurpose = fundingPurpose;
            this.amountRequested = amountRequested;
            this.bussinessOverview = bussinessOverview;
            this.bussinessName = bussinessName;
            this.mission = mission;
            this.bussinessModel = bussinessModel;
            this.totalFunding = totalFunding;
            this.useOfFunds = useOfFunds;
            this.expenses = expenses;
            this.profitability = profitability;
            this.industry = industry;
            this.competitors = competitors;
            this.marketTrends = marketTrends;
            KeyMembersandRoles = keyMembersandRoles;
            this.keyMilestones = keyMilestones;
            this.timeline = timeline;
            this.risks = risks;
            this.riskPlan = riskPlan;
            this.summary = summary;
            this.name = name;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        public int FundingGuideId { get; set; } // Add this property
        public int studentNum { get; set; } // Change from string to int
        public string fundingPurpose { get; set; } // Change here
        public int amountRequested { get; set; }
        public string bussinessOverview { get; set; }
        public string bussinessName { get; set; }
        public string mission { get; set; }
        public string bussinessModel { get; set; }
        public string totalFunding { get; set; }
        public string useOfFunds { get; set; }
        public string expenses { get; set; }
        public string profitability { get; set; }
        public string industry { get; set; }
        public string competitors { get; set; }
        public string marketTrends { get; set; }
        public string KeyMembersandRoles { get; set; }
        public string keyMilestones { get; set; }
        public string timeline { get; set; }
        public string risks { get; set; }
        public string riskPlan { get; set; }
        public string summary { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }


    }
}
